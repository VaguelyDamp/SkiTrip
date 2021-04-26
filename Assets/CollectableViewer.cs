using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableViewer : MonoBehaviour
{
    private string[] collectableNames = new string[]{"Queen", "Car", "Whale", "Red", "Clippy"};
    //private Collectable.CollectableType cType;

    public void ShowCollectables ()
    {
        int totalUnlocked = 0;
        for  (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < collectableNames.Length; j++)
            {
                bool unlocked = PlayerPrefs
                    .GetFloat(collectableNames[j] + (i + 1)) == 1 ?
                    true : false;
                if(unlocked) totalUnlocked += 1;
                GameObject.Find(collectableNames[j] + (i + 1))
                    .transform.Find("Unlocked")
                    .gameObject.SetActive(unlocked);
                GameObject.Find(collectableNames[j] + (i + 1))
                    .transform.Find("Locked")
                    .gameObject.SetActive(!unlocked);
            }
        }
        if (totalUnlocked == 15)
        {
            //update cool text
        }
    }
}
