/*
 * 　　　　　順田雅士　　　　時間経過で指定したオブジェクトを生成する
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cCallObject : MonoBehaviour
{
    public GameObject ObjectPrefab;                    // 呼び出すオブジェクトのデータを保管している場所
    public GameObject Object;
    public float Timer;

    [SerializeField]
    float TimeCount = 0;

    [SerializeField]
    bool DestroyFlg = true;                            // オブジェクトを呼び出した後にこのオブジェクトを消すかどうか

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Object == null)
        {
            if (Timer < TimeCount)
            {
                CreateObject();
            }
            else
            {
                TimeCount += Time.deltaTime;
            }
        }
    }

    public void CreateObject()
    {
        Object = ObjectPrefab;
        Instantiate(Object, transform.position, Quaternion.identity);

        if (DestroyFlg == true)
        {
            // エフェクト呼び出し
            Destroy(this.gameObject);
        }
    }
}
