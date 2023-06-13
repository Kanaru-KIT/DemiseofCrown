using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioSource start;

   


   
    public void OnClick()
    {
        start.PlayOneShot(start.clip);
        Invoke("scene", 2);  //�P�b���scene���Ăяo��
    }

    void scene()
    {

        SceneManager.LoadScene("TutorialScene"); //sceneName�����[�h����

    }
}
