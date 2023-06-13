using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackStateBehaviour : StateMachineBehaviour
{

	private ProcessEnemyIronGuardAnimEvent processEnemyIronGuardAnimEvent;


	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (processEnemyIronGuardAnimEvent == null)
		{
			processEnemyIronGuardAnimEvent = animator.transform.GetComponent<ProcessEnemyIronGuardAnimEvent>();
		}

		processEnemyIronGuardAnimEvent.AttackEnd();
	}

}