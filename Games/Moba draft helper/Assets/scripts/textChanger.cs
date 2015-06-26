using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class textChanger : MonoBehaviour {

	Text thisText;

	// Use this for initialization
	void Start () {
		thisText = this.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeText (string tmpText){
		thisText.text = tmpText;
	}

}
