 extension ] );
			this.buildPhase = PBXFileReference.typePhases[ extension ];
		}
		
		private void SetFileType( string fileType )
		{
			this.Remove( EXPLICIT_FILE_TYPE_KEY );
			this.Remove( LASTKNOWN_FILE_TYPE_KEY );
			
			this.Add( EXPLICIT_FILE_TYPE_KEY, fileType );
		}
	}
	
	public enum TreeEnum {
		ABSOLUTE,
        GROUP,
        BUILT_PRODUCTS_DIR,
        DEVELOPER_DIR,
        SDKROOT,
        SOURCE_ROOT
	}
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            