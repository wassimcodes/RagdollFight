using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsControl : MonoBehaviour
{
    //guns in the scene
    public GameObject[] Pistols;
    public GameObject[] Aks;

    //guns in the inventory
    public GameObject lootedPistol;
    public GameObject lootedAk;

    public float delay; //delay to to turn on the pistol in hand.
    private Coroutine pistolVisibilityCoroutine;
    private Coroutine AkVisibilityCoroutine; //used to control the delay above.

    public KeyCode GunLootKey;


    public bool playerHasPistol = false;
    public bool playerHasAk = false;

    public static GunsControl gunsControlScript; //this script reference.
    private void Awake()
    {
        gunsControlScript = this;
    }

    void Start()
    {//since the player doesn't spawn with a gun.
        Aim.AimScript.enabled = false; 
        //player won't have the gun infront of him.
        lootedPistol.SetActive(false); 
        lootedAk.SetActive(false);
        //turning off the box collider so it won't make the player go backward because of the collision.
        lootedPistol.GetComponent<BoxCollider>().enabled = false;
        lootedAk.GetComponent<BoxCollider>().enabled = false;

    }
    private void Update()
    {

        //pistol
        if (Aim.AimScript.isRotatingTowardsMouse && playerHasPistol) //check if the player is aiming and has a pistol
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
        //Ak
        if (Aim.AimScript.isRotatingTowardsMouse && playerHasAk) //check if the player is aiming and has a pistol
        {
            if (AkVisibilityCoroutine == null)
            {
                AkVisibilityCoroutine = StartCoroutine(ActivateAKVisibilityAfterDelay()); //display the weapon after the delay.
            }
        }
        else
        {
            lootedAk.SetActive(false);
            if (AkVisibilityCoroutine != null)
            {
                StopCoroutine(AkVisibilityCoroutine);
                AkVisibilityCoroutine = null;
            }
        }


    }

    private IEnumerator ActivatePistolVisibilityAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        lootedPistol.SetActive(true);
    }

    private IEnumerator ActivateAKVisibilityAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        lootedAk.SetActive(true);
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
            if (col.gameObject == Aks[0])    //check if the player collides with gun 0.
            {
                playerHasAk = true; //player now has pistol 0
                if (playerHasAk)
                {
                    Destroy (Aks[0]);
                    Aim.AimScript.enabled = true;
                }
            }



        }
         
        
    }

}
