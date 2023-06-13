using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//UnityEngine��AI�R�[�h���g����悤�ɂ����B
using UnityEngine.AI;

//���̃X�N���v�g���A�^�b�`����Ǝ����I��NavMeshAgent��ǉ������I
[RequireComponent(typeof(NavMeshAgent))]

public class EnemyMove : MonoBehaviour
{
    //NavMeshAgent�^��ϐ�a_agent�Ő錾���܂��B
    private NavMeshAgent a_agent;

    void Start()
    {
        //GetComponent��NavMeshAgent���擾���ĕϐ�a_agent�ŎQ�Ƃ��܂��B
        a_agent = GetComponent<NavMeshAgent>();
    }

    //CollisionEnemy�X�N���v�g��OnTrrigerStay�ɃZ�b�g���A�Փ˔�����󂯎�郁�\�b�h�B
    public void OnDetectObject(Collider collider)
    {
        //���m�����I�u�W�F�N�g��Player�^�O�����Ă������̏����B
        if (collider.CompareTag("Player"))
        {
            //Player�̃R���C�_�[�̈ʒu���擾���Ēǂ������܂��B
            a_agent.destination = collider.transform.position;
        }
    }

}