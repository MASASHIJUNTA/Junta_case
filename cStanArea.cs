/*
 *  順田　　触れた対象にスタンさせる
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cStanArea : MonoBehaviour
{
    public float StanTime;

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

    void OnTriggerEnter(Collider other)        // Triggerが他のオブジェクトに触れていたら
    {
        if(Enemy)
        {
            if (other.gameObject.tag == "Player")        // 触れているオブジェクトがプレイヤーオブジェクトなら
            {
                other.gameObject.GetComponent<cStatus>().StanTime = StanTime;

                other.gameObject.GetComponent<cStatus>().Damage(Atk);

                if (Single)
                {
                    Destroy(this.gameObject);
                }
            }
        }
        else
        {
            if (other.gameObject.tag == "Enemy")        // 触れているオブジェクトがプレイヤーオブジェクトなら
            {
                other.gameObject.GetComponent<cStatus>().StanTime = StanTime;

                other.gameObject.GetComponent<cStatus>().Damage(Atk);

                if (Single)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
