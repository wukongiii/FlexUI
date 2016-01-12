using System;
using System.Collections.Generic;

using UnityEngine;


public class FontManager
{
    static Dictionary<string, Font> fonts = new Dictionary<string, Font>();

    public static void RegisterFont(string fontName, Font font)
    {
        fonts [fontName] = font;

    }

    public static Font GetFont(string fontName)
    {
        if (fonts.ContainsKey(fontName))
        {
            return fonts[fontName];
        }
        return null;
    }

    static FontManager()
    {
        RegisterFont("Arial", Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font);
    }

}