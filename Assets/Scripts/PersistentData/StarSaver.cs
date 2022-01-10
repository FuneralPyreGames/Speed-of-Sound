using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class StarSaver
{
    public int level1Stars;
    public int level2Stars;
    public int level3Stars;
    public int level4Stars;
    public int level5Stars;
    public int level6Stars;
    public int level7Stars;
    public int level8Stars;
    public int level9Stars;
    public int level10Stars;
    public StarSaver(StarTracker starTracker)
    {
        level1Stars = starTracker.level1Stars;
        level2Stars = starTracker.level2Stars;
        level3Stars = starTracker.level3Stars;
        level4Stars = starTracker.level4Stars;
        level5Stars = starTracker.level5Stars;
        level6Stars = starTracker.level6Stars;
        level7Stars = starTracker.level7Stars;
        level8Stars = starTracker.level8Stars;
        level9Stars = starTracker.level9Stars;
        level10Stars = starTracker.level10Stars;
    }
}