#region 'Using' info
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
#endregion

public class LevelInfo : MonoBehaviour
{
    [SerializeField] TMP_Text DescText;

    [SerializeField] Button L1;
    [SerializeField] Button L2;
    [SerializeField] Button L3;
    [SerializeField] Button L4;
    [SerializeField] Button L5;
    [SerializeField] Button L6;

    private int levelsUnlocked;

    public GameObject level2Button; // button to access level 2
    public GameObject level3Button; // ditto, level 3
    public GameObject level4Button; // ditto, level 4
    public GameObject level5Button; // ditto, level 5
    public GameObject thatIsAll; // thanks for playing message

    private void Start()
    {
        levelsUnlocked = DataManager.Instance.LevelsComplete;

        switch (levelsUnlocked)
        {
            case 1:
                level2Button.SetActive(true);
                break;
            case 2:
                level2Button.SetActive(true);
                level3Button.SetActive(true);
                break;
            case 3:
                level2Button.SetActive(true);
                level3Button.SetActive(true);
                level4Button.SetActive(true);
                break;
            case 4:
                level2Button.SetActive(true);
                level3Button.SetActive(true);
                level4Button.SetActive(true);
                level5Button.SetActive(true);
                break;
            case 5:
                level2Button.SetActive(true);
                level3Button.SetActive(true);
                level4Button.SetActive(true);
                level5Button.SetActive(true);
                break;
        } // shows buttons depending on how many levels have been beaten
    }

    public void Back2Normal()
    { DescText.text = "Level Select"; }

    // when clicking on hephy / dionysus

    public void GVClick()
    {  DescText.text = "The Grapevine"; }

    public void FClick()
    { DescText.text = "Hephy's Forge"; }

    // when clicking on docks

    public void L1Click()
    { DescText.text = "Rising Tides"; }

    public void L2Click()
    { DescText.text = "Sculptor Sands"; }

    public void L3Click()
    { DescText.text = "Level 3 Todo"; }

    public void L4Click()
    { DescText.text = "Level 4 Todo"; }

    public void L5Click()
    { DescText.text = "L5 Todo"; }

    public void L6Click()
    { DescText.text = "Here be Hydras"; }
}
