using UnityEngine;

using System;
using System.Collections.Generic;

namespace FlexUI
{
    public class Document
    {
        static IDocParser parser;
        public static IDocParser DocParser
        {
            get
            {
                if (parser == null)
                {
                    parser = new XMLDocParser();
                }
                return parser;
            }
        }

        private GameObject container = null;
        private string docContent;

        public Element Root;

        public Document()
        {
            InitScriptEngine();
        }
        public Document(GameObject container):this()
        {
            this.container = container;
        }
        public Document(string docStr):this()
        {
            Content = docStr;
        }
        public Document(GameObject container, string docStr):this()
        {
            this.container = container;
            Content = docStr;
        }


        public GameObject Container
        {
            set { this.container = value;}
        }

        public string Content
        {
            set
            {
                Clear();

                docContent = value;
                Root = CreateElement(docContent);
                if (container != null)
                {
                    FlexUIUtil.AddChild(container, Root.GameObject);
                }

                Root.Update();
            }

        }

        //element
        public Element CreateElement(string elementStr)
        {
            return DocParser.Parse(elementStr, Root, this);
        }

        private Dictionary<string, List<Element>> elements = new Dictionary<string, List<Element>>();
        internal void RegisterElement(string id, Element element)
        {
            UnregisgerElement(id, element);
            if (!elements.ContainsKey(id))
            {
                elements[id] = new List<Element>();
            }

            elements [id].Add(element);
        }

        internal void UnregisgerElement(string id, Element element)
        {
            if (elements.ContainsKey(id))
            {
                elements[id].Remove(element);
            }
        }

        public Element GetElementByID(string id)
        {
            if (elements.ContainsKey(id) && elements[id].Count > 0)
            {
                return elements[id][0];
            }
            return null;
        }

        public T GetElementByID<T>(string id) where T:Element
        {
            Element e = GetElementByID(id);
            if (e == null)
            {
                return null;
            }
            if (e is T)
            {
                return (T)e;
            }
            return null;
        }

        public List<Element> GetElementsByID(string id)
        {
            if (elements.ContainsKey(id))
            {
                return elements[id];
            }
            return null;
        }

        public List<T> GetElementsByID<T>(string id) where T:Element
        {
            List<T> typedList = new List<T>();
            var sameIDElements = GetElementsByID(id);
            if (sameIDElements == null || sameIDElements.Count == 0)
            {
                return null;
            }

            for (int i = 0; i <sameIDElements.Count; i++)
            {
                if (sameIDElements[i] is T)
                {
                    typedList.Add((T)sameIDElements[i]);
                }
            }
            return typedList;
        }


        //scripting

        public CSEngine ScriptEngine;
        public void InitScriptEngine()
        {
            ScriptEngine = new CSEngine();
            RegTypes();
            RegValues();
        }

        private void RegTypes()
        {
            ScriptEngine.env.RegType<Debug>();
            ScriptEngine.env.RegType<Document>();
            ScriptEngine.env.RegType<Element>();
            ScriptEngine.env.RegType<TagText>();
            ScriptEngine.env.RegType<MonoBehaviour>();
            ScriptEngine.env.RegType<Type>();
            ScriptEngine.env.RegType<EventHub>();
        }

        private void RegValues()
        {
            ScriptEngine.SetValue("document", this);
        }


        //event

        public const string DOCUMENT_EVENT = "__doc_event__";
        private EventHub eventHub = new EventHub();
        public void AddEventListener(string eventName, EventHub.Handler handler)
        {
            eventHub.AddEventListener(eventName, handler);
        }
        public void RemoveEventListener(string eventName, EventHub.Handler handler)
        {
            eventHub.RemoveEventListener(eventName, handler);
        }

        public void DispatchEvent(string eventName, object sender = null, object data = null)
        {
            eventHub.DispatchEvent(eventName, sender, data);
        }


        //clear and dispose
        public void Clear()
        {
            docContent = null;
            if (Root != null)
            {
                Root.Dispose();
                Root = null;
            }

            ScriptEngine.ClearValue();
            RegValues();

            elements = new Dictionary<string, List<Element>>();

            eventHub.Clear();
        }

        public void Dispose()
        {
            Clear();

            ScriptEngine.Dispose();
            ScriptEngine = null;

            elements = null;
            eventHub = null;
        }
    }
}

