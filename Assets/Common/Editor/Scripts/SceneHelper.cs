using UnityEditor;
using UnityEditor.SceneManagement;

namespace Common.Editor.Scripts
     {
         public static class SceneLoad
         {
             
             [MenuItem("Scenes/Loading")]
             private static void OpenLobbyScene()
             {
                 OpenScene("Loading");
             }
             
             [MenuItem("Scenes/Main")]
             private static void Login()
             {
                OpenScene("Main");
             }
   
             private static void OpenScene(string sceneName)
             {
                 if(EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                 {
                     // user said yes -> scene was saved
                     EditorSceneManager.OpenScene("Assets/Scenes/"+sceneName+".unity");
                 }
                 else
                 {
                     // user said no -> abort or do nothing?
                 }
             }
         }
     }

