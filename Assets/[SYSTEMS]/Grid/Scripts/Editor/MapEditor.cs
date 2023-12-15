using Grid;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapGenerator))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator _mapGenerator = target as MapGenerator;
        
        if (DrawDefaultInspector())
            _mapGenerator.GenerateMap();

        if (GUILayout.Button("Generate Map"))
            _mapGenerator.GenerateMap();

    }
}
