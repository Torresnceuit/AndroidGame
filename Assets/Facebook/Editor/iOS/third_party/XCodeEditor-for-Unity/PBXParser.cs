 W             ���<  bolts/MeasurementEvent.classPK
   	�E��Ǳ  J             ���I  bolts/Task$1.classPK
   	�Eʔ�  �             ���L  bolts/Task$10$1.classPK
   	�E�2��  �             ���O  bolts/Task$10.classPK
   	�E�B�PP  K             ��)S  bolts/Task$2.classPK
   	�E\K�  r	             ���U  bolts/Task$3.classPK
   	�Ex�OY  �             ���Z  bolts/Task$4.classPK
   	�E2�'�  #             ��^  bolts/Task$5.classPK
   	�E�F�3�  '             ��a  bolts/Task$6.classPK
   	�E����  �             ���c  bolts/Task$7.classPK
   	�E��>��  �             ��g  bolts/Task$8.classPK
   	�EpHZ;�               ��7j  bolts/Task$9.classPK
   	�E�]��  2  %           ��m  bolts/Task$TaskCompletionSource.classPK
   	�E 	YK�  F4             ��r  bolts/Task.classPK
   	�E9e��  L  $           ��ւ  bolts/WebViewAppLinkResolver$1.classPK
   	�E����  �  &           ���  bolts/WebViewAppLinkResolver$2$1.classPK
   	�E���z�  �  &           ��݉  bolts/WebViewAppLinkResolver$2$2.classPK
   	�E��W  �  $           ��ό  bolts/WebViewAppLinkResolver$2.classPK
   	�E�{���  6	  $           ��h�  bolts/WebViewAppLinkResolver$3.classPK
   	�EA��'�  &&  "           ��}�  bolts/WebViewAppLinkResolver.classPK    ( ( 1  E�                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;


namespace UnityEditor.XCodeEditor
{
    public class PBXParser
    {
        public const string PBX_HEADER_TOKEN = "// !$*UTF8*$!\n";
        public const char WHITESPACE_SPACE = ' ';
        public const char WHITESPACE_TAB = '\t';
        public const char WHITESPACE_NEWLINE = '\n';
        public const char WHITESPACE_CARRIAGE_RETURN = '\r';
        public const char ARRAY_BEGIN_TOKEN = '(';
        public const char ARRAY_END_TOKEN = ')';
        public const char ARRAY_ITEM_DELIMITER_TOKEN = ',';
        public const char DICTIONARY_BEGIN_TOKEN = '{';
        public const char DICTIONARY_END_TOKEN = '}';
        public const char DICTIONARY_ASSIGN_TOKEN = '=';
        public const char DICTIONARY_ITEM_DELIMITER_TOKEN = ';';
        public const char QUOTEDSTRING_BEGIN_TOKEN = '"';
        public const char QUOTEDSTRING_END_TOKEN = '"';
        public const char QUOTEDSTRING_ESCAPE_TOKEN = '\\';
        public const char END_OF_FILE = (char)0x1A;
        public const string COMMENT_BEGIN_TOKEN = "/*";
        public const string COMMENT_END_TOKEN = "*/";
        public const string COMMENT_LINE_TOKEN = "//";
        private const int BUILDER_CAPACITY = 20000;

        private char[] data;
        private int index;
        private int indent;
    
        public PBXDictionary Decode( string data )
        {
            if( !data.StartsWith( PBX_HEADER_TOKEN ) ) {
                Debug.Log( "Wrong file format." );
                return null;
            }

            data = data.Substring( 13 );
            this.data = data.ToCharArray();
            index = 0;
            
            return (PBXDictionary)ParseValue();
        }

        public string Encode( PBXDictionary pbxData)
        {
            indent = 0;

            StringBuilder builder = new StringBuilder( PBX_HEADER_TOKEN, BUILDER_CAPACITY );
            bool success = SerializeValue( pbxData, builder);

            return ( success ? builder.ToString() : null );
        }

        #region Move

        private char NextToken()
        {
            SkipWhitespaces();
            return StepForeward();
        }
        
        private string Peek( int step = 1 )
        {
            string sneak = string.Empty;
            for( int i = 1; i <= step; i++ ) {
                if( data.Length - 1 < index + i ) {
                    break;
                }
                sneak += data[ index + i ];
            }
            return sneak;
        }

        private bool SkipWhitespaces()
        {
            bool whitespace = false;
            while( Regex.IsMatch( StepForeward().ToString(), @"\s" ) )
                whitespace = true;

            StepBackward();
            
            if( SkipComments() ) {
                whitespace = true;
                SkipWhitespaces();
            }

            return whitespace;
        }

        private bool SkipComments()
        {
            string s = string.Empty;
            string tag = Peek( 2 );
            switch( tag ) {
                case COMMENT_BEGIN_TOKEN: {
                        while( Peek( 2 ).CompareTo( COMMENT_END_TOKEN ) != 0 ) {
                            s += StepForeward();
                        }
                        s += StepForeward( 2 );
                        break;
                    }
                case COMMENT_LINE_TOKEN: {
                        while( !Regex.IsMatch( StepForeward().ToString(), @"\n" ) )
                            continue;

                        break;
                    }
                default:
                    return false;
            }
            return true;
        }
        
        private char StepForeward( int step = 1 )
        {
            index = Math.Min( data.Length, index + step );
            return data[ index ];
        }
        
        private char StepBackward( int step = 1 )
        {
            index = Math.Max( 0, index - step );
            return data[ index ];
        }

        #endregion
        #region Parse

        private object ParseValue()
        {
            switch( NextToken() ) {
                case END_OF_FILE:
                    Debug.Log( "End of file" );
                    return null;
                case DICTIONARY_BEGIN_TOKEN:
                    return ParseDictionary();
                case ARRAY_BEGIN_TOKEN:
                    return ParseArray();
                case QUOTEDSTRING_BEGIN_TOKEN:
                    return ParseString();
                default:
                    StepBackward();
                    return ParseEntity();
            }
        }
        

        private PBXDictionary ParseDictionary()
        {
            SkipWhitespaces();
            PBXDictionary dictionary = new PBXDictionary();
            string keyString = string.Empty;
            object valueObject = null;

            bool complete = false;
            while( !complete ) {
                switch( NextToken() ) {
                    case END_OF_FILE:
                        Debug.Log( "Error: reached end of file inside a dictionary: " + index );
                        complete = true;
                        break;

                    case DICTIONARY_ITEM_DELIMITER_TOKEN:
                        keyString = string.Empty;
                        valueObject = null;
                        break;

                    case DICTIONARY_END_TOKEN:
                        keyString = string.Empty;
                        valueObject = null;
                        complete = true;
                        break;

                    case DICTIONARY_ASSIGN_TOKEN:
                        valueObject = ParseValue();
                        dictionary.Add( keyString, valueObject );
                        break;

                    default:
                        StepBackward();
                        keyString = ParseValue() as string;
                        break;
                }
            }
            return dictionary;
        }

        private PBXList ParseArray()
        {
            PBXList list = new PBXList();
            bool complete = false;
            while( !complete ) {
                switch( NextToken() ) {
                    case END_OF_FILE:
                        Debug.Log( "Error: Reached end of file inside a list: " + list );
                        complete = true;
                        break;
                    case ARRAY_END_TOKEN:
                        complete = true;
                        break;
                    case ARRAY_ITEM_DELIMITER_TOKEN:
                        break;
                    default:
                        StepBackward();
                        list.Add( ParseValue() );
                        break;
                }
            }
            return list;
        }

        private object ParseString()
        {
            string s = string.Empty;
            s += "\"";
            char c = StepForeward();
            while( c != QUOTEDSTRING_END_TOKEN ) {
                s += c;

                if( c == QUOTEDSTRING_ESCAPE_TOKEN )
                    s += StepForeward();

                c = StepForeward();
            }
            s += "\"";
            return s;
        }
        
        //there has got to be a better way to do this
        private string GetDataSubstring(int begin, int length)
        {
            string res = string.Empty;
            
            
            for(int i=begin; i<begin+length && i<data.Length; i++)
            {
                res += data[i];
            }
            return res;
        }
        
        private int CountWhitespace(int pos)
        {
            int i=0;
            for(int currPos=pos; currPos<data.Length && Regex.IsMatch( GetDataSubstring(currPos, 1), @"[;,\s=]" ); i++, currPos++) {}
            return i;
        }
        
        private string ParseCommentFollowingWhitespace()
        {
            int currIdx = index+1;
            int whitespaceLength = CountWhitespace(currIdx);
            currIdx += whitespaceLength;
            
            if(currIdx + 1 >= data.Length)
                return "";
            
            
            
            if(data[currIdx] == '/' && data[currIdx+1] == '*')
            {
                
                while(!GetDataSubstring(currIdx, 2).Equals(COMMENT_END_TOKEN))
                {
                    if(currIdx >= data.Length)
                    {
                        Debug.LogError("Unterminated comment found in .pbxproj file.  Bad things are probably going to start happening");
                        return "";
                    }
                    
                    currIdx++;
                }
                
                return GetDataSubstring (index+1, (currIdx-index+1));
                
            }
            else
            {
                return "";
            }
        }
        
        private object ParseEntity()
        {
            string word = string.Empty;
            
            while(!Regex.IsMatch( Peek(), @"[;,\s=]" )) 
            {
                word += StepForeward();
            }
            
            string comment = ParseCommentFollowingWhitespace();
            if(comment.Length > 0)
            {
                word += comment;
                index += comment.Length;
            }
            
            if( word.Length != 24 && Regex.IsMatch( word, @"^\d+$" ) ) {
                return Int32.Parse( word );
            }
            
            return word;
        }

        #endregion
        #region Serialize
        
        private void AppendNewline(StringBuilder builder)
        {
            builder.Append(WHITESPACE_NEWLINE);
            for(int i=0; i<indent; i++)
            {
                builder.Append (WHITESPACE_TAB);
            }
        }
        
        private void AppendLineDelim(StringBuilder builder, bool newline)
        {
            if(newline)
            {
                AppendNewline(builder);
            }
            else
            {
                builder.Append(WHITESPACE_SPACE);
            }
        }

        private bool SerializeValue( object value, StringBuilder builder)
        {
            bool internalNewlines = false;
            if(value is PBXObject)
            {
                internalNewlines = ((PBXObject)value).internalNewlines;
            }
            else if(value is PBXDictionary)
            {
                internalNewlines = ((PBXDictionary)value).internalNewlines;
            }
            else if(value is PBXList)
            {
                internalNewlines = ((PBXList)value).internalNewlines;
            }
            
            if( value == null ) {
                builder.Append( "null" );
            }
            else if( value is PBXObject ) {
          