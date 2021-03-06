pyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               using UnityEngine;
using System.Collections;

namespace NativeX.UnityEditor.XCodeEditor
{
	public class XCBuildConfiguration : PBXObject
	{
		protected const string BUILDSETTINGS_KEY = "buildSettings";
		protected const string HEADER_SEARCH_PATHS_KEY = "HEADER_SEARCH_PATHS";
		protected const string LIBRARY_SEARCH_PATHS_KEY = "LIBRARY_SEARCH_PATHS";
		protected const string FRAMEWORK_SEARCH_PATHS_KEY = "FRAMEWORK_SEARCH_PATHS";
		protected const string OTHER_C_FLAGS_KEY = "OTHER_CFLAGS";
		protected const string OTHER_LDFLAGS_KEY = "OTHER_LDFLAGS";
		
		public XCBuildConfiguration( string guid, PBXDictionary dictionary ) : base( guid, dictionary )
		{
			
		}
		
		public PBXSortedDictionary buildSettings {
			get {
				if( ContainsKey( BUILDSETTINGS_KEY ) ) {
					if (_data[BUILDSETTINGS_KEY].GetType() == typeof(PBXDictionary)) {
						PBXSortedDictionary ret = new PBXSortedDictionary();
						ret.Append((PBXDictionary)_data[BUILDSETTINGS_KEY]);						
						return ret;
					}
					return (PBXSortedDictionary)_data[BUILDSETTINGS_KEY];	
				}
				return null;
			}
		}
		
		protected bool AddSearchPaths( string path, string key, bool recursive = true )
		{
			PBXList paths = new PBXList();
			paths.Add( path );
			return AddSearchPaths( paths, key, recursive );
		}
		
		protected bool AddSearchPaths( PBXList paths, string key, bool recursive = true, bool quoted = false ) //we want no quoting whenever we can get away with it
		{	
			//Debug.Log ("AddSearchPaths " + paths + key + (recursive?" recursive":"") + " " + (quoted?" quoted":""));
			bool modified = false;
			
			if( !ContainsKey( BUILDSETTINGS_KEY ) )
				this.Add( BUILDSETTINGS_KEY, new PBXSortedDictionary() );
			
			foreach( string path in paths ) {
				string currentPath = path;
				//Debug.Log ("path " + currentPath);
				if( !((PBXDictionary)_data[BUILDSETTINGS_KEY]).ContainsKey( key ) ) {
					((PBXDictionary)_data[BUILDSETTINGS_KEY]).Add( key, new PBXList() );
				}
				else if( ((PBXDictionary)_data[BUILDSETTINGS_KEY])[key] is string ) {
					PBXList list = new PBXList();
					list.Add( ((PBXDictionary)_data[BUILDSETTINGS_KEY])[key] );
					((PBXDictionary)_data[BUILDSETTINGS_KEY])[key] = list;
				}
				
				//Xcode uses space as the delimiter here, so if there's a space in the filename, we *must* quote. Escaping with slash may work when you are in the Xcode UI, in some situations, but it doesn't work here.
				if (currentPath.Contains(@" ")) quoted = true;
				
				if (quoted) {
					//if it ends in "/**", it wants to be recursive, and the "/**" needs to be _outside_ the quotes
					if (currentPath.EndsWith("/**")) {
						currentPath = "\\\"" + currentPath.Replace("/**", "\\\"/**");
					} else {
						currentPath = "\\\"" + currentPath + "\\\"";
					}
				}
				//Debug.Log ("currentPath = " + currentPath);
				if( !((PBXList)((PBXDictionary)_data[BUILDSETTINGS_KEY])[key]).Contains( currentPath ) ) {
					((PBXList)((PBXDictionary)_data[BUILDSETTINGS_KEY])[key]).Add( currentPath );
					modified = true;
				}
			}
		
			return modified;
		}
		
		public bool AddHeaderSearchPaths( PBXList paths, bool recursive = true )
		{
			return this.AddSearchPaths( paths, HEADER_SEARCH_PATHS_KEY, recursive );
		}
		
		public bool AddLibrarySearchPaths( PBXList paths, bool recursive = true )
		{