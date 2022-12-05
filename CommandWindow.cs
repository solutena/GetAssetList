#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CommandWindow : EditorWindow
{
	[MenuItem("Window/CommandWindow")]
	static void GetWindow()
	{
		GetWindow<CommandWindow>();
	}

	List<Object> list;
	int select = 0;
	string path = "Assets/Script";
	string filter = "t:MonoScript";

	private void OnGUI()
	{
		if (list == null)
			list = GetAssetList(path, filter);
		select = GUILayout.SelectionGrid(select, list.ConvertAll(obj => obj.name).ToArray(), 1, "PreferencesKeysElement");
		if (GUILayout.Button("Open"))
		{
			Object script = AssetDatabase.LoadAssetAtPath<MonoScript>(path + "/" + list[select].name + ".cs");
			AssetDatabase.OpenAsset(script);
		}
	}

	public static List<Object> GetAssetList(string path, string filter)
	{
		List<string> assets = AssetDatabase.FindAssets(filter, new[] { path }).ToList();
		return assets.ConvertAll(guid => AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GUIDToAssetPath(guid)));
	}
}
#endif