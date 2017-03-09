using UnityEngine;
using System.Collections;

public class PlayerMove_Wii : MonoBehaviour
{

    public int index = 0;

    //public static PlayerMove_Wii _instance = null;

    //public static PlayerMove_Wii Instance()
    //{
    //    return _instance;
    //}


    float[] BalanceList = new float[12]; // 평균 배열  [4]은 실시간 평균, [5] 총합 평균  , [6] 왼쪽 , [7] 오른쪽 
    float Timer = 7.0f;
    float Count = 0.0f;


    bool isAvgData = false; // 평균 데이터 측정을 했는가? MakeAvg()
    bool isGround = true; // 땅에 착지 유무
    bool isCol_Left = false; // 왼쪽 콜라이더
    bool isCol_Right = false; // 오른쪽 콜라이더 
    public bool isReady = false; // 게임 준비 완료 시 TRUE 이동 가능
    public bool isGame = false;

    public float Speed = 10.0f;
    public float JumpPow = 10.0f;


    [SerializeField]
    protected WiiBalanceBoardCliant wiiBalanceBoardCliant;

    [SerializeField]
    protected BalanceBoardData balanceBoardData;

    void Awake()
    {

        if (wiiBalanceBoardCliant == null)
        {
            wiiBalanceBoardCliant = GameObject.FindGameObjectWithTag("Wii").GetComponent<WiiBalanceBoardCliant>();
        }
    }

    protected void Start()
    {

        //if (_instance == null)
        //{
        //    _instance = this;
        //}
        //else
        //    Destroy(gameObject);




        if (wiiBalanceBoardCliant == null)
        {
            Debug.LogError("WiiBalanceBoardCliant is null");
        }

        BalanceList[4] = (balanceBoardData.sensorLoad.TopRight + balanceBoardData.sensorLoad.TopLeft + balanceBoardData.sensorLoad.BottomLeft + balanceBoardData.sensorLoad.BottomRight) / 4;


    }


    // Update is called once per frame
    void Update()
    {
        if (!GetInputData())
        {

            return;
        }
        // Wii BalanceBoard Controller 
        Output();



        // 만약 실시간 몸무게가 평균을 초과하면 평균으로 세팅
        if (BalanceList[4] < BalanceList[5])
        {
            BalanceList[4] = BalanceList[5];
        }

    }

    bool GetInputData()
    {

        if (wiiBalanceBoardCliant == null)
        {
            Debug.LogError("BalanceBoardCliant is null");
            return false;
        }

        if (wiiBalanceBoardCliant.recvBalanceBoardDatalist.balanceBoardData == null)
        {
            Debug.LogError("데이터 누락");
            return false;
        }

        if (!(index < wiiBalanceBoardCliant.recvBalanceBoardDatalist.balanceBoardData.Count))
        {
            Debug.LogWarning("정보 수신문제"
                + index
                + " count:"
                + wiiBalanceBoardCliant.recvBalanceBoardDatalist.balanceBoardData.Count);
            return false;
        }


        balanceBoardData = wiiBalanceBoardCliant.recvBalanceBoardDatalist.balanceBoardData[index];
        return true;
    }

    protected virtual void Output()
    {

        wiiController();
    }

    public void wiiController()
    {


        if (isAvgData == false)
        {

            if (((balanceBoardData.sensorLoad.TopRight + balanceBoardData.sensorLoad.TopLeft + balanceBoardData.sensorLoad.BottomLeft + balanceBoardData.sensorLoad.BottomRight) / 4) >= (5.0f + BalanceList[4]))
            {

                MakeAvg();
            }
        }
        if (isAvgData == true)
        {
            PlayerControll();
        }

    }

