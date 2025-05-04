using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    public Animator doorAnimator; 
    private bool isOpen = false; 

    void Start()
    {
        // Ensure the animator is assigned
        if (doorAnimator == null)
        {
            doorAnimator = GetComponent<Animator>();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) // Press 'O' to open the door
        {
            OpenDoor();
        }
        if (Input.GetKeyDown(KeyCode.C)) // Press 'C' to close the door
        {
            CloseDoor();
        }
    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            doorAnimator.SetTrigger("Open");
            isOpen = true;
        }
    }

    public void CloseDoor()
    {
        if (isOpen)
        {
            doorAnimator.SetTrigger("Close");
            isOpen = false;
        }
    }


}
