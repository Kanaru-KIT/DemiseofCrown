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
        //　プレイヤーキャラクターを発見
        if (col.tag == "Player")
        {
            Enemy.EnemyState state = enemy.GetState();
           
            //　敵キャラクターが追いかける状態でなければ追いかける設定に変更
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