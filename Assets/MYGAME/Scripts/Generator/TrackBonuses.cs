using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="track bonuses list", menuName ="MYGAME/Track bonuses list")]
public class TrackBonuses : ScriptableObject
{
    public GameObject[] bonuses;
    
    public GameObject this[int index]
    {
        get { return bonuses[index]; }
    }
}
