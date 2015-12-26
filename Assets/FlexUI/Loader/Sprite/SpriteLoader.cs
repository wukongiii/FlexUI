
using System.Collections.Generic;
using UnityEngine;

namespace FlexUI
{

    public class SpriteLoader
    {
		static Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
		static Dictionary<string ,SpriteSheet> spriteSheets = new Dictionary<string, SpriteSheet>();
		
		public static Sprite Load(string path, bool spriteInSheet = false)
		{
			// first search referenced sprites
			Sprite sprite = SpriteReferences.GetSpriteByName (path);
			if (sprite != null)
			{
				return sprite;
			}
			
			//then try to find sprite in Resource folder.
			
			//if it's not loading a sprite in sheet
			if (!spriteInSheet)
			{
				if (sprites.ContainsKey(path))
				{
					return sprites[path];
				}
				sprite = Resources.Load<Sprite>(path);
				sprites[path] = sprite;
				return sprite;
			}
			
			// find a sprite in a sprite sheet.
			string spriteSheetPath = path;
			string spriteName = "";
			
			int lastSlashPos = path.LastIndexOf('/');
			if (lastSlashPos != -1)
			{
				spriteSheetPath = path.Substring(0, lastSlashPos);
				spriteName = path.Substring(lastSlashPos + 1);
			}
			
			//Problem here. If there are lots of invalid sprite paths, here will consume as many invalid SpriteSheet objects.
			if (!spriteSheets.ContainsKey(spriteSheetPath))
			{
				SpriteSheet spriteSheet = new SpriteSheet();
				spriteSheet.LoadFromPath(spriteSheetPath);
				spriteSheets[spriteSheetPath] = spriteSheet;
			}
			if (spriteSheets[spriteSheetPath].Sprites.ContainsKey(spriteName))
			{
				return spriteSheets[spriteSheetPath].Sprites[spriteName];
			}
			return null;
			
		}
    }

}
