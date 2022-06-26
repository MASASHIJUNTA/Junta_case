/*
 *   順田　時間を計算して消すスクリプト（シリンダーを使ってバーを表示可能）
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cTimer : MonoBehaviour
{
    public Slider TimeSlider;       // UIのシリンダーを取得する
    [SerializeField] float Timetolive;    　// 生存時間
    float MaxTime;                // 生存時間の最大時を記憶

    public ParticleSystem Effect;  // 消えるときに出すエフェクト
    bool EffectPlay = false;

    // Start is called before the first frame update
    void Start()
    {
        MaxTime = Timetolive;
    }

    // Update is called once per frame
    void Update()
    {


        Timetolive -= Time.deltaTime * Time.timeScale;    // 時間を減らす

        if (TimeSlider != null)            // バーを表示したければインスペクターからいれる
        {
            // スライダーの向きをカメラ方向に固定
            TimeSlider.transform.rotation = Camera.main.transform.rotation; // カメラの方を向き続ける

            TimeSlider.value = Timetolive / MaxTime;                        // 現在÷最大で０～１の割合をとってvalueに代入
        }

        if (Effect != null && Timetolive <= 0.5 && EffectPlay == false)                            // 消える少し前にエフェクトを出す
        {
            Effect.Play();

            EffectPlay = true;
        }

        if (Timetolive <= 0)                                              // 0時間以下になったらオブジェクトを消す
        {

            Destroy(this.gameObject);
        }
    }
}
