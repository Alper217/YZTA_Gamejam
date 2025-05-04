using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTeleport : MonoBehaviour
{
    public Transform characterPos;
    public Transform shadowPos;
    private Transform tempPos;

    public Animator animator;

    void Awake(){
        animator = GetComponent<Animator>();
    }

    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))//Swap
        {
            animator.SetTrigger("teleport");
            tempPos = characterPos;
            characterPos = shadowPos;
            shadowPos = tempPos;

            transform.position = characterPos.position; //Character teleport

            tempPos = shadowPos;
            shadowPos = characterPos;
            characterPos = tempPos;

            tempPos = null;
        }
    }*/
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(TeleportWithAnimation());
        }
    }

    IEnumerator TeleportWithAnimation()
    {
        animator.SetTrigger("teleportOut"); // Trigger the disappearing animation

        yield return new WaitForSeconds(0.5f); // Wait for the teleport-out animation to finish (adjust to your animation length)

        // Swap positions
        tempPos = characterPos;
        characterPos = shadowPos;
        shadowPos = tempPos;

        transform.position = characterPos.position; // Teleport the character

     //   animator.SetTrigger("teleportIn"); // Trigger the appearing animation

        tempPos = shadowPos;
        shadowPos = characterPos;
        characterPos = tempPos;

        // Optional: Clean up
        tempPos = null;
    }
}
