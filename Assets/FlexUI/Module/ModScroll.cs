using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace FlexUI
{
    public class ModScroll:BaseMod
    {
        public const string SCROLL_CONTENT = "scrollcontent";

        public const string SCROLL_DIRECTION = "scrolldirection";

        public const string SCROLL_DIRECTION_BOTH = "both";
        public const string SCROLL_DIRECTION_NONE = "none";
        public const string SCROLL_DIRECTION_HORIZONTAL = "horizontal";
        public const string SCROLL_DIRECTION_VERTICAL = "vertical";

//        private List<string> scrollDirectionTable = new List<string>(){
//            SCROLL_DIRECTION_BOTH,
//            SCROLL_DIRECTION_NONE,
//            SCROLL_DIRECTION_HORIZONTAL,
//            SCROLL_DIRECTION_VERTICAL
//        };

        private ScrollRect scroll;
        protected override void OnAdd()
        {
            scroll = element.GameObject.AddComponent<ScrollRect>();
        }

        public override void Update()
        {
            if (element.HasDirtyProperty(SCROLL_CONTENT))
            {
                string scrollContentID = element.GetString(SCROLL_CONTENT, true);
                Element scrollContentElement = element.document.GetElementByID(scrollContentID);
                if (scrollContentElement != null)
                {
                    scroll.content = scrollContentElement.RectTransform;
                }
            }

            if (element.HasDirtyProperty(SCROLL_DIRECTION))
            {
                string direction = element.GetString(SCROLL_DIRECTION);
                switch(direction)
                {
                    case SCROLL_DIRECTION_BOTH:
                        scroll.vertical = scroll.horizontal = true;
                        break;

                    case SCROLL_DIRECTION_NONE:
                        scroll.vertical = scroll.horizontal = false;
                        break;

                    case SCROLL_DIRECTION_HORIZONTAL:
                        scroll.horizontal = true;
                        scroll.vertical = false;
                        break;

                    case SCROLL_DIRECTION_VERTICAL:
                        scroll.horizontal = false;
                        scroll.vertical = true;
                        break;
                }
            }
            
        }


    }
    
}
