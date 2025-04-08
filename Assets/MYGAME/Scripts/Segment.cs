using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;

    private float length;

    private void Awake()
    {
        length = (start.position - end.position).magnitude;
    }

    public float Length
    {
        get { return length; }
    }
}
