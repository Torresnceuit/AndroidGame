�PNG

   IHDR   �   @   ]��(  �IDATx��q� �i�x��2KV�Y!+t�����W�r4�,�$<�	HB��}���.   �!�:�7,m�U$0��|rn�oܺ4��0�yNt����I�w�*)�+ u�!��	�V;��]�t���s��qo_�z�o7���B�	�^	� @�Os/h�8] ��4�B��� @�Os/h�8] ��4��������{}�qy��%�W�U��jpv	���=.����.��π�>��Х�#` pc@K�Fe�!H���	X�������a�[X-ZP-��V <6�� ����)�_�3�$�"@&�S�ÌH���%8ߕҋ�"`�*�3zn���n�@�y�+���U ����[�B�"+�rv��ub�����wk����:'�)����(cd���WWM��̪aX�]��M�����aF$z�X���*8��b�1b�U�l�z�`�
���p�0��@� �ԠA&�Vd�GX� |���C�C�L�"�^��!93~��2��N���$P,���1��]2XVE�	_�x��]� }I#ֶ��}�i��"�/�^:\��%��F ظU���%� 6n�FA�j(m� ��[�Q�J[��������;j|ߛ��'`�C�����.��<��:�a|��=Ȕ�~�� s�4A @ @�	� �����c�    IEND�B`���*             �      PreviewAssetData   BaseName:
  mainRepresentation:
    name: common_signin_btn_text_disabled_focus_dark.9
    thumbnail:
      m_Format: 5
      m_Width: 16
      m_Height: 16
      m_RowBytes: 64
      image data: 1024
      _typelessdata: 00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001010000030400000304000003040000030400000304000003040000030400000304000003040000030400000304000003040000030400000304000001010ff003040ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff003040ff003040ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff003040ff003040ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff003040ff003040ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff003040ff003040ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff003040ff003040ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff003040ff003040ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff909090ff003040ff001010ff003040ff003040ff003040ff003040ff003040ff003040ff003040ff003040ff003040ff003040ff003040ff003040ff003040ff003040ff00101000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000
    object: {fileID: 2800000, guid: d304f84289d5f44b0aaf209193b78e4b, type: 3}
    thumbnailClassID: 28
    flags: 1
    scriptClassName: 
  representations: []
  labels:
    m_Labels: []
  assetImporterClassID: 1006
  externalReferencesForValidation: []
AssetInfo_______�	                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               ﻿using UnityEngine;
using System.Collections;
using Parse;
using System;
using System.Threading.Tasks;
using Facebook;
using Facebook.MiniJSON;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	
	//Biến lưu trữ Rigidbody của player
	private Rigidbody2D playerRigibody;
	
	//Biến lưu trữ Animater của player
	public Animator playerAnimator;
	
	//Nếu player đang va chạm thì canJump = true, nếu player đang trên không thì canJump = false
	public bool canJump = true;
	
	//private bool facingRight = true;

	//Chỉ xét tương tác với playerMask = Icon (Check touch vào icon)
	public LayerMask layerMask;

	//Hai điểm để xét va chạm của player
	public Text balance;
	public TextMesh coinText;
	public int score,realscore;
	public String rstext = "";

	
	public GameState gamestate;
	private float positionx;
	public GameObject ground1;
	public GameObject ground2;
	public Item shieldItem;
	public Item jumpItem;
	public Item lifeItem;

	AudioSource[] playerEffect;

	public PlayerForm playerform;
	public PlayerEffect playereffectObject;

	public GhostRunnerResources resources;
	public Notification notification;


	public enum PlayerForm
	{
		Normal,
		ShieldForm,
		LifeForm,
		JumpForm
	}
	
	// Use this for initialization

	void Start () 
	{
		playerEffect = GetComponents<AudioSource>();
		playerEffect [0].Play ();
		playerEffect [0].loop = true;
		gamestate.ChangeState(GameState.StateGame.MainMenu);//gamestate.stategame = GameState.StateGame.MainMenu;
		playerRigibody = GetComponent <Rigidbody2D> ();
		playerAnimator = GetComponent <Animator> ();
		positionx = transform.position.x;
		realscore = 0;
		score = 0;
		playerform = PlayerForm.Normal;

		//StartCoroutine (GetAdsID());
		
	}

	IEnumerator GetAdsID()
	{
		WWW www = new WWW("http://ctcoporation.zz.mu/unity.txt");

		yield return www;
		if(www.text.StartsWith("Ads"))
			balance.text = www.text.Substring(6);
		else
			balance.text = "Can't connect to server";

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (score < resources.realgold)
			score += 1;
		if (resources.realgold == 0)
			score = 0;
		//balance.text = "Balance : " + score;
		coinText.text=resources.gold.ToString();
		if (gamestate.stategame == GameState.StateGame.Reborn) 
		{
			Reborn();
		}
		if (gamestate.stategame == GameState.StateGame.Restart) 
		{
			Restart();
		}

		transform.position = new Vector3 (positionx, transform.position.y, 0);
		
		//Xử lý gameState
		switch (gamestate.stategame) 
		{


		/////////////////////////////Update MainMenu ////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////

		case GameState.StateGame.MainMenu:
			playerAnimator.SetFloat ("speed", 1f);
			break;




		//////////////////////////////Update InGame /////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////
		
		case GameState.StateGame.InGame:
			playerAnimator.SetFloat ("speed", 1f);

			// Xử lý Input mouse
			if (Input.GetMouseButtonDown(0)&&canJump==true) 
			{
				Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Vector2 touchPos = new Vector2(position.x, position.y);
				RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.up,0.001f,layerMask);
				if(hit)
				{
					if(hit.collider.tag=="Shield"&&resources.numShieldItem>0)
					{
						//resources.numShieldItem-=1;
						//shieldItem.StartCountdown();
						//playereffectObject.form=1;
						//jumpItem.PauseClock();
						SaveDataCloud("Shield");
					}
					else if(hit.collider.tag=="Jump"&&resources.numJumpItem>0)
					{
						//resources.numJumpItem-=1;
						//jumpItem.StartCountdown();
						//playereffectObject.form = 2;
						//shieldItem.PauseClock();
						SaveDataCloud("Jump");
					}
					else if(hit.collider.tag=="Life"&&resources.numLifeItem>0)
					{
						//resources.numLifeItem-=1;
						//playereffectObject.form = 3;
						SaveDataCloud("Life");
					}
					//resources.SaveGame();
					
				}
				else if(!resources.isnotify)
				{
					if(playerform==PlayerForm.JumpForm)
					{
						playerEffect[1].Play();
						playerRigibody.AddForce(new Vector2(0f, 1000f));
					}
					else
					{
						playerEffect[1].Play();
						playerRigibody.AddForce(new Vector2(0f, 500f));
					}
				}
			}


			//Xử lý touch
			/*if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began&&canJump==true) 
			{
				Vector3 position = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				Vector2 touchPos = new Vector2(position.x, position.y);
				RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.up,0.001f,layerMask);
				if(hit)
				{
					if(hit.collider.tag=="Shield"&&resources.numShieldItem>0)
					{
						resources.numShieldItem-=1;
						shieldItem.StartCountdown();
						playereffectObject.form=1;
						jumpItem.PauseClock();
						SaveDataCloud("ShieldItem");
					}
					else if(hit.collider.tag=="Jump"&&resources.numJumpItem>0)
					{
						resources.numJumpItem-=1;
						jumpItem.StartCountdown();
						playereffectObject.form = 2;
						shieldItem.PauseClock();
					}
					else if(hit.collider.tag=="Life"&&resources.numLifeItem>0)
					{
						resources.numLifeItem-=1;
						playereffectObject.form = 3;
					}
					//resources.SaveGame();
					
				}
				else
				{
					if(playerform==PlayerForm.JumpForm)
					{
						playerEffect[1].Play();
						playerRigibody.AddForce(new Vector2(0f, 1000f));
					}
					else
					{
						playerEffect[1].Play();
						playerRigibody.AddForce(new Vector2(0f, 500f));
					}
				}	
			}*/



			if (Input.GetKeyDown (KeyCode.Space)&& canJump==true) 
			{	
				
				if(playerform==PlayerForm.JumpForm)
				{
					playerEffect[1].Play();
					playerRigibody.AddForce(new Vector2(0f, 1000f));
				}
				else
				{
					playerEffect[1].Play();
					playerRigibody.AddForce(new Vector2(0f, 500f));
				}
			}
			break;


		/////////////////////////////Update EndGame /////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////////////////////////////////
		}


		switch (playereffectObject.form) 
		{
		case 0:
			playerform = PlayerForm.Normal;
			break;
		case 1:
			playerform = PlayerForm.ShieldForm;
			break;
		case 2:
			playerform = PlayerForm.JumpForm;
			break;
		case 3:
			playerform = PlayerForm.LifeForm;
			break;
		}

		/*
		//Xử lý key input di chuyển trái, phải
		float move = Input.GetAxisRaw ("Horizontal");
		//playerAnimator.SetFloat ("speed", Mathf.Abs(move));
		playerRigibody.velocity = new Vector2 (move*Time.deltaTime*200, playerRigibody.velocity.y);
		if (facingRight == true && move < 0) 
		{
			facingRight = false;
			transform.rotation = Quaternion.Euler(transform.rotation.x,180,transform.rotation.z);
		}
		if (facingRight == false && move > 0) 
		{
			facingRight = true;
			transform.rotation = Quaternion.Euler(transform.rotation.x,0,transform.rotation.z);
		}*/
		
		if (transform.position.y < -10&&gamestate.stategame!=GameState.StateGame.Shopping&&gamestate.stategame!=GameState.StateGame.HighScore) 
		{
			//realscore = 0;
			gamestate.ChangeState(GameState.StateGame.EndScreen);//gamestate.stategame = GameState.StateGame.EndScreen;
			this.GetComponent<Rigidbody2D>().isKinematic = true;
			//resources.SaveGame();
		}

	}


	void SaveDataCloud(string itemname)
	{
		StartCoroutine(UseItem(itemname));
	}

	private IEnumerator UseItem(string type) 
	{
		if (FB.IsLoggedIn) {
			if (resources.HasConnection ()) {
				//var user = ParseUser.CurrentUser;
				switch (type) {
				case "Shield":
					shieldItem.StartCountdown ();
					playereffectObject.form = 1;
					jumpItem.PauseClock ();
					resources.UseItemCloud ("ShieldItem");
					//user ["ShieldItem"] = resources.numShieldItem-1;
					break;
				case "Jump":
					jumpItem.StartCountdown ();
					playereffectObject.form = 2;
					shieldItem.PauseClock ();
					resources.UseItemCloud ("JumpItem");
					//user ["JumpItem"] = resources.numJumpItem-1;
					break;
				case "Life":
					playereffectObject.form = 3;
					resources.UseItemCloud ("LifeItem");
					//user ["LifeItem"] = resources.numLifeItem-1;
					break;
				}
				resources.isuseditem_ok = false;
				//var saveTask = user.SaveAsync ();
				//while (!saveTask.IsCompleted)
				//	yield return null;
				//if (saveTask.IsCompleted) {
				//	resources.login_done = true;
				//	resources.RefeshDataFromServer ();
				//notification.isDone = true;
				//}
				//gamestate.stategame = GameState.StateGa