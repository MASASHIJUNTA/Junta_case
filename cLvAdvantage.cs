/*
 * 　　　　　順田雅士　　　　触れた敵が設定したレベル以下なら消す処理
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cLvAdvantage : MonoBehaviour
{
    [SerializeField]
    Vector3 Speed;             // 落下スピード

    public int Lv;    // このレベル以下の物が当たった場合消滅させる

    public int Atk;   // 攻撃力
    public bool Single;

    public cWeaponRender WeaponRender;      // 武器描画のスクリプト

    public bool FloorFlg;

    // Start is called before the first frame update
    void Start()
    {
        WeaponRender = this.GetComponent<cWeaponRender>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Speed.x * Time.deltaTime, -Speed.y * Time.deltaTime, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")        // 触れているオブジェクトがプレイヤーオブジェクトなら
        {
            other.gameObject.GetComponent<cStatus>().LvAdvantage(Lv, Atk);

            if(Single)
            {
                Destroy(this.gameObject);
            }
        }

        if (FloorFlg)
        {
            if (other.gameObject.tag == "Floor" || other.gameObject.tag == "Wall")
            {
                if (WeaponRender != null)
                {
                    WeaponRender.Delete = true;

                    this.GetComponent<cLvAdvantage>().enabled = false;
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
