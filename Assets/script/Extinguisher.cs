using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Extinguisher : MonoBehaviour
{
    
    public Animator animFire;
    public Transform createPoint;
    public GameObject particlePrefab; // 프리팹으로 변경

    void Start()
    {
        XRGrabInteractable XGI = GetComponent<XRGrabInteractable>();
        
        XGI.activated.AddListener(FireEx);
        XGI.selectEntered.AddListener(GrapHose);
        XGI.selectExited.AddListener(ReleaseHose);
    }

    public void FireEx(ActivateEventArgs arg)
    {
        animFire.SetBool("Push_Btn", true);
        //파티클 생성
        Instantiate(particlePrefab, createPoint.position, createPoint.rotation, createPoint);

        // 2초 뒤에 Push_Btn 상태를 반전시킴
        StartCoroutine(ReversePushBtnAfterDelay());
    }

    IEnumerator ReversePushBtnAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        animFire.SetBool("Push_Btn_Reverse", true);
    }

    public void GrapHose(SelectEnterEventArgs args)
    {
        animFire.SetBool("Grap_Hose", true);
    }

    public void ReleaseHose(SelectExitEventArgs args)
    {
        animFire.SetBool("Grap_Hose", false);
    }
}
