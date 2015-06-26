using UnityEngine;
using System.Collections;

public class MasterCtrScr : MonoBehaviour {

	weightedWinRate weightedScript;

	//are we blue or red side
	public bool blueSide = true;

	//track heros selected so far
	int selectedByUs;
	int selectedByThem;
	//int selectedTotal;
	//string[] selectedNames;
	string[] selectedUsNames;
	string[] selectedThemNames;

	//keeps track of how many of each role our team needs left
	public int TanksLeft = 1;
	public int SupportsLeft = 1;
	public int DamageLeft = 3;

	//array of all heros data
	GameObject[] heroObjArr;

	//blank hero for calculation purposes
	hero blankhero;

	//is the programing accepting enemy info
	public bool enemyTurn;
	//hero in the process of being selected
	public string heroOnSelect = "none";

	//make sure the game is in startup phase
	bool gameIsStarting;

	//text fields for displaying heros
	public GameObject op1;
	public GameObject op2;
	public GameObject op3;
	public GameObject op4;
	public GameObject op5;
	public GameObject en1;
	public GameObject en2;
	public GameObject en3;
	public GameObject en4;
	public GameObject en5;

	textChanger op1t;
	textChanger op2t;
	textChanger op3t;
	textChanger op4t;
	textChanger op5t;
	textChanger en1t;
	textChanger en2t;
	textChanger en3t;
	textChanger en4t;
	textChanger en5t;


