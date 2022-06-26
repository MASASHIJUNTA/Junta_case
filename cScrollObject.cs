/*
 *     順田　　　　　背景のスクロールに合わせてこのオブジェクトも流す
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.Stage;

public class cScrollObject : MonoBehaviour
{
    cStageScroll StageScroll;

    [SerializeField] bool move = false;          // 順田     スクロールするかどうか（地面についたらtrueにするなど

    // Start is called before the first frame update
    void Start()
    {
        StageScroll = FindObjectOfType<cStageScroll>();
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            if (Time.timeScale != 1)
            {
                // 背景スクロールしているオブジェクトからスピードを読み取ってそのスピードを格納する
                transform.Translate(-StageScroll._SpeedData._Speed * Time.deltaTime * StageScroll._SpeedData._CuttingSpeedAjust, 0f, 0f);
            }
            else
            {
                transform.Translate(-StageScroll._SpeedData._Speed * Time.deltaTime, 0f, 0f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Floor")
            move = true;
    }
}
