using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollZoom : MonoBehaviour
{
   
    public float zoomSpeed = 5f;
    public float minZoom = 5.5f;
    public float maxZoom = 7.19f;
    public float newSize;

    public static ScrollZoom scrollZoomScript;
    private Camera mainCamera;

    private void Awake()
    {
        scrollZoomScript = this;

    }

    

    private void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
    }

    private void Update()
    {
        float zoomDelta = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        ZoomCamera(zoomDelta);


     //   AimScriptConstrictions();
    }

    private void ZoomCamera(float delta)
    {
        newSize = mainCamera.orthographicSize - delta;
        newSize = Mathf.Clamp(newSize, minZoom, maxZoom);
        mainCamera.orthographicSize = newSize;

    }


   // void AimScriptConstrictions()
   // {
       // if(newSize <= Aim.AimScript.targetSize)
     //   {
           // Aim.AimScript.enabled = false;
      //  }
      //  else
      //  {
          //  Aim.AimScript.enabled = true;
      //  }
   // }
}
