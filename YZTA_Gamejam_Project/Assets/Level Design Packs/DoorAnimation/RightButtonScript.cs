using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightButtonScript : MonoBehaviour
{
    public static bool isOpen = false;
    private bool isIn = false;
    private bool isClicked = false;

    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;

    void Start()
    {
        OpenDoor(false);
        // Ensure the animator is assigned
        /*if (doorAnimator == null)
        {
            doorAnimator = GetComponent<Animator>();
        }*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isIn = true;
            Debug.Log("Button");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isIn = false;
        }
    }
    void Update()
    {
        if (isIn && Input.GetKeyDown(KeyCode.E) && !isClicked)
        {
            CloseDoor(false);
            OpenDoor(true);

            isClicked = true;
        }
    }
    //true = sol 
    //false = sað
    public void OpenDoor(bool selection)
    {
        if (!isOpen)
        {
            if (selection)
            {
                Debug.Log("denemeememem");
                leftDoor.GetComponent<Animator>().SetTrigger("Open");
                leftDoor.GetComponent<Collider2D>().enabled = false;
            }
                
            else
            {
                rightDoor.GetComponent<Animator>().SetTrigger("Open");
                rightDoor.GetComponent<Collider2D>().enabled = false;
            }
                
            isOpen = true;
        }
    }

    public void CloseDoor(bool selection)
    {
        if (isOpen)
        {
            if (selection)
            {
                Debug.Log("kapama deneme");
                leftDoor.GetComponent<Animator>().SetTrigger("Close");
                leftDoor.GetComponent<Collider2D>().enabled = true;
            }

            else
            {
                rightDoor.GetComponent<Animator>().SetTrigger("Close");
                rightDoor.GetComponent<Collider2D>().enabled = true;
            }
            isOpen = false;
        }
    }


}
