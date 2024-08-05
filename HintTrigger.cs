using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class HintSelection
{
    public HintAsset hintAsset;
    public string hintKey;

    public List<string> GetHintKeys()
    {
        if(hintAsset != null)
        {
            return hintAsset.HintList.ConvertAll(h => h.HintKey);
        }
        return new List<string>();
    }

}

public class HintTrigger : MonoBehaviour
{
    public List<HintSelection> hintSelections; //List of HintSelections containing HintAssets and selected HintKeys

    private HintSystem hintSystem;

    private void Start()
    {
        //Find and assign the HintSytstem
        hintSystem = FindObjectOfType<HintSystem>();

        if (hintSystem == null)
        {
            Debug.LogError("HintSystem not found in the scene.");
        }

        ValidateHintSelections();

    }

    #region Validation
    void ValidateHintSelections()
    {
        foreach (var selection in hintSelections)
        {
            if (selection.hintAsset == null)
            {
                Debug.LogError("HintAsset is missing in HintSelection.");
                continue;
            }

            if (string.IsNullOrEmpty(selection.hintKey))
            {
                Debug.LogError($"HintKey is missing in HintAsset '{selection.hintAsset.name}'.");
            }
            else
            {
                var hint = selection.hintAsset.HintList.Find(h => h.HintKey == selection.hintKey);
                if (hint == null)
                {
                    Debug.LogError($"HintKey '{selection.hintKey}' not found in HintAsset '{selection.hintAsset.name}'.");
                }
            }
        }
    }
    #endregion

    public void TriggerHint()
    {

        if (hintSystem == null || hintSelections.Count == 0) return;

        foreach (var selection in hintSelections)
        {
            var hint = selection.hintAsset.HintList.Find(h => h.HintKey == selection.hintKey);
            if (hint != null)
            {
                hintSystem.ShowHint(hint.HintKey);
                return; // Exit after triggering the first valid hint
            }
        }

        Debug.LogWarning("No hints found.");

    }

    public void OnInteract()
    {
        TriggerHint();
    }



}

