using System;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace catwins.flexui
{
    public class CtrlButton : ModInteractable 
    {

        public const string TEXT = "text";


        private GameObject buttonGO;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            string uriStr = "";
            if (element.HasDirtyProperty(URI.TAG))
            {
                uriStr = element.GetString(URI.TAG);
                URI uri = URI.GetFromString(uriStr);
                
                
                if (uri != null)
                {
                    buttonGO = PrefabLoader.Load(uri.relativePath);
                    if (buttonGO != null)
                    {
						FlexUIUtil.AddChild(element.GameObject, buttonGO);
						RectTransform rect = buttonGO.GetComponent<RectTransform>();
						FlexUIUtil.ExpandRect(rect);
                    }
                } else
                {
                    buttonGO = element.GameObject;
                }
            }
        }
        protected override void ProcessOnClick()
        {
            AddEventHandler_OnClick(buttonGO);
        }

        public override void Update()
        {
            base.Update();


            if (element.HasDirtyProperty(TEXT))
            {
                if (buttonGO != null)
                {
                    Text text = buttonGO.GetComponentInChildren<Text>();
                    if (text != null)
                    {
                        text.text = element.GetString(TEXT, true);
                    }
                }
            }

        }
    }
}

