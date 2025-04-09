using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObstacle : MonoBehaviour
{
    [SerializeField] private int maxOnLane;

    public int MaxOnLane
    {
        get { return maxOnLane; }
    }
}
