﻿//Not Use

using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	private Rigidbody2D playerRigibody;
	private Animator playerAnimator;
	private bool facingRight=true;
	private bool canJump = true;
	private bool moveforward = true;
	public Transform startPointCheck;
	public Transform endPointCheck;
	public Transform player;
	public LayerMask layerMask;
	// Use this for initialization
	void Start () 
	{
		
		playerRigibody = GetComponent <Rigidbody2D> ();
		playerAnimator = GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{

		float move = Input.GetAxisRaw ("Horizontal");
		if (transform.position.x >= player.position.x - 1)
			moveforward = false;
			
		if (transform.position.x <= player.position.x - 5)
			moveforward = true;
			
		if (moveforward == true&& canJump==true) 
		{
			playerRigibody.velocity = new Vector2 (1*Time.deltaTime*50, playerRigibody.velocity.y);
		} 
		else if (moveforward == false|| canJump==false) 
		{
			playerRigibody.velocity = new Vector2 (-1*Time.deltaTime*50, playerRigibody.velocity.y);
		}
		//playerAnimator.SetFloat ("speed", Mathf.Abs(move));
		playerAnimator.SetFloat ("speed", 1f);
		//playerRigibody.velocity = new Vector2 (move*Time.deltaTime*200, playerRigibody.velocity.y);
		if (facingRight == true && move < 0) 
		{