/*
 * 　　　　　順田雅士　　　　ポーズメニューを解除する処理
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cPauseButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackMenu()
    {
        Time.timeScale = 1.0f;

        Destroy(this.gameObject);
    }
}
