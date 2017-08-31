Copyright (c) 2012 Wei Wang @onevcat

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            namespace NativeXJSON {
	
	using System;
	using System.Collections;
	using System.Text;
	using System.Collections.Generic;
	
	
	/* Based on the JSON parser from 
	 * http://techblog.procurios.nl/k/618/news/view/14605/14863/How-do-I-write-my-own-parser-for-JSON.html
	 * 
	 * I simplified it so that it doesn't throw exceptions
	 * and can be used in Unity iPhone with maximum code stripping.
	 */
	/// <summary>
	/// This class encodes and decodes JSON strings.
	/// Spec. details, see http://www.json.org/
	/// 
	/// JSON uses Arrays and Objects. These correspond here to the datatypes ArrayList and Hashtable.
	/// All numbers are parsed to doubles.
	/// </summary>
	public class NativeXMiniJSON
	{
		private const int TOKEN_NONE = 0;
		private const int TOKEN_CURLY_OPEN = 1;
		private const int TOKEN_CURLY_CLOSE = 2;
		private const int TOKEN_SQUARED_OPEN = 3;
		private const int TOKEN_SQUARED_CLOSE = 4;
		private const int TOKEN_COLON = 5;
		private const int TOKEN_COMMA = 6;
		private const int TOKEN_STRING = 7;
		private const int TOKEN_NUMBER = 8;
		private const int TOKEN_TRUE = 9;
		private const int TOKEN_FALSE = 10;
		private const int TOKEN_NULL = 11;
		private const int BUILDER_CAPACITY = 2000;
		
		/// <summary>
		/// On decoding, this value holds the position at which the parse failed (-1 = no error).
		/// </summary>
		protected static int lastErrorIndex = -1;
		protected static string lastDecode = "";
		
		
		/// <summary>
		/// Parses the string json into a value
		/// </summary>
		/// <param name="json">A JSON string.</param>
		/// <returns>An ArrayList, a Hashtable, a double, a string, null, true, or false</returns>
		public static object jsonDecode(string json)
		{
			// save the string for debug information
			NativeXMiniJSON.lastDecode = json;
			
			if (json != null)
			{
				char[] charArray = json.ToCharArray();
				int index = 0;
				bool success = true;
				object value = NativeXMiniJSON.parseValue(charArray, ref index, ref success);
				
				if (success)
					NativeXMiniJSON.lastErrorIndex = -1;
				else
					NativeXMiniJSON.lastErrorIndex = index;
				
				return value;
			}
			else
			{
				return null;
			}
		}
		
		
		/// <summary>
		/// Converts a Hashtable / ArrayList / Dictionary(string,string) object into a JSON string
		/// </summary>
		/// <param name="json">A Hashtable / ArrayList</param>
		/// <returns>A JSON encoded string, or null if object 'json' is not serializable</returns>
		public static string jsonEncode(object json)
		{
			var builder = new StringBuilder(BUILDER_CAPACITY);
			var success = NativeXMiniJSON.serializeValue(json, builder);
			
			return (success ? builder.ToString() : null);
		}
		
		
		/// <summary>
		/// On decoding, this function returns the position at which the parse failed (-1 = no error).
		/// </summary>
		/// <returns></returns>
		public static bool lastDecodeSuccessful()
		{
			return (NativeXMiniJSON.lastErrorIndex == -1);
		}
		
		
		/// <summary>
		/// On decoding, this function returns the position at which the parse failed (-1 = no error).
		/// </summary>
		/// <returns></returns>
		public static int getLastErrorIndex()
		{
			return NativeXMiniJSON.lastErrorIndex;
		}
		
		
		/// <summary>
		/// If a decoding error occurred, this function returns a piece of the JSON string 
		/// at which the error took place. To ease debugging.
		/// </summary>
		/// <returns></returns>
		public static string getLastErrorSnippet()
		{
			if (NativeXMiniJSON.lastErrorIndex == -1)
			{
				return "";
			}
			else
			{
				int startIndex = NativeXMiniJSON.lastErrorIndex - 5;
				int endIndex = NativeXMiniJSON.lastErrorIndex + 15;
				if (startIndex < 0)
					startIndex = 0;
				
				if (endIndex >= NativeXMiniJSON.lastDecode.Length)
					endIndex = NativeXMiniJSON.lastDecode.Length - 1;
				
				return NativeXMiniJSON.lastDecode.Substring(startIndex, endIndex - startIndex + 1);
			}
		}
		
		
		#region Parsing
		
		protected static Hashtable parseObject(char[] json, ref int index)
		{
			Hashtable table = new Hashtable();
			int token;
			
			// {
			nextToken(json, ref index);
			
			bool done = false;
			while (!done)
			{
				token = lookAhead(json, index);
				if (token == NativeXMiniJSON.TOKEN_NONE)
				{
					return null;
				}
				else if (token == NativeXMiniJSON.TOKEN_COMMA)
				{
					nextToken(json, ref index);
				}
				else if (token == NativeXMiniJSON.TOKEN_CURLY_CLOSE)
				{
					nextToken(json, ref index);
					return table;
				}
				else
				{
					// name
					string name = parseString(json, ref index);
					if (name == null)
					{
						return null;
					}
					
					// :
					token = nextToken(json, ref index);
					if (token != NativeXMiniJSON.TOKEN_COLON)
						return null;
					
					// value
					bool success = true;
					object value = parseValue(json, ref index, ref success);
					if (!success)
						return null;
					
					table[name] = value;
				}
			}
			
			return table;
		}
		
		
		protected static ArrayList parseArray(char[] json, ref int index)
		{
			ArrayList array = new ArrayList();
			
			// [
			nextToken(json, ref index);
			
			bool done = false;
			while (!done)
			{
				int token = lookAhead(json, index);
				if (token == NativeXMiniJSON.TOKEN_NONE)
				{
					return null;
				}
				else if (token == NativeXMiniJSON.TOKEN_COMMA)
				{
					nextToken(json, ref index);
				}
				else if (token == NativeXMiniJSON.TOKEN_SQUARED_CLOSE)
				{
					nextToken(json, ref index);
					break;
				}
				else
				{
					bool success = true;
					object value = parseValue(json, ref index, ref success);
					if (!success)
						return null;
					
					array.Add(value);
				}
			}
			
			return array;
		}
		
		
		protected static object parseValue(char[] json, ref int index, ref bool success)
		{
			switch (lookAhead(json, index))
			{
			case NativeXMiniJSON.TOKEN_STRING:
				return parseString(json, ref index);
			case NativeXMiniJSON.TOKEN_NUMBER:
				return parseNumber(json, ref index);
			case NativeXMiniJSON.TOKEN_CURLY_OPEN:
				return parseObject(json, ref index);
			case NativeXMiniJSON.TOKEN_SQUARED_OPEN:
				return parseArray(json, ref index);
			case NativeXMiniJSON.TOKEN_TRUE:
				nextToken(json, ref index);
				return Boolean.Parse("TRUE");
			case NativeXMiniJSON.TOKEN_FALSE:
				nextToken(json, ref index);
				return Boolean.Parse("FALSE");
			case NativeXMiniJSON.TOKEN_NULL:
				nextToken(json, ref index);
				return null;
			case NativeXMiniJSON.TOKEN_NONE:
				break;
			}
			
			success = false;
			return null;
		}
		
		
		protected static string parseString(char[] json, ref int index)
		{
			string s = "";
			char c;
			
			eatWhitespace(json, ref index);
			
			// "
			c = json[index++];
			
			bool complete = false;
			while (!complete)
			{
				if (index == json.Length)
					break;
				
				c = json[index++];
				if (c == '"')
				{
					complete = true;
					break;
				}
				else if (c == '\\')
				{
					if (index == json.Length)
						break;
					
					c = json[index++];
					if (c == '"')
					{
						s += '"';
					}
					else if (c == '\\')
					{
						s += '\\';
					}
					else if (c == '/')
					{
						s += '/';
					}
					else if (c == 'b')
					{
						s += '\b';
					}
					else if (c == 'f')
					{
						s += '\f';
					}
					else if (c == 'n')
					{
						s += '\n';
					}
					else if (c == 'r')
					{
						s += '\r';
					}
					else if (c == 't')
					{
						s += '\t';
					}
					else if (c == 'u')
					{
						int remainingLength = json.Length - index;
						if (remainingLength >= 4)
						{
							char[] unicodeCharArray = new char[4];
							Array.Copy(json, index, unicodeCharArray, 0, 4);
							
							// Drop in the HTML markup for the unicode character
							s += "&#x" + new string(unicodeCharArray) + ";";
							
							/*
	uint codePoint = UInt32.Parse(new string(unicodeCharArray), NumberStyles.HexNumber);
	// convert the integer codepoint to a unicode char and add to string
	s += Char.ConvertFromUtf32((int)codePoint);
	*/
							
							// skip 4 chars
							index += 4;
						}
						else
						{
							break;
						}
						
					}
				}
				else
				{
					s += c;
				}
				
			}
			
			if (!complete)
				return null;
			
			return s;
		}
		
		
		protected static double parseNumber(char[] json, ref int index)
		{
			eatWhitespace(json, ref index);
			
			int lastIndex = getLastIndexOfNumber(json, index);
			int charLength = (lastIndex - index) + 1;
			char[] numberCharArray = new char[charLength];
			
			Array.Copy(json, index, numberCharArray, 0, charLength);
			index = lastIndex + 1;
			return Double.Parse(new string(numberCharArray)); // , CultureInfo.InvariantCulture);
		}
		
		
		protected static int getLastIndexOfNumber(char[] json, int index)
		{
			int lastIndex;
			for (lastIndex = index; lastIndex < json.Length; lastIndex++)
				if ("0123456789+-.eE".IndexOf(json[lastIndex]) == -1)
			{
				break;
			}
			return lastIndex - 1;
		}
		
		
		protected static void eatWhitespace(char[] json, ref int index)
		{
			for (; index < json.Length; index++)
				if (" \t\n\r".IndexOf(json[index]) == -1)
			{
				break;
			}
		}
		
		
		protected static int lookAhead(char[] json, int index)
		{
			int saveIndex = index;
			return nextToken(json, ref saveIndex);
		}
		
		
		protected static int nextToken(char[] json, ref int index)
		{
			eatWhitespace(json, ref index);
			
			if (index == json.Length)
			{
				return NativeXMiniJSON.TOKEN_NONE;
			}
			
			char c = json[index];
			index++;
			switch (c)
			{
			case '{':
				return NativeXMiniJSON.TOKEN_CURLY_OPEN;
			case '}':
				return NativeXMiniJSON.TOKEN_CURLY_CLOSE;
			case '[':
				return NativeXMiniJSON.TOKEN_SQUARED_OPEN;
			case ']':
				return NativeXMiniJSON.TOKEN_SQUARED_CLOSE;
			case ',':
				return NativeXMiniJSON.TOKEN_COMMA;
			case '"':
				return NativeXMiniJSON.TOKEN_STRING;
			case '0':
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
			case '-':
				return NativeXMiniJSON.TOKEN_NUMBER;
			case ':':
				return NativeXMiniJSON.TOKEN_COLON;
			}
			index--;
			
			int remainingLength = json.Length - index;
			
			// false
			if (remainingLength >= 5)
			{
				if (json[index] == 'f' &&
				    json[index + 1] == 'a' &&
				    json[index + 2] == 'l' &&
				    json[index + 3] == 's' &&
				    json[index + 4] == 'e')
				{
					index += 5;
					return NativeXMiniJSON.TOKEN_FALSE;
				}
			}
			
			// true
			if (remainingLength >= 4)
			{
				if (json[index] == 't' &&
				    json[index + 1] == 'r' &&
				    json[index + 2] == 'u' &&
				    json[index + 3] == 'e')
				{
					index += 4;
					return NativeXMiniJSON.TOKEN_TRUE;
				}
			}
			
			// null
			if (remainingLength >= 4)
			{
				if (json[index] == 'n' &&
				    json[index + 1] == 'u' &&
				    json[index + 2] == 'l' &&
				    json[index + 3] == 'l')
				{
					index += 4;
					return NativeXMiniJSON.TOKEN_NULL;
				}
			}
			
			return NativeXMiniJSON.TOKEN_NONE;
		}
		
		#endregion
		
		
		#region Serialization
		
		protected static bool serializeObjectOrArray(object objectOrArray, StringBuilder builder)
		{
			if (objectOrArray is Hashtable)
			{
				return serializeObject((Hashtable)objectOrArray, builder);
			}
			else if (objectOrArray is ArrayList)
			{
				return serializeArray((ArrayList)objectOrArray, builder);
			}
			else
			{
				return false;
			}
		}
		
		
		protected static bool serializeObject(Hashtable anObject, StringBuilder builder)
		{
			builder.Append("{");
			
			IDictionaryEnumerator e = anObject.GetEnumerator();
			bool first = true;
			while (e.MoveNext())
			{
				string key = e.Key.ToString();
				object value = e.Value;
				
				if (!