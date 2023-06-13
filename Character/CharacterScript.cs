using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterScript : MonoBehaviour
{
    public enum MyState
    {
        Normal,
        Damage,
        Attack
    };

    private MyState state;

    private CharacterController characterController;
	private Vector3 velocity;
	[SerializeField]
	private float walkSpeed;
    private float roatedSpeed;
	private Animator animator;

    public int maxHp = 100;
    int hp;
    public Slider slider;

    public FloatingJoystick joystick;



    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioSource hitSound;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] public AudioSource attackSound;



    // Use this for initialization
    void Start()
	{
		characterController = GetComponent<CharacterController>();

		animator = GetComponent<Animator>();

        AudioSource[] audioSources = GetComponents<AudioSource>();
        hitSound = audioSources[0];
        deathSound = audioSources[1];
        attackSound = audioSources[2];

        slider.value = 100;
        hp = maxHp;
        
       
    }

    // Update is called once per frame
    void Update()
    {
        float x = joystick.Horizontal;
        float y = joystick.Vertical;
       

        if (state == MyState.Normal)
        {
            //　地面に接地してる時は速度を初期化
            if (characterController.isGrounded)
            {
                velocity = Vector3.zero;

                var input = new Vector3(x, 0f, y);

                if (input.magnitude > 0f)
                {
                    animator.SetFloat("Speed", input.magnitude);
                    transform.LookAt(transform.position + input);
                    velocity += input.normalized * walkSpeed;
                }
                else
                {
                    animator.SetFloat("Speed", 0f);
                }
            }
        }
        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    public void SetState(MyState tempState)
    {
        if (tempState == MyState.Normal)
        {
            state = MyState.Normal;
        }
        else if (tempState == MyState.Attack)
        {
            velocity = Vector3.zero;
            state = MyState.Attack;
            animator.SetTrigger("Attack");
           

        }
    }
    public void TakeDamage(Transform enemyTransform)
    {
        state = MyState.Damage;
        velocity = Vector3.zero;
        animator.SetTrigger("Damage");
        hitSound.PlayOneShot(hitSound.clip);
        hp -= 10;
        slider.value = (float)hp / (float)maxHp;
        if (hp <= 0)
        {
            hp = 0;
            characterController.enabled = false;
            animator.SetTrigger("Death");
            deathSound.PlayOneShot(deathSound.clip);
            Invoke("scene", 5);


        }
        //	characterController.Move (enemyTransform.forward * 0.5f);
    }
    void scene()
    {
        SceneManager.LoadScene("StartScene");
    }


}