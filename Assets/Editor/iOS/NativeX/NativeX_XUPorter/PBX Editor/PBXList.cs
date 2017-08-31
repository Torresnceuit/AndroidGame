using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NativeX.UnityEditor.XCodeEditor
{
	public class PBXGroup : PBXObject
	{
		protected const string NAME_KEY = "name";
		protected const string CHILDREN_KEY = "children";
		protected const string PATH_KEY = "path";
		protected const string SOURCETREE_KEY = "sourceTree";
		
		#region Constructor
		
		public PBXGroup( string name, string path = null, string tree = "SOURCE_ROOT" ) : base()
		{	
			this.Add( CHILDREN_KEY, new PBXList() );
			this.Add( NAME_KEY, name );
			
			if( path != null ) {
				this.Add( PATH_KEY, path );
				this.Add( SOURCETREE_KEY, tree );
			}
			else {
				this.Add( SOURCETREE_KEY, "<group>" );
			}
		}
		
		public PBXGroup( string guid, PBXDictionary dictionary ) : base( guid, dictionary )
		{
			
		}
		
		#endregion
		#region Properties
		
		public PBXList children {
			get {
				if( !ContainsKey( CHILDREN_KEY ) ) {
					this.Add( CHILDREN_KEY, new PBXList() );
				}
				return (PBXList)_data[CHILDR