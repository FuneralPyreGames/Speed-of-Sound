using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StarTracker : MonoBehaviour
{
    public int level1Stars, level2Stars, level3Stars, level4Stars, level5Stars, level6Stars, level7Stars, level8Stars, level9Stars, level10Stars, bonusStars;
    public bool bonusStar1Got = false;
    public bool bonusStar2Got = false;
    public bool bonusStar3Got = false;
    public bool bonusStar4Got = false;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public int GetTotalStars()
    {
        return level1Stars + level2Stars + level3Stars + level4Stars + level5Stars + level6Stars + level7Stars + level8Stars + level9Stars + level10Stars + bonusStars;
    }
    public void SaveStars()
    {
        SaveSystem.SaveStars(this);
    }
    public void LoadStars()
    {
        StarSaver data = SaveSystem.LoadStars();
        if(data == null)
        {
            return;
        }
        level1Stars = data.level1Stars;
        level2Stars = data.level2Stars;
        level3Stars = data.level3Stars;
        level4Stars = data.level4Stars;
        level5Stars = data.level5Stars;
        level6Stars = data.level6Stars;
        level7Stars = data.level7Stars;
        level8Stars = data.level8Stars;
        level9Stars = data.level9Stars;
        level10Stars = data.level10Stars;
        bonusStars = data.bonusStars;
        bonusStar1Got = data.bonusStar1Got;
        bonusStar2Got = data.bonusStar2Got;
        bonusStar3Got = data.bonusStar3Got;
        bonusStar4Got = data.bonusStar4Got;
    }
    public void ResetStars()
    {
        level1Stars = 0;
        level2Stars = 0;
        level3Stars = 0;
        level4Stars = 0;
        level5Stars = 0;
        level6Stars = 0;
        level7Stars = 0;
        level8Stars = 0;
        level9Stars = 0;
        level10Stars = 0;
        bonusStars = 0;
        bonusStar1Got = false;
        bonusStar2Got = false;
        bonusStar3Got = false;
        bonusStar4Got = false;
        SaveSystem.SaveStars(this);
    }
}