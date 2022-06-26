/*
 * 　　　　　順田雅士　　　　こけないように回転をゆっくり制御する
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cRotationSave : MonoBehaviour
{
    Quaternion SetRotion;

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
        SetRotion.z = this.transform.rotation.z;

        if (SetRotion.z > 0)
        {
            SetRotion.z--;

            if (SetRotion.z < 0)
            {
                SetRotion.z = 0;
            }
        }

        if(SetRotion.z < 0)
        {
            SetRotion.z++;

            if (SetRotion.z > 0)
            {
                SetRotion.z = 0;
            }
        }

        this.transform.rotation = SetRotion;
    }
}
