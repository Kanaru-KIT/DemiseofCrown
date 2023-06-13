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
    //　目的地
    private Vector3 destination;
    //　歩くスピード
    [SerializeField]
    private float walkSpeed = 1.0f;
    //　速度
    private Vector3 velocity;
    //　移動方向
    private Vector3 direction;
    //　到着フラグ
    private bool arrived;
    //　SetPositionスクリプト
    private BossSetPosition bossSetPosition;
    //　待ち時間
    [SerializeField]
    private float waitTime = 3;
    //　経過時間
    private float elapsedTime;
    private float counterTime = 2.5f;
    // 敵の状態
    private BossState state;
    //　プレイヤーTransform
    private Transform playerTransform;

    private bool dying;
    private bool counter;

    private float freezeTime = 1.5f;

    //敵のステータス
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
        //　見回りまたはキャラクターを追いかける状態
        if (state == BossState.Walk || state == BossState.Chase)
        {
            //　キャラクターを追いかける状態であればキャラクターの目的地を再設定
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

            //　目的地に到着したかどうかの判定
            if (Vector3.Distance(transform.position, bossSetPosition.GetDestination()) < 0.5f)
            {
                SetState(BossState.Wait);
                animator.SetFloat("Speed", 0.0f);
            }
            else if (state == BossState.Chase)
            {
                //　攻撃する距離だったら攻撃
                if (Vector3.Distance(transform.position, bossSetPosition.GetDestination()) < 2.0f)
                {
                    SetState(BossState.Attack);
                }

            }
            //　到着していたら一定時間待つ
        }
        else if (state == BossState.Wait)
        {
            elapsedTime += Time.deltaTime;
            

            //　待ち時間を越えたら次の目的地を設定
            if (elapsedTime > waitTime)
            {
                SetState(BossState.Walk);
            }

        }//　攻撃後のフリーズ状態
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


    //　敵キャラクターの状態変更メソッド
    public void SetState(BossState tempState, Transform targetObj = null)
    {
        state = tempState;

        if (tempState == BossState.Chase)
        {
            //　追いかける対象をセット
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




    //　敵キャラクターの状態取得メソッド
    public BossState GetState() {
        return state;
    }

    
}