	// Use this for initialization
	void Start () {

		weightedScript = this.GetComponent<weightedWinRate> ();

		heroObjArr = GameObject.FindGameObjectsWithTag ("hero");
	
		selectedUsNames = new string[5];
		selectedThemNames = new string[5];
		selectedByUs = 0;
		selectedByThem = 0;

		op1t = op1.GetComponent<textChanger> ();
		op2t = op2.GetComponent<textChanger> ();
		op3t = op3.GetComponent<textChanger> ();
		op4t = op4.GetComponent<textChanger> ();
		op5t = op5.GetComponent<textChanger> ();
		en1t = en1.GetComponent<textChanger> ();
		en2t = en2.GetComponent<textChanger> ();
		en3t = en3.GetComponent<textChanger> ();
		en4t = en4.GetComponent<textChanger> ();
		en5t = en5.GetComponent<textChanger> ();

		gameIsStarting = true;

		blankhero = GameObject.FindGameObjectWithTag ("BlHero").GetComponent<hero> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//adds heros to list
	public void addHero(bool addedByThem, string tmpName){
		if (!addedByThem) {
			selectedUsNames[selectedByUs] = tmpName;
			selectedByUs = selectedByUs + 1;
		}
		if (addedByThem) {
			selectedThemNames[selectedByThem] = tmpName;
			selectedByThem = selectedByThem + 1;
		}
	}

	//swaps the side
	public void selectSide(bool blueSide){
		Debug.Log ("selectSide called");
		if(gameIsStarting){
			gameIsStarting = false;
			if (blueSide) {
				Debug.Log("blue Selected");
				//crashes unity
				StartCoroutine(DraftBlue());
				//DraftBlue();
			} else {
				Debug.Log("red selected");
				StartCoroutine(DraftRed());
			}
		}
	}

	//starts the draft after the swap is slected
	IEnumerator DraftBlue(){
		Debug.Log ("blue draft started");
		enemyTurn = false;
		heroOnSelect = "none";
		addHero(false, draft (1));
		op1t.changeText (selectedUsNames [0]);
		Debug.Log ("" + selectedUsNames [0]);
		enemyTurn = true;
		yield return StartCoroutine (waitForHeroSelect());
		en1t.changeText (heroOnSelect);
		pushSelectedHero ();
		yield return StartCoroutine (waitForHeroSelect());
		en2t.changeText (heroOnSelect);
		pushSelectedHero ();
		enemyTurn = false;
		addHero(false, draft (2));
		op2t.changeText (selectedUsNames [1]);
		addHero(false, draft (3));
		op3t.changeText (selectedUsNames [2]);
		enemyTurn = true;
		yield return StartCoroutine (waitForHeroSelect());
		en3t.changeText (heroOnSelect);
		pushSelectedHero ();
		yield return StartCoroutine (waitForHeroSelect());
		en4t.changeText (heroOnSelect);
		pushSelectedHero ();
		enemyTurn = false;
		addHero(false, draft (4));
		op4t.changeText (selectedUsNames [3]);
		addHero(false, draft (5));
		op5t.changeText (selectedUsNames [4]);
		enemyTurn = true;
		yield return StartCoroutine (waitForHeroSelect());
		en5t.changeText (heroOnSelect);
		pushSelectedHero ();
		yield return new WaitForSeconds (0.1f);
	}

	IEnumerator DraftRed(){
		enemyTurn = true;
		heroOnSelect = "none";
		yield return StartCoroutine (waitForHeroSelect());
		en1t.changeText (heroOnSelect);
		pushSelectedHero ();
		enemyTurn = false;
		addHero(false, draft (1));
		op1t.changeText (selectedUsNames [0]);
		addHero(false, draft (2));
		op2t.changeText (selectedUsNames [1]);
		enemyTurn = true;
		yield return StartCoroutine (waitForHeroSelect());
		en2t.changeText (heroOnSelect);
		pushSelectedHero ();
		yield return StartCoroutine (waitForHeroSelect());
		en3t.changeText (heroOnSelect);
		pushSelectedHero ();
		enemyTurn = false;
		addHero(false, draft (3));
		op3t.changeText (selectedUsNames [2]);
		addHero(false, draft (4));
		op4t.changeText (selectedUsNames [3]);
		enemyTurn = true;
		yield return StartCoroutine (waitForHeroSelect());
		en4t.changeText (heroOnSelect);
		pushSelectedHero ();
		yield return StartCoroutine (waitForHeroSelect());
		en5t.changeText (heroOnSelect);
		pushSelectedHero ();
		enemyTurn = false;
		addHero(false, draft (5));
		op5t.changeText (selectedUsNames [4]);
	}

	//adds hero currently up for selection then sets the slection space back to "none"
	void pushSelectedHero(){

		addHero (true, heroOnSelect);
		heroOnSelect = "none";

	}

	IEnumerator waitForHeroSelect(){
		while (heroOnSelect.Equals("none")) {
			yield return null;
		}
		//yield return new WaitForSeconds (.01f);
	}

	//calls one round of drafting for a player
	string draft (int playerNum){
		hero tmpBestFit = blankhero;
		int tmpWhile = 0;
		while (tmpWhile < heroObjArr.Length) {

			//extracts each hero info
			hero tmpHero = heroObjArr[tmpWhile].GetComponent<hero>();

			//calcutates weighted win rate
			if (playerNum == 1){
				tmpHero.adjWinRate = weightedScript.weightedWin(tmpHero.player1WinRate, tmpHero.player1TotalPlayed, tmpHero.globalWinRate);
			}
			if (playerNum == 2){
				tmpHero.adjWinRate = weightedScript.weightedWin(tmpHero.player2WinRate, tmpHero.player2TotalPlayed, tmpHero.globalWinRate);
			}
			if (playerNum == 3){
				tmpHero.adjWinRate = weightedScript.weightedWin(tmpHero.player3WinRate, tmpHero.player3TotalPlayed, tmpHero.globalWinRate);
			}
			if (playerNum == 4){
				tmpHero.adjWinRate = weightedScript.weightedWin(tmpHero.player4WinRate, tmpHero.player4TotalPlayed, tmpHero.globalWinRate);
			}
			if (playerNum == 5){
				tmpHero.adjWinRate = weightedScript.weightedWin(tmpHero.player5WinRate, tmpHero.player5TotalPlayed, tmpHero.globalWinRate);
			}

			//see if this new hero is the best fit
			//first make sure its still needed
			if(roleFree(tmpHero.role)){
				//make sure this hero is not already picked
				if(!isPickedAlrdy(tmpHero.heroName)){
					//compair the weighted win rate to whats alrdy there and take the higher of the 2
					if (tmpBestFit.adjWinRate < tmpHero.adjWinRate){
						tmpBestFit = tmpHero;
					}
				}
			}

			tmpWhile = tmpWhile + 1;
		}
		//return the best hero avalible
		roleDec (tmpBestFit.role);
		return tmpBestFit.heroName;
	}

	//takes a role string and sees if slots are left for that role
	bool roleFree(string role){
		if (role.Equals ("wa")) {
			if(TanksLeft > 0){
				return true;
			} else {
				return false;
			}
		}
		if (role.Equals ("as")) {
			if(DamageLeft > 0){
				return true;
			} else {
				return false;
			}
		}
		if (role.Equals ("sup")) {
			if(SupportsLeft > 0){
				return true;
			} else {
				return false;
			}
		}
		if (role.Equals ("spec")) {
			if(DamageLeft > 0){
				return true;
			} else {
				return false;
			}
		}
		else {
			return false;
		}
	}

	//decrements role counter if a hero is picked for a specific role
	void roleDec(string role){

		if (role.Equals ("wa")) {
			TanksLeft = TanksLeft - 1;
		}
		if (role.Equals ("as")) {
			DamageLeft = DamageLeft - 1;
		}
		if (role.Equals ("sup")) {
			SupportsLeft = SupportsLeft - 1;
		}
		if (role.Equals ("spec")) {
			DamageLeft = DamageLeft - 1;
		}

	}

	//returns if a hero string has already been picked
	bool isPickedAlrdy (string tmpName){
		int i1 = 0;
		int i2 = 0;
		while (i1 < selectedUsNames.Length) {
			if(selectedUsNames[i1] != null){
				if(selectedUsNames[i1].Equals(tmpName)){
					return true;
				}
			}
			i1 = i1 + 1;
		}
		while (i2 < selectedThemNames.Length) {
			if(selectedThemNames[i2] != null){
				if(selectedThemNames[i2].Equals(tmpName)){
					return true;
				}
			}
			i2 = i2 + 1;
		}
		return false;

	}

}
