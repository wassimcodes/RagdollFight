using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarManager : MonoBehaviour
{
    [Header("key for each selected item")]
    public KeyCode[] keyItemNum1;
    public KeyCode[] keyItemNum2;
    public KeyCode[] keyItemNum3;
    public KeyCode[] keyItemNum4;

    [Header("selected Item Highlight")]
    [SerializeField] GameObject[] selectedSlotHighlight;

    [Header("slot buttons")]
    [SerializeField] GameObject[] slotButtons;

    [Header("animators")]
    [SerializeField] private Animator slotNumAnimator;

    [Header("Ammo Management")]
    [SerializeField] private GameObject PistolIocn;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SelectedSlotsController();
    }

    void SelectedSlotsController()
    {
        if (Input.GetKey(keyItemNum1[0]) || Input.GetKey(keyItemNum1[1]))
        {
            SelectedSlotsVisibility(selectedSlotHighlight[0]);
            slotNumAnimator.SetBool("num1Pressed", true);
            StartCoroutine(AnimationToggleFalse());
        }
        else if (Input.GetKey(keyItemNum2[0]) || Input.GetKey(keyItemNum2[1]))
        {
            SelectedSlotsVisibility(selectedSlotHighlight[1]);
            slotNumAnimator.SetBool("num2Pressed", true);
            StartCoroutine(AnimationToggleFalse());
        }
        else if (Input.GetKey(keyItemNum3[0]) || Input.GetKey(keyItemNum3[1]))
        {
            SelectedSlotsVisibility(selectedSlotHighlight[2]);
            slotNumAnimator.SetBool("num3Pressed", true);
            StartCoroutine(AnimationToggleFalse());
        }
        else if (Input.GetKey(keyItemNum4[0]) || Input.GetKey(keyItemNum4[1]))
        {
            SelectedSlotsVisibility(selectedSlotHighlight[3]);
            slotNumAnimator.SetBool("num4Pressed", true);
            StartCoroutine(AnimationToggleFalse());
        }
    }

    private IEnumerator AnimationToggleFalse()
    {
        yield return new WaitForSeconds(.25f);
        slotNumAnimator.SetBool("num1Pressed", false);
        slotNumAnimator.SetBool("num2Pressed", false);
        slotNumAnimator.SetBool("num3Pressed", false);
        slotNumAnimator.SetBool("num4Pressed", false);
    }



    void SelectedSlotsVisibility(GameObject selectedHighlight)
    {
        selectedSlotHighlight[0].SetActive(selectedHighlight == selectedSlotHighlight[0]);
        selectedSlotHighlight[1].SetActive(selectedHighlight == selectedSlotHighlight[1]);
        selectedSlotHighlight[2].SetActive(selectedHighlight == selectedSlotHighlight[2]);
        selectedSlotHighlight[3].SetActive(selectedHighlight == selectedSlotHighlight[3]);
    }

    //to select slots with mouse
    public void slot1()
    {
        SelectedSlotsVisibility(selectedSlotHighlight[0]);
    }
    public void slot2()
    {
        SelectedSlotsVisibility(selectedSlotHighlight[1]);
    }
    public void slot3()
    {
        SelectedSlotsVisibility(selectedSlotHighlight[2]);
    }
    public void slot4()
    {
        SelectedSlotsVisibility(selectedSlotHighlight[3]);
    }
}
