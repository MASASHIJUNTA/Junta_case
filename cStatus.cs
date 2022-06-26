/*
 *     順田雅士　　　　基本的なステータス　HPなど
 *     
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cStatus : MonoBehaviour
{
    [SerializeField] int Lv;                     // レベル
    public  int Hp;        　           // 体力
    public  int MaxHp;                  // 体力の最大値
    public  int Atk;       　           // 攻撃力
    [SerializeField]  float Speed;   　           // 移動速度
    public float CoolTime;             // 攻撃間隔
    public float StanTime;             // 気絶時間
    public float KnockBackTime;        // ノックバックの距離時間
    [SerializeField] float KnockBackTime_D;      // プレイヤーに攻撃した時のノックバックの距離時間
    public float BackSpeed;            // ノックバックするときのスピード
    [SerializeField] float BackSpeed_D;          // 死ぬ時のスピード
    public float SlowTime;             // スロウになってる時間

    float SlowSpeed = 0.3f;

    float AtkTime = 0;
    public bool AtkFlag = false;

    public bool Long;
    public bool Boss;
    public bool Special;


    public GameObject AtkAreaPrefab;  // 遠距離攻撃用の弾のデータ

    [SerializeField]
    GameObject AtkArea;               // 遠距離攻撃の弾

    public bool Animation = false;    // アニメーションするか
    Animator anime; // 自分のアニメーションコンポーネント

    [SerializeField]
    ParticleSystem StanEffect;              // スタン時のエフェクト

    [SerializeField]
    ParticleSystem AtkEffect;               // 攻撃時のエフェクト

    [SerializeField]
    ParticleSystem LvAdvantageEffect1;      // レベルアドバンテージの即死時のエフェクト

    [SerializeField]
    ParticleSystem LvAdvantageEffect2;      // レベルアドバンテージの被ダメ時のエフェクト

    [SerializeField]
    ParticleSystem HitEffect;               // 被ダメ時のエフェクト

    [SerializeField]
    ParticleSystem DiedEffect;              // 死亡時のエフェクト

    [SerializeField]
    ParticleSystem SlowEffect;              // スロウ状態のエフェクト

    [SerializeField]
    GameObject CounterObjectPrefab;        // カウンターするときに呼び出すオブジェクト

    float CountTime;
    public float DeleteTime;
    float SetAlpha = 0;
    public bool End = false;

    Color32 AddColor;

    public cAudioCall AudioCall;

    [SerializeField]
    float OutLinePos_x = 18;               // キャラのx座標（中心点）がこの値を超えているかどうか

    public enum eAction  // キャラの状態
    {
        Walking,	// 探索
        Attacking,	// 攻撃
        Died,       // 死亡
        Destroy,    // 消す
        Stan,       // スタン
        KnockBack,  // ノックバック
        Slow,       // スロウ状態
    };

    public eAction Action = eAction.Walking;

    // Start is called before the first frame update
    void Start()
    {
        Hp = MaxHp;        // 最大体力を今の体力に代入

        if (Animation == true)
        {
            anime = GetComponent<Animator>();
        }

        KnockBackTime_D = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float tmpSpeed = this.Speed;
        if (true == MyGame.Stage.cStageScroll._S_FlgScrollStop)
            this.Speed *= MyGame.Stage.cStageScroll._S_CuttingSpeedAjust;

        //if (DiedEffect != null)
        //{
        //    if (DiedEffect.gameObject.transform.parent != null)
        //    {
        //        DiedEffect.gameObject.transform.SetParent(null);
        //    }
        //}


        if (Action != eAction.Died && Action != eAction.Destroy)
        {
            if (Action != eAction.Attacking)
            {
                if (AtkEffect != null)
                {
                    if (AtkEffect.isPlaying == true)
                    {
                        AtkEffect.Stop();
                    }
                }
            }

            if (StanTime > 0)
            {
                StanTime -= Time.deltaTime;

                if (StanTime <= 0)
                {
                    StanTime = 0;

                    StanEffect.Stop();

                    if (Action == eAction.Stan)
                        Action = eAction.Walking;
                }
                else if (StanEffect != null)
                {
                    if (StanEffect.isPlaying == false)
                    {
                        StanEffect.Play();
                    }
                }
            }
            else if (StanEffect != null)
            {
                if (StanEffect.isPlaying == true)
                {
                    StanEffect.Stop();
                }
            }

            if (KnockBackTime > 0)
            {
                KnockBackTime -= Time.deltaTime;

                if (this.transform.position.x > OutLinePos_x)
                    KnockBackTime = 0;

                if (KnockBackTime <= 0)
                {
                    KnockBackTime = 0;

                    if (Action == eAction.KnockBack)
                        Action = eAction.Walking;
                }
            }

            if (SlowTime > 0)
            {
                SlowTime -= Time.deltaTime;

                if (SlowTime <= 0)
                {
                    SlowTime = 0;

                    if (SlowEffect != null)
                        SlowEffect.Stop();

                    if (Action == eAction.Slow)
                        Action = eAction.Walking;
                }
                else if (SlowEffect != null)
                {
                    if (SlowEffect.isPlaying == false)
                    {
                        SlowEffect.Play();
                    }
                }
            }
            else if (SlowEffect != null)
            {
                if (SlowEffect.isPlaying == true)
                {
                    SlowEffect.Stop();
                }
            }

            if (KnockBackTime > 0)
            {
                Action = eAction.KnockBack;
            }
            else if (StanTime > 0)
            {
                Action = eAction.Stan;
            }
            else if (SlowTime > 0)
            {
                Action = eAction.Slow;
            }
        }

        switch (Action)
        {
            case eAction.Walking: // ウォーキング状態の時だけ歩く

                transform.Translate(Speed * Time.deltaTime, 0f, 0f);

                if (anime != null)
                {
                    anime.SetBool("Atk", false);
                }

                break;

            case eAction.Attacking:



                if (AtkFlag == false)
                {
                    AtkTime += Time.deltaTime;
                }

                if (AtkTime >= 1.5f)　　　　　　// 本来はアニメーションから関数を呼び出す
                {
                    Attack();
                }

                break;

            case eAction.Stan:  // 気絶状態

                break;

            case eAction.KnockBack:

                transform.Translate(BackSpeed * Time.deltaTime * KnockBackTime, 0f, 0f);

                break;

            case eAction.Slow:

                if (KnockBackTime > 0)
                {
                    Action = eAction.KnockBack;
                }
                else if (StanTime > 0)
                {
                    Action = eAction.Stan;
                }
                else
                {
                    transform.Translate(Speed * SlowSpeed * Time.deltaTime, 0f, 0f);
                }

                break;

            case eAction.Destroy:

                transform.Translate(BackSpeed_D * Time.deltaTime * KnockBackTime_D, 0f, 0f);

                if (KnockBackTime_D > 0)
                {
                    KnockBackTime_D -= Time.deltaTime;

                    if (KnockBackTime_D <= 0)
                    {
                        KnockBackTime_D = 0;

                        if (DiedEffect != null)
                        {
                            if (DiedEffect.gameObject.transform.parent != null)
                            {
                                var SetScale = DiedEffect.gameObject.transform.localScale;

                                DiedEffect.gameObject.transform.SetParent(null, true);

                                DiedEffect.gameObject.transform.localScale = SetScale;
                            }

                            DiedEffect.Play();
                        }
                    }
                }
                else
                {
                    Delete();
                }

                break;

            case eAction.Died:

                Delete();

                break;
        }

        //if(Action == eAction.Died && Action == eAction.Destroy)
        //{
        //    if (AudioCall != null)
        //        AudioCall.DeadSE();
        //}

        this.Speed = tmpSpeed;
    }

    public void Damage(int damage)    // ダメージ計算
    {
        Hp -= damage;

        if (AudioCall != null)
            AudioCall.DamageSE();

        if (Hp <= 0 && Action != eAction.Destroy && Action != eAction.Died)
        {
            Action = eAction.Died;

            AudioCall.DeadSE();

            if (this.gameObject.tag != "Treasure")
            {
                DeleteStandby();
            }

            if (DiedEffect != null)
            {
                if (DiedEffect.gameObject.transform.parent != null)
                {
                    var SetScale = DiedEffect.gameObject.transform.localScale;

                    DiedEffect.gameObject.transform.SetParent(null, true);

                    DiedEffect.gameObject.transform.localScale = SetScale;
                }

                DiedEffect.Play();
            }
        }
    }

    public void Attack()
    {
        if (anime != null)
        {
            anime.SetBool("Atk", false);
        }

        if (Action != eAction.Destroy && Action != eAction.Died)
        {
            if (AtkEffect != null)
            {
                if (AtkEffect.isPlaying == false)
                {
                    AtkEffect.Play();
                }
            }

            AtkFlag = true;

            AtkTime = 0;

            AtkArea = AtkAreaPrefab;

            if (!Special)
                AtkArea.GetComponent<cBullet>().Status = this.gameObject.GetComponent<cStatus>();   // ステータスの参照場所を取得;

            Instantiate(AtkArea, transform.position, Quaternion.identity);

            if (AudioCall != null)
                AudioCall.EnemyAtkSE();
        }
    }

    public void AttackStart()
    {
        if (anime != null)
        {
            anime.SetBool("Atk", true);
        }

        Action = eAction.Attacking;
    }

    public void LvAdvantage(int Lv, int Atk)
    {
        int MaxAtk = 999;

        if (this.Lv <= Lv)
        {
            //if (LvAdvantageEffect1 != null)
            //{
            //    if (LvAdvantageEffect1.isPlaying == false)
            //    {
            //        LvAdvantageEffect1.Play();

            //    }
            //}

            Damage(MaxAtk);
        }
        else
        {
            // Debug.Log(Atk + "アドバンテージ");
            Damage(Atk);

            //if (LvAdvantageEffect2 != null)
            //{
            //    if (LvAdvantageEffect2.isPlaying == false)
            //    {
            //        LvAdvantageEffect2.Play();
            //    }
            //}
        }
    }

    public void Destroy()
    {
        Hp = 0;

        Action = eAction.Destroy;

        DeleteStandby();

        AudioCall.DeadSE();

    }

    void Delete()
    {
        if (CountTime < DeleteTime)
        {
            CountTime += Time.deltaTime;

            if (CountTime > DeleteTime)
            {
                CountTime = DeleteTime;
            }

            SetAlpha = 255 - 255 * CountTime / DeleteTime;

            AddColor = this.GetComponent<SpriteRenderer>().color;
            AddColor.a = (byte)SetAlpha;
            this.GetComponent<SpriteRenderer>().color = AddColor;

        }
        else
        {
            End = true;
            //Destroy(this.gameObject);
        }
    }

    public void Counter(GameObject Target)
    {
        HitEffect.Play();

        if (Hp > 0 && eAction.Died != Action)
        {
            GameObject CounterObject;

            CounterObject = CounterObjectPrefab;
            CounterObject.GetComponent<cCounteCrow>().Target = Target;

            Instantiate(CounterObject, transform.position, Quaternion.identity);
        }
    }

    void DeleteStandby()
    {
        if (StanEffect != null)
            StanEffect.gameObject.SetActive(false);

        if (AtkEffect != null)
            AtkEffect.gameObject.SetActive(false);

        if (LvAdvantageEffect1 != null)
            LvAdvantageEffect1.gameObject.SetActive(false);

        if (LvAdvantageEffect2 != null)
            LvAdvantageEffect2.gameObject.SetActive(false);

        if (HitEffect != null)
            HitEffect.gameObject.SetActive(false);

        if (SlowEffect != null)
            SlowEffect.gameObject.SetActive(false);

        this.GetComponent<BoxCollider>().enabled = false;
        this.GetComponent<Rigidbody>().useGravity = false;
    }
}
