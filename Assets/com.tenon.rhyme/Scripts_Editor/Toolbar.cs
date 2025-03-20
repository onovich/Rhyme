using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEditor;

namespace TenonKit.Rhyme.MenuTool.Editor {

    public static class Toolbar {

        [InitializeOnLoadMethod]
        public static void Init() {

            ToolbarEditorCore.Initialize();

            ToolbarEditorCore.RegisterLeftGUIDraw(() => {
                if (GUILayout.Button("合并L10N")) {
                    CommonMenuTool.CombineL10N();
                }
            });

            ToolbarEditorCore.RegisterRightGUIDraw(() => {
            });

        }

    }
}