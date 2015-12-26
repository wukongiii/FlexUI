using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

[CustomEditor(typeof(SpriteReferences))]
public class SpriteReferencesEditor:Editor
{
    SpriteReferences sr;
    SerializedProperty propSprites;

    bool showSprites = false;

    AnimBool showProgress = new AnimBool(false);

    void OnEnable()
    {
        sr = (SpriteReferences)target;
        propSprites = serializedObject.FindProperty("Sprites");
    }

    public override void OnInspectorGUI()
    {
        if (sr.Folder != null)
        {
            EditorGUILayout.LabelField("Selected Folder:");
            EditorGUILayout.LabelField(AssetDatabase.GetAssetPath(sr.Folder));
        }

        UnityEngine.Object newFolder = EditorGUILayout.ObjectField(sr.Folder, typeof(DefaultAsset), false);

        if (newFolder == null && sr.Folder != null)
        {
            sr.Folder = null;
        } else if (newFolder != null && newFolder != sr.Folder)
        {
            string path = AssetDatabase.GetAssetPath(newFolder);
            if (!AssetDatabase.IsValidFolder(path))
            {
                EditorGUILayout.HelpBox("Must selecte a Folder.", MessageType.Info);
            } else
            {
                sr.Folder = newFolder;
            }
        }

        if (sr.Folder != null)
        {
            if (GUILayout.Button("Load"))
            {
                SetNewReferencedSprites(AssetDatabase.GetAssetPath(newFolder));
            }
        }

        if (sr.Sprites != null)
        {
            EditorGUILayout.Separator();
            showSprites = EditorGUILayout.Foldout(showSprites, "Referenced Sprites:(Total " + sr.Sprites.Count +")");
            if (showSprites)
            {
                for (int i = 0; i < sr.Sprites.Count; i++)
                {
                    if (GUILayout.Button(sr.Sprites[i].name))
                    {
                        EditorGUIUtility.PingObject(sr.Sprites[i]);
                    }
                }
            }

        }

    }

    private void SetNewReferencedSprites(string path)
    {
        List<Sprite> sprites = new List<Sprite>();
        var spriteGUIDs = AssetDatabase.FindAssets("t:Sprite", new string[]{path});
        for (int i = 0; i < spriteGUIDs.Length; i++)
        {
            string spritePath = AssetDatabase.GUIDToAssetPath(spriteGUIDs[i]);
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);
            sprites.Add(sprite);

        }
        sr.Sprites = sprites;

        serializedObject.ApplyModifiedProperties();
    }

}