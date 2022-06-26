/*
 *       経験値を視覚的にとってるように見せるオブジェクトの動き
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cExpBall : MonoBehaviour
{
    public GameObject Target;   
    public Vector3 Speed;           // 弾速
    public float Near = 0;

    public float MoveTime;
    float CountTime = 0;

    bool move = false;

    public cAudioCall AudioCall;

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("pTreasure");

        this.GetComponent<Rigidbody>().maxDepenetrationVelocity = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {
            if (move)
            {
                transform.Translate(Speed.x * Time.deltaTime, Speed.y * Time.deltaTime, 0f);

                // 対象物と自分自身の座標からベクトルを算出
                Vector3 vector3 = Target.transform.position - this.transform.position;

                Quaternion quaternion = Quaternion.Euler(0, 0, Mathf.Atan2(vector3.y, vector3.x) * Mathf.Rad2Deg - 90);

                this.transform.rotation = quaternion;

                if (Near >= Mathf.Sqrt(vector3.x * vector3.x + vector3.y * vector3.y + vector3.z * vector3.z))
                {
                    AudioCall.SystemSE();               

                    Destroy(this.gameObject);
                }
            }
            else if (move == false)
            {
                CountTime += Time.deltaTime * Time.timeScale;
            }

            if (CountTime > MoveTime && move == false)
            {
                move = true;

                this.GetComponent<Rigidbody>().useGravity = false;
            }
        }
        else
        {
            Target = GameObject.Find("pTreasure");
        }

        
    }
}
