using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="track themes list", menuName ="MYGAME/Track themes list")]
public class TrackThemes : ScriptableObject
{
    public TrackTheme[] themes;
    
    
    public TrackTheme this[int index]
    {
        get { return themes[index]; }
    }
}
