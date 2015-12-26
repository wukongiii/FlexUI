using System;
using System.Collections.Generic;
using UnityEngine;

namespace catwins.flexui
{

    public class ModScript:BaseMod
    {
        public const string SCRIPT = "script";
        public const string TYPE = "type";
        public const string TYPE_CLASS = "class";
        public const string TYPE_INSTANT = "instant";

        public override void Update()
        {
            if (element.HasDirtyProperty(Element.VALUE))
            {
                string scriptStr = element.GetString(Element.VALUE, true);
                if (element.HasDirtyProperty(TYPE))
                {
                    string codeType = element.GetString(TYPE);
                    if (codeType == TYPE_CLASS)
                    {
                        element.document.ScriptEngine.BuildFile("test", scriptStr);
                    } else if (codeType == TYPE_INSTANT)
                    {
                        element.document.ScriptEngine.Execute(scriptStr);
                    }
                } else 
                {
                    element.document.ScriptEngine.Execute(scriptStr);
                }
            }
            
        }
    }



    
}
