using UnityEngine;
using System.Collections;
 
public class EnemyrAttackStateBehaviour : StateMachineBehaviour
{

	private ProcessEnemyAnimEvent processEnemyAnimEvent;

	
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (processEnemyAnimEvent == null)
		{
			processEnemyAnimEvent = animator.transform.GetComponent<ProcessEnemyAnimEvent>();
		}

		processEnemyAnimEvent.AttackEnd();
	}

}