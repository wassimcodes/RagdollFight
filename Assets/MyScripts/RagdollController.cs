using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    public CapsuleCollider mainCollider;
    public GameObject characterRig;
    public Animator characterAnimator;
    public Rigidbody characterRigidbody;
    public GameObject Knocker;

    Collider[] ragdollColliders;
    Rigidbody[] limbsRigidbodies;

    public static RagdollController RagdollControllerScript;
    private void Awake()
    {
        RagdollControllerScript = this;
    }


    void Start()
    {
        GetRagdollBits();
        RagdollModeOff();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {

        //this  is what happens when the player collides with a knocker
        if (collision.gameObject.tag == "Knocker")
        {
            PlayerMovement.PlayerMovementScript.enabled = false;
            PlayerRotation.PlayerRotationScript.enabled = false;
            Aim.AimScript.enabled = false;
            RagdollModeOn();
        }
    }


    void GetRagdollBits()
    {
        ragdollColliders = characterRig.GetComponentsInChildren<Collider>();
        limbsRigidbodies = characterAnimator.GetComponentsInChildren<Rigidbody>();

    }

    public void RagdollModeOn()
    {

        characterAnimator.enabled = false;

        foreach (Collider col in ragdollColliders)
        {
            col.enabled = true;
        }

        foreach (Rigidbody rigid in limbsRigidbodies)
        {
            rigid.isKinematic = false;

            float forceVal = BoxFly.BoxFlyScript.ForceValue;
            rigid.AddForce( new Vector3(forceVal,0,0), ForceMode.Impulse);
        }
        
        mainCollider.enabled = false;
        characterRigidbody.isKinematic = true;
    }
    void RagdollModeOff()
    {
        foreach (Collider col in ragdollColliders)
        {
            col.enabled = false;
        }

        foreach (Rigidbody rigid in limbsRigidbodies)
        {
            rigid.isKinematic = true;
        }
        characterAnimator.enabled = true;
        mainCollider.enabled = true;
        characterRigidbody.isKinematic = false;
    }

}
