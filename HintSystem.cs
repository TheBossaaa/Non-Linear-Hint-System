using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class HintSystem : MonoBehaviour
{
    [Header("UI Elements")]
    public CanvasGroup hintUIPanel;
    public TMP_Text hintUIText;

    [Header("Place Scriptable Hint Assets")]
    public List<HintAsset> hintAssets;

    [Header("Control Lerping")]
    [SerializeField] private float fadeDurationVar = 0.5f;

    private Dictionary<string, HintAsset.HintText> hintDictionary = new();

    private void Start()
    {
        //Initialize the hint UI panel as invisible
        hintUIPanel.alpha = 0f;

        //Subscribe all GStrings in the HintAsset to localisation
        foreach (var hintAsset in hintAssets)
        {

            foreach(var hint in hintAsset.HintList)
            {
                hint.Text.SubscribeGloc();
                hintDictionary[hint.HintKey] = hint; // Populate dictionary for quick lookup
            }


        }

    }

    public void ShowHint(string hintKey)
    {
        if(hintDictionary.TryGetValue(hintKey, out var hint))
        {
            StartCoroutine(HandleHintText(hint));
        }
        else
        {
            Debug.LogWarning($"Hint with key '{hintKey}' not found. ");
        }
    }

    private IEnumerator HandleHintText(HintAsset.HintText hint)
    {
        //Fade in panel
        float elapsedTime = 0f;
        float fadeDuration = fadeDurationVar;
        float startAlpha = hintUIPanel.alpha;
        float targetAlpha = 1f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            hintUIPanel.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }
        hintUIPanel.alpha = targetAlpha; //Ensure final value is set

        //Set the hint text
        hintUIText.text = hint.Text.Value;

        //Wait for the specified hint duration
        yield return new WaitForSeconds(hint.Time);

        //Fade out the panel
        elapsedTime = 0f;
        startAlpha = hintUIPanel.alpha;
        targetAlpha = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            hintUIPanel.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }
        //Ensure the final value is set
        hintUIPanel.alpha = targetAlpha;
    }

}
