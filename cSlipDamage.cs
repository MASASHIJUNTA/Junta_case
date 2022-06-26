/*
 * 　　　　　順田雅士　　　　範囲に入っている敵に継続ダメージ
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSlipDamage : MonoBehaviour
{
    public float DamageInterval;          // ダメージの間隔　何秒毎に固定ダメージを与えるのか

    public GameObject target;

    public int Atk;                       // 攻撃力

    [SerializeField]
    float TimeCount = 0;                   // 時間計測のための変数

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // 経過時間 >= 何秒ごとに攻撃するかの時間
            if (TimeCount >= DamageInterval)
            {
                TimeCount = 0;            // 経過時間の初期化

                target.gameObject.GetComponent<cStatus>().Damage(Atk);   // このオブジェクトの攻撃力を参照してその値を振れているオブジェクトに教える

                if(target.gameObject.GetComponent<cStatus>().Hp <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
            else
            {
                TimeCount += Time.deltaTime * Time.timeScale;
            }

            this.gameObject.transform.position = target.transform.position;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
