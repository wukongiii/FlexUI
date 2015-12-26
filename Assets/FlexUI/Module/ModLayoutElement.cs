
using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace catwins.flexui
{
    [InterestedProperty(PREFFERED_WIDTH, PREFFERED_HEIGHT, MIN_HEIGHT, MIN_WIDTH, FLEXIBLE_HEIGHT, FLEXIBLE_WIDTH, IGNORE_LAYOUT)]
    public class ModLayoutElement:BaseMod
    {
        public const string PREFFERED_WIDTH = "prefferedwidth";
        public const string PREFFERED_HEIGHT = "prefferedheight";
        public const string MIN_HEIGHT = "minheight";
        public const string MIN_WIDTH = "minwidth";
        public const string FLEXIBLE_HEIGHT = "flexibleheight";
        public const string FLEXIBLE_WIDTH = "flexiblewidth";

        public const string IGNORE_LAYOUT = "ignorelayout";

        LayoutElement layoutElement;
        protected override void OnAdd()
        {
            layoutElement = element.GameObject.AddComponent<LayoutElement>();
        }

        public override void Update()
        {

            if (element.HasDirtyProperty(PREFFERED_WIDTH))
            {
                float prefferedWidth = element.GetFloat(PREFFERED_WIDTH);
                layoutElement.preferredWidth = prefferedWidth;
            }

            if (element.HasDirtyProperty(PREFFERED_HEIGHT))
            {
                float prefferedHeight = element.GetFloat(PREFFERED_HEIGHT);
                layoutElement.preferredHeight = prefferedHeight;
            }

            if (element.HasDirtyProperty(MIN_WIDTH))
            {
                float minWidth = element.GetFloat(MIN_WIDTH);
                layoutElement.minWidth = minWidth;
            }

            if (element.HasDirtyProperty(MIN_HEIGHT))
            {
                float minHeight = element.GetFloat(MIN_HEIGHT);
                layoutElement.minHeight = minHeight;
            }

            if (element.HasDirtyProperty(FLEXIBLE_WIDTH))
            {
                float flexibleWidth = element.GetFloat(FLEXIBLE_WIDTH);
                layoutElement.flexibleWidth = flexibleWidth;
            }

            if (element.HasDirtyProperty(FLEXIBLE_HEIGHT))
            {
                float flexibleHeight = element.GetFloat(FLEXIBLE_HEIGHT);
                layoutElement.flexibleHeight = flexibleHeight;
            }

            if (element.HasDirtyProperty(IGNORE_LAYOUT))
            {
                bool ignore = element.GetBool(IGNORE_LAYOUT);
                layoutElement.ignoreLayout = ignore;
            }

        }
    }

}