using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//UnityEngine��Events�̃R�[�h���g����悤�ɂ����B
using UnityEngine.Events;

//�X�N���v�g��ǉ��������Ɏ�����Collider��ǉ������I
[RequireComponent(typeof(Collider))]

public class EnemyCollision : MonoBehaviour
{
    //SerializeField��UnityEvent��Inspector����g����悤�ɂ��܂��B
    //UnityEvent�^�̕ϐ�onTriggerStay��錾���܂��B
    //�����V����new UnityEvent()�Ɛ錾�����I
    [SerializeField] private UnityEvent onTriggerStay = new UnityEvent();

    private void OnTriggerStay(Collider other)
    {
        //onTriggerStay�Ŏw�肳�ꂽ���������s���܂��B
        onTriggerStay.Invoke();
    }

    //Inspector��OnTriggerStay()�����ۂɕ\�������Ďg����悤�ɂ��܂��B
    [SerializeField]
    public class TriggerEvent : UnityEvent<Collider>
    {

    }
}