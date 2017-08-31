.IsCompleted) yield return null;
		if (saveTask.IsCompleted) {
			resources.login_done = true;
			resources.RefeshDataFromServer();
			//notification.isDone = true;
		}
		//gamestate.stategame = GameState.StateGame.Reborn;
		//UpdateProfile();
		//}
	}
	
	private string getDataValueForKey(Dictionary<string, object> dict, string key) {
		object objectForKey;
		if (dict.TryGetValue(key, out objectForKey)) {
			return (string)objectForKey;
		} else {
			return "";
		}
	}
	
	void OnMouseDown(){
		//Screen.orientation = ScreenOrientation.Portrait;
		//FB.Logout();
		if (resources.HasConnection ()) {
			if (gamestate.isfacebookclick == false) {
				if (!FB.IsLoggedIn) {
					gamestate.isfacebookclick = true;
					FB.Login ("public_profile,email,user_friends", LoginCallback);
					//gamestate.stategame = GameState.StateGame.Reborn;
				} else {
					//gamestate.stategame = GameState.StateGame.Reborn;
					gamestate.isfacebookclick = true;
					StartCoroutine ("ParseLogin");
					/*FB.Feed(
				link: "http://s1.anh.im/2015/05/01/icon17e07.png",
				linkName: "Test thôi mà",
				linkCaption: "Test tí cho vui",
				linkDescription: "Test nào",
				picture: "http://s1.anh.i