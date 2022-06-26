/*
 *  　順田　　ゲームSystemの管理（勝ち・負けの判断など）
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MyGame.LevelUp;


public class cGameController : MonoBehaviour
{
    public int ExpTotal = 0;          // 経験値の合計
    public int NextExp = 10;          // 次のレベルまでにいる経験値
    public float ExpMagnifi = 1.4f;   // 次のレベルまでの値の倍率
                                         
    public int Lv = 334;                // レベル
                                         
    //public Text EnemyCounter;         // 現在の敵の数（テキスト
    public string FixedText;          // 定型文（敵　あとn)
    public int MaxEnemy = 1;              // 敵の数を記憶

    int[] NextLvUp = { 1, 40, 120, 210, 350, 1 };    // 次のレベルになるための総数

    public Slider ExpSlider;           // UIのシリンダーを取得する
    public TextMeshProUGUI LvText;                // レベル表示
    public TextMeshProUGUI EnemyCntText;          // 敵の数表示
    public string EnemyCntFront = "敵残り";

    public bool DebugFlg = false;

    [SerializeField]
    ParticleSystem LvUpEffect;              // レベルアップ時のエフェクト

    [System.Serializable] public class cTextNumData
    {
        [Header("名前")] public string _Name;
        [Header("この数値以上の場合")] public int _Over;
        [Header("残り数の代わりにこのテキストを表示する")] public string _Text;
    }
    public List<cTextNumData> _ListTextNumData;

    public cLevelUpAnimaMastar _LevelUpAnimaMastar;


    public enum eGameStatus
    {
        Main,	　　// メイン
        Clear,	　　// クリア
        Failure,    // 失敗
    };

    public eGameStatus GameStatus;    // ゲームの状態
    public GameObject Treasure;       // このステージの宝の状態を参照する場所
    string LvTextFront = "Lv　";
    string LvTextUnder = "壱";

    public cAudioCall AudioCall;

    // Start is called before the first frame update
    void Start()
    {
        GameStatus = eGameStatus.Main;

        ExpSlider.value = 0;                                 // シリンダーの値を0にする
        ExpTotal = NextLvUp[Lv - 1];

        LvText.text = LvTextFront + LvTextUnder;             // レベル表示


        this._ListTextNumData.Sort((a, b) => b._Over - a._Over); // 降順ソート
        this.UpdateEnemyCntText();

        UpDataLvText();
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateEnemyCntText();
        this.UpDataLvText();

        switch (GameStatus)
        {
            case eGameStatus.Main:

                if(Treasure.GetComponent<cStatus>().Hp <= 0)   // 宝の体力がなくなれば失敗
                {
                    Debug.Log("失敗");
                    GameStatus = eGameStatus.Failure;
                    
                    this._LevelUpAnimaMastar.AllDestroy();
                }
                else if(MaxEnemy <= 0)                          // 敵がいなくなればクリア
                {
                    Debug.Log("クリア");
                    GameStatus = eGameStatus.Clear;

                    this._LevelUpAnimaMastar.AllDestroy();
                }
                break;

            case eGameStatus.Clear:
                // クリア演出
                break;

            case eGameStatus.Failure:
                // 失敗演出
                break;
        }

        try
        {
            float value = ExpTotal - NextLvUp[Lv - 1];
            float MaxValue = NextLvUp[Lv] - NextLvUp[Lv - 1];

            float Setvalue = value / MaxValue;

            if(Setvalue > 1)
            {
                Setvalue = 1.0f;
            }

            ExpSlider.value = Setvalue;  // シリンダーの値を計算する　0 ～ 1
        }
        catch(System.Exception ex) { }

    }

    public void GetExp(int exp)    // 経験値を取得
    {
        if (ExpTotal < NextLvUp[4])
            ExpTotal += exp;
    
        try
        {

            if (ExpTotal >= NextLvUp[Lv])
            {
                if (Lv <= 4)
                {
                    AudioCall.SystemSE();

                    Lv++;

                    NextExp = NextLvUp[Lv];

                    this._LevelUpAnimaMastar.DoAnima(Lv);

                    LvUpEffect.Play();
                }

                UpDataLvText();
                // NextExp = NextExp + (int)(NextExp * ExpMagnifi);   // 次のレベルアップまでの経験値を計算（適当    
            }

        }
        catch(System.Exception ex) { }

        this.EnemyCountDown();
    }


    public void EnemyCountDown()
    {
        MaxEnemy--;                  // 経験値を獲得　＝　敵が死んだ　から敵の総数から1引く

        if (MaxEnemy < 0)
        {
            MaxEnemy = 0;
        }

        // EnemyCounter.text = FixedText + MaxEnemy;    // テキストの更新
    }


    public void EnemyCountUp()   // 敵のカウントアップ
    {
        MaxEnemy++;

        // EnemyCounter.text = FixedText + MaxEnemy;   // テキストの更新
    }

    public void UpDataLvText()
    {
        switch(Lv)
        {
            case 1:
                LvTextUnder = "壱";
                break;
            case 2:
                LvTextUnder = "弐";
                break;
            case 3:
                LvTextUnder = "参";
                break;
            case 4:
                LvTextUnder = "肆";
                break;
            case 5:
                LvTextUnder = "伍";
                break;

            default:
                break;
        }

        LvText.text = LvTextFront + LvTextUnder;             // レベル表示

    }

    private void UpdateEnemyCntText()
    {
        int numX = MaxEnemy % 10;
        int numX0 = MaxEnemy / 10;

        string numText = "";

        foreach (var val in this._ListTextNumData)
        {
            bool flg = this.MaxEnemy >= val._Over;
            if (false == flg)
                continue;

            numText = val._Text;
            break;
        }

        if ("" == numText)
        {
            // 10の位
            if (numX0 != 0)
                numText += this.GetKanzi_FromNum(numX0) + this.GetKanzi_FromNum(10);
            numText += this.GetKanzi_FromNum(numX);
        }

    
        EnemyCntText.text = EnemyCntFront + numText;
    }


    private string GetKanzi_FromNum(int num)
    {
        switch (num)
        {
            case 1: return "壱";
            case 2: return "弐";
            case 3: return "参";
            case 4: return "肆";
            case 5: return "伍";
            case 6: return "陸";
            case 7: return "漆";
            case 8: return "捌";
            case 9: return "玖";
            case 10: return "拾";
        }

        return "";
    }

    public void AllClear()
    {
        GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] PlayerAtks = GameObject.FindGameObjectsWithTag("PlayerAtk");
        GameObject[] Enemys = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] EnemyAtks = GameObject.FindGameObjectsWithTag("EnemyAtk");
        GameObject[] Effects = GameObject.FindGameObjectsWithTag("Effect");
        GameObject[] Bullets = GameObject.FindGameObjectsWithTag("Bullet");
        GameObject[] Walls = GameObject.FindGameObjectsWithTag("Wall");

        foreach (GameObject Player_Soccer in Players)
        {
            Destroy(Player_Soccer);
        }

        foreach (GameObject PlayerAtk_Soccer in PlayerAtks)
        {
            Destroy(PlayerAtk_Soccer);
        }

        foreach (GameObject Enemy_Soccer in Enemys)
        {
            Destroy(Enemy_Soccer);
        }

        foreach (GameObject EnemyAtk_Soccer in EnemyAtks)
        {
            Destroy(EnemyAtk_Soccer);
        }

        foreach (GameObject Effect_Soccer in Effects)
        {
            Destroy(Effect_Soccer);
        }

        foreach (GameObject Bullet_Soccer in Bullets)
        {
            Destroy(Bullet_Soccer);
        }

        foreach (GameObject Wall_Soccer in Walls)
        {
            Destroy(Wall_Soccer);
        }
    }

}
