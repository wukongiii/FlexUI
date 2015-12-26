
using System;
using System.Collections.Generic;

using UnityEngine;

namespace FlexUI
{
    // sprite@res://a/b/c
    public class URI
    {
        public const string TAG = "uri";


        public string uri = null;
        public string relativePath = null;

        public ContentType contentType = ContentType.UNKNOWN;
        public LocationType locationType = LocationType.UNKNOWN;

        private URI(string uriStr)
        {
            this.uri = uriStr;
            Parse();
        }

        public static URI GetFromString(string uriStr)
        {
            if (string.IsNullOrEmpty(uriStr))
            {
                return null;
            }
            return new URI(uriStr);
        }

        private void Parse()
        {
            //get head and relative path
            string separator = "://";

            int index = uri.IndexOf(separator);
            //There is no head in the uri
            if (index == -1 || index + separator.Length >= uri.Length - 1)
            {
                //throw new Exception("FlexUI: Invalid URI: " + uri);
                relativePath = uri;
            } else
            {
                string head = uri.Substring(0, index);
                relativePath = uri.Substring(index + separator.Length);
                
                string contentTypeStr = "";
                string locationTypeStr = head;

                index = head.IndexOf('@');
                //There is no content type description in head, then by default the head string means location type description.
                if (index == -1)
                {

                } else
                {
                    contentTypeStr = head.Substring(0, index).ToLower();
                    //If '@' is at the last position
                    if (index != head.Length - 1)
                    {
                        //throw new Exception("FlexUI: Invalid URI: Head format wrong: " + head);
                        locationTypeStr = head.Substring(index + 1).ToLower();
                    } else 
                    {
                        locationTypeStr = "";
                    }

                }
                
                contentType = ContentHead.GetContentTypeByName(contentTypeStr);
                locationType = LocationHead.GetLocationTypeByName(locationTypeStr);
            }
        }

    }

    //-------location----------
    public enum LocationType
    {
        UNKNOWN,
        RES,
        HTTP,
    }

    class LocationHead
    {
        public const string RES = "res";
        public const string HTTP = "http";

        static Dictionary<string, LocationType> tab = new Dictionary<string, LocationType>() {
            {RES, LocationType.RES},
            {HTTP, LocationType.HTTP}
        };

        internal static LocationType GetLocationTypeByName(string name)
        {
            if (tab.ContainsKey(name))
            {
                return tab[name];
            }
            return LocationType.UNKNOWN;
        }
    }


    //--------content-----------
    public enum ContentType
    {
        UNKNOWN,
        TEXTURE,
        SPRITE,
        SPRITE_IN_SHEET,
        PREFAB
    }

    class ContentHead
    {
        public const string SPRITE = "sprite";
        public const string TEXTURE = "texture";
        public const string SPRITE_IN_SHEET = "spriteinsheet";
        public const string PREFAB = "prefab";
        
        static Dictionary<string, ContentType> tab = new Dictionary<string, ContentType>() {
            {SPRITE, ContentType.SPRITE},
            {TEXTURE, ContentType.TEXTURE},
            {SPRITE_IN_SHEET, ContentType.SPRITE_IN_SHEET},
            {PREFAB, ContentType.PREFAB}
        };
        
        internal static ContentType GetContentTypeByName(string name)
        {
            if (tab.ContainsKey(name))
            {
                return tab[name];
            }
            return ContentType.UNKNOWN;
        }
        
    }

}
