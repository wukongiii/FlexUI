


namespace FlexUI
{
    
    public class TagToggle:Element
    {
        public new const string TAG = "toggle";
        
        public TagToggle():base(TAG)
        {
            
        }
        
        protected override void LoadDefaultMods()
        {
            base.LoadDefaultMods();
            AddMod<CtrlToggle>();
        }

        public bool IsOn
        {
            get
            {
                return GetMod<CtrlToggle>().Toggle.isOn;
            }
        }
        
    }
}