using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrackGenerator : MonoBehaviour
{
    [SerializeField] public Transform start;
    [SerializeField] private TrackThemes themesList;

    private int themesCount;
    private TrackTheme currentTheme;

    private Vector3 lastSegmentPosition;

    private void Start()
    {
        themesCount = themesList.themes.Length;
        currentTheme = themesList[Random.Range(0, themesCount)];

        lastSegmentPosition = start.transform.position;
        GenerateSegment(5);
    }


    public void GenerateSegment(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var segmentsCount = currentTheme.segments.Length;
            var newSegment = currentTheme[Random.Range(0, segmentsCount)];
            var newSegmentPosition = lastSegmentPosition;
            newSegmentPosition.z -= newSegment.GetComponent<Segment>().Length;
            Debug.Log(newSegment.GetComponent<Segment>().Length);
            lastSegmentPosition = newSegmentPosition;
            Instantiate(newSegment, newSegmentPosition, quaternion.identity);
        }
    }
}
