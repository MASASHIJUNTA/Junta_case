/*
 *  順田　メニューの呼び出し
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cButtonManager : MonoBehaviour
{
    public GameObject MenuPanelPrefab;

    [SerializeField]
    //　ポーズUIのインスタンス
    private GameObject pauseUIInstance;

    [SerializeField]
    bool Puse = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void CollMenu()
    {
        if (pauseUIInstance == null)
        {
            pauseUIInstance = GameObject.Instantiate(MenuPanelPrefab) as GameObject;
            pauseUIInstance.GetComponent<GraphicRaycaster>().enabled = true;

            Puse = true;

            Time.timeScale = 0.0f;
        }
    }

    public void BackMenu()
    {
        if(pauseUIInstance != null)
        {
            Time.timeScale = 1.0f;

            Puse = false;

            Destroy(pauseUIInstance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick button 7") || Input.GetKeyDown("x"))      // オプション
        {
            if(Puse == false)
            {
                CollMenu();
            }
            else if(Puse == true)
            {
                BackMenu();
            }
        }
    }
}
