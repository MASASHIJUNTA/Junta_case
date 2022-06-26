/*
 *  順田　　遠距離攻撃用の判定エリア
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cAtkAreaLong : MonoBehaviour
{
    public GameObject Parent;        // 親オブジェクト
    cStatus Status;                  // このオブジェクトのステータスを取得する場所
    public bool Enemy;               // 敵オブジェクトかどうか

    public float Counter = 0;        // 時間計測（クールタイムに使う）

    public GameObject BulletPrefab;  // 遠距離攻撃用の弾のデータ

    [SerializeField]
    GameObject Bullet;               // 遠距離攻撃の弾

    // Start is called before the first frame update
    void Start()
    {
        Status = Parent.GetComponent<cStatus>();   // ステータスの参照場所を取得
    }

    // Update is called once per frame
    void Update()
    {
        if (Counter > 0)          // 攻撃した後ならクールタイムが終わるまで待機
        {
            Counter -= Time.deltaTime * Time.timeScale;

            if(Status.Action != cStatus.eAction.KnockBack && Status.Action != cStatus.eAction.Stan)
            {
                Status.Action = cStatus.eAction.Attacking;
            }

            if (Counter <= 0)
            {
                Status.Action = cStatus.eAction.Walking;

                Counter = 0;
            }
        }
    }

    void OnTriggerStay(Collider other)        // Triggerが他のオブジェクトに触れていたら
    {
        if (Counter <= 0)      // 攻撃中（クールタイム含め）じゃなかったら
        {
            if (Enemy) // 敵オブジェクトなら
            {
                if (other.gameObject.tag == "Player")        // 触れているオブジェクトがプレイヤーオブジェクトなら
                {
                    Counter = Status.CoolTime;                                     // このオブジェクトのクールタイムを参照して変数に代入する

                    Status.Action = cStatus.eAction.Attacking;                     // このオブジェクトの状態を攻撃中に変える

                    Bullet = BulletPrefab;
                    Instantiate(Bullet, transform.position, Quaternion.identity);

                    Bullet.GetComponent<cBullet>().Status = Status;
                }

                if (other.gameObject.tag == "Treasure")                             // 触れているオブジェクトが宝ならば
                {
                    Counter = Status.CoolTime;                                     // このオブジェクトのクールタイムを参照して変数に代入する

                    Status.Action = cStatus.eAction.Attacking;

                    Bullet = BulletPrefab;
                    Instantiate(Bullet, transform.position, Quaternion.identity);

                    Bullet.GetComponent<cBullet>().Status = Status;

                    // Status.Action = cStatus.eAction.Died;                          // このオブジェクトの状態を死亡に変える

                    // Status.Hp = -1;                                                // バグ回避のために一様体力を０以下にしておく
                }
            }
            else
            {
                if (other.gameObject.tag == "Enemy")                               // 触れているオブジェクトが敵オブジェクトなら
                {
                    Bullet = BulletPrefab;
                    Instantiate(Bullet, transform.position, Quaternion.identity);

                    Counter = Status.CoolTime;

                    Status.Action = cStatus.eAction.Attacking;
                }
            }
        }
    }
}
