using System;

using UnityEngine;

namespace FlexUI
{
    public class TagScript:Element
    {
        
        public new const string TAG = "script";


        public TagScript():base(TAG)
        {
            
        }

        
        protected override void LoadDefaultMods()
        {
            base.LoadDefaultMods();
            AddMod<ModScript>();
        }
    }
}

