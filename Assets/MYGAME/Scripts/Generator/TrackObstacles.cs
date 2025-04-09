using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="track obstacles list", menuName ="MYGAME/Track obstacles list")]
public class TrackObstacles : ScriptableObject
{
    public GameObject[] obstacles;
    
    
    public GameObject this[int index]
    {
        get { return obstacles[index]; }
    }
}
