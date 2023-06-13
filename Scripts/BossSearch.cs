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
        //�@�v���C���[�L�����N�^�[�𔭌�
        if (col.tag == "Player")
        {
            bgm.PlayOneShot(bgm.clip);
            Boss.BossState state = boss.GetState();
           
            //�@�G�L�����N�^�[���ǂ��������ԂłȂ���Βǂ�������ݒ�ɕύX
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