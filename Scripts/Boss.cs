using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class Boss : MonoBehaviour {

    public enum BossState {
        Walk,
        Wait,
        Chase,
        Attack,
        Freeze,
        Freeze2,
        Damage,
        Counter
    };

    private CharacterController enemyController;
    private Animator animator;
    //�@�ړI�n
    private Vector3 destination;
    //�@�����X�s�[�h
    [SerializeField]
    private float walkSpeed = 1.0f;
    //�@���x
    private Vector3 velocity;
    //�@�ړ�����
    private Vector3 direction;
    //�@�����t���O
    private bool arrived;
    //�@SetPosition�X�N���v�g
    private BossSetPosition bossSetPosition;
    //�@�҂�����
    [SerializeField]
    private float waitTime = 3;
    //�@�o�ߎ���
    private float elapsedTime;
    private float counterTime = 2.5f;
    // �G�̏��
    private BossState state;
    //�@�v���C���[Transform
    private Transform playerTransform;

    private bool dying;
    private bool counter;

    private float freezeTime = 1.5f;

    //�G�̃X�e�[�^�X
    public int bossMaxHP = 210;
    int hp;
    float sceneTime;
    float freez2time = 2;

    [SerializeField] private AudioSource bossSound;

    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip CounterSound;

    // Use this for initialization
    void Start() {
        enemyController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        bossSetPosition = GetComponent<BossSetPosition>();
        bossSetPosition.CreateRandomPosition();
        velocity = Vector3.zero;
        arrived = false;
        elapsedTime = 0f;
        SetState(BossState.Walk);

        hp = bossMaxHP;

        dying = false;
        counter = false;
    }

    // Update is called once per frame
    void Update() {
        //�@�����܂��̓L�����N�^�[��ǂ���������
        if (state == BossState.Walk || state == BossState.Chase)
        {
            //�@�L�����N�^�[��ǂ��������Ԃł���΃L�����N�^�[�̖ړI�n���Đݒ�
            if (state == BossState.Chase)
            {
                bossSetPosition.SetDestination(playerTransform.position);
            }
            if (enemyController.isGrounded)
            {
                velocity = Vector3.zero;
                animator.SetFloat("Speed", 2.0f);
                direction = (bossSetPosition.GetDestination() - transform.position).normalized;
                transform.LookAt(new Vector3(bossSetPosition.GetDestination().x, transform.position.y, bossSetPosition.GetDestination().z));
                velocity = direction * walkSpeed;
            }

            //�@�ړI�n�ɓ����������ǂ����̔���
            if (Vector3.Distance(transform.position, bossSetPosition.GetDestination()) < 0.5f)
            {
                SetState(BossState.Wait);
                animator.SetFloat("Speed", 0.0f);
            }
            else if (state == BossState.Chase)
            {
                //�@�U�����鋗����������U��
                if (Vector3.Distance(transform.position, bossSetPosition.GetDestination()) < 2.0f)
                {
                    SetState(BossState.Attack);
                }

            }
            //�@�������Ă������莞�ԑ҂�
        }
        else if (state == BossState.Wait)
        {
            elapsedTime += Time.deltaTime;
            

            //�@�҂����Ԃ��z�����玟�̖ړI�n��ݒ�
            if (elapsedTime > waitTime)
            {
                SetState(BossState.Walk);
            }

        }//�@�U����̃t���[�Y���
        else if (state == BossState.Freeze)
        {
            elapsedTime += Time.deltaTime;
           



            if (elapsedTime > freezeTime)
            {
                SetState(BossState.Counter);

            }

        }
        else if (state == BossState.Counter)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > counterTime)
            {
                counter = false;
                SetState(BossState.Freeze2);
            }
        }
        else if (state == BossState.Freeze2)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime > freez2time)
            {
                SetState(BossState.Walk);
            }
        }

            velocity.y += Physics.gravity.y * Time.deltaTime;
        enemyController.Move(velocity * Time.deltaTime);

        if(hp <= 0)
        {
            sceneTime += Time.deltaTime;
           
        }
        if (sceneTime > 4.5f)
        {
            SceneManager.LoadScene("EndScene");

        }

    }


    //�@�G�L�����N�^�[�̏�ԕύX���\�b�h
    public void SetState(BossState tempState, Transform targetObj = null)
    {
        state = tempState;

        if (tempState == BossState.Chase)
        {
            //�@�ǂ�������Ώۂ��Z�b�g
            playerTransform = targetObj;
        }
        else if (tempState == BossState.Wait)
        {
            elapsedTime = 0f;
            arrived = true;
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
            
            counter = false;

        }
        else if (tempState == BossState.Attack && dying == false)
        {
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
            animator.SetTrigger("Attack");
            SetState(BossState.Freeze);
        }
        else if (tempState == BossState.Attack && dying == true)
        {
            velocity = Vector3.zero;
            
            animator.SetFloat("Speed", 0f);
            animator.SetTrigger("Attack2");
            SetState(BossState.Freeze);

        }
        else if (tempState == BossState.Freeze)
        {
            elapsedTime = 0f;
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
           
            
        }
        else if (tempState == BossState.Counter)
        {
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
            elapsedTime = 0;
            animator.SetTrigger("Counter");
            counter = true;

        }
        else if(tempState == BossState.Freeze2)
        {
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
            elapsedTime = 0;
        }
        else if (tempState == BossState.Damage && counter == false)
        {
            
            bossSound.PlayOneShot(hitSound);
            bossSound.PlayOneShot(damageSound);
            velocity = Vector3.zero;
            animator.ResetTrigger("Attack");
            animator.ResetTrigger("Attack2");
            animator.SetTrigger("Damage");
            SetState(BossState.Walk);
            hp -= 30;

            if (hp <= 120)
            {
                dying = true;
            }
            if (hp <= 0)
            {
                bossSound.PlayOneShot(deathSound);
                enemyController.enabled = false;
                animator.SetTrigger("Death");
                Destroy(gameObject, 5f);
            }

        }
        else if(tempState == BossState.Damage && counter == true)
        {
            velocity = Vector3.zero;
            bossSound.PlayOneShot(CounterSound);
            animator.ResetTrigger("Counter");
            animator.SetTrigger("CA");
            SetState(BossState.Wait);
        }
        
    }




    //�@�G�L�����N�^�[�̏�Ԏ擾���\�b�h
    public BossState GetState() {
        return state;
    }

    
}