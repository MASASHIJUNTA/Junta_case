/*
 *  順田　　触れた対象にノックバックさせる
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cKnockBackArea : MonoBehaviour
{
    public float KnockBackTime;

    public bool Enemy;

    public bool Single;

    public int Atk;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)        // Triggerが他のオブジェクトに触れていたら
    {
        if (Enemy)
        {
            if (other.gameObject.tag == "Player")        // 触れているオブジェクトがプレイヤーオブジェクトなら
            {
                if (other.gameObject.GetComponent<cStatus>().Action != cStatus.eAction.KnockBack)
                {
                    other.gameObject.GetComponent<cStatus>().KnockBackTime = KnockBackTime;

                    other.gameObject.GetComponent<cStatus>().Damage(Atk);

                    if (Single)
                    {
                        Destroy(this.gameObject);
                    }
                }
            }
        }
        else
        {
            if (other.gameObject.tag == "Enemy")        // 触れているオブジェクトがプレイヤーオブジェクトなら
            {
                if (other.gameObject.GetComponent<cStatus>().Action != cStatus.eAction.KnockBack)
                {
                    other.gameObject.GetComponent<cStatus>().KnockBackTime = KnockBackTime;

                    other.gameObject.GetComponent<cStatus>().Damage(Atk);

                    if (Single)
                    {
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }
}
