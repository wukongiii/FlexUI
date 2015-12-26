using System.Collections.Generic;

using UnityEngine;

namespace FlexUI
{
    public class CommonProperty
    {
        //alignment
        public const string UPPER_LEFT = "upperleft";
        public const string UPPER_CENTER = "uppercenter";
        public const string UPPER_RIGHT = "upperright";
        public const string MIDDLE_LEFT = "middleleft";
        public const string MIDDLE_CENTER = "middlecenter";
        public const string MIDDLE_RIGHT = "middleright";
        public const string LOWER_LEFT = "lowerleft";
        public const string LOWER_CENTER = "lowercenter";
        public const string LOWER_RIGHT = "lowerright";

        //alignment
        public static Dictionary<string, TextAnchor> AlignTable = new Dictionary<string, TextAnchor>() {
            {UPPER_LEFT, TextAnchor.UpperLeft},
            {UPPER_CENTER, TextAnchor.UpperCenter},
            {UPPER_RIGHT, TextAnchor.UpperRight},
            {MIDDLE_LEFT, TextAnchor.MiddleLeft},
            {MIDDLE_CENTER, TextAnchor.MiddleCenter},
            {MIDDLE_RIGHT, TextAnchor.MiddleRight},
            {LOWER_LEFT, TextAnchor.LowerLeft},
            {LOWER_CENTER, TextAnchor.LowerCenter},
            {LOWER_RIGHT, TextAnchor.LowerRight}
        };


    }

}
