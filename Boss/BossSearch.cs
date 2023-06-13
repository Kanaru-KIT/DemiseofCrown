using UnityEngine;
using System.Collections;

public class BossSearch : MonoBehaviour
{

    private Boss boss;
    [SerializeField] private AudioSource bgm;

    void Start()
    {
        boss = GetComponentInParent<Boss>();
    }

    void OnTriggerStay(Collider col)
    {
        //　プレイヤーキャラクターを発見
        if (col.tag == "Player")
        {
            bgm.PlayOneShot(bgm.clip);
            Boss.BossState state = boss.GetState();
           
            //　敵キャラクターが追いかける状態でなければ追いかける設定に変更
            if (state == Boss.BossState.Wait || state == Boss.BossState.Walk)
            {
                
                boss.SetState(Boss.BossState.Chase, col.transform);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
           
            boss.SetState(Boss.BossState.Wait);
        }
    }
}