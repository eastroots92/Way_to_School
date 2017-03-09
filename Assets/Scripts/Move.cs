using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

     float move_speed = 20;
    bool none_collision = true;
	// Use this for initialization
	void Start () {
        StartCoroutine(obj_Destroy());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
      if (none_collision)
            this.transform.Translate(Vector3.left * Time.deltaTime * move_speed);
        
    }
    IEnumerator obj_Destroy()
    {
        yield return new WaitForSeconds(15f);
        Destroy(gameObject);
    }
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Hand"))
        {
            
            this.GetComponent<BoxCollider>().enabled = false;
            none_collision = false;
            Debug.Log("손");
            StartCoroutine(coll_Destroy());
        }
        else if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
       {
            Debug.Log("아픔");
            GameManager.Instance().playerAtt();
            Destroy(gameObject);
        }

     }
    void OnTriggerEnter(Collider coll)
    {
        
        if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("맞음");
            GameManager.Instance().playerAtt();
            Destroy(gameObject);
        }

    }
    IEnumerator coll_Destroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    
}
