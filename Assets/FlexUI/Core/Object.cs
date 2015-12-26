using System;
using System.Collections.Generic;

namespace FlexUI
{
    public class Object
    {
        protected Dictionary<string, object> data = new Dictionary<string, object>();
        protected HashSet<string> dirtyProperties = new HashSet<string>();
        protected bool isDirty
        {
            get
            {
                return dirtyProperties.Count > 0;
            }
        }

    }
}