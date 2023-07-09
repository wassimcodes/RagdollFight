using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsControl : MonoBehaviour
{
    //guns in the scene
    
    public GameObject Pistol;

    //guns in the inventory
    public GameObject lootedPistol;

    public float delay;
    private Coroutine pistolVisibilityCoroutine;
    public static GunsControl gunsControlScript;
    public KeyCode GunLootKey;

    private void Awake()
    {
        gunsControlScript = this;
    }
    void Start()
    {
        Aim.AimScript.enabled = false;
        lootedPistol.SetActive(false);
        lootedPistol.GetComponent<BoxCollider>().enabled = false;
    }
    private void Update()
    {
        if (Aim.AimScript.isRotatingTowardsMouse)
        {
            if (pistolVisibilityCoroutine == null)
            {
                pistolVisibilityCoroutine = StartCoroutine(ActivatePistolVisibilityAfterDelay());
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

    //looting 

    private void OnTriggerStay(Collider col)
    {
 
         if (Input.GetKey(GunLootKey))
         {
            if (col.gameObject == GameObject.Find("Pistol"))
            {
                Destroy(GameObject.Find("Pistol"));
                Aim.AimScript.enabled = true;
            }
            if (col.gameObject == GameObject.Find("Pistol2"))
            {
                Destroy(GameObject.Find("Pistol2"));
                Aim.AimScript.enabled = true;
            }

        }  
        
    }

}
