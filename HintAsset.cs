using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UHFPS.Runtime;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "Hints", menuName = "BossaCustom/Hints/Hints Asset")]
public class HintAsset : ScriptableObject
{

    public List<HintText> HintList = new();

    [Serializable]
    public sealed class HintText
    {
        public string HintKey;
        public float Time;
        public GString Text;

    }

}
