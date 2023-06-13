using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour
{

    void OnTriggerStay(Collider col)
    {
        {
            if (col.tag == "Player")
            {
                transform.Rotate(new Vector3(0, 200, 0));
                Debug.Log("open");
            }
        }
    }
}