using UnityEngine;

namespace FlexUI
{
    [InterestedProperty(POSITION, POS_X, POS_Y, POS_Z, 
                        WIDTH, HEIGHT, 
                        ALIGN,
                        ANCHOR, ANCHOR_MIN_X, ANCHOR_MIN_Y, ANCHOR_MAX_X, ANCHOR_MAX_Y, 
                        ROTATION,ROTATION_X, ROTATION_Y, ROTATION_Z, 
                        SCALE, SCALE_X, SCALE_Y, SCALE_Z
                        )]
    public class ModRect:BaseMod
    {
        public const string POSITION = "position";
        public const string POS_X = "posx";
        public const string POS_Y = "posy";
        public const string POS_Z = "posz";

        public const string WIDTH = "width";
        public const string HEIGHT = "height";

        public const string ALIGN = "align";//align effects both anchor and pivot.

        public const string ANCHOR = "anchor";
        public const string ANCHOR_MIN_X = "anchorminx";
        public const string ANCHOR_MIN_Y = "anchorminy";
        public const string ANCHOR_MAX_X = "anchormaxx";
        public const string ANCHOR_MAX_Y = "anchormaxy";

        public const string PIVOT = "pivot";
        public const string PIVOT_X = "pivotx";
        public const string PIVOT_Y = "pivoty";

        public const string ROTATION = "rotation";
        public const string ROTATION_X = "rotationx";
        public const string ROTATION_Y = "rotationy";
        public const string ROTATION_Z = "rotationz";

        public const string SCALE = "scale";
        public const string SCALE_X = "scalex";
        public const string SCALE_Y = "scaley";
        public const string SCALE_Z = "scalez";

        RectTransform rect;
        protected override void OnAdd()
        {
            rect = element.GameObject.GetComponent<RectTransform>();
        }


