

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

		public string Text
		{
			set
			{
				SetProperty(CtrlButton.TEXT, value, true);
			}
			get
			{
				if (HasProperty(CtrlButton.TEXT))
				{
					return GetString(CtrlButton.TEXT);
				}
				return null;
			}
		}

    }
}