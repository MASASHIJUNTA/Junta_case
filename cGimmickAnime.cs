/*
 *    順田　　　　　　　　アイテムをお助けキャラが投げる処理
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cGimmickAnime : MonoBehaviour
{
    // Start is called before the first frame update

    public float StartTime;
    float CountTime = 0;

    bool Play = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CountTime += Time.deltaTime;

        if(Play == false && CountTime > StartTime)
        {
            Play = true;

            this.GetComponent<Animator>().SetTrigger("Atk");
        }
    }
}
