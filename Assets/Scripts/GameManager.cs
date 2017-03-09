using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {

    public Transform[] heart;
    int count = 2;
    public Slider Var;
    public static GameManager _instance = null;
	AudioSource audio;

    public Transform ui;

    public static GameManager Instance()
    {
        return _instance;
    }
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
        ui = GameObject.Find("Player").transform.FindChild("Canvas");
        ui.gameObject.SetActive(true);
        heart[0] = ui.transform.FindChild("heart1");
        heart[1] = ui.transform.FindChild("heart2");
        heart[2] = ui.transform.FindChild("heart3");
        Var = ui.GetComponentInChildren<Slider>();
        if (_instance == null)
        {
            _instance = this;
       


        }
        else
            Destroy(gameObject);

	
	}
	
	// Update is called once per frame
	void Update () {
     Var.value = 60.0f - Time.timeSinceLevelLoad;
      
    }
    public void playerAtt()
    {

			audio.Play();
            heart[count].gameObject.SetActive(false);
            count--;
            if (count == -1)
            {
				SceneManager.LoadScene ("Lose");
            }
    }
    
    
}
