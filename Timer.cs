using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	float time = 120f;
	public PlayerStats[] playersStats;
	int topScore = 0;
	string winner;
	bool roundFinished;
	// Use this for initialization
	void Start () {
		winner = "Draw!";
	}
	
	// Update is called once per frame
	void Update () {
		if(0 < time)
		{
			time -= Time.deltaTime;
			this.GetComponent<Text>().text = ""+(int)(time);
		}
		else if(!roundFinished)
		{
			for(int i = 0; i <= playersStats.Length-1; i++)
			{
				if(playersStats[i].score > topScore)
				{
					topScore = playersStats[i].score;
					winner = playersStats[i].transform.GetChild(0).GetComponent<Text>().text;
				}
			}
			this.GetComponent<Text>().text = "Winner : " + winner;
			roundFinished = true;
		}
	}
}
