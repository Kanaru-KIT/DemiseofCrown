using UnityEngine;
using System.Collections;

public class ProcessEnemyAnimEvent : MonoBehaviour
{ 

    private Enemy enemy;
    [SerializeField]
    private SphereCollider sphereCollider;

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void AttackStart()
    {
        sphereCollider.enabled = true;
    }

    public void AttackEnd()
    {
        if (sphereCollider != null)
        {
            sphereCollider.enabled = false;
        }
    }

    public void StateEnd()
    {
        enemy.SetState(Enemy.EnemyState.Freeze);
    }

    public void EndDamage()
    {
        enemy.SetState(Enemy.EnemyState.Walk);
    }
}