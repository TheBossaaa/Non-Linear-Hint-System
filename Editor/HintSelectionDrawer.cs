using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(HintSelection))]
public class HintSelectionDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty hintAssetProp = property.FindPropertyRelative("hintAsset");
        SerializedProperty hintKeyProp = property.FindPropertyRelative("hintKey");

        float halfWidth = position.width / 2;

        // Draw HintAsset field
        Rect hintAssetRect = new Rect(position.x, position.y, halfWidth - 5, position.height);
        EditorGUI.PropertyField(hintAssetRect, hintAssetProp, GUIContent.none);

        // Fetch hint keys if HintAsset is set
        List<string> hintKeys = new List<string>();
        if (hintAssetProp.objectReferenceValue != null)
        {
            HintAsset hintAsset = (HintAsset)hintAssetProp.objectReferenceValue;
            hintKeys = hintAsset.HintList.ConvertAll(h => h.HintKey);
        }

        // Draw HintKey dropdown
        Rect hintKeyRect = new Rect(position.x + halfWidth + 5, position.y, halfWidth - 5, position.height);
        if (hintKeys.Count > 0)
        {
            int selectedIndex = Mathf.Max(hintKeys.IndexOf(hintKeyProp.stringValue), 0);
            selectedIndex = EditorGUI.Popup(hintKeyRect, selectedIndex, hintKeys.ToArray());
            hintKeyProp.stringValue = hintKeys[selectedIndex];
        }
        else
        {
            EditorGUI.PropertyField(hintKeyRect, hintKeyProp, GUIContent.none);
        }

        EditorGUI.EndProperty();
    }
}
