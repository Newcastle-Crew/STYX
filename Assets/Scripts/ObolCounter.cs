#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
#endregion

public class ObolCounter : MonoBehaviour
{
    #region coins from levels
    public static int level1Obols;
    public static int level2Obols;
    public static int level3Obols;
    public static int level4Obols;
    public static int level5Obols;
    public static int totalCoins; // use this for upgrades n stuff
    public int thisLevelCoins;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        QuickUpdate();
        thisLevelCoins = 15;
        DataManager.Instance.LoadGame();
    }

    private void QuickUpdate()
    {
        DataManager.Instance.TotalObols = DataManager.Instance.Level1Obols += DataManager.Instance.Level2Obols 
                                            += DataManager.Instance.Level3Obols += DataManager.Instance.Level4Obols += DataManager.Instance.Level5Obols;
        DataManager.Instance.SaveGame();
    }

    //void Update()
    //{
    //    Debug.Log(DataManager.Instance.TotalObols);
    //}
    
    public void UpdateTotal()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "LevelSelect":
                    break;
            case "GameScene":
                if (DataManager.Instance.Level1Obols >= 15)
                    return;
                level1Obols = thisLevelCoins;
                thisLevelCoins += totalCoins;
                DataManager.Instance.Level1Obols = thisLevelCoins;
                DataManager.Instance.SaveGame();
                break;
            case "Level2":
                if (DataManager.Instance.Level2Obols >= 15)
                    return;
                level2Obols = thisLevelCoins;
                thisLevelCoins += totalCoins;
                DataManager.Instance.SaveGame();
                break;
            case "Level3":
                if (DataManager.Instance.Level3Obols >= 15)
                    return;
                level3Obols = thisLevelCoins;
                thisLevelCoins += totalCoins;
                DataManager.Instance.SaveGame();
                break;
            case "Level4":
                if (DataManager.Instance.Level4Obols >= 15)
                    return;
                level4Obols = thisLevelCoins;
                thisLevelCoins += totalCoins;
                DataManager.Instance.SaveGame();
                break;
            case "Level5":
                if (DataManager.Instance.Level5Obols >= 15)
                    return;
                level5Obols = thisLevelCoins;
                thisLevelCoins += totalCoins;
                DataManager.Instance.SaveGame();
                break;
        }
    }
}
