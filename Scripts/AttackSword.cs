using UnityEngine;
using System.Collections;

public class AttackSword : MonoBehaviour
{

	public int attackdamage;

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Enemy")
		{
			
			col.GetComponent<Enemy>().SetState(Enemy.EnemyState.Damage);
		}
		if (col.tag == "Boss")
		{

			col.GetComponent<Boss>().SetState(Boss.BossState.Damage);
		}

	}
}