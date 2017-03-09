using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		Destroy(GameObject.FindGameObjectWithTag ("Player"));
		StartCoroutine (Delay ());

	}

	IEnumerator Delay(){
		yield return new WaitForSeconds (10f);
		SceneManager.LoadScene ("ReMenu");
	}
		
	// Update is called once per frame
	void Update () {
	
	}
}
