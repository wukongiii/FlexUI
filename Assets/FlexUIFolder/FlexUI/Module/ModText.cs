using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using URI = catwins.flexui.URI;

namespace catwins.flexui
{
    public class ModText:BaseMod
    {
        public const string TEXT = "text";
        public const string TEXT_KEY = "textkey";

        public const string TEXT_ALIGN = "textalign";
        public const string RICH_TEXT = "richtext";
        public const string FONT = "font";
        public const string SIZE = "size";
        public const string COLOR = "color";
        public const string SPACING = "spacing";

        //font style
        public const string STYLE = "style";
        public const string STYLE_NORMAL = "normal";
        public const string STYLE_BOLD = "bold";
        public const string STYLE_ITALIC = "italic";
        public const string STYLE_BOLD_ITALIC = "bolditalic";
        static Dictionary<string, FontStyle> styleTable = new Dictionary<string, FontStyle>(){
            {STYLE_NORMAL, FontStyle.Normal},
            {STYLE_BOLD, FontStyle.Bold},
            {STYLE_ITALIC, FontStyle.Italic},
            {STYLE_BOLD_ITALIC, FontStyle.BoldAndItalic}
        };

        //overflow
        public const string HORIZONTAL_OVERFLOW = "hoverflow";
        public const string VERTICAL_OVERFLOW = "voverflow";
//        public const string OVERFLOW_MODE_TRUNCATE = "truncate";
//        public const string OVERFLOW_MODE_OVERFLOW = "overflow";

        //bestfit
        public const string BEST_FIT = "bestfit";

        Text text = null;
        
        protected override void OnAdd()
        {
            text = element.GameObject.GetComponentInChildren<Text>();
            if (text == null)
            {
                text = element.GameObject.AddComponent<Text>();
            }
        }
        
        public override void Update()
        {
            //text
            if (element.HasDirtyProperty(TEXT))
            {
                string textValue = text.text;
                textValue = element.GetString(TEXT, true);
                text.text = textValue;
            }

			//localization
//            if (element.HasDirtyProperty(TEXT_KEY))
//            {
//                LocalizedAndStyledText localizedAndStyledText = element.GameObject.GetComponent<LocalizedAndStyledText>();
//                if (localizedAndStyledText == null)
//                {
//                    localizedAndStyledText = element.GameObject.AddComponent<LocalizedAndStyledText>();
//                }
//                localizedAndStyledText.key = element.GetInt(TEXT_KEY);
//                localizedAndStyledText.UpdateContent();
//            }

            //richtext
            if (element.HasDirtyProperty(RICH_TEXT))
            {
                bool richText = element.GetBool(RICH_TEXT);
                text.supportRichText = richText;
            }

            //font name
            //by default : Arial
            string fontName = "Arial";
            if (element.HasDirtyProperty(FONT))
            {
                fontName = element.GetString(FONT);
            }
            Font font = FontManager.GetFont(fontName);
            text.font = font;

            //font size
            if (element.HasDirtyProperty(SIZE))
            {
                int fontSize = element.GetInt(SIZE);
                text.fontSize = fontSize;
            }

            //font color
            if (element.HasDirtyProperty(COLOR))
            {
                Color textColor = element.GetColor(COLOR);
                text.color = textColor;
            }

            //spacing
            if (element.HasDirtyProperty(SPACING))
            {
                float spacing = element.GetFloat(SPACING);
                text.lineSpacing = spacing;
            }

            //font style
            if (element.HasDirtyProperty(STYLE))
            {
                string style = element.GetString(STYLE);
                if (styleTable.ContainsKey(style))
                {
                    FontStyle fontStyle = FontStyle.Normal;
                    fontStyle = styleTable[style];
                    text.fontStyle = fontStyle;
                }
            }

            //align
            if (element.HasDirtyProperty(TEXT_ALIGN))
            {
                if (CommonProperty.AlignTable.ContainsKey(element.GetString(TEXT_ALIGN)))
                {
                    TextAnchor alignment = TextAnchor.MiddleLeft;
                    alignment = CommonProperty.AlignTable[element.GetString(TEXT_ALIGN)];
                    text.alignment = alignment;
                }
            }

            //Wrap mode
            if (element.HasDirtyProperty(HORIZONTAL_OVERFLOW))
            {
                HorizontalWrapMode hWrapMode = HorizontalWrapMode.Overflow;
                bool overflow = element.GetBool(HORIZONTAL_OVERFLOW);
                if (overflow)
                {
                    hWrapMode = HorizontalWrapMode.Overflow;
                } else 
                {
                    hWrapMode = HorizontalWrapMode.Wrap;
                }
                text.horizontalOverflow = hWrapMode;
            }
            if (element.HasDirtyProperty(VERTICAL_OVERFLOW))
            {
                VerticalWrapMode vWrapMode = VerticalWrapMode.Overflow;
                bool overflow = element.GetBool(VERTICAL_OVERFLOW);
                if (overflow)
                {
                    vWrapMode = VerticalWrapMode.Overflow;
                } else {
                    vWrapMode = VerticalWrapMode.Truncate;
                }
                text.verticalOverflow = vWrapMode;
            }

            //best fit
            if (element.HasDirtyProperty(BEST_FIT))
            {
                bool bestFit = element.GetBool(BEST_FIT);
                text.resizeTextForBestFit = bestFit;
            }

        }
    }
    
}