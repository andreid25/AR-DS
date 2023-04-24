using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData: MonoBehaviour
{
    int plushAcquired = 0;
    bool collectionActive = false;

    public void Reset()
    {
        PlayerPrefs.SetInt("plushAcquired", plushAcquired);
        PlayerPrefs.Save();
    }
    public void PlushAcquired()
    {
        plushAcquired = 1;
        collectionActive = true;
        PlayerPrefs.SetInt("plushAcquired", plushAcquired);
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
    public bool CollectionActive()
    {
        return collectionActive;
    }
}
