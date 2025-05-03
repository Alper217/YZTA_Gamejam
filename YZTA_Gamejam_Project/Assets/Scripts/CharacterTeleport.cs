using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTeleport : MonoBehaviour
{
    public Transform characterPos;
    public Transform shadowPos;
    private Transform tempPos;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))//Swap
        {
            tempPos = characterPos;
            characterPos = shadowPos;
            shadowPos = tempPos;

            transform.position = characterPos.position; //Character teleport

            tempPos = shadowPos;
            shadowPos = characterPos;
            characterPos = tempPos;

            tempPos = null;
        }
    }
}
