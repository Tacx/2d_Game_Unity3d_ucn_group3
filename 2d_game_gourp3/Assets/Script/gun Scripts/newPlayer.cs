﻿using UnityEngine;
using System.Collections;

public class newPlayer : MonoBehaviour {
	
	
	[HideInInspector]
	public bool jump = false;
	[HideInInspector]
	public bool facingRight = true;
	
	public float moveForce = 200f;
	public float maxSpeed = 5f;
	public float jumpForce = 10f;
	
	Animator anim;
	private Transform groundCheck;
	private bool grounded = false;
	public int gunType=0;
	private bool guiEnabled = false;
	
	void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("groundCheck");
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		
		if(Input.GetButtonDown("Jump") && grounded)
			jump = true;
		
		if(Input.GetKeyDown("escape")){
			if(Time.timeScale == 1){
				Time.timeScale = 0;
			}
			else {
				Time.timeScale = 1;
			}
		}
	}
	
	void FixedUpdate()
	{
		float hrizontal = Input.GetAxis ("Horizontal");
		
		if (hrizontal * rigidbody2D.velocity.x < maxSpeed)
			rigidbody2D.AddForce (Vector2.right * hrizontal * moveForce);
		if (Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		if (jump) 
		{
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			
			jump = false;
		}
		anim.SetBool ("falling", grounded);
		if (Input.GetButtonDown ("1")) {
			this.GetComponentInChildren<newGun>().setGunType(0);
		}
		if (Input.GetButtonDown ("2")) {
			this.GetComponentInChildren<newGun>().setGunType(1);
		}
		if (Input.GetButtonDown ("3")) {
			this.GetComponentInChildren<newGun>().setGunType(2);
		}
	}
	
	public void Flip()
	{
		facingRight = !facingRight;
		
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
