using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTrigger : MonoBehaviour
{
    private TrackGenerator generator;

    public void SetTrigger(TrackGenerator generator)
    {
        this.generator = generator;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            generator.GenerateNewSegments(generator.NewSegmentsCount);
        }
    }
}