    // 평균 데이터 저장 ( 재 시 작 시 다 시 해 야 함 )
    public void MakeAvg()
    {

        Count += Time.deltaTime;

        if (Count >= 1.0f && Count <= 1.02f)
        {
            print("발판의 평균을 구합니다.");
        }
        if (Count >= 3.0f && Count <= 3.02f)
        {

            BalanceList[0] = (balanceBoardData.sensorLoad.TopRight + balanceBoardData.sensorLoad.TopLeft + balanceBoardData.sensorLoad.BottomLeft + balanceBoardData.sensorLoad.BottomRight) / 4;
            print("첫번째 평균을 추출 합니다. " + BalanceList[0]);

        }
        if (Count >= 4.0f && Count <= 4.02f)
        {

            BalanceList[1] = (balanceBoardData.sensorLoad.TopRight + balanceBoardData.sensorLoad.TopLeft + balanceBoardData.sensorLoad.BottomLeft + balanceBoardData.sensorLoad.BottomRight) / 4;
            print("두번째 평균을 추출 합니다. " + BalanceList[1]);

        }
        if (Count >= 5.0f && Count <= 5.02f)
        {

            BalanceList[2] = (balanceBoardData.sensorLoad.TopRight + balanceBoardData.sensorLoad.TopLeft + balanceBoardData.sensorLoad.BottomLeft + balanceBoardData.sensorLoad.BottomRight) / 4;
            print("세번째 평균을 추출 합니다. " + BalanceList[2]);

        }
        if (Count >= 6.0f && Count <= 6.02f)
        {

            BalanceList[3] = (balanceBoardData.sensorLoad.TopRight + balanceBoardData.sensorLoad.TopLeft + balanceBoardData.sensorLoad.BottomLeft + balanceBoardData.sensorLoad.BottomRight) / 4;
            print("네번째 평균을 추출 합니다. " + BalanceList[3]);

        }
        if (Count >= Timer)
        {

            BalanceList[5] = (BalanceList[0] + BalanceList[1] + BalanceList[2] + BalanceList[3]) / 4;
            print("데이터 평균값 추출 완료" + BalanceList[5]);
            isAvgData = true;
            isReady = true;
        }


    }

    public void PlayerControll()
    {

        if (isGame == true)
        {

            BalanceList[4] = (balanceBoardData.sensorLoad.TopRight + balanceBoardData.sensorLoad.TopLeft + balanceBoardData.sensorLoad.BottomLeft + balanceBoardData.sensorLoad.BottomRight) / 4;
            BalanceList[6] = (balanceBoardData.sensorLoad.TopLeft + balanceBoardData.sensorLoad.BottomLeft) / 2;
            BalanceList[7] = (balanceBoardData.sensorLoad.TopRight + balanceBoardData.sensorLoad.BottomRight) / 2;

            if (isGround == true)
            {
                //왼쪽 이동
                if ((BalanceList[6] - BalanceList[5]) >= (BalanceList[5] / 3) && (BalanceList[5] - BalanceList[7]) >= (BalanceList[5] / 4))
                {
                    print("Left");
                    if (isCol_Left == false)
                    {
                        this.transform.Translate(0, 0, Speed * Time.deltaTime);
                    }
                }

                // 오른쪽 이동
                if ((BalanceList[7] - BalanceList[5]) >= (BalanceList[5] / 3) && (BalanceList[5] - BalanceList[6]) >= (BalanceList[5] / 4))
                {
                    print("Right");
                    if (isCol_Right == false)
                    {
                        this.transform.Translate(0, 0, -Speed * Time.deltaTime);
                    }
                }

                //점프 
                if ((BalanceList[4] - BalanceList[5]) >= (BalanceList[5] / 2))
                {
                    print("Jump");
                    this.GetComponent<Rigidbody>().velocity = Vector3.up * JumpPow;
                    //this.GetComponent<Rigidbody> ().MovePosition (transform.position + Vector3.up * 6.0f);
                    isGround = false;
                }
            }
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.transform.tag == "Map")
        {
            isGround = true;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Side_L")
        {
            isCol_Left = true;
        }

        if (col.gameObject.tag == "Side_R")
        {
            isCol_Right = true;
        }

    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Side_L")
        {
            isCol_Left = false;
        }
        if (col.gameObject.tag == "Side_R")
        {
            isCol_Right = false;
        }
    }


}
