using UnityEngine;
using System.Collections;

public class StartManager : MonoBehaviour {

	GameObject Player ;
	public GameObject Bt_Ready;
	public GameObject Bt_Start;

	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player");

	}
	
	// Update is called once per frame
	void Update () {

		if (Player.GetComponent<PlayerMove_Wii> ().isReady == true) {
			Bt_Ready.SetActive (false); 
			Bt_Start.SetActive (true);
		}
	}
}
