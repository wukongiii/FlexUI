using UnityEngine;
using UnityEngine.UI;

using System;
using System.Reflection;
using System.Xml;
using System.Collections.Generic;

namespace catwins.flexui
{

    public class Element:Object
    {
        public const string TAG = "element";
        public const string VALUE = "value";

        internal Document document;

        //gameobject
        protected GameObject gameObject;
        public GameObject GameObject
        {
            get{ return gameObject;}
        }

        //recttransform
        protected RectTransform rectTransform;
        public RectTransform RectTransform
        {
            get{ return rectTransform;}
        }

        //data
        public Dictionary<string, object> Data
        {
            get{ return data;}
        }

        //constructers
        public Element(string tagName):this()
        {
            SetProperty(Tag.TAG, tagName);
        }

        public Element()
        {
            InitElement();
            LoadDefaultMods();
        }

        protected virtual void InitElement()
        {
            gameObject = new GameObject(TAG, typeof(RectTransform));
            rectTransform = gameObject.GetComponent<RectTransform>();
        }

        protected virtual void LoadDefaultMods()
        {
            AddMod<ModNameID>();
        }

        public Element Duplicate()
        {
            Element newElement = (Element)Activator.CreateInstance(GetType());
            newElement.document = document;
            newElement.data = new Dictionary<string, object>(data);
            newElement.dirtyProperties = new HashSet<string>(data.Keys);


            if (parent != null)
            {
                parent.AddChild(newElement);
            }
            for (int i = 0; i < children.Count; i++)
            {
                Element newChild = children[i].Duplicate();
                newElement.AddChild(newChild);
            }

            return newElement;
        }

        public override string ToString()
        {
            return GetString(Tag.TAG);
        }



        public void Update()
        {
            if (isDirty)
            {
                for (int i = 0; i < children.Count; i++)
                {
                    children[i].Update();
                }
                
                UpdateMods();
                
                dirtyProperties.Clear();
            }
        }
        
        protected void UpdateMods()
        {
            foreach (string property in dirtyProperties)
            {
                Type modType = Mod.GetModTypeByProperty(property);
                if (modType == null)
                {
                    continue;
                }
                if (!HasMod(modType))
                {
                    AddMod(modType);
                }
            }
            
            foreach (var item in mods)
            {
                item.Value.Update();
            }
            
        }

        public virtual void Dispose()
        {
            for (int i = 0; i < children.Count; i++)
            {
                children[i].Dispose();
            }
            
            foreach (var item in mods)
            {
                item.Value.Dispose();
            }
            
            mods = null;
            children = null;
            document = null;
            rectTransform = null;
            parent = null;
            data = null;
            dirtyProperties = null;
            GameObject.Destroy(gameObject);
        }

        //-------------object tree section-----------
        protected Element parent;
        public Element Parent
        {
            get{ return parent;}
        }
        
        protected List<Element> children = new List<Element>();
        public void AddChild(Element child)
        {
            if (child.parent != null)
            {
                child.parent.RemoveChild(child);
            }
            children.Add(child);
            child.parent = this;
            FlexUIUtil.AddChild(gameObject, child.gameObject);
        }
        
        public void RemoveChild(Element child)
        {
            if (children.IndexOf(child) != -1)
            {
                children.Remove(child);
                child.parent = null;
                child.gameObject.transform.SetParent(null);
            } else
            {
                Debug.Log("FlexUI: Element: Does not contains this child:" + child);
            }
        }
        
        public List<Element> Children
        {
            set{ children = value;}
            get{ return children;}
        }

        public Element GetChildAt(int index)
        {
            if (index < 0 || index >= children.Count)
            {
                return null;
            }
            return children [index];
        }

        public Element GetChildByID(string id)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].HasProperty(ModNameID.ID))
                {
                    if (id == children[i].GetString(ModNameID.ID))
                    {
                        return children[i];
                    }
                }
            }
            return null;
        }

        //-------------module section-------------------

        private static Type baseModType = typeof(BaseMod);
        private static MethodInfo methodAdd = baseModType.GetMethod("Add",BindingFlags.Instance|BindingFlags.NonPublic|BindingFlags.DeclaredOnly);
        private static MethodInfo methodRemove = baseModType.GetMethod("Remove",BindingFlags.Instance|BindingFlags.NonPublic|BindingFlags.DeclaredOnly);

        protected Dictionary<Type, BaseMod> mods = new Dictionary<Type, BaseMod>();

        internal bool HasMod<T>() where T:BaseMod
        {
            return mods.ContainsKey(typeof(T));
        }

        internal bool HasMod(Type modType)
        {
            return mods.ContainsKey(modType);
        }

        internal BaseMod AddMod<T>() where T:BaseMod
        {
            return AddMod(typeof(T));
        }

        internal BaseMod AddMod(Type modType)
        {
            BaseMod mod = null;
            if (!HasMod(modType))
            {
                mod = (BaseMod)Activator.CreateInstance(modType);
                methodAdd.Invoke(mod, new object[]{this});
                mods[modType] = mod;
            }
            mod = mods [modType];
            return mod;
        }
        internal void RemoveMod<T>() where T:BaseMod
        {
            RemoveMod(typeof(T));
        }
        internal void RemoveMod(Type modType)
        {
            if (HasMod(modType))
            {
                BaseMod mod = mods[modType];
                methodRemove.Invoke(mod, null);
                
                mods.Remove(modType);
            }
        }

        internal T GetMod<T>() where T:BaseMod
        {
            return (T)GetMod(typeof(T));
        }

        internal BaseMod GetMod(Type modType)
        {
            if (mods.ContainsKey(modType))
            {
                return mods[modType];
            }
            return null;
        }
        
        //----------property section-----------------
        
        
        public bool IsDirty
        {
            get { return isDirty;}
        }
        
        public void SetProperty(string property, object value, bool update = false)
        {
            data [property] = value;
            dirtyProperties.Add(property);
            
            if (update)
            {
                Update();
            }
        }
        
        public bool IsPropertyDirty(string property)
        {
            return dirtyProperties.Contains(property);
        }
        
        public bool HasProperty(string property)
        {
            return data.ContainsKey(property);
        }
        
        public bool HasDirtyProperty(string property)
        {
            return HasProperty(property) && IsPropertyDirty(property);
        }
        
        
        public object GetProperty(string property)
        {
            //Not check HasProperty() first , check outside.
            return data[property];
        }
        
        public T GetProperty<T>(string property)
        {
            return (T)data[property];
        }
        
        public string GetString(string property, bool caseSensitive = false)
        {
            if (caseSensitive)
            {
                return GetProperty<string>(property);
            } else
            {
                return GetProperty<string>(property).ToLower();
            }
        }

        public int GetInt(string property)
        {
            return int.Parse(GetString(property));
        }

        public float GetFloat(string property)
        {
            return float.Parse(GetString(property));
        }

        public bool GetBool(string property)
        {
            return bool.Parse(GetString(property));
        }

        public Color GetColor(string property)
        {
            return FlexUIUtil.GetColorFromString(GetString(property));
        }


        //--------common properties---------

        public string Name
        {
            get
            {
                if (data.ContainsKey(ModNameID.NAME))
                {
                    return data[ModNameID.NAME].ToString();
                }
                return null;
            }

        }

        public string ID
        {
            get
            {
                if (data.ContainsKey(ModNameID.ID))
                {
                    return data[ModNameID.ID].ToString();
                }
                return null;
            }
        }

    }
}