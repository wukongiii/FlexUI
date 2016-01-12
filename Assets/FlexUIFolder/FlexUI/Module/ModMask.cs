
using UnityEngine;
using UnityEngine.UI;

namespace catwins.flexui
{
    [InterestedProperty(MASK)]
    public class ModMask:BaseMod
    {
        public const string MASK = "mask";

        public const string SHOW_MASK = "showmask";
        
        private Mask mask;
        protected override void OnAdd()
        {
        }
        
        public override void Update()
        {
            if (element.HasDirtyProperty(MASK))
            {
                bool hasMask = bool.Parse(element.GetString(MASK));

                if (hasMask && mask == null)
                {
                    mask = element.GameObject.AddComponent<Mask>();
                } else if (!hasMask && mask != null) 
                {
                    GameObject.Destroy(mask);
                }
            }

            if (element.HasDirtyProperty(SHOW_MASK))
            {
                bool showMask = bool.Parse(element.GetString(SHOW_MASK));
                if (mask != null)
                {
                    mask.showMaskGraphic = showMask;
                }
            }
            
        }
        
        
    }
    
}
