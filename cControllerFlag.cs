/*
 *   順田　コントローラーの入力確認
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cControllerFlag : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick button 0"))      // A
        {
            Debug.Log("button0");
        }
        if (Input.GetKeyDown("joystick button 1"))      // B
        {
            Debug.Log("button1");
        }
        if (Input.GetKeyDown("joystick button 2"))      // X
        {
            Debug.Log("button2");
        }
        if (Input.GetKeyDown("joystick button 3"))      // Y
        {
            Debug.Log("button3");
        }
        if (Input.GetKeyDown("joystick button 4"))      // LB
        {
            Debug.Log("button4");
        }
        if (Input.GetKeyDown("joystick button 5"))      // RB
        {
            Debug.Log("button5");
        }
        if (Input.GetKeyDown("joystick button 6"))　　　// オプションの左にあるやつ（名称わからん
        {
            Debug.Log("button6");
        }
        if (Input.GetKeyDown("joystick button 7"))      // オプション
        {
            Debug.Log("button7");
        }
        if (Input.GetKeyDown("joystick button 8"))      // Lスティック押し込み
        {
            Debug.Log("button8");
        }
        if (Input.GetKeyDown("joystick button 9"))      // Rスティック押し込み
        {
            Debug.Log("button9");
        }

        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        if ((hori != 0) || (vert != 0))
        {
            Debug.Log("stick:" + hori + "," + vert);
        }
    }
}
