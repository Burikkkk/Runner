using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;

    private float length;
    
    public void SetComponent()
    {
        length = (start.position - end.position).magnitude;
    }
    
    public float Length
    {
        get { return length; }
    }

    public Vector3 End
    {
        get { return end.position; }
    }
}
