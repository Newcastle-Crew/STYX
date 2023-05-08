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
    public GameObject level6Button; // hydra

    private void Start()
    {
        levelsUnlocked = DataManager.Instance.LevelsComplete;

        switch (levelsUnlocked)
        {
            case 1:
                level2Button.SetActive(true); // if level 1 beaten, show level 2
                break;
            case 2: // if level 2 beaten, show level 3 and 4
                level2Button.SetActive(true);
                level3Button.SetActive(true);
                level4Button.SetActive(true);
                break;
            case 3: // if three levels have been beaten, show 2, 3, 4 and 5
                level2Button.SetActive(true);
                level3Button.SetActive(true);
                level4Button.SetActive(true);
                level5Button.SetActive(true);
                break;
            case 4: // if four levels have been beaten, show them all
                level2Button.SetActive(true);
                level3Button.SetActive(true);
                level4Button.SetActive(true);
                level5Button.SetActive(true);
                level6Button.SetActive(true);
                break;
            case 5: // if five levels have been beaten, show them all
                level2Button.SetActive(true);
                level3Button.SetActive(true);
                level4Button.SetActive(true);
                level5Button.SetActive(true);
                level6Button.SetActive(true);
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
    { DescText.text = "Muddy Waters"; }

    public void L4Click()
    { DescText.text = "Muddier Waters"; }

    public void L5Click()
    { DescText.text = "The Creek"; }

    public void L6Click()
    { DescText.text = "Here be Hydras"; }
}