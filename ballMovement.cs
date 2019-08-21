using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballMovement : MonoBehaviour {

	public int player;
	public enum movementType {Sin = 0, Square = 1, Triangle = 2, Sawtooth = 3};
	public movementType chosenMovement;
	public float amplitude = 0.01f;
	public float frequency = 1f;
	public float wavelenght = 1f;
	public bool throwBall = false;
	public PlayerStats playerStats;
	public bool flying = false;

	[HideInInspector]
	public int score;

	Vector3 startPos;

	void Start () 
	{
		startPos = this.transform.position;
	}

	void Update () 
	{
		
		if(Input.GetButtonDown("SwitchLeft"+player))
		{
			chosenMovement = (int)chosenMovement <= 0 ? chosenMovement+3 : chosenMovement-1;
			playerStats.setWave((int)chosenMovement);
		}
		else if(Input.GetButtonDown("SwitchRight"+player))
		{
			chosenMovement = (int)chosenMovement <= 2 ? chosenMovement+1 : chosenMovement-3;
			playerStats.setWave((int)chosenMovement);
		}
		//print(chosenMovement);

		if(Input.GetButton("Fire"+player))
		{
			if(!throwBall)
			{
				wavelenght = Mathf.Clamp(wavelenght + 0.25f, 0f, 15f);
				playerStats.setStrenght(wavelenght);
			}
		}

		if(throwBall && !flying)
		{
			switch (chosenMovement)
			{
			case movementType.Sin :
				sineShot(); break;
			case movementType.Square :
				squareShot(); break;
			case movementType.Triangle :
				triangleShot(); break;
			case movementType.Sawtooth :
				sawtoothShot(); break;
			}
		}

		if(this.transform.position.y < -100f)
		{
			this.transform.position = startPos;
		}
	}

	void sineShot()
	{
		if(wavelenght > 0f)
		{
			this.transform.localEulerAngles = (new Vector3(0f, Mathf.Cos(Time.fixedTime * frequency), 0f) * amplitude );
			this.transform.position += this.transform.forward * Time.deltaTime * wavelenght; 

			wavelenght -= 0.05f;
			playerStats.setStrenght(wavelenght);
		}
	}

	void squareShot()
	{
		if(wavelenght > 0f)
		{
			this.transform.localEulerAngles = (new Vector3(0f, Mathf.Round(Mathf.Cos(Time.fixedTime * frequency)), 0f) * amplitude);
			this.transform.position += this.transform.forward * Time.deltaTime * wavelenght; 

			wavelenght -= 0.05f;
			playerStats.setStrenght(wavelenght);
		}
	}

	void triangleShot()
	{
		if(wavelenght > 0f)
		{
			this.transform.localEulerAngles = new Vector3(0f, Mathf.Sign(Mathf.Cos(Time.fixedTime * frequency))*0.5f, 0f) * amplitude;
			this.transform.position += this.transform.forward * Time.deltaTime * wavelenght; 

			wavelenght -= 0.05f;
			playerStats.setStrenght(wavelenght);
		}
	}

	void sawtoothShot()
	{
		if(wavelenght > 0f)
		{
			if(Mathf.Cos(Time.fixedTime * frequency) < 0f)
				this.transform.localEulerAngles = new Vector3(0f, Mathf.Sign(Mathf.Cos(Time.fixedTime * frequency))*0.75f, 0f) * amplitude;
			else
				this.transform.localEulerAngles = new Vector3(0f, 1f, 0f) * amplitude;
			
			this.transform.position += this.transform.forward * Time.deltaTime * wavelenght; 

			wavelenght -= 0.05f;
			playerStats.setStrenght(wavelenght);
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.name.Contains("Player"))
		{
			if(other.gameObject.name != "Player"+player)
			{
				//TODO add death sound
				StartCoroutine(respaun(other.gameObject, 5f));
			}
		}/*else {
			print(other.gameObject.name);
			if (other.gameObject.name == "Cube") {
				//this.transform.forward = new Vector3(Random.Range(0.0f, 0.1f), 10.0f, Random.Range(0.0f,0.1f));
				this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0.0f, 10f), 20.0f, Random.Range(0.0f,10f)),ForceMode.Impulse);
				this.flying = true;
			} else if (other.gameObject.name == "Arena"){
				this.flying = false;
			}
		}*/
	}

	IEnumerator respaun(GameObject obj, float seconds)
	{
		obj.SetActive(false);
		playerStats.setScore(++score);
		yield return new WaitForSeconds (seconds);
		obj.SetActive(true);
	}
}
