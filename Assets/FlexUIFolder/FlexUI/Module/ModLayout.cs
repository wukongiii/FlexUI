
using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace catwins.flexui
{
    [InterestedProperty(LAYOUT)]
    public class ModLayout:BaseMod
    {
        public const string LAYOUT = "layout";

        public const string CHILD_ALIGN = "childalign";
        //layouts
        public const string HORIZONTAL = "horizontal";
        public const string VERTICAL = "vertical";
        public const string GRID = "grid";

        //parameters for common layout
//        public const string LAYOUT_PRIORITY = "layoutpriority";
        public const string PADDING = "padding";
		public const string PADDINGTOP = "paddingtop";
		public const string PADDINGBOTTOM = "paddingbottom";
		public const string PADDINGLEFT = "paddingleft";
		public const string PADDINGRIGHT = "paddingright";

        //parameters for horizontal and vertical layout
        public const string FORCE = "force";
        public const string FORCE_WIDTH = "forcewidth";
        public const string FORCE_HEIGHT = "forceheight";
        public const string SPACING = "spacing";

        //parameters for grid layout
        public const string CELL_SIZE = "cellsize";

//        public const string SPACINGX = "spacingx";
//        public const string SPACINGY = "spacingy";

        public const string START_CORNER = "startcorner";
        public static Dictionary<string, GridLayoutGroup.Corner> startCornerTable = new Dictionary<string, GridLayoutGroup.Corner>() {
            {CommonProperty.UPPER_LEFT, GridLayoutGroup.Corner.UpperLeft},
            {CommonProperty.UPPER_RIGHT, GridLayoutGroup.Corner.UpperRight},
            {CommonProperty.LOWER_LEFT, GridLayoutGroup.Corner.LowerLeft},
            {CommonProperty.LOWER_RIGHT, GridLayoutGroup.Corner.LowerRight}
        };


        public const string START_AXIS = "startaxis";
        public const string AXIS_HORIZONTAL = "horizontal";
        public const string AXIS_VERTICAL = "vertical";
        public static Dictionary<string, GridLayoutGroup.Axis> startAxisTable = new Dictionary<string, GridLayoutGroup.Axis>() {
            {AXIS_HORIZONTAL, GridLayoutGroup.Axis.Horizontal},
            {AXIS_VERTICAL, GridLayoutGroup.Axis.Vertical}
        };

        public const string CONSTRAINT = "constraint";
        public const string CONSTRAINT_FLEXIBLE = "flexible";
        public const string CONSTRAINT_FIXED_COLUMN = "column";
        public const string CONSTRAINT_FIXED_ROW = "row";
        public static Dictionary<string, GridLayoutGroup.Constraint> constraintTable = new Dictionary<string, GridLayoutGroup.Constraint>() {
            {CONSTRAINT_FLEXIBLE, GridLayoutGroup.Constraint.Flexible},
            {CONSTRAINT_FIXED_COLUMN, GridLayoutGroup.Constraint.FixedColumnCount},
            {CONSTRAINT_FIXED_ROW, GridLayoutGroup.Constraint.FixedRowCount}
        };
        public const string CONSTRAINT_COUNT = "constraintcount";


        protected override void OnAdd()
        {

        }

        public override void Update()
        {
            string layout = element.Data [LAYOUT].ToString();

            if (layout == HORIZONTAL || layout == VERTICAL)
            {
                ProcessHorizontalOrVerticalLayout(layout);
            } else if (layout == GRID)
            {
                ProcessGridLayout();
            }


        }

        private void ProcessHorizontalOrVerticalLayout(string layout)
        {
            HorizontalOrVerticalLayoutGroup layoutGroup = null;

            if (layout == HORIZONTAL)
            {
                layoutGroup = element.GameObject.GetComponent<HorizontalLayoutGroup>();
                if (layoutGroup == null)
                {
                    layoutGroup = element.GameObject.AddComponent<HorizontalLayoutGroup>();
                }
                
            } else if (layout == VERTICAL)
            {
                layoutGroup = element.GameObject.GetComponent<VerticalLayoutGroup>();
                if (layoutGroup == null)
                {
                    layoutGroup = element.GameObject.AddComponent<VerticalLayoutGroup>();
                }
            }


            if (element.HasDirtyProperty(FORCE))
            {
                bool force = element.GetBool(FORCE);
                if (!force)
                {
                    element.SetProperty(FORCE_WIDTH, "false");
                    element.SetProperty(FORCE_HEIGHT, "false");
                } else {
                    element.SetProperty(FORCE_WIDTH, "true");
                    element.SetProperty(FORCE_HEIGHT, "true");
                }
            }
            //force expand width/height
            if (element.HasDirtyProperty(FORCE_WIDTH))
            {
                bool forceExpandWidth = element.GetBool(FORCE_WIDTH);
                layoutGroup.childForceExpandWidth = forceExpandWidth;
            }
            if (element.HasDirtyProperty(FORCE_HEIGHT))
            {
                bool forceExpandHeight = element.GetBool(FORCE_HEIGHT);
                layoutGroup.childForceExpandHeight = forceExpandHeight;
            }

            //spacing
            if (element.HasDirtyProperty(SPACING))
            {
                float spacing = element.GetFloat(SPACING);
                layoutGroup.spacing = spacing;
            }

			if(element.HasDirtyProperty(PADDINGTOP))
			{
				int vo = element.GetInt(PADDINGTOP);
				layoutGroup.padding.top =  vo;
			}

			if(element.HasDirtyProperty(PADDINGBOTTOM))
			{
				int vo = element.GetInt(PADDINGBOTTOM);
				layoutGroup.padding.bottom =  vo;
			}

			if(element.HasDirtyProperty(PADDINGLEFT))
			{
				int vo = element.GetInt(PADDINGLEFT);
				layoutGroup.padding.left =  vo;
			}

			if(element.HasDirtyProperty(PADDINGRIGHT))
			{
				int vo = element.GetInt(PADDINGRIGHT);
				layoutGroup.padding.right =  vo;
			}



            //align
            if (element.HasDirtyProperty(CHILD_ALIGN))
            {
                if (CommonProperty.AlignTable.ContainsKey(element.GetString(CHILD_ALIGN)))
                {
                    TextAnchor childAlignment = TextAnchor.MiddleLeft;
                    childAlignment = CommonProperty.AlignTable[element.GetString(CHILD_ALIGN)];
                    layoutGroup.childAlignment = childAlignment;
                }
            }

        }


        private void ProcessGridLayout()
        {
            GridLayoutGroup layoutGroup = element.GameObject.GetComponent<GridLayoutGroup>();
            if (layoutGroup == null)
            {
                layoutGroup = element.GameObject.AddComponent<GridLayoutGroup>();
            }

            //cellsize
            if (element.HasDirtyProperty(CELL_SIZE))
            {
                string cellSizeStr = element.GetString(CELL_SIZE);
                Vector2? cellSize = FlexUIUtil.GetVector2FromString(cellSizeStr);
                if (cellSize != null)
                {
                    layoutGroup.cellSize = (Vector2)cellSize;
                }
            }

            //cell spacing
            if (element.HasDirtyProperty(SPACING))
            {
                string spacingStr = element.GetString(SPACING);
                Vector2? spacing = FlexUIUtil.GetVector2FromString(spacingStr);
                if (spacing != null)
                {
                    layoutGroup.spacing = (Vector2)spacing;
                }
            }

            if (element.HasDirtyProperty(START_CORNER))
            {
                string startCornerStr = element.GetString(START_CORNER);
                if (startCornerTable.ContainsKey(startCornerStr))
                {
                    layoutGroup.startCorner = startCornerTable[startCornerStr];
                }
            }

            if (element.HasDirtyProperty(START_AXIS))
            {
                string startAxisStr = element.GetString(START_AXIS);
                if (startAxisTable.ContainsKey(startAxisStr))
                {
                    layoutGroup.startAxis = startAxisTable[startAxisStr];
                }
            }

            if (element.HasDirtyProperty(CONSTRAINT))
            {
                string constraintStr = element.GetString(CONSTRAINT);
                if (constraintTable.ContainsKey(constraintStr))
                {
                    layoutGroup.constraint = constraintTable[constraintStr];
                }
            }

            if (element.HasDirtyProperty(CONSTRAINT_COUNT))
            {
                int constraintCount = element.GetInt(CONSTRAINT_COUNT);
                layoutGroup.constraintCount = constraintCount;
            }
        }
    }

}