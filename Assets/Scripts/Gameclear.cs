using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Gameclear : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
      
	
	}
    void OnTriggerEnter(Collider coll)

    {
        if(coll.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            
			SceneManager.LoadScene("Clear");
            
            
        }
    }
}
