                                                      M[�>    �       N[�>           Icon   Untagged                                                       L[�>                  �?9o�@��@              �?        ��~                                            L[�>                 *                                          �?       ?  �B                       ,E      �?  �?  �?  �?                                          @`�E    �       B`�E           Description    Untagged                                                       ?`�E                  �?��)Aff@              �?        Ń�O                                           ?`�E                 *                                          �?       ?  �B                       *E      �?  �?  �?  �?                                          Ń�O    r       ƃ�O           Item 2     Untagged                                                           ă�O                  �?  �  ��      �?  �?  �?       ��        @`�E        ��3[                                           ă�O              �y�                                                               P�Z    �       P�Z           Description    Untagged                                                       P�Z                  �?��)Aff@              �?        ��~                                           P�Z                 *                                          �?       ?  �B                       .E      �?  �?  �?  �?                                          ��3[    r       ��3[           ListItem   Untagged                                                           ��3[                  �?{�>�G��      �?  �?  �?       -�u        Ń�O        ��~        ��(                                                       ��3[              �y�                                                       �cxe    �       �cxe           Icon   Untagged                                                       �cxe                 *                                          �?       ?  �B                        E      �?  �?  �?  �?                                    �cxe                  �?9o�@��@       @   @  �?        -�u                                                  $��k    �       %��k           Icon   Untagged                                                       #��k                  �?9o�@��@              �?        ��(                                            #��k                 *                                          �?       ?  �B                       $E      �?  �?  �?  �?                                          ��~    r       ��~           Item 3     Untagged                                                           ��~                  �?  �  ��      �?  �?  �?       M[�>        P�Z        ��3[                                           ��~              �y�                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;


namespace NativeX.UnityEditor.XCodeEditor
{
	public class PBXResolver
	{
		private class PBXResolverReverseIndex : Dictionary<string, string> {}

		private PBXDictionary objects;
		private string rootObject;
		private PBXResolverReverseIndex index;

		public PBXResolver( PBXDictionary pbxData ) {
			this.objects = (PBXDictionary)pbxData[ "objects" ];
			this.index = new PBXResolverReverseIndex();
			this.rootObject = (string)pbxData[ "rootObject" ];
			BuildReverseIndex();
		}

		private void BuildReverseIndex()
		{
			foreach( KeyValuePair<string, object> pair in this.objects )
			{
				if( pair.Value is PBXBuildPhase )
				{
					foreach( string guid in ((PBXBuildPhase)pair.Value).files )
					{
						index[ guid ] = pair.Key;
					}
				}
				else if( pair.Value is PBXGroup )
				{
					foreach( string guid in ((PBXGroup)pair.Value).children )
					{
						index[ guid ] = pair.Key;
					}
				}
			}
		}

		public string ResolveName( string guid )
		{
			
			if (!this.objects.ContainsKey(guid)) {
				Debug.LogWarning(this + " ResolveName could not resolve " + guid);
				return string.Empty; //"UNRESOLVED GUID:" + guid;
			}
			
			object entity = this.objects[ guid ];

			if( entity is PBXBuildFile )
			{
				return ResolveName( ((PBXBuildFile)entity).fileRef );
			}
			else if( entity is PBXFileReference )
			{
				PBXFileReference casted = (PBXFileReference)entity;
				return casted.name != null ? casted.name : casted.path;
			}
			else if( entity is PBXGroup )
			{
				PBXGroup casted = (PBXGroup)entity;
				return casted.name != null ? casted.name : casted.path;
			}
			else if( entity is PBXProject || guid == this.rootObject )
			{
				return "Project object";
			}
			else if( entity is PBXFrameworksBuildPhase )
			{
				return "Frameworks";
			}
			else if( entity is PBXResourcesBuildPhase )
			{
				return "Resources";
			}
			else if( entity is PBXShellScriptBuildPhase )
			{
				return "ShellScript";
			}
			else if( entity is PBXSourcesBuildPhase )
			{
				return "Sources";
			}
			else if( entity is PBXCopyFilesBuildPhase )
			{
				return "CopyFiles";
			}
			else if( entity is XCConfigurationList )
			{
				XCConfigurationList casted = (XCConfigurationList)entity;
				//Debug.LogWarning ("XCConfigurationList " + guid + " " + casted.ToString());
				
				if( casted.data.ContainsKey( "defaultConfigurationName" ) ) {
					//Debug.Log ("XCConfigurationList " + (string)casted.data[ "defaultConfigurationName" ] + " " + guid);
					return (string)casted.data[ "defaultConfigurationName" ];
				}

				return null;
			}
			else if( entity is PBXNativeTarget )
			{
				PBXNativeTarget obj = (PBXNativeTarget)entity;
				//Debug.LogWarning ("PBXNativeTarget " + guid + " " + obj.ToString());
				
				if( obj.data.ContainsKey( "name" ) ) {
					//Debug.Log ("PBXNativeTarget " + (string)obj.data[ "name" ] + " " + guid);
					return (string)obj.data[ "name" ];
				}

				return null;
			}
			else if( entity is XCBuildConfiguration )
			{
				XCBuildConfiguration obj = (XCBuildConfiguration)entity;
				//Debug.LogWarning ("XCBuildConfiguration UNRESOLVED GUID:" + guid + " " + (obj==null?"":obj.ToString()));

				if( obj.data.ContainsKey( "name" ) ) {
					//Debug.Log ("XCBuildConfiguration " + (string)obj.data[ "name" ] + " " + guid + " " + (obj==null?"":obj.ToString()));
					return (string)obj.data[ "name" ];
				}
				
			}
			else if( entity is PBXObject )
			{
				PBXObject obj = (PBXObject)entity;

				if( obj.data.ContainsKey( "name" ) )
					Debug.Log ("PBXObject " + (string)obj.data[ "name" ] + " " + guid + " " + (obj==null?"":obj.ToString()));
					return (string)obj.data[ "name" ];
			}

			//return "UNRESOLVED GUID:" + guid;
			Debug.LogWarning ("UNRESOLVED GUID:" + guid);
			return null;
		}

		public string ResolveBuildPhaseNameForFile( string guid )
		{
			if( this.objects.ContainsKey( guid ) )
			{
				object obj = this.objects[ guid ];

				if( obj is PBXObject )
				{
					PBXObject entity = (PBXObject)obj;

					if( this.index.ContainsKey( entity.guid ) )
					{
						string parent_guid = this.index[ entity.guid ];

						if( this.objects.ContainsKey( parent_guid ) )
						{
							object parent = this.objects[ parent_guid ];

							if( parent is PBXBuildPhase ) {
								string ret = ResolveName( ((PBXBuildPhase)parent).guid );
								//Debug.Log ("ResolveBuildPhaseNameForFile = " + ret);
								return ret;
							}
						}
					}
				}
			}

			return null;
		}

	}

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
		private PBXResolver resolver;

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

		public string Encode( PBXDictionary pbxData, bool readable = false )
		{
			this.resolver = new PBXResolver( pbxData );
			StringBuilder builder = new StringBuilder( PBX_HEADER_TOKEN, BUILDER_CAPACITY );

			bool success = SerializeValue( pbxData, builder, readable );
			this.resolver = null;

			// Xcode adds newline at the end of file
			builder.Append( "\n" );

			return ( success ? builder.ToString() : null );
		}

		#region Pretty Print

		private void Indent( StringBuilder builder, int deep )
		{
			builder.Append( "".PadLeft( deep, '\t' ) );
		}

		private void Endline( StringBuilder builder, bool useSpace = false )
		{
			builder.Append( useSpace ? " " : "\n" );
		}

		private string marker = null;
		private void MarkSection( StringBuilder builder, string name )
		{
			if( marker == null && name == null ) return;

			if( marker != null && name != marker )
			{
				builder.Append( String.Format( "/* End {0} section */\n", marker ) );
			}

			if( name != null && name != marker )
			{
				builder.Append( String.Format( "\n/* Begin {0} section */\n", name ) );
			}

			marker = name;
		}

		private bool GUIDComment( string guid, StringBuilder builder )
		{
			string filename = this.resolver.ResolveName( guid );
			string location = this.resolver.ResolveBuildPhaseNameForFile( guid );

			//Debug.Log( "RESOLVE " + guid + ": " + filename + " in " + location );

			if( filename != null ) {
				if( location != null ) {
					//Debug.Log( "GUIDComment " + guid + " " + String.Format( " /* {0} in {1} */", filename, location )  );
					builder.Append( String.Format( " /* {0} in {1} */", filename, location ) );
				} else {
					//Debug.Log( "GUIDComment " + guid + " " + String.Format( " /* {0} */", filename) );
					builder.Append( String.Format( " /* {0} */", filename) );
				}
				return true;
			} else {
				//string other = this.resolver.ResolveConfigurationNameForFile( guid );
				Debug.Log ("GUIDComment " + guid + " [no filename]");	
			}

			return false;
		}

		#endregion

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
						if (!dictionary.ContainsKey(keyString)) {
							dictionary.Add( keyString, valueObject );
						}
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
			char c = StepForeward();
			while( c != QUOTEDSTRING_END_TOKEN ) {
				s += c;

				if( c == QUOTEDSTRING_ESCAPE_TOKEN )
					s += StepForeward();

				c = StepForeward();
			}

			return s;
		}

		private object ParseEntity()
		{
			string word = string.Empty;

			while( !Regex.IsMatch( Peek(), @"[;,\s=]" ) ) {
				word += StepForeward();
			}

			if( word.Length != 24 && Regex.IsMatch( word, @"^\d+$" ) ) {
				return Int32.Parse( word );
			}

			return word;
		}

		#endregion
		#region Serialize

		private bool SerializeValue( object value, StringBuilder builder, bool readable = false, int indent = 0 )
		{
			if( value == null ) {
				builder.Append( "null" );
			}
			else if( value is PBXObject ) {
				SerializeDictionary( ((PBXObject)value).data, builder, readable, indent );
			}
			else if( value is Dictionary<string, object> ) {
				SerializeDictionary( (Dictionary<string, object>)value, builder, readable, indent );
			}
			else if( value.GetType().IsArray ) {
				Seria