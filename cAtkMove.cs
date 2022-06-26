/*
 *       順田　　　　カーブ変数を利用した敵の攻撃挙動（攻撃Animationに合わせて位置を移動させるなど）
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cAtkMove : MonoBehaviour
{
    [Header("ローテーションカーブ")]
    public AnimationCurve RotationCurve;   //Animationカーブ

    [SerializeField] float AddSpeed;
    [SerializeField] float CountTime;
    [SerializeField] float SetPos_x;

    bool move;

    public cStatus Status;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 攻撃時にキャラクターを前後させて動きを作る
        if(Status.Action == cStatus.eAction.Attacking && Status.AtkFlag == false)
        {
            CountTime += Time.deltaTime;

            SetPos_x = RotationCurve.Evaluate(CountTime) * AddSpeed;

            transform.Translate(SetPos_x * Time.deltaTime, 0f, 0f);
        }
        else if(CountTime != 0)
        {
            CountTime = 0;
        }
    }
}
