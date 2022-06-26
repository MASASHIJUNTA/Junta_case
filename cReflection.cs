/*
 * 　　　　　順田雅士　　　　遠距離攻撃を跳ね返す処理
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cReflection : MonoBehaviour
{
    public cAudioCall AudioCall;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            if (other.gameObject.GetComponent<cBullet>().reflection == false)
            {
                other.gameObject.GetComponent<cBullet>().reflection = true;

                Vector3 reflectionSpeed;

                // 反転
                reflectionSpeed = other.gameObject.GetComponent<cBullet>().Speed;

                reflectionSpeed.x = reflectionSpeed.x * (-1);
                reflectionSpeed.y = reflectionSpeed.y * (-1);
                reflectionSpeed.z = reflectionSpeed.z * (-1);

                other.gameObject.GetComponent<cBullet>().Speed = reflectionSpeed;

                if (other.gameObject.GetComponent<cBullet>().Enemy)
                {
                    other.gameObject.GetComponent<cBullet>().Enemy = false;
                }
                else
                {
                    other.gameObject.GetComponent<cBullet>().Enemy = true;
                }

                AudioCall.GimicSE();

                Destroy(this.gameObject);
            }
        }
    }
}
