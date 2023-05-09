#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
#endregion

public class ObolDisplayer : MonoBehaviour
{

    [SerializeField] TMP_Text forgeText, grapevineText;
    [SerializeField] TMP_Text oneObols, twoObols, threeObols, fourObols, fiveObols;

    private void Start()
    {
        oneObols.text = "" + DataManager.Instance.Level1Obols + " / 15";
        twoObols.text = "" + DataManager.Instance.Level2Obols + " / 15";
        threeObols.text = "" + DataManager.Instance.Level3Obols + " / 15";
        fourObols.text = "" + DataManager.Instance.Level4Obols + " / 15";
        fiveObols.text = "" + DataManager.Instance.Level5Obols + " / 15";
    }

    private void FixedUpdate()
    {
        forgeText.text = "" + DataManager.Instance.TotalObols;
        grapevineText.text = "" + DataManager.Instance.TotalObols;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            DataManager.Instance.TotalObols = 20;
        }
    }
}