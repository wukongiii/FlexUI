
using System;
namespace FlexUI
{
    public class TagImage:Element
    {

        public new const string TAG = "image";

        public TagImage():base(TAG)
        {

        }

        protected override void LoadDefaultMods()
        {
            base.LoadDefaultMods();
            AddMod<ModImage>();
        }

    }
}

