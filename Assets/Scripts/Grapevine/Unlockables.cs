#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Unlockables : MonoBehaviour
{
    #region Cerberus
    public GameObject honeyCake; // honey cake button in the grapevine
    public GameObject cerbText; // grapevine text that says 'cerberus unlocked!'
    #endregion

    private void Start()
    {
        DataManager.Instance.LoadGame();

        if(DataManager.Instance.CerberusUnlocked == true)
        { RemoveCake(); }
    }

    public void RemoveCake()
    {
        honeyCake.SetActive(false); // hides the cake
        cerbText.SetActive(true); // shows the unlocked text

        DataManager.Instance.CerberusUnlocked = true; // hides the sprite, shows the text
        DataManager.Instance.SaveGame(); // saves the game
    }

    private void Update () 
    {

        //if(cerbButton.activeInHierarchy && Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    // todo: text for cerberus attacking
        //}

        if(Input.GetKeyDown(KeyCode.M))
        { Cursor.lockState = CursorLockMode.None; }
    }
}