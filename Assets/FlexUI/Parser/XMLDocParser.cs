
using System;
using System.Xml;
using System.Collections.Generic;

using NanoXML;

using UnityEngine;

namespace FlexUI
{
    public class XMLDocParser:IDocParser
    {
        public Element Parse(string source , Element parent, Document document)
        {
            NanoXMLDocument xmlDoc = new NanoXMLDocument(source);
            return Parse(xmlDoc.RootNode, parent, document);
        }

        public Element Parse(NanoXMLNode node, Element parent, Document document)
        {
            Element element;
            if (!Tag.IsTagRegistered(node.Name))
            {
                Debug.LogWarning("Node: " + node.Name + " can not be recognized.");
                element = new Element(Element.TAG);
            } else
            {
                Type tagType = Tag.GetTagType(node.Name);
                element = (Element)Activator.CreateInstance(tagType);
            }
            element.document = document;

            foreach (NanoXMLAttribute attribute in node.Attributes)
            {
                element.SetProperty(attribute.Name.ToLower(), attribute.Value);
            }

            if (!string.IsNullOrEmpty(node.Value))
            {
                element.SetProperty(Element.VALUE, node.Value);
            }

            if (parent != null)
            {
                parent.AddChild(element);
            }
            
            foreach (NanoXMLNode childNode in node.SubNodes)
            {
                Element child = Parse(childNode, element, document);
            }
            return element;
        }
    }


}