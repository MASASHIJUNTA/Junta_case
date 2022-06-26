/*
 *    順田　　画面切り替えのマスク処理
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class cMaskControl : MonoBehaviour
{
    [SerializeField]
    GameObject MaskImage;                  // マスクに使う画像
    [SerializeField]
    GameObject BackGround;                  // マスクに使う画像

    [Header("ローテーションカーブ")]
    public AnimationCurve RotationCurve;   //　マスクが大きくなる倍率のカーブ

    [SerializeField]                       // 最大値まで大きくなるまでの時間
    float MaxTime;
    [SerializeField]
    float MaxSize;                         // 大きさの最大値

    Vector3 StartSize = new Vector3(0.0f,0.0f,1.0f);

    Vector3 ReSize = new Vector3(0.0f, 0.0f, 1.0f);

    [SerializeField]
    string SceneName;

    float CountTime;
    float AddSize;

    [SerializeField]
    bool Debug = false;

    enum eMaskAction
    {
        Start,
        FadeIn,
        StandBy,
        FadeOut,
        End,
    };

    eMaskAction MaskAction = eMaskAction.End;


    // Start is called before the first frame update
    void Start()
    {
        StartSize = new Vector3(MaxSize, MaxSize, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // 挙動見るためのデバックをするかどうか
        if (Debug)
        {
            if (Input.GetKey(KeyCode.P))
            {
                MaskPlay();  // 初期化
            }
        }

        switch (MaskAction)
        {
            case eMaskAction.Start:

                MaskAction = eMaskAction.FadeIn;

                break;

            case eMaskAction.FadeIn:

                CountTime += Time.deltaTime;
                //インスペクターで設定したカーブを　指定した時間から値を取り出して倍率をかける
                //                          Evaluate(1)　一秒たった時の値が返ってくる
                AddSize = RotationCurve.Evaluate(CountTime / MaxTime) * MaxSize;

                ReSize.x = AddSize;
                ReSize.y = AddSize;

                MaskImage.transform.localScale = ReSize;

                if (CountTime > MaxTime)
                {
                    if (Debug)
                    {
                        MaskAction = eMaskAction.FadeOut;
                    }
                    else
                    {
                        MaskAction = eMaskAction.StandBy;
                    }

                    CountTime = MaxTime;
                }

                break;

            case eMaskAction.StandBy:     // シーンを切り替えるまでFadeOutしないように待つ

                if(SceneManager.GetActiveScene().name != SceneName) // 初期化したときに取得したシーンの名前と現在が変わっていたら切り替えができたと判断
                {
                    MaskAction = eMaskAction.FadeOut;
                }

                break;

            case eMaskAction.FadeOut:

                CountTime -= Time.deltaTime;
                //インスペクターで設定したカーブを　指定した時間から値を取り出して倍率をかける
                //                          Evaluate(1)　一秒たった時の値が返ってくる
                AddSize = RotationCurve.Evaluate(CountTime / MaxTime) * MaxSize;

                ReSize.x = AddSize;
                ReSize.y = AddSize;

                MaskImage.transform.localScale = ReSize;

                if (CountTime < 0)
                {
                    MaskAction = eMaskAction.End;

                    MaskImage.SetActive(false);
                    BackGround.SetActive(false);
                }

                break;

            case eMaskAction.End:
                break;
        }
    }

    public void MaskPlay()   // FadeIn/Outの為の初期化
    {
        MaskImage.transform.localScale = StartSize;

        MaskAction = eMaskAction.Start;

        MaskImage.SetActive(true);
        BackGround.SetActive(true);

        SceneName = SceneManager.GetActiveScene().name;    // 現在のシーンの名前を取得（シーンの切り替えを判断する為）
    }
}
