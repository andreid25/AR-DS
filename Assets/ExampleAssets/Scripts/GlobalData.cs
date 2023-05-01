using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData: MonoBehaviour
{
    int plushAcquired = 0;
    int catEarsAcquired = 0;
    int collectionActive = 0;

    public void Reset()
    {
        PlayerPrefs.SetInt("plushAcquired", 0);
        PlayerPrefs.SetInt("collectionActive", 0);
        PlayerPrefs.SetInt("catEarsAcquired", 0);
        PlayerPrefs.Save();
    }
    public void PlushAcquired()
    {
        plushAcquired = 1;
        collectionActive = 1;
        PlayerPrefs.SetInt("plushAcquired", plushAcquired);
        PlayerPrefs.SetInt("collectionActive", collectionActive);
        PlayerPrefs.Save();
    }
    public void CatEarsAcquired()
    {
        catEarsAcquired = 1;
        collectionActive = 1;
        PlayerPrefs.SetInt("catEarsAcquired", catEarsAcquired);
        PlayerPrefs.SetInt("collectionActive", collectionActive);
        PlayerPrefs.Save();
    }
    public bool CheckPlush()
    {
        if (PlayerPrefs.GetInt("plushAcquired") == 1)
        {
            return true;
        }
        return false;
    }
    public bool CheckCatEars()
    {
        if (PlayerPrefs.GetInt("catEarsAcquired") == 1)
        {
            return true;
        }
        return false;
    }
    public bool CollectionActive()
    {
        if (PlayerPrefs.GetInt("collectionActive") == 1)
        {
            return true;
        }
        return false;
    }
}
