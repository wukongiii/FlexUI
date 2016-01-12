using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// usage:
/// 1.Create an empty GameObject.
/// 2.Drag this script to it.
/// 3.Drag the sprite folder to the Folder field.
/// 4.Press Load button.
/// 5.Drag this GameObject to SPRITE_REFERECE_PREFAB_PATH, make it a prefab.
/// 
/// if you modified the content in the folder, you need drag the prefab to the scene, press Load button again, then apply the prefab.
/// 
/// </summary>
[Serializable]
public class SpriteReferences : MonoBehaviour {

	//Modify this path by yourself. This path is where your SpriteRefeces prefab saves to.
	private const string SPRITE_REFERECE_PREFAB_PATH = "UI/Sprite/SpriteReferences";

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
		GameObject prefab = Resources.Load<GameObject>(SPRITE_REFERECE_PREFAB_PATH);
		if (prefab != null) 
		{
			instance = prefab.GetComponent<SpriteReferences>();
			instance.nameIndexedSprites = new Dictionary<string, Sprite>();
			for (int i = 0; i < instance.Sprites.Count; i++)
			{
				instance.nameIndexedSprites[instance.Sprites[i].name] = instance.Sprites[i];
			}
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
