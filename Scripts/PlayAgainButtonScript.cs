using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioSource End;

    public void OnClick()
    {
        End.PlayOneShot(End.clip);
        Invoke("scene", 2);  //ÇPïbå„Ç…sceneÇåƒÇ—èoÇ∑
    }
    void scene()
    {

        SceneManager.LoadScene("StartScene");

    }
}
