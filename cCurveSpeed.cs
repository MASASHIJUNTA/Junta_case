using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cCurveSpeed : MonoBehaviour
{

    [Header("ローテーションカーブ")]
    public AnimationCurve RotationCurve;   //ボールのAnimationカーブ

    [SerializeField] Vector3 MaxSpeed;

    [SerializeField] Vector3 SetSpeed;

    [SerializeField] float SpeedTime;

    public bool Stop;

    // Start is called before the first frame update
    void Start()
    {
        SetSpeed.x = 0;
        SetSpeed.y = 0;
        SetSpeed.z = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Stop)
        {
            if (false == MyGame.Stage.cStageScroll._S_FlgScrollStop)
            {
                SpeedTime += Time.deltaTime;

                SetSpeed.x = RotationCurve.Evaluate(SpeedTime) * MaxSpeed.x * Time.deltaTime;
                SetSpeed.y = RotationCurve.Evaluate(SpeedTime) * MaxSpeed.y * Time.deltaTime;
                SetSpeed.z = RotationCurve.Evaluate(SpeedTime) * MaxSpeed.z * Time.deltaTime;

                // transform.Translate(SetSpeed.x * Time.deltaTime, SetSpeed.y * Time.deltaTime, SetSpeed.z * Time.deltaTime);

                transform.position += SetSpeed;
            }

        }

    }
}
