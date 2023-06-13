using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//UnityEngineでAIコードを使えるようにするよ。
using UnityEngine.AI;

//このスクリプトをアタッチすると自動的にNavMeshAgentを追加するよ！
[RequireComponent(typeof(NavMeshAgent))]

public class EnemyMove : MonoBehaviour
{
    //NavMeshAgent型を変数a_agentで宣言します。
    private NavMeshAgent a_agent;

    void Start()
    {
        //GetComponentでNavMeshAgentを取得して変数a_agentで参照します。
        a_agent = GetComponent<NavMeshAgent>();
    }

    //CollisionEnemyスクリプトのOnTrrigerStayにセットし、衝突判定を受け取るメソッド。
    public void OnDetectObject(Collider collider)
    {
        //検知したオブジェクトにPlayerタグがついていた時の処理。
        if (collider.CompareTag("Player"))
        {
            //Playerのコライダーの位置を取得して追いかけます。
            a_agent.destination = collider.transform.position;
        }
    }

}