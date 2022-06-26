using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cCounteCrow : MonoBehaviour
{
    public GameObject Target;

    public Vector3 FastSpeed;        // 敵に向かっての突進するときのスピード
    public Vector3 SecondSpeed;      // 敵にぶつかった後画面右上に行くときのスピード

    bool Hit;

    [SerializeField] Vector3 ResetRotion;
    [SerializeField] Vector3 SetRotion;

    [SerializeField] float Near;

    // Start is called before the first frame update
    void Start()
    {
        ResetRotion.x = this.transform.eulerAngles.x;
        ResetRotion.y = this.transform.eulerAngles.y;
        ResetRotion.z = this.transform.eulerAngles.z;

        SetRotion = ResetRotion;
    }

    // Update is called once per frame
    void Update()
    {
        if (Hit == false)

        {
            Vector3 vector3 = Target.transform.position - this.transform.position;
            if (Target != null)
            {
                // 対象物と自分自身の座標からベクトルを算出


                float Z = Mathf.Atan2(vector3.y, vector3.x) * Mathf.Rad2Deg;

                SetRotion.z = Z;

                transform.eulerAngles = SetRotion;
            }

            transform.Translate(FastSpeed.x * Time.deltaTime, FastSpeed.y * Time.deltaTime, 0f);

            if (Near >= Mathf.Sqrt(vector3.x * vector3.x + vector3.y * vector3.y + vector3.z * vector3.z))
            {
                Hit = true;

                transform.eulerAngles = ResetRotion;
            }
        }
        else
        {
            transform.Translate(SecondSpeed.x * Time.deltaTime, SecondSpeed.y * Time.deltaTime, 0f);
        }
    }
}
