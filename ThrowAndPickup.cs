using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAndPickup : MonoBehaviour {

	public int player;
	bool haveBall = false;
	public GameObject ball;
	public GameObject playerHand;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetButtonUp("Fire"+player))
		{
			
			//pick up the ball if you are near it
			if(!haveBall)
			{
				if(Vector3.Distance(playerHand.transform.position, ball.transform.position) <= 2f)
				{
					haveBall = true;
					ball.GetComponent<Rigidbody>().isKinematic = true;
					ball.GetComponent<ballMovement>().throwBall = false;
					this.transform.parent = playerHand.transform;
					this.transform.position = playerHand.transform.position;
					ball.transform.localPosition = Vector3.zero;
				}
			}
			else //throw the ball
			{
				haveBall = false;
				ball.GetComponent<Rigidbody>().isKinematic = false;
				ball.GetComponent<ballMovement>().throwBall = true;
				this.transform.forward = playerHand.transform.forward;
				this.transform.parent = null;
			}
		}
	}
}
