# region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

// using this playlist: https://youtube.com/playlist?list=PLcRSafycjWFcwCxOHnc83yA0p4Gzx0PTM

public abstract class Detector : MonoBehaviour
{
    public abstract void Detect(AIData aiData);
}