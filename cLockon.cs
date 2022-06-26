/*
 * 　　　　　順田雅士　　　　ターゲットのほうに角度を変え続けて追尾する処理
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cLockon : MonoBehaviour
{
    public GameObject Target;
    public GameObject SearchArea;

    bool StartFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SearchArea == null)
        {
            this.GetComponent<cWeapon>().AtkFlag = true;
        }
        else
        {
            if (Target != null)
            {
                // 対象物と自分自身の座標からベクトルを算出
                Vector3 vector3 = Target.transform.position - this.transform.position;

                Quaternion quaternion = Quaternion.Euler(0, 0, Mathf.Atan2(vector3.y, vector3.x) * Mathf.Rad2Deg + 90);

                this.transform.rotation = quaternion;

                if (Target.GetComponent<cStatus>().Action == cStatus.eAction.Destroy || Target.GetComponent<cStatus>().Action == cStatus.eAction.Died)
                {
                    //Destroy(this.gameObject);

                    this.GetComponent<cWeapon>().AtkFlag = false;
                    StartFlag = false;

                    Target = null;

                    SearchArea.SetActive(true);

                }
            }
            else
            {
                //Destroy(this.gameObject);

                if (SearchArea == null)
                {
                    this.GetComponent<cWeapon>().AtkFlag = true;
                }

                this.GetComponent<cWeapon>().AtkFlag = false;
                StartFlag = false;

                SearchArea.SetActive(true);
            }

            if (StartFlag == false && SearchArea.activeSelf == false)
            {
                this.GetComponent<cWeapon>().AtkFlag = true;
                StartFlag = true;
            }

            if (this.GetComponent<cWeapon>().enabled == false)
            {
                this.GetComponent<cLockon>().enabled = false;
            }
        }
    }
}
