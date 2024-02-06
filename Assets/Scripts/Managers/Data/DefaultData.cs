using System;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR // Editor namespaces can only be used in the editor.
using Sirenix.OdinInspector.Editor.Examples;
#endif

[Serializable]
[LabelText("Default Data", SdfIconType.Cloud)]
public class DefaultData
{
    public string brandName = "Default Brand Name";

    public string brandImageDownloadLink =
        "https://drive.google.com/uc?export=download&id=1irLG5jAF6w3qJG4Lota4cnWFiTJoDbQO";

    [HideLabel]
    [PreviewField(70, ObjectFieldAlignment.Right)]
    [HorizontalGroup("row1", 50), VerticalGroup("row1/right")]
    public object preview;
#if UNITY_EDITOR
    [OnInspectorInit]
    private void CreateData()
    {
        preview = Resources.Load<Sprite>("Simulanis_Logo");
    }
#endif
}