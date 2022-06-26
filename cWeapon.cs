/*
 *  順田　攻撃アイテムの基本的なシステム
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cWeapon : MonoBehaviour
{
    [SerializeField]
    int Lv;                    // この武器のレベル
    [SerializeField]
    string Name;               // アイテムの名前
    [SerializeField]
    string Explanation;        // 効果の説明
    [SerializeField]
    int Atk;                   // 攻撃力
    [SerializeField]
    Vector3 Speed;             // 落下スピード
    [SerializeField]
    float AtkCount;            // 攻撃数（1人だけ攻撃するのかなど
    [SerializeField]
    float Count = 0;           // このオブジェクトで攻撃した数

    public bool AtkFlag;       // 攻撃するかどうか

    public cAudioCall AudioCall;
    public bool SeStart;
    public cWeaponRender WeaponRender;      // 武器描画のスクリプト

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

    private void OnTriggerEnter(Collider other)
    {
        if (AtkFlag == true)
        {
            if (other.gameObject.tag == "Enemy")        // 触れているオブジェクトがプレイヤーオブジェクトなら
            {

                other.gameObject.GetComponent<cStatus>().Damage(Atk);   // このオブジェクトの攻撃力を参照してその値を振れているオブジェクトに教える

                if (!SeStart && AudioCall != null)
                {
                    AudioCall.GimicSE();

                    SeStart = true;
                }

                Count++;

                if (Count >= AtkCount)
                {
                    End();
                }
            }

            if (other.gameObject.tag == "Floor" || other.gameObject.tag == "Wall")
            {
                if (!SeStart && AudioCall != null)
                {
                    AudioCall.GimicSE();

                    SeStart = true;
                }

                End();
            }
        }
    }

    public void End()
    {
        if (WeaponRender != null)
        {
            WeaponRender.Delete = true;

            this.GetComponent<cWeapon>().enabled = false;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
