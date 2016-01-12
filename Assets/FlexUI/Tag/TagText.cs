
using System;
using System.Diagnostics;

namespace catwins.flexui
{
    [DebuggerDisplay("TagText")]
    public class TagText:Element
    {
        
        public new const string TAG = "text";
        
        public TagText():base(TAG)
        {
            
        }
        
        protected override void LoadDefaultMods()
        {
            base.LoadDefaultMods();
            AddMod<ModText>();
        }

        public string Text
        {
            set
            {
                SetProperty(ModText.TEXT, value, true);
            }
            get
            {
                if (HasProperty(ModText.TEXT))
                {
                    return GetString(ModText.TEXT);
                }
                return null;
            }
        }

        public int TextKey
        {
            set
            {
                SetProperty(ModText.TEXT_KEY, value);
            }
            get
            {
                if (HasProperty(ModText.TEXT))
                {
                    return GetInt(ModText.TEXT);
                }
                return 0;
            }
        }

        public int Size
        {
            set
            {
                SetProperty(ModText.SIZE, value);
            }
            get
            {
                if (HasProperty(ModText.SIZE))
                {
                    return GetInt(ModText.SIZE);
                }
                return 0;
            }
        }

        public string Color
        {
            set
            {
                SetProperty(ModText.COLOR, value);
            }
            get
            {
                if (HasProperty(ModText.COLOR))
                {
                    return GetString(ModText.COLOR);
                }
                return null;
            }
        }
        
    }
}
