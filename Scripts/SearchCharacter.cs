using UnityEngine;
using System.Collections;

public class SearchCharacter : MonoBehaviour
{

    private Enemy enemy;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    void OnTriggerStay(Collider col)
    {
        //�@�v���C���[�L�����N�^�[�𔭌�
        if (col.tag == "Player")
        {
            Enemy.EnemyState state = enemy.GetState();
           
            //�@�G�L�����N�^�[���ǂ��������ԂłȂ���Βǂ�������ݒ�ɕύX
            if (state == Enemy.EnemyState.Wait || state == Enemy.EnemyState.Walk)
            {
               
                enemy.SetState(Enemy.EnemyState.Chase, col.transform);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
           
            enemy.SetState(Enemy.EnemyState.Wait);
        }
    }
}