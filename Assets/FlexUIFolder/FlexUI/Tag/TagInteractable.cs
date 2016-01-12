

namespace catwins.flexui
{
    public class TagInteractable:Element
    {
        
        public new const string TAG = "interactable";
        
        public TagInteractable():base(TAG)
        {
            
        }
        
        protected override void LoadDefaultMods()
        {
            base.LoadDefaultMods();
            AddMod<ModInteractable>();
        }
    }

}