using System;

using UnityEngine;

namespace FlexUI
{
    public class TagScroll:Element
    {
        
        public new const string TAG = "scroll";

//        private const string CONTENT_GO_NAME = "content";

        private GameObject contentGO;

        public TagScroll():base(TAG)
        {
            
        }

        
        protected override void LoadDefaultMods()
        {
            base.LoadDefaultMods();
            AddMod<ModScroll>();
        }
    }
}

