using UnityEngine;
using UnityEngine.UI;

using URI = catwins.flexui.URI;

namespace catwins.flexui
{
    [InterestedProperty(BACKGROUND, IMAGE)]
    public class ModImage:BaseMod
    {
        public const string BACKGROUND = "background";
        public const string IMAGE = "image";
		public const string TYPE = "type";
        public const string COLOR = "color";
		public const string ASPECT = "aspect";


        Image image = null;

        protected override void OnAdd()
        {
            image = element.GameObject.AddComponent<Image>();
        }

        public override void Update()
        {
           
            string uriStr = "";
            if (element.HasDirtyProperty(BACKGROUND))
            {
                uriStr = element.GetString(BACKGROUND);
            } else if (element.HasDirtyProperty(URI.TAG))
            {
                uriStr = element.GetString(URI.TAG);
            }

            URI uri = URI.GetFromString(uriStr);

            if (uri != null)
            {
                bool spriteInSheet = false;
                if (uri.contentType == ContentType.SPRITE_IN_SHEET)
                {
                    spriteInSheet = true;
                }
                Sprite sprite = SpriteLoader.Load(uri.relativePath, spriteInSheet);
                if (sprite != null)
                {
                    image.sprite = sprite;
                }
            }


            if (element.HasDirtyProperty(COLOR))
            {
                Color color = element.GetColor(COLOR);
                image.color = color;
            }

			if (element.HasDirtyProperty(TYPE))
			{				
				if(element.GetString(TYPE).Equals("sliced"))
				{
					image.type = Image.Type.Sliced;
				}else if(element.GetString(TYPE).Equals("tiled"))
				{
					image.type = Image.Type.Tiled;
				}else if(element.GetString(TYPE).Equals("filled"))
				{
					image.type = Image.Type.Filled;
				}
			}

			if(element.HasDirtyProperty(ASPECT))
			{
				bool aspectBool = element.GetBool(ASPECT);
				image.preserveAspect = aspectBool;
			}


        }
    }

}
