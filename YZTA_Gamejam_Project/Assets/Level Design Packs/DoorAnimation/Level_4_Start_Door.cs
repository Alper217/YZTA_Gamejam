using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_4_Start_Door : MonoBehaviour
{

    void Start()
    {
        OpenDoor(gameObject);
    }

    public void OpenDoor(GameObject selectedDoor)
    {
        selectedDoor.GetComponent<Animator>().SetTrigger("Open");
        selectedDoor.GetComponent<Collider2D>().enabled = false;

    }
}
