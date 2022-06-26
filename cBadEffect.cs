/*
 * 　　　　　順田雅士　　　　攻撃時のデバフ効果処理など
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cBadEffect : MonoBehaviour
{
    public float SlowTime;
    public float KnockBackTime;
    public float StanTime;

    public bool Enemy;

    public bool Single;

    public int Atk;

    public GameObject SlipDamagePrefab;
    public GameObject SlipDamage;

    [SerializeField]
    Vector3 Speed;             // 落下スピード

    public cAudioCall AudioCall;
    public cWeaponRender WeaponRender;      // 武器描画のスクリプト

    [SerializeField]
    bool WallFlag;                         // 壁にあたるかどうか


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

    void OnTriggerEnter(Collider other)        // Triggerが他のオブジェクトに触れていたら
    {
        if (Enemy)
        {
            if (other.gameObject.tag == "Player")        // 触れているオブジェクトがプレイヤーオブジェクトなら
            {
                if (SlowTime > 0)
                {
                    other.gameObject.GetComponent<cStatus>().SlowTime = SlowTime;
                }

                if (KnockBackTime > 0)
                {
                    other.gameObject.GetComponent<cStatus>().KnockBackTime = KnockBackTime;
                }

                if (StanTime > 0)
                {
                    other.gameObject.GetComponent<cStatus>().StanTime = StanTime;
                }

                if(SlipDamagePrefab != null)
                {
                    SlipDamage = SlipDamagePrefab;
                    Instantiate(SlipDamage, transform.position, Quaternion.identity);

                    SlipDamage.GetComponent<cSlipDamage>().target = other.gameObject;
                }

                other.gameObject.GetComponent<cStatus>().Damage(Atk);

                if(AudioCall != null)
                AudioCall.GimicSE();

                if (Single)
                {
                    Destroy(this.gameObject);
                }
            }

            
        }
        else
        {
            if (other.gameObject.tag == "Enemy")        // 触れているオブジェクトがエネミーオブジェクトなら
            {
                if (SlowTime > 0)
                {
                    other.gameObject.GetComponent<cStatus>().SlowTime = SlowTime;
                }

                if (KnockBackTime > 0)
                {
                    other.gameObject.GetComponent<cStatus>().KnockBackTime = KnockBackTime;
                }

                if (StanTime > 0)
                {
                    other.gameObject.GetComponent<cStatus>().StanTime = StanTime;
                }

                if (SlipDamagePrefab != null)
                {
                    SlipDamage = SlipDamagePrefab;

                    SlipDamage.GetComponent<cSlipDamage>().target = other.gameObject;

                    Instantiate(SlipDamage, transform.position, Quaternion.identity);

                    
                }

                other.gameObject.GetComponent<cStatus>().Damage(Atk);

                if (AudioCall != null)
                    AudioCall.GimicSE();

                if (Single)
                {
                    Destroy(this.gameObject);
                }
            }
        }

        if ((other.gameObject.tag == "Floor" && Speed.y != 0) || (WallFlag && other.gameObject.tag == "Wall"))
        {
            if (WeaponRender != null)
            {
                WeaponRender.Delete = true;

                this.GetComponent<cBadEffect>().enabled = false;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
