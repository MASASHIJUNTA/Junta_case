/*
 * 　　　　　順田雅士　　　　指定した時間に合わせてオブジェクトを回転させる
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cAddRotion : MonoBehaviour
{
    [SerializeField]
    float MaxTime;
    [SerializeField]
    Vector3 MaxRotion;

    Vector3 AddRotion;

    float CountTime;

    // Start is called before the first frame update
    void Start()
    {
        // 毎フレーム加算する分の回転角度を計算
        AddRotion.x = MaxRotion.x / MaxTime;
        AddRotion.y = MaxRotion.y / MaxTime;
        AddRotion.z = MaxRotion.z / MaxTime;
    }

    // Update is called once per frame
    void Update()
    {
        CountTime += Time.deltaTime * Time.timeScale;

        if(CountTime < MaxTime)
        {
            transform.Rotate(new Vector3(AddRotion.x * Time.deltaTime, AddRotion.y * Time.deltaTime, AddRotion.z * Time.deltaTime));
        }
    }
}
