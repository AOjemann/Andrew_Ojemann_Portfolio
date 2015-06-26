using UnityEngine;
using System.Collections;

public class weightedWinRate : MonoBehaviour {

	public int weightedConstant = 20;

	//stores opponents heros

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//calcultes weighted win rates
	public float weightedWin(float myRate, int totalGames, float globalRate){
		float wins = totalGames * (myRate / 100);
		float losses = totalGames - wins;

		if (totalGames > 10) {
			return ((wins + (weightedConstant * (globalRate / 100))) / (wins + losses + weightedConstant));
		} else {
			//if player has played less then 10 games penalize them by 10% for inexperience
			return (((wins + (weightedConstant * (globalRate / 100))) / (wins + losses + weightedConstant)) - .10f);
		}
	}


	//=========================================================================================================================
	//following is psudocode for adding in counterpick calculations, 
	//this is currently not enabled as I did not have time to enter test data for counterpick win rates
	//=========================================================================================================================

	float counteredMyRate;
	int counteredTotalGames;

	//stores the alphabetical indexes of all heros, parses hero names and returns indexes
	int heroIndex(string heroName){

		if (heroName.Equals ("Abathur")) {
			return 0;
		}
		//do this for all heros
		//...
		if (heroName.Equals ("Zeratul")) {
			return 35;
		}
		//error case if not given a hero will crash
		return -1;

	}

	//given a hero to consider + list of enemy picked heros so far
	public float counteredWinRate(hero givenHero, int playerNumber, string[] enemyPick){
		givenHeroPar (givenHero, playerNumber);
		float tmpAdj = weightedWin (counteredMyRate, counteredTotalGames, givenHero.globalWinRate);
		int i = 0;
		float tabulatedWin = 0.0f;
		while (i < enemyPick.Length) {
			tabulatedWin = tabulatedWin + givenHero.vsWinRates[heroIndex(enemyPick[i])];
			i = i + 1;
		}
		//average all of the counterpick rates
		tabulatedWin = tabulatedWin / enemyPick.Length;
		//average the wighted win rate with the average of the conterpick rates,
		//this places equal wight on a player being skilled with a character and that charater being good against the other team
		tmpAdj = (tmpAdj + tabulatedWin) / 2;
		return tmpAdj;
	}

	//parser for hero data from the proper player
	void givenHeroPar(hero givenHero, int playerNumber){

		if (playerNumber == 1){
			counteredMyRate = givenHero.player1WinRate;
			counteredTotalGames = givenHero.player1TotalPlayed;
		}
		if (playerNumber == 2){
			counteredMyRate = givenHero.player2WinRate;
			counteredTotalGames = givenHero.player2TotalPlayed;
		}
		if (playerNumber == 3){
			counteredMyRate = givenHero.player3WinRate;
			counteredTotalGames = givenHero.player3TotalPlayed;
		}
		if (playerNumber == 4){
			counteredMyRate = givenHero.player4WinRate;
			counteredTotalGames = givenHero.player4TotalPlayed;
		}
		if (playerNumber == 5){
			counteredMyRate = givenHero.player5WinRate;
			counteredTotalGames = givenHero.player5TotalPlayed;
		}

	}

}
