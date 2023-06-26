using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlayer : MonoBehaviour
{
    public bool isPlayerDead;


    public static DeadPlayer DeadPlayerScript;
    private void Awake()
    {
        DeadPlayerScript = this;
    }


    private void Start()
    {
        isPlayerDead = false;
    }
    void Update()
    {
        //This is what happens when the player dies
        if (isPlayerDead)
        {
        PlayerMovement.PlayerMovementScript.enabled = false;
        PlayerRotation.PlayerRotationScript.enabled = false;
        Aim.AimScript.enabled = false;
        RagdollController.RagdollControllerScript.RagdollModeOn();

        }
    }
}
