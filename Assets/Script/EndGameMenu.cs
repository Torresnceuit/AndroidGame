 Chính+Ho Chi Minh City, Vietnam+https://graph.facebook.com/822403307808270/picture?type=large&width=128&height=128&return_ssl_resources=1+5555*
	 Đoàn Minh Chính+undefined+https://graph.facebook.com/602686439834954/picture?type=large&width=128&height=128&return_ssl_resources=1+5555*
	*/
	public void QueryScore()
	{
		//StartCoroutine( PayPoints());
		//string tempstring = result;
		string[] results = rstext.Split('#');
		var topbestavatar = new Dictionary<string, string>();
		foreach (string word in results)
		{
			Debug.Log(word);
		}
		for(int i=0;i<results.Length;i++)
		{
			string[] result = results[i].Split('$');
			GameObject ScorePanel;
			ScorePanel = Instantiate (ScoreEntryPanel) as GameObject;
			ScorePanel.transform.parent = ScoreScrollList.transform;
			Transform ThisName = ScorePanel.transform.Find ("Name");
			Transform ThisScore = ScorePanel.transform.Find ("Score");
			Transform ThisAvatar = ScorePanel.transform.Find ("Avatar");
			Text ScoreName = ThisName.GetComponent<Text> ();
			Text ScoreScore = ThisScore.GetComponent<Text> ();
			Image ScoreAvatar = ThisAvatar.GetComponent<Image> ();
			ScoreName.text = result[1];
			ScoreScore.text = result[3];
			FB.API (Util.GetPictureURL(result[0],128,128),HttpMethod.GET,delegate(FBResult pictureResult)
			{
				if(pictureResult.Error!=null)
				{
					Debug.Log("Error!");
				}
				else
				{
					ScoreAvatar.sprite = Sprite.Create (pictureResult.Texture, new Rect (0, 0, 128, 128), new Vector2 (0, 0));
				}
			});
		}
	}
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             