/*
 *                順田　　　　プレイヤーのアニメーション管理
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.Stage;

public class cPlayerAnime : MonoBehaviour
{
    cStatus Status;

    int OldHp = 0;

    Animator animator;

    bool Fast = true;

    public ParticleSystem GuardEffect;
    public ParticleSystem SummonEffect;

    cStageScroll StageScroll;

    // Start is called before the first frame update
    void Start()
    {
        Status = GetComponentInParent<cStatus>();

        OldHp = Status.Hp;
        animator = GetComponent<Animator>();

        StageScroll = FindObjectOfType<cStageScroll>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Fast == false) // 一回目だけHPを正常にとるためスルーする
        //{
        //    if (OldHp != Status.Hp)
        //    {
        //        animator.SetBool("Hit", true);
        //    }
        //}
        //else
        //{
        //    Fast = false;
        //}

        //OldHp = Status.Hp;

        if (StageScroll != null)
        {
            if (StageScroll.GetCuttingStartNow())
            {
                Summon();
            }
        }
    }

    void OnTriggerStay(Collider other)        // Triggerが他のオブジェクトに触れていたら
    {
        if (other.gameObject.tag == "EnemyAtk" || other.gameObject.tag == "Bullet")        // 触れているオブジェクトがプレイヤーオブジェクトなら
        {
            // Summon();

            GuardEffect.Play();
        }
    }

        public void SummonEnd()
    {
        animator.SetBool("Summon", false);
    }

    public void Summon()
    {
        animator.SetBool("Summon", true);
        SummonEffect.Play();
    }
}
