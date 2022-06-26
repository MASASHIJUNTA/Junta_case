/*
 *  順田　　敵の死んだときの処理
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cEnemyStatus : MonoBehaviour
{
    public int Exp;         // 死んだときに発生する経験値

    cGameController GameController; 
    cStatus Status;

    public float DeleteTime = 0;   // 消すまでの演出時間
    [SerializeField] float TimeCount = 0;

    public GameObject ExpBallPrefab;
    public GameObject ExpBall;

    public int Count = 0;

    Vector3 CallPosition;

    bool PlayFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        GameController = FindObjectOfType<cGameController>();
        Status = this.gameObject.GetComponent<cStatus>();

        Count = Exp / 3;


    }

    // Update is called once per frame
    void Update()
    {
        if (Status.Action == cStatus.eAction.Died || Status.Action == cStatus.eAction.Destroy)
        {
            TimeCount += Time.deltaTime * Time.timeScale;

            if (TimeCount > DeleteTime)
            {
                if (Status.Action == cStatus.eAction.Died)
                {
                    CallPosition = this.transform.position;

                    if (PlayFlag == false )
                    {
                        if (Status.End == true)
                        {
                            PlayFlag = true;

                            for (int i = 0; i <= Count; i++)
                            {
                                CallPosition.x += Random.Range(-1.0f, 1.0f);
                                CallPosition.y += Random.Range(-0.5f, 1.0f);

                                ExpBall = ExpBallPrefab;
                                Instantiate(ExpBall, CallPosition, Quaternion.identity);
                            }

                            GameController.GetExp(Exp);
                        }
                    }
                    else
                    {
                        this.GetComponent<cEnemyStatus>().enabled = false;
                        Destroy(this.gameObject);
                    }
                }

                if (Status.Action == cStatus.eAction.Destroy)
                {
  
                        if (PlayFlag == false)
                    {
                        if (Status.End == true)
                        {
                            PlayFlag = true;

                            GameController.EnemyCountDown();
                        }
                    }
                    else
                    {
                        this.GetComponent<cEnemyStatus>().enabled = false;
                        Destroy(this.gameObject);
                    }

                }
            }
        }
    }
}
