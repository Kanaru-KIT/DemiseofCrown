using UnityEngine;
using System.Collections;

public class AttackZombie : MonoBehaviour
{

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			
			col.GetComponent<CharacterScript>().TakeDamage(transform.root);
		}
	}
}