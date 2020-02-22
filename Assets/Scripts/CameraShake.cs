using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Camera mainCam;
    float shakeAmount = 0;

    public void Shake(float amount, float length)
    {
        this.shakeAmount = amount;
        InvokeRepeating("DoShake", 0, 0.1f);
        Invoke("StopShake", length);
    }

    private void DoShake()
    {
        if (shakeAmount > 0)
        {
            Vector3 camPos = mainCam.transform.position;
            float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
            float offsetY = Random.value * shakeAmount * 2 - shakeAmount;

            camPos.x += offsetX;
            camPos.y += offsetY;

            mainCam.transform.position = camPos;
        }
    }

    private void StopShake()
    {
        CancelInvoke("DoShake");
        mainCam.transform.localPosition = new Vector3(0, 0, -10);
    }
}