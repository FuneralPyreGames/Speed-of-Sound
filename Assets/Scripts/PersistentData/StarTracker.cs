using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTracker : MonoBehaviour
{
    public int level1Stars, level2Stars, level3Stars, level4Stars, level5Stars, level6Stars, level7Stars, level8Stars, level9Stars, level10Stars, bonusStars;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
