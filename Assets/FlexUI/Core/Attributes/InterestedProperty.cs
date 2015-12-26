using System;

namespace FlexUI
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)] 
    public class InterestedProperty:Attribute
    {
        public string[] Properties;
        public InterestedProperty(params string[] properties)
        {
            Properties = properties;
        }
    }


}