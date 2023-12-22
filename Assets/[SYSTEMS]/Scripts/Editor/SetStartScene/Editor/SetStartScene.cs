using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SetStartScene : EditorWindow
{
    private void OnGUI()
    {
        EditorSceneManager.playModeStartScene = (SceneAsset)EditorGUILayout.ObjectField(new GUIContent("Start Scene"),
            EditorSceneManager.playModeStartScene, typeof(SceneAsset), false);

        var _scenePath = "Assets/[SYSTEMS]/Scenes/SampleScene.unity";
        
        if (GUILayout.Button($"Set Start Scene: {_scenePath}"))
            SetPlayeModeStartScene(_scenePath);
    }

    private void SetPlayeModeStartScene(string _scenePath)
    {
        SceneAsset _newStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(_scenePath);

        if (_newStartScene != null)
            EditorSceneManager.playModeStartScene = _newStartScene;
        else
            Debug.LogWarning($"Could not find scene {_scenePath}");
    }

    [MenuItem("Scene Setting/Start Scene")]
    static void Open()
    {
        GetWindow<SetStartScene>();
    }

}
