# region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

// using this playlist: https://youtube.com/playlist?list=PLcRSafycjWFcwCxOHnc83yA0p4Gzx0PTM

public abstract class SteeringBehaviour : MonoBehaviour
{
    public abstract (float[] danger, float[] interest)
        GetSteering(float[] danger, float[] interest, AIData aiData);
}