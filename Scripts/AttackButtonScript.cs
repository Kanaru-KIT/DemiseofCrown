using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButtonScript : MonoBehaviour
{
    Animator animator;
    static readonly int hashAttackType = Animator.StringToHash("AttackType");
    static readonly int hashFoward = Animator.StringToHash("Foward");
    static readonly int hashSide = Animator.StringToHash("Side");

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        TryGetComponent(out animator);
    }

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

       
    }

    public int AttackType
    {
        get => animator.GetInteger(hashAttackType);
        set => animator.SetInteger(hashAttackType, value);
    }
}