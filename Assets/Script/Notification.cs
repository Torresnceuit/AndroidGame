inding customType isPPtrCurve pptrCurveMapping MuscleClipInfo m_AnimationClipSettings m_StartTime m_StopTime m_OrientationOffsetY m_Level m_CycleOffset m_LoopTime m_LoopBlend m_LoopBlendOrientation m_LoopBlendPositionY m_LoopBlendPositionXZ m_KeepOriginalOrientation m_KeepOriginalPositionY m_KeepOriginalPositionXZ m_HeightFromFeet m_Mirror m_EditorCurves m_EulerEditorCurves m_HasGenericRootTransform m_HasMotionFloatCurves m_GenerateMotionCurves m_Events AnimationEvent functionName objectReferenceParameter floatParameter intParameter messageOptions      @�p         �   J   J ��         d�����F�FK�ި�                                            Normal                                       E       m_Sprite    �                 pB                                                   �        E        ���<                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  using UnityEngine;
using System.Collections;

public class Notification : MonoBehaviour
{

	public GhostRunnerResources resource;
	public GameState gamestate;
	public GameObject Title;
	public GameObject OKButton;
	public GameObject OKButton_2;
	public GameObject CancelButton;
	public Sprite WaitTitleSprite;
	public Sprite DoneTitleSprite;
	public Sprite LogoutTitleSprite;
	public Sprite QuitGameTitleSprite;
	public TextMesh detail;
	public NotificationType type;
	public bool isDone = false;
	public string OpponentName ="Enemy";
	// Use this for initialization

	public enum NotificationType
	{
		Logging,
		Logout,
		Loading,
		Saving,
		SavingChallenge,
		SavingChallenge2,
		RequestServer,
		SearchingOpponent,
		NoConnection,
		ServerNotAvaiable,
		NotEnoughMoney,
		Notify,
		ChallengeRequest_1,
		ChallengeRequest_2,
		ChallengeRequest_3,
		CheckWinCloseChallenge,
		QuitGame
	}

	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{

		switch (type) 
		{
			case NotificationType.Logging:
				if (!isDone) 
				{
					Title.GetComponent<SpriteRenderer>().sprite = WaitTitleSprite;
					detail.text = "Please wait a few seconds \n to loading data...";
					if(resource.loaddata_ok)
					{
						isDone=true;
					}
					OKButton.SetActive(false);
					OKButton_2.SetActive(false);
					CancelButton.SetActive(false);
				}
				else
				{
					Title.GetComponent<SpriteRenderer>().sprite = DoneTitleSprite;
					detail.text = "All success. Click OK to continue...";
					OKButton.SetActive(true);
					OKButton_2.SetActive(false);
					CancelButton.SetActive(false);
				}
			break;

			case NotificationType.Logout:
				Title.GetComponent<SpriteRenderer>().sprite = LogoutTitleSprite;
				detail.text = "Do you want to logout? \n All data will be reset to 0...";
				OKButton.SetActive(true);
				OKButton_2.SetActive(false);
				CancelButton.SetActive(false);
			break;

			case NotificationType.Loading:

			break;

			case NotificationType.Saving:
				if (!isDone) 
				{
					Title.GetComponent<SpriteRenderer>().sprite = WaitTitleSprite;
					detail.text = "Processing, please wait...";
					if(resource.loaddata_ok)
					{
						isDone=true;
					}
					OKButton.SetActive(false);
					OKButton_2.SetActive(false);
					CancelButton.SetActive(false);
				}
				else if(resource.loaddata_ok)
				{
					/*Title.GetComponent<SpriteRenderer>().sprite = DoneTitleSprite;
					detail.text = "All success. Click OK to continue...";
					OKButton.SetActive(true);*/
					isDone = false;
					resource.isnotify = false;
					this.gameObject.SetActive (false);
					resource.loaddata_ok = false;
				}
			break;

			case NotificationType.SavingChallenge:
				if (!isDone) 
				{
					Title.GetComponent<SpriteRenderer>().sprite = WaitTitleSprite;
					detail.text = "Processing, please wait...";
					if(resource.savechallenge)
					{
						isDone=true;
					}
					OKButton.SetActive(false);
					OKButton_2.SetActive(false);
					CancelButton.SetActive(false);
				}
				else if(resource.savechallenge)
				{
					Title.GetComponent<SpriteRenderer>().sprite = DoneTitleSprite;
					detail.text = "You have been finished your challenge in \n "+gamestate.deltaTime.ToString()
						+" .The result will be sent to \n"+resource.opponentname+"\n to decide who is the winner";
					OKButton.SetActive(true);
					OKButton_2.SetActive(false);
					CancelButton.SetActive(false);
					

					//isDone = false;
					//resource.isnotify = false;
					//this.gameObject.SetActive (false);
					//resource.savechallenge = false;
				}
				break;

		case NotificationType.SavingChallenge2:
			if (!isDone) 
			{
				Title.GetComponent<SpriteRenderer>().sprite = WaitTitleSprite;
				detail.text = "Processing, please wait...";
				if(resource.savechallenge)
				{
					isDone=true;
				}
				OKButton.SetActive(false);
				OKButton_2.SetActive(false);
				CancelButton.SetActive(false);
			}
			else if(resource.savechallenge)
			{
				isDone = false;
				resource.isnotify = false;
				this.gameObject.SetActive (false);
				resource.savechallenge = false;
			}
			break;

		case NotificationType.SearchingOpponent:
				if (!isDone) 
				{
					Title.GetComponent<SpriteRenderer>().sprite = WaitTitleSprite;
					detail.text = "Searching oppo