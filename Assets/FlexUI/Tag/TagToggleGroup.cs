


namespace FlexUI
{
    
    public class TagToggleGroup:Element
    {
        public new const string TAG = "togglegroup";
        
        public TagToggleGroup():base(TAG)
        {
            
        }
        
        protected override void LoadDefaultMods()
        {
            base.LoadDefaultMods();
//            AddMod<ModToggleGroup>();
        }
        
    }
}