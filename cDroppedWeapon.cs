/*
 *   順田　これ自体が武器になるわけでく落ちてからダメージを与えるものを出すスクリプト
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cDroppedWeapon : MonoBehaviour
{
    public GameObject WeaponPrefab;     // 地面にあたったときにダメージエリアを呼び出すためのデータ
    GameObject Weapon;                  // ダメージエリアオブジェクト

    public string Name;                     // アイテムの名前
    public string Explanation;              // 効果の説明
    public int AreaAtk;                     // 攻撃力
    public int Atk;                         // 攻撃力
    public float Speed;                     // 落下スピード

    public bool Special;                    // 特殊効果（ダメージを与えないものかどうか）

    public cAudioCall AudioCall;            // 音
    public cWeaponRender WeaponRender;      // 武器描画のスクリプト

    // Start is called before the first frame update
    void Start()
    {
        Weapon = WeaponPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0f, -Speed * Time.deltaTime, 0f);   // 落下処理

        WeaponRender = this.GetComponent<cWeaponRender>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (Atk > 0)
            {
                other.gameObject.GetComponent<cStatus>().Damage(Atk);

                

                Vector3 position;

                position = transform.position;

                position.y -= 2;

                Instantiate(Weapon, position, Quaternion.identity);

                if (Special == false)
                {
                    Weapon.GetComponent<cDamageArea>().Atk = AreaAtk;
                }

                AudioCall.GimicSE();
                // エフェクト呼び出し
                Destroy(this.gameObject);
            }
        }


        //   地面にあたったらダメージ判定を持っているオブジェクトを出す
        if (other.gameObject.tag == "Floor")
        {
            Weapon = WeaponPrefab;
            Instantiate(Weapon, transform.position, Quaternion.identity);

            if (Special == false)
            {
                Weapon.GetComponent<cDamageArea>().Atk = AreaAtk;
            }

            AudioCall.GimicSE();

            if (WeaponRender != null)
            {
                WeaponRender.Delete = true;

                this.GetComponent<cDroppedWeapon>().enabled = false;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
