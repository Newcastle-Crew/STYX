# region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#endregion

public class LevelLoader : MonoBehaviour
{
    public void Level1()
    { SceneManager.LoadScene("GameScene"); } // Loads level 1

    public void Level2()
    { SceneManager.LoadScene("Level2"); } // Loads level 1
}
