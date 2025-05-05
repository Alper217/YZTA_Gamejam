using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    private bool isOpen = false;
    private bool isIn = false;
    private bool isClicked = false;

    [SerializeField] private GameObject door;
    [SerializeField] private GameObject door2;

    void Start()
    {
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
            OpenDoor(door, door2);
            isClicked = true;
        }
    }

    public void OpenDoor(GameObject selectedDoor, GameObject selectedDoor2)
    {
        if (!isOpen)
        {
            Debug.Log("open");
            selectedDoor.GetComponent<Animator>().SetTrigger("Open");
            selectedDoor.GetComponent<Collider2D>().enabled = false;

            selectedDoor2.GetComponent<Animator>().SetTrigger("Open");
            selectedDoor2.GetComponent<Collider2D>().enabled = false;

            isOpen = true;

        }
    }
}
