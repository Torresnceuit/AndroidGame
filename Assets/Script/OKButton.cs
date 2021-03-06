       diff += 7;
        }
        int refDay = Time.EPOCH_JULIAN_DAY - diff;
        return (julianDay - refDay) / 7;
    }

    /**
     * Render an animator to pulsate a view in place.
     * @param labelToAnimate the view to pulsate.
     * @return The animator object. Use .start() to begin.
     */
    public static ObjectAnimator getPulseAnimator(View labelToAnimate, float decreaseRatio,
            float increaseRatio) {
        Keyframe k0 = Keyframe.ofFloat(0f, 1f);
        Keyframe k1 = Keyframe.ofFloat(0.275f, decreaseRatio);
        Keyframe k2 = Keyframe.ofFloat(0.69f, increaseRatio);
        Keyframe k3 = Keyframe.ofFloat(1f, 1f);

        PropertyValuesHolder scaleX = PropertyValuesHolder.ofKeyframe("scaleX", k0, k1, k2, k3);
        PropertyValuesHolder scaleY = PropertyValuesHolder.ofKeyframe("scaleY", k0, k1, k2, k3);
        ObjectAnimator pulseAnimator =
                ObjectAnimator.ofPropertyValuesHolder(labelToAnimate, scaleX, scaleY);
        pulseAnimator.setDuration(PULSE_ANIMATOR_DURATION);

        return pulseAnimator;
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              using UnityEngine;
using System.Collections;
using Parse;

public class OKButton : MonoBehaviour
{

	public GameState gamestate;
	public GhostRunnerResources resources;
	public Notification notification;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnMouseDown(){
		switch (this.transform.parent.GetComponent<Notification> ().type) 
		{

		case Notification.NotificationType.Logging:
			this.transform.parent.GetComponent<Notification> ().isDone = false;
			resources.isnotify = false;
			resources.loaddata_ok = false;
			if(gamestate.laststate == "MainMenu")
				gamestate.ChangeState(GameState.StateGame.Reborn);
				//gamestate.stategame = GameState.StateGame.Reborn;
			else if(gamestate.laststate == "EndGame")
				gamestate.ChangeState(GameState.StateGame.EndScreen);
				//gamestate.stategame = GameState.StateGame.EndScreen;
			this.transform.parent.gameObject.SetActive (false);
			break;

		case Notification.NotificationType.Logout:

			if (FB.IsInitialized && FB.IsLoggedIn) 
			{
				FB.Logout();
				if (ParseUser.CurrentUser != null)
				{
					ParseUser.LogOut();
				}
				resources.login_done=false;
			}
			resources.Reset ();
			this.transform.parent.GetComponent<Notification> ().isDone = false;
			resources.isnotify = false;
			if(gamestate.laststate == "MainMenu")
				gamestate.ChangeState(GameState.StateGame.Reborn);
				//gamestate.stategame = GameState.StateGame.Reborn;
			else if(gamestate.laststate == "EndGame")
				gamestate.ChangeState(GameState.StateGame.EndScreen);
				//gamestate.stategame = GameState.StateGame.EndScreen;
			this.transform.parent.gameObject.SetActive (false);
			break;

		case Notification.NotificationType.NoConnection:
			
			if (FB.IsInitialized && FB.IsLoggedIn) 
			{
				FB.Logout();
				if (ParseUser.CurrentUser != null)
				{
					ParseUser.LogOut();
				}
				resources.login_done=false;
			}
			resources.Reset ();
			this.transform.parent.GetComponent<Notification> ().isDone = false;
			resources.isnotify = false;
			if(gamestate.laststate == "MainMenu")
				gamestate.ChangeState(GameState.StateGame.Reborn);
				//gamestate.stategame = GameState.StateGame.Reborn;
			else if(gamestate.laststate == "EndGame")
				gamestate.ChangeState(GameState.StateGame.EndScreen);
				//gamestate.stategame = GameState.StateGame.EndScreen;
			else if(gamestate.laststate == "InGame")
				gamestate.ChangeState(GameState.StateGame.InGame);
				//gamestate.stategame = GameState.StateGame.InGame;
			this.transform.parent.gameObject.SetActive (false);
			//resources.offlinemode=true;
			break;

		case Notification.NotificationType.ServerNotAvaiable:
			
			if (FB.IsInitialized && FB.IsLoggedIn) 
			{
				FB.Logout();
				if (ParseUser.CurrentUser != null)
				{
					ParseUser.LogOut();
				}
				resources.login_done=false;
			}
			resources.Reset ();
			this.transform.parent.GetComponent<Notification> ().isDone = false;
			resources.isnotify = false;
			if(gamestate.laststate == "MainMenu")
				gamestate.ChangeState(GameState.StateGame.Reborn);
				//gamestate.stategame = GameState.StateGame.Reborn;
			else if(gamestate.laststate == "EndGame")
				gamestate.ChangeState(Gam