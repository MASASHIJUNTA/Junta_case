/*
 *  順田　　弾の攻撃判定
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cBullet : MonoBehaviour
{
    public Vector3 Speed;           // 弾速
    public bool Enemy;             // 敵オブジェクトかどうか
    public cStatus Status;         // このオブジェクトのステータスを取得する場所

    public bool reflection = false;

    public int Atk = 0;

    public bool Single = true;               // 単発か貫通か 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Speed.x * Time.deltaTime, Speed.y * Time.deltaTime, 0f);
    }

    void OnTriggerEnter(Collider other)        // Triggerが他のオブジェクトに触れていたら
    {
        if (Status != null && Atk == 0)
        {
            Atk = Status.Atk;
        }
            if (Enemy) // 敵オブジェクトなら
            {
                if (other.gameObject.tag == "Player")        // 触れているオブジェクトがプレイヤーオブジェクトなら
                {
                    other.gameObject.GetComponent<cStatus>().Damage(Atk);   // このオブジェクトの攻撃力を参照してその値を振れているオブジェクトに教える

                    if (Single)
                        Destroy(this.gameObject);
                }

                if (other.gameObject.tag == "Treasure")                             // 触れているオブジェクトが宝ならば
                {
                    other.gameObject.GetComponent<cStatus>().Damage(Atk);

                    if (Status.Long == false && Status.Boss == false && other.gameObject.GetComponent<cStatus>().Hp > 0)
                    {
                        other.gameObject.GetComponent<cStatus>().Counter(Status.gameObject);

                        Status.Destroy();                                           // 敵オブジェクトを消す処理
                    }

                    if (Single)
                        Destroy(this.gameObject);
                }
            }
            else
            {
                if (other.gameObject.tag == "Enemy")                               // 触れているオブジェクトが敵オブジェクトなら
                {
                    other.gameObject.GetComponent<cStatus>().Damage(Atk);

                    if (Single)
                        Destroy(this.gameObject);
                }
            }

        if (other.gameObject.tag == "Wall")                               // 触れているオブジェクトが壁オブジェクトなら
        {
            if (Single)
                Destroy(this.gameObject);
        }

    }
}
