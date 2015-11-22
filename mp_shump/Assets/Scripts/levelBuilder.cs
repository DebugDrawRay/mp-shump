using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

public class levelBuilder : EditorWindow
{
    private levelAsset selectedLevel;
    private string levelName = "New Level";
    private int tagRecordSize = 1;
    private string[] tagsToRecord = new string[1];

    [MenuItem("Window/Level Builder")]

    static void Init()
    {
        GetWindow<levelBuilder>();
        GetWindow<levelBuilder>().minSize = new Vector2(400, 0);
    }

    void OnGUI()
    {        
        GUILayout.Label("Edit Level", EditorStyles.boldLabel);
        selectedLevel = EditorGUI.ObjectField(new Rect(10, 70, position.width - 20, 16), new GUIContent("Selected Level"), selectedLevel, typeof(levelAsset), true) as levelAsset;

        EditorGUI.LabelField(new Rect(10, 90, position.width - 20, 20), new GUIContent("New Level"), EditorStyles.boldLabel);
        levelName = EditorGUI.TextField(new Rect(10, 110, position.width - 20, 16), new GUIContent("Level Name"), levelName);
        tagRecordSize = EditorGUI.IntField(new Rect(10, 128, position.width - 20, 16), new GUIContent("Tags to Record"), tagRecordSize);

        if (tagsToRecord.Length != tagRecordSize)
        {     
            tagsToRecord = new string[tagRecordSize];
        }
        for (int i = 0; i < tagRecordSize; i++)
        {
            float xOffset = 146 + (18 * i);
            tagsToRecord[i] = EditorGUI.TagField(new Rect(160, xOffset, position.width - 170, 16), new GUIContent("Tag"), tagsToRecord[i]);
        }

        if (GUILayout.Button("Save"))
        {
            levelAsset level = CreateInstance<levelAsset>();
            level.levelObjects = getTaggedObjects(tagsToRecord);
            string path = "Assets/" + levelName + ".asset";

            for (int i = 1; i <= 100; i++)
            {
                levelAsset preexist = AssetDatabase.LoadAssetAtPath<levelAsset>(path);
                if(preexist)
                {
                    if (preexist == selectedLevel)
                    {
                        AssetDatabase.CreateAsset(level, path);
                        AssetDatabase.SaveAssets();
                        selectedLevel = AssetDatabase.LoadAssetAtPath<levelAsset>(path);
                        break;
                    }
                    else
                    {
                        path = "Assets/" + levelName + i.ToString() + ".asset";
                    }
                }
                else
                {
                    AssetDatabase.CreateAsset(level, path);
                    AssetDatabase.SaveAssets();
                    selectedLevel = null;
                    break;
                }
            }
        }
        if (GUILayout.Button("Load"))
        {
            loadLevel(selectedLevel);
        }
    }

    List<levelObject> getTaggedObjects(string[] tags)
    {
        List<levelObject> list = new List<levelObject>();
        for (int i = 0; i < tags.Length; i++)
        {
            GameObject[] sceneObj = GameObject.FindGameObjectsWithTag(tags[i]);
            for (int j = 0; j < sceneObj.Length; j++)
            {
                GameObject prefab = PrefabUtility.GetPrefabParent(sceneObj[j]) as GameObject;
                levelObject newObj = new levelObject(prefab, sceneObj[j].transform.position);
                list.Add(newObj);
            }
        }
        return list;
    }

    void loadLevel(levelAsset level)
    {
        List<levelObject> objects = level.levelObjects;
        for(int i = 0; i< objects.Count; i++)
        {
            GameObject obj = Instantiate(objects[i].prefab) as GameObject;
            Vector2 pos = objects[i].prefabPos;
            obj.transform.position = pos;
        }
    }
}
