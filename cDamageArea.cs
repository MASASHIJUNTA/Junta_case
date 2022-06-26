/*
 *   順田　当たっている敵の数をカウントし物理判定の処理の時にダメージを与えながら数を数え手処理をしてる
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cDamageArea : MonoBehaviour
{
    public float DamageInterval;          // ダメージの間隔　何秒毎に固定ダメージを与えるのか

    public int FastAtk;                   // 継続ダメージ以外の普通の攻撃
    public int Atk;                       // 攻撃力

    [SerializeField]
    float TimeCount = 0;                   // 時間計測のための変数

    [SerializeField]
    bool AtkFlag = false;                 // 攻撃のタイミングかどうか
    [SerializeField]
    bool FastAtkFlag = false;             // 最初の攻撃をしたかどうか

    [SerializeField]
    int MaxCount = 0;                // 何体の敵が範囲に入っているか
    [SerializeField]
    int Count = 0;                   // 何体目の敵の処理をしているか

    [SerializeField]
    Vector3 Speed;           // 弾速

    [SerializeField]
    bool Enemy;             // 敵かどうか



    // Start is called before the first frame update
    void Start()
    {
        if(FastAtk == 0)
        {
            FastAtkFlag = true;
        }

        Speed.z = 0;
    }

    // Update is called once per frame
    void Update()
    {     
        // 経過時間 >= 何秒ごとに攻撃するかの時間
        if(TimeCount >= DamageInterval)
        {
            AtkFlag = true;           // 攻撃OK

            TimeCount = 0;            // 経過時間の初期化

            Count = 0;           // 攻撃した敵の数の初期化
        }
        else
        {
            TimeCount += Time.deltaTime * Time.timeScale; 
        }

        if(TimeCount > 0.1 && FastAtkFlag == false)
        {
            FastAtkFlag = true;
        }

        transform.Translate(Speed.x * Time.deltaTime, Speed.y * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Enemy)
        {
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "Treasure")
            {
                MaxCount++;          // エリア内にいる敵の合計値を増やす

                if (FastAtkFlag == false)
                {
                    other.gameObject.GetComponent<cStatus>().Damage(FastAtk);   // このオブジェクトの攻撃力を参照してその値を振れているオブジェクトに教える
                }

                if (Speed.x != 0 || Speed.y != 0)
                {
                    Speed.x = 0;
                    Speed.y = 0;
                }
            }
        }
        else
        {
            if (other.gameObject.tag == "Enemy")
            {
                MaxCount++;          // エリア内にいる敵の合計値を増やす

                if (FastAtkFlag == false)
                {
                    other.gameObject.GetComponent<cStatus>().Damage(FastAtk);   // このオブジェクトの攻撃力を参照してその値を振れているオブジェクトに教える
                }

                if (Speed.x != 0 || Speed.y != 0)
                {
                    Speed.x = 0;
                    Speed.y = 0;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Atk != 0)
        {
            if (Enemy)
            {
                if (other.gameObject.tag == "Player" || other.gameObject.tag == "Treasure")
                {
                    if (Count < MaxCount && AtkFlag == true)
                    {
                        other.gameObject.GetComponent<cStatus>().Damage(Atk);   // このオブジェクトの攻撃力を参照してその値を振れているオブジェクトに教える

                        if (other.gameObject.GetComponent<cStatus>().Hp <= 0)    // 攻撃した敵が死んだら敵の合計数を減らす
                        {
                            MaxCount--;
                        }

                        Count++;                                           // 攻撃した敵の数を増やす
                    }

                    if (Count >= MaxCount)                             // 攻撃した数が敵の総数を超えていたら攻撃禁止
                    {
                        AtkFlag = false;
                    }
                }
            }
            else
            {
                if (other.gameObject.tag == "Enemy")
                {
                    if (Count < MaxCount && AtkFlag == true)
                    {
                        other.gameObject.GetComponent<cStatus>().Damage(Atk);   // このオブジェクトの攻撃力を参照してその値を振れているオブジェクトに教える

                        if (other.gameObject.GetComponent<cStatus>().Hp <= 0)    // 攻撃した敵が死んだら敵の合計数を減らす
                        {
                            MaxCount--;
                        }

                        Count++;                                           // 攻撃した敵の数を増やす
                    }

                    if (Count >= MaxCount)                             // 攻撃した数が敵の総数を超えていたら攻撃禁止
                    {
                        AtkFlag = false;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)                          // 敵が範囲から出たら敵の総数から減らす
    {
        if (Enemy)
        {
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "Treasure")
            {
                MaxCount--;
            }
        }
        else
        {
            if (other.gameObject.tag == "Enemy")
            {
                MaxCount--;
            }
        }
    }
}
