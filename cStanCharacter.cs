/*
 * 　　　　　順田雅士　　　　指定したオブジェクトを呼び出す（仮
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cStanCharacter : MonoBehaviour
{
    public Vector3 SetPosition;
    public Vector3 AddPosition;
    public GameObject StanAreaPrefab;
    GameObject StanArea;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = SetPosition;

        SetPosition.x = AddPosition.x + transform.position.x;
        SetPosition.y = AddPosition.y + transform.position.y;
        SetPosition.z = AddPosition.z + transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if(StanArea == null)
        {
            StanArea = StanAreaPrefab;
            Instantiate(StanArea, transform.position, Quaternion.identity);
        }
        
    }
}
