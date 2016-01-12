using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


namespace catwins.flexui
{
    [InterestedProperty(AUTOSIZE, HFITMODE, VFITMODE)]
    public class ModContentSizeFitter:BaseMod
    {

        public const string AUTOSIZE = "autosize";
        public const string HFITMODE = "hfitmode";
        public const string VFITMODE = "vfitmode";

        public const string UNCONSTRAINED = "unconstrained";
        public const string PREFFEREDSIZE = "preffered";
        public const string MINSIZE = "min";
        static Dictionary<string, ContentSizeFitter.FitMode> modeTable = new Dictionary<string, ContentSizeFitter.FitMode>(){
            {UNCONSTRAINED, ContentSizeFitter.FitMode.Unconstrained},
            {PREFFEREDSIZE, ContentSizeFitter.FitMode.PreferredSize},
            {MINSIZE, ContentSizeFitter.FitMode.MinSize}
        };


        ContentSizeFitter fitter = null;

        protected override void OnAdd()
        {
            fitter = element.GameObject.AddComponent<ContentSizeFitter>();

        }

        protected override void OnRemove()
        {

        }


        public override void Update()
        {
            //autosize enable/disable
            if (element.HasDirtyProperty(AUTOSIZE))
            {
                bool autosize = element.GetBool(AUTOSIZE);
                fitter.enabled = autosize;
            }

            //fit mode
            if (element.HasDirtyProperty(HFITMODE))
            {
                string mode = element.GetString(HFITMODE);
                if (modeTable.ContainsKey(mode))
                {
                    ContentSizeFitter.FitMode fitModeH = ContentSizeFitter.FitMode.Unconstrained;
                    fitModeH = modeTable[mode];
                    fitter.horizontalFit = fitModeH;
                }
            }
            if (element.HasDirtyProperty(VFITMODE))
            {
                string mode = element.GetString(VFITMODE);
                if (modeTable.ContainsKey(mode))
                {
                    ContentSizeFitter.FitMode fitModeV = ContentSizeFitter.FitMode.Unconstrained;
                    fitModeV = modeTable[mode];
                    fitter.verticalFit = fitModeV;
                }
            }

        }

    }

}