using MPUIKIT;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Common.Editor.Scripts
{
    public static class SceneLoad
    {
        [MenuItem("Scenes/Main")]
        private static void Login()
        {
            OpenScene("Main");
        }
        
        [MenuItem("Scenes/Network")]
        private static void Network()
        {
            OpenScene("Network");
        }

        private static void OpenScene(string sceneName)
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                // user said yes -> scene was saved
                EditorSceneManager.OpenScene("Assets/Scenes/" + sceneName + ".unity");
            }
            else
            {
                // user said no -> abort or do nothing?
            }
        }

        private static float CurrentAlpha;

        [MenuItem("FourtyFourty/ShowReference _F10")]
        public static void ShowHideReference()
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            GameObject.Find("Reference").GetComponent<CanvasGroup>().alpha = CurrentAlpha == 0.25f ? 0f : 0.25f;
            CurrentAlpha = GameObject.Find("Reference").GetComponent<CanvasGroup>().alpha;
        }

        [MenuItem("FourtyFourty/ReferenceIncreaseVisibility _F11")]
        public static void ReferenceIncreaseVisibility()
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            var refImage = GameObject.Find("Reference").GetComponent<CanvasGroup>();


            if (refImage.alpha < 1)
            {
                refImage.alpha += 0.25f;
            }
            else
            {
                refImage.alpha = 0;
            }
        }
    }
}