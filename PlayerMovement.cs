using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public int player;
	public float speed = 6.0F;
	private Vector3 moveDirection = Vector3.zero;

	void FixedUpdate() {
		
		CharacterController controller = GetComponent<CharacterController>();

		moveDirection = new Vector3(Input.GetAxis("Horizontal"+player), 0, Input.GetAxis("Vertical"+player));
		this.transform.LookAt(this.transform.position + moveDirection);

		float curSpeed = speed * moveDirection.magnitude;
		controller.SimpleMove(this.transform.forward * curSpeed);
		this.transform.localEulerAngles += new Vector3(0f,0f,Mathf.Sin(Time.fixedTime*20f))*2f;
		if(moveDirection.magnitude < 0.1f)
		{
			this.GetComponent<ParticleSystem>().Stop();
		}
		else if(!this.GetComponent<ParticleSystem>().isPlaying)
		{
			this.GetComponent<ParticleSystem>().Play();
		}
	}
}