        public override void Update()
        {
            #region position
            //position
            Vector3 pos = rect.anchoredPosition3D;
            if (element.HasDirtyProperty(POSITION))
            {
                string posStr = element.GetString(POSITION);
                Vector3? v = FlexUIUtil.GetVector3FromString(posStr);
                if (v != null)
                {
                    pos = (Vector3)v;
                }
            }
            if (element.HasDirtyProperty(POS_X))
            {
                pos.x = element.GetFloat(POS_X);
            }
            if (element.HasDirtyProperty(POS_Y))
            {
                pos.y = element.GetFloat(POS_Y);
            }
            if (element.HasDirtyProperty(POS_Z))
            {
                pos.z = element.GetFloat(POS_Z);
            }
            rect.anchoredPosition3D = pos;
            #endregion

            #region align
            //align = anchor+pivot
            if (element.HasDirtyProperty(ALIGN))
            {
                string alignValue = element.GetString(ALIGN);
                if (CommonProperty.AlignTable.ContainsKey(alignValue))
                {
                    element.SetProperty(ANCHOR, alignValue);
                    element.SetProperty(PIVOT, alignValue);
                }
            }
            #endregion

            //anchor
            Vector2 anchorMin = rect.anchorMin;
            Vector2 anchorMax = rect.anchorMax;

            #region anchor

            if (element.HasDirtyProperty(ANCHOR))
            {
                TextAnchor anchor = TextAnchor.MiddleLeft;
                if (CommonProperty.AlignTable.ContainsKey(element.GetString(ANCHOR)))
                {
                    anchor = CommonProperty.AlignTable[element.GetString(ANCHOR)];
                }

                switch (anchor)
                {
                    case TextAnchor.UpperLeft:
                        anchorMin.x = 0f; anchorMin.y = 1f;
                        anchorMax.x = 0f; anchorMax.y = 1f;
                        break;

                    case TextAnchor.UpperCenter:
                        anchorMin.x = 0.5f; anchorMin.y = 1f;
                        anchorMax.x = 0.5f; anchorMax.y = 1f;
                        break;

                    case TextAnchor.UpperRight:
                        anchorMin.x = 1f; anchorMin.y = 1f;
                        anchorMax.x = 1f; anchorMax.y = 1f;
                        break;

                    case TextAnchor.MiddleLeft:
                        anchorMin.x = 0f; anchorMin.y = 0.5f;
                        anchorMax.x = 0f; anchorMax.y = 0.5f;
                        break;

                    case TextAnchor.MiddleCenter:
                        anchorMin.x = 0.5f; anchorMin.y = 0.5f;
                        anchorMax.x = 0.5f; anchorMax.y = 0.5f;
                        break;

                    case TextAnchor.MiddleRight:
                        anchorMin.x = 1f; anchorMin.y = 1f;
                        anchorMax.x = 1f; anchorMax.y = 1f;
                        break;

                    case TextAnchor.LowerLeft:
                        anchorMin.x = 0f; anchorMin.y = 0f;
                        anchorMax.x = 0f; anchorMax.y = 0f;
                        break;

                    case TextAnchor.LowerCenter:
                        anchorMin.x = 0.5f; anchorMin.y = 0f;
                        anchorMax.x = 0.5f; anchorMax.y = 0f;
                        break;

                    case TextAnchor.LowerRight:
                        anchorMin.x = 1f; anchorMin.y = 0f;
                        anchorMax.x = 1f; anchorMax.y = 0f;
                        break;
                }
            }
            //anchor
            if (element.HasDirtyProperty(ANCHOR_MIN_X))
            {
                anchorMin.x = element.GetFloat(ANCHOR_MIN_X);
            }
            if (element.HasDirtyProperty(ANCHOR_MIN_Y))
            {
                anchorMin.y = element.GetFloat(ANCHOR_MIN_Y);
            }
            rect.anchorMin = anchorMin;
            
            if (element.HasDirtyProperty(ANCHOR_MAX_X))
            {
                anchorMax.x = element.GetFloat(ANCHOR_MAX_X);
            }
            if (element.HasDirtyProperty(ANCHOR_MAX_Y))
            {
                anchorMax.y = element.GetFloat(ANCHOR_MAX_Y);
            }
            rect.anchorMax = anchorMax;

            #endregion

            #region width height
            //width height
            //Say a value 80% on axis X, how to allocate it to each side?
            //using 0.5f as the center, the smaller one is 0.5 - (0.8/2) = 0.1, the bigger one is 0.5 + (0.8/2) = 0.9;
            float parentAnchorCenterX = 0.5f;
            float parentAnchorCenterY = 0.5f;

            Vector2 offsetMin = new Vector2();
            Vector2 offsetMax = new Vector2();

            if (element.HasDirtyProperty(WIDTH))
            {
                float widthPercentage;
                bool isPercent = FlexUIUtil.TryGetPercentage(element.GetString(WIDTH), out widthPercentage);
                if (isPercent)
                {
                    float halfPercent = widthPercentage/2.0f;
                    anchorMin.x = parentAnchorCenterX - halfPercent;
                    anchorMax.x = parentAnchorCenterX + halfPercent;
                    //apply anchors
                    rect.anchorMin = anchorMin;
                    rect.anchorMax = anchorMax;

                    //set left,right to 0;
                    offsetMin.x = 0f;
                    offsetMax.x = 0f;

                    rect.offsetMin = offsetMin;
                    rect.offsetMax = offsetMax;

                } 
                else
                {
                    float width = element.GetFloat(WIDTH);
                    rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
                }
            }
            if (element.HasDirtyProperty(HEIGHT))
            {
                float heightPercentage;
                bool isPercent = FlexUIUtil.TryGetPercentage(element.GetString(HEIGHT), out heightPercentage);
                if (isPercent)
                {
                    float halfPercent = heightPercentage/2.0f;
                    anchorMin.y = parentAnchorCenterX - halfPercent;
                    anchorMax.y = parentAnchorCenterX + halfPercent;
                    //apply anchors
                    rect.anchorMin = anchorMin;
                    rect.anchorMax = anchorMax;

                    //set top,bottom to 0;
                    offsetMin.y = 0f;
                    offsetMax.y = 0f;

                    rect.offsetMin = offsetMin;
                    rect.offsetMax = offsetMax;
                }
                else
                {
                    float height = element.GetFloat(HEIGHT);
                    rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
                }

            }
            
            #endregion

            #region pivot
            //pivot
            Vector2 pivot = rect.pivot;

            if (element.HasDirtyProperty(PIVOT))
            {
                TextAnchor p = TextAnchor.MiddleLeft;
                if (CommonProperty.AlignTable.ContainsKey(element.GetString(PIVOT)))
                {
                    p = CommonProperty.AlignTable[element.GetString(PIVOT)];
                }
                
                switch (p)
                {
                    case TextAnchor.UpperLeft:
                        pivot.x = 0f; pivot.y = 1f;
                        break;
                        
                    case TextAnchor.UpperCenter:
                        pivot.x = 0.5f; pivot.y = 1f;
                        break;
                        
                    case TextAnchor.UpperRight:
                        pivot.x = 1f; pivot.y = 1f;
                        break;
                        
                    case TextAnchor.MiddleLeft:
                        pivot.x = 0f; pivot.y = 0.5f;
                        break;
                        
                    case TextAnchor.MiddleCenter:
                        pivot.x = 0.5f; pivot.y = 0.5f;
                        break;
                        
                    case TextAnchor.MiddleRight:
                        pivot.x = 1f; pivot.y = 1f;
                        break;
                        
                    case TextAnchor.LowerLeft:
                        pivot.x = 0f; pivot.y = 0f;
                        break;
                        
                    case TextAnchor.LowerCenter:
                        pivot.x = 0.5f; pivot.y = 0f;
                        break;
                        
                    case TextAnchor.LowerRight:
                        pivot.x = 1f; pivot.y = 0f;
                        break;
                }
            }

            if (element.HasDirtyProperty(PIVOT_X))
            {
                pivot.x = element.GetFloat(PIVOT_X);
            }
            if (element.HasDirtyProperty(PIVOT_Y))
            {
                pivot.y = element.GetFloat(PIVOT_Y);
            }
            rect.pivot = pivot;
            #endregion

            #region rotation
            Vector3 rotation = rect.localRotation.eulerAngles;
            if (element.HasDirtyProperty(ROTATION))
            {
                string rotationStr = element.GetString(ROTATION);
                Vector3? r = FlexUIUtil.GetVector3FromString(rotationStr);
                if (r != null)
                {
                    rotation = (Vector3)r;
                }
            }
            if (element.HasDirtyProperty(ROTATION_X))
            {
                rotation.x = element.GetFloat(ROTATION_X);
            }
            if (element.HasDirtyProperty(ROTATION_Y))
            {
                rotation.y = element.GetFloat(ROTATION_Y);
            }
            if (element.HasDirtyProperty(ROTATION_Z))
            {
                rotation.z = element.GetFloat(ROTATION_Z);
            }
            rect.localRotation = Quaternion.Euler(rotation);
            #endregion

            #region scale
            Vector3 scale = rect.localScale;
            if (element.HasDirtyProperty(SCALE))
            {
                string scaleStr = element.GetString(SCALE);
                Vector3? s = FlexUIUtil.GetVector3FromString(scaleStr);
                if (s != null)
                {
                    scale = (Vector3)s;
                }
            }
            if (element.HasDirtyProperty(SCALE_X))
            {
                scale.x = element.GetFloat(SCALE_X);
            }
            if (element.HasDirtyProperty(SCALE_Y))
            {
                scale.y = element.GetFloat(SCALE_Y);
            }
            if(element.HasDirtyProperty(SCALE_Z))
            {
                scale.z = element.GetFloat(SCALE_Z);
            }
            rect.localScale = scale;
            #endregion
        }

    }

}