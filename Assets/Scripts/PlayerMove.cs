using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    public float speed = 10.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.A)){
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
    }
}
