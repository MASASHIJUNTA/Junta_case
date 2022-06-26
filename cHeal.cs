/*
 * 　　　　　順田雅士　　　　プレイヤーの回復処理
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cHeal : MonoBehaviour
{
    cGameController GameController;

    cStatus Status;

    public int Hp; // 回復させる値

    bool Heal = false;

    // Start is called before the first frame update
    void Start()
    {
        GameController = FindObjectOfType<cGameController>();

        Status = GameController.Treasure.GetComponent<cStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Heal == false)
        {
            // エフェクト呼び出し
            Status.Hp += Hp;

            if (Status.Hp > Status.MaxHp)
            {
                Status.Hp = Status.MaxHp;
            }

            Heal = true;

        }
    }
}
