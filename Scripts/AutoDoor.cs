using UnityEngine;
using System.Collections;

public class AutoDoor : MonoBehaviour
{
    //　ドアのアニメーター
    private Animator animator;
    GameObject[] tagObjects;

    [SerializeField] private AudioSource fire;
    [SerializeField] private AudioSource open;

    bool call;
    void Start()
    {
        // 親のAnimatorを取得
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
    /// 自動ドア検知エリアに入った時
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        GameObject bgm = GameObject.Find("BGM");

        if (tagObjects.Length == 0)
        {
            // アニメーションパラメータをtrueにする。(ドアが開く)
            animator.SetBool("Open", true);
            
            Destroy(bgm);
            fire.PlayOneShot(fire.clip);

        }
    }

    /// <summary>
    /// 自動ドア検知エリアを出た時
    /// </summary>
    /// <param name="other"></param>
	private void OnTriggerExit(Collider other)
    {
        // アニメーションパラメータをfalseにする。(ドアが閉まる)
        animator.SetBool("Open", false);
    }
}

