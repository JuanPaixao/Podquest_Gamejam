using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    private Animator _animator;
    public bool doorStatus;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void SetDoorStatus(bool status)
    {
        _animator.SetBool("isOpen", status);
        doorStatus = status;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (doorStatus)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                SetDoorStatus(false);
            }
        }
    }
}
