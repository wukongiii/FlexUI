using UnityEngine;
using System.Collections.Generic;

namespace FlexUI
{
	public class SpriteSheet
	{
	    public string SpriteSheetPath;
	    public Dictionary<string, Sprite> Sprites = new Dictionary<string, Sprite>();

	    public void LoadFromPath(string path)
	    {
	        Sprite[] sprites = Resources.LoadAll<Sprite>(path);
	        for (int i = 0; i < sprites.Length; i++)
	        {
	            Sprites[sprites[i].name] = sprites[i];
	        }
	    }

	    public Sprite Get(string spriteName)
	    {
	        if (Sprites.ContainsKey(spriteName))
	        {
	            return Sprites[spriteName];
	        }
	        return null;
	    }

	    public void Dispose()
	    {
	        Sprites = null;
	    }
	}
}