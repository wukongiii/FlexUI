#pragma warning disable 0219

using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


namespace FlexUI
{
    public class FlexUIUtil
    {
        public static Vector3? GetVector3FromString(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            char[] separator = new char[]{','};
            string[] parts = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            float[] values = new float[3]{0f, 0f, 0f};
            for (int i = 0; i < parts.Length && i < values.Length; i++)
            {
                values[i] = float.Parse(parts[i]);
            }
            Vector3 v = new Vector3(values [0], values [1], values [2]);
            return v;
        }

        public static Vector2? GetVector2FromString(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            char[] separator = new char[]{','};
            string[] parts = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            float[] values = new float[2]{0f, 0f};
            for (int i = 0; i < parts.Length && i < values.Length; i++)
            {
                values[i] = float.Parse(parts[i]);
            }
            Vector2 v = new Vector2(values [0], values [1]);
            return v;
        }

        public static Color HexToColor(string hex)
        {
            byte[] colorBytes = new byte[4];
            colorBytes [0] = colorBytes [1] = colorBytes [2] = 0;//b,g,r
            colorBytes [3] = 255;//a
            
            // read b,g,r,a
            for (int i = 0; i < 4 && hex.Length > 0; i++)
            {
                string currentColor = "";
                if (hex.Length > 1)
                {
                    currentColor = hex.Substring(hex.Length - 2);
                    hex = hex.Substring(0, hex.Length - 2);
                } else 
                {
                    currentColor = hex;
                    hex = "";
                }
                colorBytes[i] = byte.Parse(currentColor, System.Globalization.NumberStyles.HexNumber);
            }
            
            return new Color32(colorBytes[2], colorBytes[1], colorBytes[0], colorBytes[3]);
        }


        static Dictionary<string, Color> colorNameTable = new Dictionary<string, Color>()
        {
            {"black", Color.black},
            {"blue", Color.blue},
            {"clear", Color.clear},
            {"cyan", Color.cyan},
            {"gray", Color.gray},
            {"grey", Color.grey},
            {"magenta", Color.magenta},
            {"red", Color.red},
            {"white", Color.white},
            {"yellow", Color.yellow},
        };

        public static Color GetColorFromString(string colorStr)
        {
            if (colorNameTable.ContainsKey(colorStr.ToLower()))
            {
                return colorNameTable[colorStr.ToLower()];
            }

            return HexToColor(colorStr);
        }

        public static bool TryGetPercentage(string value, out float percentage)
        {
            percentage = 0f;

            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            if (value [value.Length - 1] == '%')
            {
                string valueBody = value.Substring(0, value.Length -1);
                int number;
                bool isValid = int.TryParse(valueBody, out number);
                if (isValid)
                {
                    percentage = number/100.0f;
                }
                return isValid;
            }
            return false;
        }

		//---------------transfor section---------------
		public static void AddChild(GameObject parent, GameObject child, bool resetTransform = true, bool inheritHideFlagsFromParent = true)
		{
			child.transform.SetParent(parent.transform);
			ChangeAllChildrenLayer(child, parent.layer);
			
			if (inheritHideFlagsFromParent)
			{
				child.hideFlags = parent.hideFlags;
			}
			
			if (resetTransform)
			{
				ResetTransform(child.transform);
			}
		}
		
		public static void RemoveFromParent(GameObject child)
		{
			child.transform.parent = null;
			GameObject.Destroy(child);
		}
		
		public static void RemoveAllChildren(GameObject go)
		{
			RemoveAllChildren(go.transform);
		}
		
		public static void RemoveAllChildren(Transform transform)
		{
			if (transform == null)
			{
				return;
			}
			
			while (transform.childCount > 0)
			{
				GameObject child = transform.GetChild(0).gameObject;
				child.transform.SetParent(null);
				GameObject.Destroy(child);
			}
		}
		
		public static void ResetTransform(GameObject go)
		{
			ResetTransform(go.transform);
		}
		
		public static void ResetTransform(Transform trans, bool resetScale = true, bool resetLocalPostion = true, bool resetRotation = true)
		{
			if (resetScale)
			{
				trans.localScale = Vector3.one;
			}
			if (resetLocalPostion)
			{
				trans.localPosition = Vector3.zero;
			}
			if (resetRotation)
			{
				trans.localRotation = new Quaternion(0f, 0f, 0f, 0f);
			}
		}
		
		public static void ForEachChild(Transform parent, System.Action<Transform> callback)
		{
			for (int i = 0; i < parent.childCount; i++)
			{
				try
				{
					callback(parent.GetChild(i));
				}
				catch(System.Exception e)
				{
					Debug.LogException(e);
				}
			}
		}
		
		/// <summary>
		/// Gets the name of the child by child's path, like "child" and "path/child"
		/// </summary>
		/// <returns>The child by name.</returns>
		/// <param name="parent">Parent.</param>
		/// <param name="path">Path.</param>
		public static GameObject GetChildByName(GameObject parent, string path)
		{
			string[] parts = path.Split("/".ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
			
			Transform searchingTrans = parent.transform;
			Transform child = null;
			for (int i = 0; i < parts.Length; i++)
			{
				searchingTrans = searchingTrans.Find(parts[i]);
				if (searchingTrans == null)
				{
					return null;
				}
			}
			
			return searchingTrans.gameObject;
		}
		
		public static string GetGameObjectPath(GameObject go)
		{
			return GetGameObjectPath(go.transform);
		}
		public static string GetGameObjectPath(Transform transform)
		{
			string path = transform.name;
			while (transform.parent != null)
			{
				transform = transform.parent;
				path = transform.name + "/" + path;
			}
			return path;
		}
		
		public static void ChangeAllChildrenLayer(GameObject go, int layer)
		{
			foreach (Transform trans in go.GetComponentsInChildren<Transform>(true)) {
				trans.gameObject.layer = layer;
			}
		}
		private static  Transform childTransform;
		/// <summary>
		/// Finds the name in childen
		/// </summary>
		/// <returns>The name in child.</returns>
		/// <param name="parent">Parent.</param>
		/// <param name="childName">Child name.</param>
		public static Transform GetTransformInChilden(Transform parent,string childName)
		{  
			if (parent.name.Equals (childName))
			{
				childTransform =parent;
				return childTransform;
			}
			if(parent.childCount >0)
			{  
				for(int i=0;i<parent.transform.childCount;i++)
				{   
					GetTransformInChilden(parent.GetChild(i),childName); 
				}
			}
			
			return childTransform;
		}
		
		public static void CloneTransform(Transform from, Transform to, bool includeParent = true)
		{
			if (includeParent)
			{
				to.SetParent(from.parent);
			}
			
			to.localScale = from.localScale;
			to.localRotation = from.localRotation;
			to.localPosition = from.localPosition;
			
		}

    }

}