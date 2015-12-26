using System;
using System.Collections.Generic;

using UnityEngine;

namespace catwins.flexui
{
    public class Mod
    {


        static Mod()
        {
            RegMods(
                typeof(ModNameID),
                typeof(ModRect),
                typeof(ModImage),
                typeof(ModLayout),
                typeof(ModText),
                typeof(ModContentSizeFitter),
                typeof(ModLayoutElement),
                typeof(ModInteractable),
                typeof(ModToggleGroup),
                typeof(ModMask),
                typeof(ModScript)
                );
        }

        private static Dictionary<string, Type> propertyModType = new Dictionary<string, Type>();

        private static void RegMods(params Type[] modTypes)
        {
            for (int i = 0; i < modTypes.Length; i++)
            {
                Type modType = modTypes[i];
                var interestedPropertyAtts = modType.GetCustomAttributes(typeof(InterestedProperty), true);

                if (interestedPropertyAtts.Length > 0)
                {
                    InterestedProperty interestedPropertyAtt = interestedPropertyAtts [0] as InterestedProperty;
                    for (int j = 0; j < interestedPropertyAtt.Properties.Length; j++)
                    {
                        RegInterestingProperty(interestedPropertyAtt.Properties[j], modType);
                    }
                } else {
                    //throw new Exception("[InterestedProperty] not set in "+ modType.Name +".");
                }

            }
        }


        public static void RegInterestingProperty<T>(string property) where T:BaseMod
        {
            RegInterestingProperty(property, typeof(T));
        }

        public static void RegInterestingProperty(string property, Type modType)
        {
            if (HasModIntestingProperty(property))
            {
                Debug.LogWarning("Warning! FlexUI Mod: Same property:" + property + " has another interester:" + modType.Name);
            }

//            Debug.Log("Property: " + property + " has registered.");
            propertyModType[property] = modType;
        }

        public static bool HasModIntestingProperty(string property)
        {
            return propertyModType.ContainsKey(property);
        }

        public static Type GetModTypeByProperty(string property)
        {
            if (propertyModType.ContainsKey(property))
            {
                return propertyModType[property];
            }
            return null;
        }

    }

}
