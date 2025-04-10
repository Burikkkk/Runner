using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrackGenerator : MonoBehaviour
{
    [SerializeField] private int newSegmentsCount;
    [SerializeField] private float laneOffset = 1.0f;
    public Transform start;
    [SerializeField] private TrackThemes themesList;
    [SerializeField] private TrackObstacles obstaclesList;
    [SerializeField] private TrackBonuses bonusesList;
    [SerializeField] private GameObject fishPrefab;
    [SerializeField] private float fishOffset;
    [SerializeField] private GameObject generatorTriggerPrefab;

    [HideInInspector]
    public float[] lanesX;
    private int themesCount;
    private int obstaclesCount;
    private int bonusesCount;
    private TrackTheme currentTheme;
    private Vector3 lastSegmentPosition;

    private void Start()
    {
        themesCount = themesList.themes.Length;
        obstaclesCount = obstaclesList.obstacles.Length;
        
        currentTheme = themesList[Random.Range(0, themesCount)];

        lastSegmentPosition = start.transform.position;
        lanesX = new float[3] { lastSegmentPosition.x - laneOffset, lastSegmentPosition.x, lastSegmentPosition.x + laneOffset };
        
        GenerateNewSegments(newSegmentsCount);
        GenerateNewSegments(newSegmentsCount);
        GenerateNewSegments(newSegmentsCount);
    }

    public int NewSegmentsCount
    {
        get { return newSegmentsCount; }
    }
    
    public void GenerateNewSegments(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var newSegment = GenerateNewSegment();
            GenerateSegmentObstacles(newSegment);
            GenerateSegmentFish(newSegment);
            if (Random.Range(0.0f, 1.0f) < 0.2f)    // 10% шанс на бонус
            {
                GenerateSegmentBonus(newSegment);
            }
            if (i == count - 1) // последний из новых сегментов
            {
                GenerateTrigger(newSegment);
            }
        }
        currentTheme = themesList[Random.Range(0, themesCount)];
    }

    private Segment GenerateNewSegment()
    {
        var segmentsCount = currentTheme.segments.Length;
        var segmentPrefab = currentTheme[Random.Range(0, segmentsCount)];

        var newSegment = Instantiate(segmentPrefab, lastSegmentPosition, segmentPrefab.transform.rotation, transform);

        var segmentComponent = newSegment.GetComponent<Segment>();
        segmentComponent.SetComponent();
            
        lastSegmentPosition.z += segmentComponent.Length;
        return segmentComponent;
    }

    private void GenerateTrigger(Segment lastSegment)
    {
        var triggerPosition = lastSegment.End;
        var generatorTrigger = Instantiate(generatorTriggerPrefab, triggerPosition, Quaternion.identity, transform);
        generatorTrigger.GetComponent<GeneratorTrigger>().SetTrigger(this);
    }

    private void GenerateSegmentObstacles(Segment segment)
    {
        var position = segment.End;
        var obstaclePrefab = obstaclesList[Random.Range(0, obstaclesCount)];
        var obstacleComponent = obstaclePrefab.GetComponent<TrackObstacle>();
        int obstacleCount = Random.Range(1, obstacleComponent.MaxOnLane + 1);

        float[] lanes;
        if (obstacleComponent.MaxOnLane == 1)   // для тех, что на всю ширину дороги
        {
            lanes = new float[] { lanesX[1] };  // стоят в центре
        }
        else
        {
            lanes = GetRandomLanes(obstacleCount);  // другие в рандомных
        }
        
        for (int i = 0; i < obstacleCount; i++)
        {
            Instantiate(obstaclePrefab, new Vector3(lanes[i], 0.0f, position.z), Quaternion.identity, transform);
        }
    }

    private float[] GetRandomLanes(int count)
    {
        float[] lanes = new float[count];   // тут массив стандартно заполняется нулями

        for (int i = 0; i < count; i++)
        {
            lanes[i] = float.NaN;   // меняем нули на это, потому что иначе цикл ниже не сможет добавить в массив значение 0
        }
        
        for (int i = 0; i < count; i++)
        {
            float randomLane;
            do
            {
                randomLane = lanesX[Random.Range(0, lanesX.Length)];
            } while (lanes.Contains(randomLane));

            lanes[i] = randomLane;
        }

        return lanes;
    }

    private void GenerateSegmentFish(Segment segment)
    {
        var positionZ = segment.End.z + segment.Length * 0.3f;
        var maxFishCount = (int)(segment.Length * 0.6f / fishOffset);
        int fishCount = Random.Range(2, maxFishCount);
        //var positionZ = segment.End.z + (segment.Length - fishCount * fishOffset) * 0.5f;
        int lanesCount = Random.Range(1, 4);

        float[] lanes = GetRandomLanes(lanesCount);  // другие в рандомных
        
        for (int i = 0; i < fishCount; i++)
        {
            for (int j = 0; j < lanesCount; j++)
            {
                Instantiate(fishPrefab, new Vector3(lanes[j], 0.0f, positionZ), Quaternion.identity, transform);
            }
            positionZ += fishOffset;
        }
    }
    
    private void GenerateSegmentBonus(Segment segment)
    {
        var positionZ = segment.End.z + segment.Length * 0.5f;
        var bonusPrefab = bonusesList[Random.Range(0, bonusesCount)];
        var randomLane = GetRandomLanes(1)[0];
        Instantiate(bonusPrefab, new Vector3(randomLane, 0.3f, positionZ), Quaternion.identity, transform);
    }
}
