using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {
    Transform set;

    void Start()
    {
        set = GameObject.Find("Player").transform.FindChild("Canvas");
    }
	// Use this for initialization
	public void OnClick() {
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMove_Wii> ().isGame = true;
       
            SceneManager.LoadScene ("InGame");
      //  set.gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
