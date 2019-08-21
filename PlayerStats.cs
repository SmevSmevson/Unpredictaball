using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

	[HideInInspector]
	public int score;

	public void setScore(int score)
	{
		this.score = score;
		this.transform.GetChild(1).GetComponent<Text>().text = "Score : " + score;
	}

	public void setWave(int wave)
	{
		//sin uv rect = x0 y0.76 w0 h0.25
		//square y0.51
		//triangle y0.26
		//sawtooth y0.01
		if(wave == 0)
		{
			this.transform.GetChild(3).GetComponent<RawImage>().uvRect = new Rect(0f, 0.76f, 1f, 0.25f);
		}
		else if(wave == 1)
		{
			this.transform.GetChild(3).GetComponent<RawImage>().uvRect = new Rect(0f, 0.51f, 1f, 0.25f);
		}
		else if(wave == 2)
		{
			this.transform.GetChild(3).GetComponent<RawImage>().uvRect = new Rect(0f, 0.26f, 1f, 0.25f);
		}
		else if(wave == 3)
		{
			this.transform.GetChild(3).GetComponent<RawImage>().uvRect = new Rect(0f, 0.01f, 1f, 0.25f);
		}
	}

	public void setStrenght(float str)
	{
		this.transform.GetChild(4).GetComponent<Image>().fillAmount = str/15f;
	}
}
