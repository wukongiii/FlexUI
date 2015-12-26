using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SpriteReferences : MonoBehaviour {

#if UNITY_EDITOR
    [SerializeField, HideInInspector]
    public UnityEngine.Object Folder;
#endif

    [SerializeField]
    public List<Sprite> Sprites;

    private static SpriteReferences instance;
    private static bool isInited = false;
    
    private Dictionary<string, Sprite> nameIndexedSprites;

    private  static SpriteReferences Instance
    {
        get
        {
            if (!isInited)
            {
                Init();
            } 
            return instance;
        }
    }

    private static void Init()
    {
        GameObject go = Resources.Load<GameObject>("UI/Sprite/SpriteReferences");
        instance = go.GetComponent<SpriteReferences>();
        instance.nameIndexedSprites = new Dictionary<string, Sprite>();
        for (int i = 0; i < instance.Sprites.Count; i++)
        {
            instance.nameIndexedSprites[instance.Sprites[i].name] = instance.Sprites[i];
        }
#if !UNITY_EDITOR
        instance.Sprites = null;
#endif
        isInited = true;
    }

    private Sprite InstanceGetSpriteByName(string name)
    {
        if (nameIndexedSprites.ContainsKey(name))
        {
            return nameIndexedSprites[name];
        }
        return null;
    }

    public static Sprite GetSpriteByName(string name)
    {
        return Instance.InstanceGetSpriteByName(name);
    }

	
}
