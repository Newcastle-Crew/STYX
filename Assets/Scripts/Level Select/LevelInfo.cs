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
    //[SerializeField] Button L7;

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

    //public void L7Click() // L7 is currently hidden to avoid giving ourselves extra work
    //{ DescText.text = "L7 Todo"; }
}
