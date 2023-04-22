/*using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HexMap))]
public class HexMapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        HexMap hexMap = (HexMap)target;

        if (GUILayout.Button("Generate Tile Map"))
        {
            hexMap.createHexTileMap();
        }
    }
}
*/