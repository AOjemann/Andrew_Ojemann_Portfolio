using UnityEngine;
using System.Collections;

public class buttonHero : MonoBehaviour {

	MasterCtrScr master;

	// Use this for initialization
	void Start () {

		master = GameObject.FindGameObjectWithTag ("ctr").GetComponent<MasterCtrScr>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		if (master.enemyTurn) {
			master.heroOnSelect = this.GetComponent<hero>().heroName;
			//master.addHero(true, this.GetComponent<hero>().heroName);
		}
	}

}
