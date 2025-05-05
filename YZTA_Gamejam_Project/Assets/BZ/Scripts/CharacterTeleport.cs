using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTeleport : MonoBehaviour
{
    public Transform characterPos;
    public Transform shadowPos;
    private Transform tempPos;

    public Animator animator;

    [Header("Sound")]
    public AudioSource audioSource;

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

        // PlayTeleportSound();

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

       void PlayTeleportSound()
    {   
        int randomIndex = Random.Range(1, 3); // Teleport1, Teleport2
        SoundType teleportSound = (SoundType)System.Enum.Parse(typeof(SoundType), "Teleport" + randomIndex);
        SFXManager.PlaySound(teleportSound);
    }
}
