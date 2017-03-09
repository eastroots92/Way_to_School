using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour {

    public float speed = 3.0f;
    public GameObject[] maps; // 생성되게할 맵
    public GameObject mapBefore; // 뒤에 나타낼 맵
    public GameObject mapNow; // 현재 맵
    public GameObject mapAfter; // 앞에 나타날 맵
    public GameObject mapMoreAfter; // 더 앞에 나타날 맵
    public GameObject FinalMap;
    bool OnFinalmap = false;
    bool Stopmap = false;


	// Use this for initialization
	void Start () {
	
	}
	//x - 167
	// Update is called once per frame
	void Update () {
      
        
            MoveMap();
        
	}

    void MoveMap()
    {
        float mapMove = speed * Time.deltaTime; // 이동속도


        if (GameManager.Instance().Var.value == 0 && !OnFinalmap)
        {
            Destroy(mapMoreAfter);

            mapMoreAfter = Instantiate(FinalMap, mapMoreAfter.transform.position, transform.rotation) as GameObject;
            Stopmap = true;
            OnFinalmap = true;
        }
        // 하이라키에 있는 맵 이동
        mapBefore.transform.Translate(Vector3.left * mapMove);
        mapNow.transform.Translate(Vector3.left * mapMove);
        mapAfter.transform.Translate(Vector3.left * mapMove);
        mapMoreAfter.transform.Translate(Vector3.left * mapMove);

        if (mapBefore.transform.position.x <= -245.0f && !Stopmap)
        {
            Destroy(mapBefore);
            mapBefore = mapNow;
            mapNow = mapAfter;
            mapAfter = mapMoreAfter;
           
            MakeMap();
        }
    
    }

    void MakeMap()
    {
        float posFin = mapAfter.transform.FindChild("Fin").position.x;
       
      
            int randomNum = Random.Range(0, maps.Length);
            //float startPos = mapNow.transform.position.x;
            //print(posFin);
            mapMoreAfter = Instantiate(maps[randomNum], new Vector3(posFin, 0, 0), transform.rotation) as GameObject;
        
    }
    

}

