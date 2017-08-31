using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NativeX.UnityEditor.XCodeEditor
{
	public class PBXObject
	{
		protected const string ISA_KEY = "isa";
		
		protected string _guid;
		protected PBXDictionary _data;
		
		#region Properties
		
		public string guid {
			get {
				if( string.IsNullOrEmpty( _guid ) )
					_guid = GenerateGuid();
				
				return _guid;
			}
		}
		
		public PBXDictionary data {
			get {
				if( _data == null )
					_data = new PBXDictionary();
				
				return _data;
			}
		}
		
		
		#endregion
		#region Constructors
		
		public PBXObject()
		{
			_data = new PBXDictionary();
			_data[ ISA_KEY ] = this.GetType().Name;
			_guid = GenerateGuid();
		}
		
		public PBXObject( string guid ) : this()
		{
			if( IsGuid( guid ) )
				_guid = guid;
		}
		
		public PBXObject( string gui