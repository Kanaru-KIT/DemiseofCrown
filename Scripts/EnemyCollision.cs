using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//UnityEngineでEventsのコードを使えるようにするよ。
using UnityEngine.Events;

//スクリプトを追加した時に自動でColliderを追加するよ！
[RequireComponent(typeof(Collider))]

public class EnemyCollision : MonoBehaviour
{
    //SerializeFieldでUnityEventをInspectorから使えるようにします。
    //UnityEvent型の変数onTriggerStayを宣言します。
    //それを新しくnew UnityEvent()と宣言するよ！
    [SerializeField] private UnityEvent onTriggerStay = new UnityEvent();

    private void OnTriggerStay(Collider other)
    {
        //onTriggerStayで指定された処理を実行します。
        onTriggerStay.Invoke();
    }

    //InspectorでOnTriggerStay()を実際に表示させて使えるようにします。
    [SerializeField]
    public class TriggerEvent : UnityEvent<Collider>
    {

    }
}