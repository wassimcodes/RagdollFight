using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsControl : MonoBehaviour
{
    //guns in the scene
    public GameObject[] Pistols;

    //guns in the inventory
    public GameObject lootedPistol;

    public float delay; //delay to to turn on the pistol in hand.
    private Coroutine pistolVisibilityCoroutine; //used to control the delay above.

    public KeyCode GunLootKey;
    public bool playerHasPistol = false; //bool to check if the player has a pistol.

    public static GunsControl gunsControlScript; //this script reference.
    private void Awake()
    {
        gunsControlScript = this;
    }

    void Start()
    {
        Aim.AimScript.enabled = false; //since the player doesn't spawn with a gun.
        lootedPistol.SetActive(false); //player won't have the gun infront of him.
        lootedPistol.GetComponent<BoxCollider>().enabled = false; //turning off the box collider so it won't make the player go backward because of the collision.
    }
    private void Update()
    {
        if (Aim.AimScript.isRotatingTowardsMouse) //check if the player is aiming
        {
            if (pistolVisibilityCoroutine == null)
            {
                pistolVisibilityCoroutine = StartCoroutine(ActivatePistolVisibilityAfterDelay()); //display the weapon after the delay.
            }
        }
        else
        {
            lootedPistol.SetActive(false);
            if (pistolVisibilityCoroutine != null)
            {
                StopCoroutine(pistolVisibilityCoroutine);
                pistolVisibilityCoroutine = null;
            }
        }
    }

    private IEnumerator ActivatePistolVisibilityAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        lootedPistol.SetActive(true);
    }


    //looting system
    private void OnTriggerStay(Collider col)
    {
 
         if (Input.GetKey(GunLootKey))
         {
            if (col.gameObject == Pistols[0])    //check if the player collides with gun 0.
            {
                playerHasPistol = true; //player now has pistol 0
                if (playerHasPistol)
                {
                    Destroy(Pistols[0]);
                    Aim.AimScript.enabled = true;
                }

                
            }
            if (col.gameObject == (Pistols[1])) //check if the player collides with gun 1.
            {
                playerHasPistol = true;
               
                if (playerHasPistol) 
                {
                    Destroy(Pistols[1]);
                    Aim.AimScript.enabled = true; //player now has pistol 1
                }        
            }
            

        }
         
        
    }

}
