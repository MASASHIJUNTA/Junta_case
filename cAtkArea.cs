/*
 * 　　　　　順田雅士　　　　攻撃の範囲
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cAtkArea : MonoBehaviour
{
    public GameObject Parent;               // 親オブジェクト
    cStatus Status;                  // このオブジェクトのステータスを取得する場所
    public bool Enemy;               // 敵オブジェクトかどうか

    public float Counter = 0;        // 時間計測（クールタイムに使う）

    // Start is called before the first frame update
    void Start()
    {
        Status = Parent.GetComponent<cStatus>();   // ステータスの参照場所を取得
    }

    // Update is called once per frame
    void Update()
    {
        if(Status.Action == cStatus.eAction.Attacking)          // 攻撃した後ならクールタイムが終わるまで待機
        {
            Counter -= Time.deltaTime;

            if(Counter <= 0)
            {
                Status.Action = cStatus.eAction.Walking;
            }
        }
    }

    void OnTriggerStay(Collider other)        // Triggerが他のオブジェクトに触れていたら
    {
        if (Status.Action != cStatus.eAction.Attacking)      // 攻撃中（クールタイム含め）じゃなかったら
        {
            if (Enemy) // 敵オブジェクトなら
            {
                if (other.gameObject.tag == "Player")        // 触れているオブジェクトがプレイヤーオブジェクトなら
                {
                    other.gameObject.GetComponent<cStatus>().Damage(Status.Atk);   // このオブジェクトの攻撃力を参照してその値を振れているオブジェクトに教える

                    Counter = Status.CoolTime;                                     // このオブジェクトのクールタイムを参照して変数に代入する

                    Status.Action = cStatus.eAction.Attacking;                     // このオブジェクトの状態を攻撃中に変える
                }

                if(other.gameObject.tag == "Treasure")                             // 触れているオブジェクトが宝ならば
                {
                    other.gameObject.GetComponent<cStatus>().Damage(Status.Atk);

                    Status.Destroy();                                              // 敵オブジェクトを消す処理    
                }
            }
            else
            {
                if (other.gameObject.tag == "Enemy")                               // 触れているオブジェクトが敵オブジェクトなら
                {
                    other.gameObject.GetComponent<cStatus>().Damage(Status.Atk);

                    Counter = Status.CoolTime;

                    Status.Action = cStatus.eAction.Attacking;
                }
            }
        }
    }
}
