using UnityEngine;
using System.Collections;

public class sideSelectButton : MonoBehaviour {

	public bool isBlueSide;

	MasterCtrScr master;

	// Use this for initialization
	void Start () {
		master = GameObject.FindGameObjectWithTag ("ctr").GetComponent<MasterCtrScr> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		master.selectSide (isBlueSide);
	}
}
