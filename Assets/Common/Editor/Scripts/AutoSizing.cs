
using TMPro;

using UnityEditor;

public class AutoSizing : Editor
{    private static void MakeResponsive(TMP_Text tmpText)
    {
        tmpText.enableAutoSizing=true;
        tmpText.fontSizeMax = tmpText.fontSize;
        tmpText.fontSizeMin = 1;

    }
    [MenuItem("FourtyFourty/Responsive Text _F2")]
    private static void ResponsiveText()
    {
        for (int i = 0; i < Selection.gameObjects.Length; i++)
        {
            TMP_Text rectTransform = Selection.gameObjects[i].GetComponent<TMP_Text>();
            if (rectTransform)
                MakeResponsive(rectTransform);
        }
    }
}
