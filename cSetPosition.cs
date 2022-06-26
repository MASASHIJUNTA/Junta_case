/*
 * 　　　　　順田雅士　　　　生成されたときに指定場所にワープさせる
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSetPosition : MonoBehaviour
{
    public Vector3 Position;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.position = Position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
