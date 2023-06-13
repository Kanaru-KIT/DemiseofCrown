using UnityEngine;
using System.Collections;
 
public class PlayerAttackStateBehaviour : StateMachineBehaviour
{

	private ProcessCharaAnimEvent processCharaAnimEvent;

	
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (processCharaAnimEvent == null)
		{
			processCharaAnimEvent = animator.transform.GetComponent<ProcessCharaAnimEvent>();
		}
		
		processCharaAnimEvent.AttackEnd();
	}

}