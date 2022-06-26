/*
 *        順田　　　　　カーブ変数を使ってオブジェクトを回転させる（ひらひらした動きや、放物線など）
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cCurveRotation : MonoBehaviour
{

    [Header("ローテーションカーブ")]
    public AnimationCurve RotationCurve;   //ボールのAnimationカーブ

    [SerializeField] Vector3 MaxRotion;

    [SerializeField]  Vector3 SetRotion;

    [SerializeField]  float RotionTime;

    public bool Stop;

    // Start is called before the first frame update
    void Start()
    {
        SetRotion.x = 0;
        SetRotion.y = 0;
        SetRotion.z = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Stop)
        {
            if (false == MyGame.Stage.cStageScroll._S_FlgScrollStop)
            {
                RotionTime += Time.deltaTime;
                //インスペクターで設定したカーブを　指定した時間から値を取り出して回転角度をかける
                //                          Evaluate(1)　一秒たった時の値が返ってくる
                SetRotion.x = RotationCurve.Evaluate(RotionTime) * MaxRotion.x;
                SetRotion.y = RotationCurve.Evaluate(RotionTime) * MaxRotion.y;
                SetRotion.z = RotationCurve.Evaluate(RotionTime) * MaxRotion.z;

                transform.eulerAngles = SetRotion;
            }

        }

    }
}
