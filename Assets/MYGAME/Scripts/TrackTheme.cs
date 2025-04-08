using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="track theme", menuName ="MYGAME/Track theme")]
public class TrackTheme : ScriptableObject
{
    public GameObject[] segments;
    
    public GameObject this[int index]
    {
        get { return segments[index]; }
    }
}
