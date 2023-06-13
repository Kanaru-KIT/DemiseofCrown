using UnityEngine;
using System.Collections;

public class ProcessEnemyIronGuardAnimEvent : MonoBehaviour
{ 

    private Boss boss;
    [SerializeField]
    private CapsuleCollider capsuleCollider;

    void Start()
    {
        boss = GetComponent<Boss>();
    }

    void AttackStart()
    {
        capsuleCollider.enabled = true;
    }

    public void AttackEnd()
    {
        capsuleCollider.enabled = false;
    }

    public void StateEnd()
    {
        boss.SetState(Boss.BossState.Freeze);
    }

    public void EndDamage()
    {
        boss.SetState(Boss.BossState.Walk);
    }
}