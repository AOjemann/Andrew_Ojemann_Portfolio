using UnityEngine;
using System.Collections;

public class hero : MonoBehaviour {

	//all hero rate arrays are stored alphabeticaly
	
	//relevent hero information
	public string heroName = "none";
	public float globalWinRate;

	public float player1WinRate;
	public float player2WinRate;
	public float player3WinRate;
	public float player4WinRate;
	public float player5WinRate;

	public int player1TotalPlayed;
	public int player2TotalPlayed;
	public int player3TotalPlayed;
	public int player4TotalPlayed;
	public int player5TotalPlayed;
	//role is "wa" "as" "sup" "spec"
	public string role;

	//adjusted win rate used in maths
	public float adjWinRate = 0;

	//win rates against other heros
	public float[] vsWinRates;
	//where this hero falls in aphabetical order starting from 0 and increasing
	public int alphabeticalIndex;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
