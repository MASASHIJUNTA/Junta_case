/*
 *              順田雅士　　　　Statusの表示、HPバーなど
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cCharacterStatusGui : MonoBehaviour
{
    public Canvas CanvasData;         // UICanvas取得
    public Slider HpSlider;           // UIのシリンダーを取得する
    cStatus Status;                   // このオブジェクトのステータスの参照場所を記憶する場所
    float OldHp;                      // ダメージをくらったかを判断するために体力を記憶しておく

    private cGameController _GameController;
    [SerializeField] private GameObject _AuraObj;  // オーラ

    int LayerValue = 0;               // レイヤーの値

    // Start is called before the first frame update
    void Start()
    {
        this._GameController = FindObjectOfType<cGameController>();

        Status = this.gameObject.GetComponent<cStatus>();   
                                                             
        HpSlider.value = 1;                                 // シリンダーの値をMaxにする
        OldHp = Status.MaxHp;                               // 体力を記憶
    }

    // Update is called once per frame
    void Update()
    {
        // スライダーの向きをカメラ方向に固定
        HpSlider.transform.rotation = Camera.main.transform.rotation; // カメラの方を向き続ける

        if(OldHp != Status.Hp)                                        // 今の体力を記憶していた体力が違った場合　シリンダーを更新
        {
            HpSlider.value = (float)Status.Hp / (float)Status.MaxHp;  // シリンダーの値を計算する　0 ～ 1

            LayerValue = (int)((1.15f - HpSlider.value) * 10.0f);      // valueの値が小さいほどレイヤー値を高くする

            CanvasData.sortingOrder = LayerValue + 1;

            OldHp = Status.Hp;                                        // 体力を記憶
        }

        if(Status.Hp <= 0)
        {
            CanvasData.enabled = false;

            this.GetComponent<cCharacterStatusGui>().enabled = false;
        }


        // クリア・リタイア
        switch (this._GameController.GameStatus)
        {
            case cGameController.eGameStatus.Main:
                break;
            case cGameController.eGameStatus.Clear:
            case cGameController.eGameStatus.Failure:
                if (null != this._AuraObj)
                    this._AuraObj.gameObject.SetActive(false);
                this.CanvasData?.gameObject.SetActive(false);
                break;
        }

    }


}
