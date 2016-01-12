using System;
using System.Reflection;
using System.Collections.Generic;

using UnityEngine;

namespace catwins.flexui
{
    public class Tag
    {
        public const string TAG = "tag";

        static Tag()
        {
            RegisterImps(
                typeof(TagRoot),
                typeof(TagImage),
                typeof(TagDiv),
                typeof(TagText),
                typeof(TagInteractable),
                typeof(TagButton),
                typeof(TagToggle),
                typeof(TagToggleGroup),
                typeof(TagScroll),
                typeof(TagScript)
                );
        }



        private static Dictionary<string, Type> tagImps;// Tag implementations.
        private static void RegisterImps(params Type[] tags)
        {
            tagImps = new Dictionary<string, Type>();

            for (int i = 0; i < tags.Length; i++)
            {
                Type tagType = tags[i];
                if (!tagType.IsSubclassOf(typeof(Element)))
                {
                    Debug.LogWarning("Type: " + tagType.Name + " is not a tag.");
                    continue;
                }

                FieldInfo field = tagType.GetField("TAG", BindingFlags.Public|BindingFlags.Static);
                if (field == null)
                {
                    Debug.LogWarning("Tag: " + tagType.Name + " dose not define TAG.");
                    continue;
                }
                string tagValue = (string)field.GetValue(null);

                if (string.IsNullOrEmpty(tagValue))
                {
                    Debug.LogWarning("Tag: " + tagType.Name + " defines a bad tag name.");
                    continue;
                }

                tagValue = tagValue.ToLower();
                if (tagImps.ContainsKey(tagValue))
                {
                    Debug.Log("Tag: " + tagValue + " has registered by " + tagImps[tagValue].Name + ".");
                }

//                Debug.Log("Tag: " + tagValue + " has registered to " + tagType.Name);
                tagImps[tagValue] = tagType;
            }
        }

        public static bool IsTagRegistered(string tagName)
        {
            return tagImps.ContainsKey(tagName.ToLower());
        }

        public static Type GetTagType(string tagName)
        {
            if (IsTagRegistered(tagName))
            {
                return tagImps[tagName.ToLower()];
            }
            return null;
        }


    }

}