using System;
using System.Collections.Generic;
using UnityEngine;

namespace catwins.flexui
{

    public class ModScript:BaseMod
    {
        public const string SCRIPT = "script";

		public const string META_CLASS_NAME = "classname";

        public override void Update()
        {
            if (element.HasDirtyProperty(Element.VALUE))
            {
                string scriptStr = element.GetString(Element.VALUE, true);

				if (element.HasDirtyProperty (META_CLASS_NAME)) {
					string className = element.GetString (META_CLASS_NAME, true);

					string classWrap = "public class {0} \n{ \n {1} \n}";
					classWrap = classWrap.Replace ("{0}", className);
					classWrap = classWrap.Replace ("{1}", scriptStr);
					element.document.ScriptEngine.BuildFile (className, classWrap);
				} 
				else 
				{
					element.document.ScriptEngine.Execute(scriptStr);
				}
                
            }
            
        }
    }



    
}
