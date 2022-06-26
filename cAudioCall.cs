/*
 *      順田　　　　AudioMng（叶君が作った音関係をすべて管理しているシステム）を利用して鳴らしたい音を鳴らす
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cAudioCall : MonoBehaviour
{
    [SerializeField]   cAudioMng AudioMng;

    [SerializeField] 　cAudioMng.SE_GIMIC  SE_G;   // SE　ギミック       // AudioMngで定義されている音関係（Gimmick
    [SerializeField] 　cAudioMng.SE_ENEMY  SE_E;   // SE　エネミー       // AudioMngで定義されている音関係（Enemy
    [SerializeField]   cAudioMng.SE_SYSTEM SE_S;   // SE  システム       // AudioMngで定義されている音関係（System

    [SerializeField, Range(0, 1)] float Volume = 1;                          // 0～1で音量
    public bool Loop;

    public bool StartSE;                                                    // 生成されたときに音を出すか

    // Start is called before the first frame update
    void Start()
    {
        AudioMng = FindObjectOfType<cAudioMng>();

        if(StartSE)
        {
            GimicSE();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(AudioMng == null)
        {
            AudioMng = FindObjectOfType<cAudioMng>();

            if (StartSE)
            {
                GimicSE();
            }
        }


    }

    //  敵の攻撃時音を出す
    public void EnemyAtkSE()
    {
        if (AudioMng != null)
            // 通常再生(SE再生,ループ可,重複無,音量)
            AudioMng.PlaySE_Enemy(SE_E, Loop, true, Volume);
    }

    //  ギミックの発動時（タイミングそれぞれ）に音を出す
    public void GimicSE()
    {
        if (AudioMng != null)
        {
            // 通常再生(SE再生,ループ可,重複無,音量)
            AudioMng.PlaySE_Gimic(SE_G, Loop, false, Volume);

            this.GetComponent<cAudioCall>().enabled = false;
        }
    }

    //  ダメージを受けた時に音を出す
    public void DamageSE()
    {
        if (AudioMng != null)
            // 通常再生(SE再生,ループ不可,重複無,音量)
            AudioMng.PlaySE_Enemy(cAudioMng.SE_ENEMY.SE_DAMAGE, false, false, 1.0f);
    }

    //  キャラクターが死んだ時に音を出す
    public void DeadSE()
    {
        if (AudioMng != null)
            // 通常再生(SE再生,ループ可,重複無,音量)
            AudioMng.PlaySE_Enemy(cAudioMng.SE_ENEMY.SE_DEAD, false, false, 1.0f);
    }

    //　システムの稼働時に音を出す（経験値取得時など
    public void SystemSE()
    {
        if (AudioMng != null)
        {
            // 通常再生(SE再生,ループ可,重複無,音量)
            AudioMng.PlaySE_System(SE_S, false, false, Volume);
        }
    }
}
