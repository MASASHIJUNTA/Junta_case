/*
 * 　　　　　順田雅士　　　　一番近い敵を探して親に教える
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cNeraEnemySearch : MonoBehaviour
{
    public GameObject Parent;

    cLockon Lockon;

    float Range;

    float SearchTime = 0.1f;
    float CountTime = 0;

    public ParticleSystem Fire;

    // Start is called before the first frame update
    void Start()
    {
        Lockon = Parent.GetComponent<cLockon>();
    }

    // Update is called once per frame
    void Update()
    {
        CountTime += Time.deltaTime;

        if(CountTime > SearchTime)
        {
            ResetTime();
        }
    }

    void OnTriggerEnter(Collider other)        // Triggerが他のオブジェクトに触れていたら
    {
        if (other.gameObject.tag == "Enemy")                               // 触れているオブジェクトが敵オブジェクトなら
        {
            if (other.gameObject.GetComponent<cStatus>().Action != cStatus.eAction.Destroy && other.gameObject.GetComponent<cStatus>().Action != cStatus.eAction.Died)
            {
                Vector3 vector3 = other.transform.position - this.transform.position;

                if (Lockon.Target == null)
                {
                    Lockon.Target = other.gameObject;

                    Range = vector3.x * vector3.x + vector3.y * vector3.y;
                }
                else
                {
                    if (Range > vector3.x * vector3.x + vector3.y * vector3.y)
                    {
                        Lockon.Target = other.gameObject;

                        Range = vector3.x * vector3.x + vector3.y * vector3.y;
                    }
                }
            }
        }
    }

    void ResetTime()
    {
        CountTime = 0;

        if(Lockon.Target == null)
        {
            Fire.Stop();

            Destroy(this.gameObject);
        }
        else
        this.gameObject.SetActive(false);
    }
}
