 here";
			thereIsConnection = true;
			checkinginternetisdone = true;
		}*/


		/*float timeTaken = 0.0F;
		float maxTime = 2.0F;
		bool tested = false;
		Ping testPing = new Ping( "8.8.8.8" );// ping google.com
		while(!tested)
		{
			timeTaken = 0.0F;
			while ( !testPing.isDone )
			{
				
				timeTaken += Time.deltaTime;
				
				
				if ( timeTaken > maxTime )
				{
					// if time has exceeded the max
					// time, break out and return false
					thereIsConnection = false;
					checkinginternetisdone = true;
					tested = true;
					break;
				}
				
				yield return null;
			}
			if ( timeTaken <= maxTime )
			{
				thereIsConnection = true;
				checkinginternetisdone = true;
				tested = true;
			}
			yield return null;
		}*/
	}
}

                                                   