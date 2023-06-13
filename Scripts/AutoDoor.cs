using UnityEngine;
using System.Collections;

public class AutoDoor : MonoBehaviour
{
    //�@�h�A�̃A�j���[�^�[
    private Animator animator;
    GameObject[] tagObjects;

    [SerializeField] private AudioSource fire;
    [SerializeField] private AudioSource open;

    bool call;
    void Start()
    {
        // �e��Animator���擾
        animator = transform.parent.GetComponent<Animator>();
        call = false;
    }
    void Update()
    {
        Check("Enemy");

        if (call == false)
        {
            Open();
            
            
        }
        GameObject bgm = GameObject.Find("BGM");
    }

    void Check(string Enemy)
    {
        tagObjects = GameObject.FindGameObjectsWithTag(Enemy);

    }

    void Open()
    {
        if (tagObjects.Length == 0)
        {
            open.PlayOneShot(open.clip);
            call = true;
        }
    }

  

    /// <summary>
    /// �����h�A���m�G���A�ɓ�������
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        GameObject bgm = GameObject.Find("BGM");

        if (tagObjects.Length == 0)
        {
            // �A�j���[�V�����p�����[�^��true�ɂ���B(�h�A���J��)
            animator.SetBool("Open", true);
            
            Destroy(bgm);
            fire.PlayOneShot(fire.clip);

        }
    }

    /// <summary>
    /// �����h�A���m�G���A���o����
    /// </summary>
    /// <param name="other"></param>
	private void OnTriggerExit(Collider other)
    {
        // �A�j���[�V�����p�����[�^��false�ɂ���B(�h�A���܂�)
        animator.SetBool("Open", false);
    }
}

