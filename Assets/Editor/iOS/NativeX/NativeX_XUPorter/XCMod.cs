]).ContainsKey( OTHER_C_FLAGS_KEY ) ) {
					((PBXDictionary)_data[BUILDSETTINGS_KEY]).Add( OTHER_C_FLAGS_KEY, new PBXList() );
				}
				else if ( ((PBXDictionary)_data[BUILDSETTINGS_KEY])[ OTHER_C_FLAGS_KEY ] is string ) {
					string tempString = (string)((PBXDictionary)_data[BUILDSETTINGS_KEY])[OTHER_C_FLAGS_KEY];
					((PBXDictionary)_data[BUILDSETTINGS_KEY])[ OTHER_C_FLAGS_KEY ] = new PBXList();
					((PBXList)((PBXDictionary)_data[BUILDSETTINGS_KEY])[OTHER_C_FLAGS_KEY]).Add( tempString );
				}
				
				if( !((PBXList)((PBXDictionary)_data[BUILDSETTINGS_KEY])[OTHER_C_FLAGS_KEY]).Contains( flag ) ) {
					((PBXList)((PBXDictionary)_data[BUILDSETTINGS_KEY])[OTHER_C_FLAGS_KEY]).Add( flag );
					modified = true;
				}
			}
			
			return modified;
		}

		public bool AddOtherLinkerFlags( string flag )
		{
			PBXList flags = new PBXList();
			flags.Add( flag );
			return AddOtherLinkerFlags( flags );
		}

		public bool AddOtherLinkerFlags( PBXList flags )
		{
			bool modified = false;

			if( !ContainsKey( BUILDSETTINGS_KEY ) )
				this.Add( BUILDSETTINGS_KEY, new PBXSortedDictionary() );

			foreach( string flag in flags ) {
				
				if( !((PBXDictionary)_data[BUILDSETTINGS_KEY]).ContainsKey( OTHER_LDFLAGS_KEY ) ) {
					((PBXDictionary)_data[BUILDSETTINGS_KEY]).Add( OTHER_LDFLAGS_KEY, new PBXList() );
				}
				else if ( ((PBXDictionary)_data[BUILDSETTINGS_KEY])[ OTHER_LDFLAGS_KEY ] is string ) {
					string tempString = (string)((PBXDictionary)_data[BUILDSETTINGS_KEY])[OTHER_LDFLAGS_KEY];
					((PBXDictionary)_data[BUILDSETTINGS_KEY])[ OTHER_LDFLAGS_KEY ] = new PBXList();
					if( !string.IsNullOrEmpty(tempString) ) {
						((PBXList)((PBXDictionary)_data[BUILDSETTINGS_KEY])[OTHER_LDFLAGS_KEY]).Add( tempString );
					}
				}
				
				if( !((PBXList)((PBXDictionary)_data[BUILDSETTINGS_KEY])[OTHER_LDFLAGS_KEY]).Contains( flag ) ) {
					((PBXList)((PBXDictionary)_data[BUILDSETTINGS_KEY])[OTHER_LDFLAGS_KEY]).Add( flag );
					modified = true;
				}
			}

			return modified;
		}
		
		public bool overwriteBuildSetting(string settingName, string settingValue) {
			Debug.Log ("overwriteBuildSetting " + settingName + " " + settingValue);
			bool modified = false;
			
			if( !ContainsKey( BUILDSETTINGS_KEY ) ) {
				Debug.Log ("creating key " + BUILDSETTINGS_KEY);
				this.Add( BUILDSETTINGS_KEY, new PBXSortedDictionary() );
			}
				
			if( !((PBXDictionary)_data[BUILDSETTINGS_KEY]).ContainsKey( settingName ) ) {
				Debug.Log("adding key " + settingName);
				 ((PBXDictionary)_data[BUILDSETTINGS_KEY]).Add( settingName, new PBXList() );
			}
			else if ( ((PBXDictionary)_data[BUILDSETTINGS_KEY])[ settingName ] is string ) {
				//Debug.Log("key is string:" + settingName);
				//string tempString = (string)((PBXDictionary)_data[BUILDSETTINGS_KEY])[settingName];
				((PBXDictionary)_data[BUILDSE