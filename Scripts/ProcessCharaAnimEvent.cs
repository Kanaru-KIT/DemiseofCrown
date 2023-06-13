using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessCharaAnimEvent : MonoBehaviour
{
    private CharacterScript characterScript;
    [SerializeField] private CapsuleCollider capsuleCollider;

    
    public AudioSource attackSound;



    private void Start()
    {
        characterScript = GetComponent<CharacterScript>();
        attackSound = GetComponent<AudioSource>();
        attackSound = characterScript.attackSound;
      
    }

    void AttackStart()
    {
        capsuleCollider.enabled = true;
        attackSound.PlayOneShot(attackSound.clip);


    }

    public void AttackEnd()
    {
        if (capsuleCollider != null)
        {
            capsuleCollider.enabled = false;
            
        }
    }

    void StateEnd()
    {
        characterScript.SetState(CharacterScript.MyState.Normal);
    }

    public void EndDamage()
    {
        characterScript.SetState(CharacterScript.MyState.Normal);
    }
}