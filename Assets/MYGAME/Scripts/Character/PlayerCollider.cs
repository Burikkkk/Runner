using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private CharacterController controller;

    private void Start()
    {
        controller = transform.parent.GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fish"))
        {
            LevelManager.instance.AddFish();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Bonus"))
        {
            LevelManager.instance.ActivateBonus();
            Destroy(other.gameObject);
        }
    }
}
