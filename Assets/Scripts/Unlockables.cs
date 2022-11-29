#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Unlockables : MonoBehaviour
{
    public GameObject honeyCake; // the honey cake button
    public GameObject cerbText; // the text that says 'cerberus unlocked!'

    private void Start() 
    {
        DataManager.Instance.LoadGame();

        if(DataManager.Instance.CerberusUnlocked == true)
        {
            RemoveCake();
        }
    }

    public void RemoveCake()
    {
        honeyCake.SetActive(false);
        cerbText.SetActive(true);
        DataManager.Instance.CerberusUnlocked = true; // hides the sprite, shows the text
        DataManager.Instance.SaveGame();
    }
}