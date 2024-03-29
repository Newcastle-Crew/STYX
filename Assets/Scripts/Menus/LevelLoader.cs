# region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#endregion

public class LevelLoader : MonoBehaviour
{
    public void Level1() // Loads level 1
    {
        DataManager.Instance.SaveGame();
        SceneManager.LoadScene("GameScene");
    } 

    public void Level2() // Loads level 2
    {
        DataManager.Instance.SaveGame();
        SceneManager.LoadScene("Level2");
    }

    public void Level3() // Loads level 2
    {
        DataManager.Instance.SaveGame();
        SceneManager.LoadScene("Level3");
    }

    public void Level4() // Loads level 2
    {
        DataManager.Instance.SaveGame();
        SceneManager.LoadScene("Level4");
    }

    public void Level5() // Loads level 2
    {
        DataManager.Instance.SaveGame();
        SceneManager.LoadScene("Level5");
    }

    public void LevelSelect() // Loads the level select scene
    {
        DataManager.Instance.SaveGame();
        SceneManager.LoadScene("LevelSelect");
    }
}
