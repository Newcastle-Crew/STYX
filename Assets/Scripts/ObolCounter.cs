#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
#endregion

public class ObolCounter : MonoBehaviour
{
    #region
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
        DataManager.Instance.Level1Obols += level2Obols += level3Obols += level4Obols += level5Obols;
        DataManager.Instance.SaveGame();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(DataManager.Instance.TotalObols);
    }
    
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
                thisLevelCoins += DataManager.Instance.TotalObols;
                DataManager.Instance.SaveGame();
                break;
            case "Level2":
                if (DataManager.Instance.Level2Obols >= 15)
                    return;
                level2Obols = thisLevelCoins;
                thisLevelCoins += totalCoins;
                DataManager.Instance.SaveGame();
                break;
        }
    }
}
