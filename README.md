# GetAssetList 함수
```
using System.Linq;

public static List<Object> GetAssetList(string path, string filter)
{
    List<string> assets = AssetDatabase.FindAssets(filter, new[] { path }).ToList();
    return assets.ConvertAll(guid => AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GUIDToAssetPath(guid)));
}
```

에셋 경로 내의 리스트를 가져와 커스텀 에디터에서 활용 할 수 있다.

## 예제

```
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
```
![image](https://user-images.githubusercontent.com/22467083/205657484-27f8fb8b-22e6-4e69-aebd-05d46d6cde4c.png)

![image](https://user-images.githubusercontent.com/22467083/205657504-e8600864-ce16-4906-972b-94c2ab403fd1.png)


예제에선 Asset/Script/Item 경로의 스크립트들을 가져온다.

Open 버튼을 통해서 스크립트를 열 수 있다.

스크립트로 구현된 아이템, 스킬 등을 편리하게 관리 할 수 있다.
