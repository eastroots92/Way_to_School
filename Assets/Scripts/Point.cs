using UnityEngine;
using System.Collections;

public class Point : MonoBehaviour {
	public GameObject[] obs_Point;// 생성 포인트
	public GameObject[] obstacle; //  장애물;

	bool stopObj = true;
	int point_number;//point랜덤 넘버
	int obs_number; // 장애물 랜덤 넘버
	// Use this for initialization
	void Start () {
		StartCoroutine(Obs_create());
	}

	// Update is called once per frame
	void Update()
	{

	}

	IEnumerator Obs_create()
	{
		while (stopObj)
		{
			if(GameManager.Instance().Var.value==0)
			{

				stopObj = false;
			}
			point_number = Random.Range(0, 4);
			obs_number = Random.Range(0, 4);
			if(obs_number==2)
			{
				point_number = Random.Range(0,2);
			}
			Instantiate(obstacle[obs_number], obs_Point[point_number].transform.position, obstacle[obs_number].transform.rotation);
			yield return new WaitForSeconds(1.2f);
		}
	}
}

