using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;

    private Vector3 playerStartPosition;
    private Vector3 cameraStartPosition;

    private void Start()
    {
        playerStartPosition = player.position;
        cameraStartPosition = transform.position;
    }

    private void Update()
    {
        var playerMove = player.position - playerStartPosition;
        var newPosition = cameraStartPosition;
        newPosition.z += playerMove.z;
        transform.position = newPosition;
    }
}
