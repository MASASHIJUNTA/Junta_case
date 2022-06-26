/*
 * 　　　　　順田雅士　　　　攻撃範囲に敵が入っているかどうか
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSearchArea : MonoBehaviour
{

    public GameObject Parent;        // 親オブジェクト
    cStatus Status;                  // このオブジェクトのステータスを取得する場所

    public float Counter = 0;        // 時間計測（クールタイムに使う）

    public bool Enemy;               // 敵オブジェクトかどうか

    // Start is called before the first frame update
    void Start()
    {
        Status = Parent.GetComponent<cStatus>();   // ステータスの参照場所を取得
    }

    // Update is called once per frame
    void Update()
    {
        if (Status.Action != cStatus.eAction.Destroy && Status.Action != cStatus.eAction.Died)
        {
            if (Counter > 0)          // 攻撃した後ならクールタイムが終わるまで待機
            {
                Counter -= Time.deltaTime * Time.timeScale;

                if (Status.Action != cStatus.eAction.KnockBack && Status.Action != cStatus.eAction.Stan)
                {
                    Status.Action = cStatus.eAction.Attacking;


                }

                if (Counter <= 0)
                {
                    Status.Action = cStatus.eAction.Walking;

                    Status.AtkFlag = false;

                    Counter = 0;
                }
            }
        }
    }

    void OnTriggerStay(Collider other)        // Triggerが他のオブジェクトに触れていたら
    {
        if (Counter <= 0 && Status.Action != cStatus.eAction.Died && Status.Action != cStatus.eAction.Destroy)      // 攻撃中（クールタイム含め）じゃなかったら
        {
            if (Enemy) // 敵オブジェクトなら
            {
                if (other.gameObject.tag == "Player")        　　　　　　　　　　　// 触れているオブジェクトがプレイヤーオブジェクトなら
                {
                    Counter = Status.CoolTime;                                     // このオブジェクトのクールタイムを参照して変数に代入する

                    Status.AttackStart();                                          // こうげき
                }

                if ( other.gameObject.tag == "Wall")       　　　　　　　　　　　　// 触れているオブジェクトが壁オブジェクトなら
                {
                    if (Status.Long == true)
                    {
                        Counter = Status.CoolTime;                                     // このオブジェクトのクールタイムを参照して変数に代入する
                    }

                    Status.AttackStart();                                          // こうげき
                }

                if (other.gameObject.tag == "Treasure")                             // 触れているオブジェクトが宝ならば
                {
                    Counter = Status.CoolTime;                                     // このオブジェクトのクールタイムを参照して変数に代入する

                    Status.AttackStart();                                          // こうげき

                    // Status.Action = cStatus.eAction.Died;                          // このオブジェクトの状態を死亡に変える

                    // Status.Hp = -1;                                                // バグ回避のために一様体力を０以下にしておく
                }
            }
            else
            {
                if (other.gameObject.tag == "Enemy")                               // 触れているオブジェクトが敵オブジェクトなら
                {
                    if (Status.Action != cStatus.eAction.Stan)
                    {
                        Counter = Status.CoolTime;

                        Status.AttackStart();                                          // こうげき
                    }
                }
            }
        }
    }
}
