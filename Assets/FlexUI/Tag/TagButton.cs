

namespace catwins.flexui
{

    public class TagButton:Element
    {
        public new const string TAG = "button";
        
        public TagButton():base(TAG)
        {
            
        }
        
        protected override void LoadDefaultMods()
        {
            base.LoadDefaultMods();
            AddMod<CtrlButton>();
        }

    }
}