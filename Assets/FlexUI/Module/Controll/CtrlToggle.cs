
using System;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace catwins.flexui
{
    public class CtrlToggle : ModInteractable 
    {
        public const string IS_ON = "ison";
        public const string GROUP_ID = "groupid";
        public const string ON_VALUE_CHANGED = "onvaluechanged";
        
        public const string TEXT = "text";
        
        private GameObject toggleGO = null;
        public Toggle Toggle = null;
        private Text text = null;
        
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
                    toggleGO = PrefabLoader.Load(uri.relativePath);
                    if (toggleGO != null)
                    {
						FlexUIUtil.AddChild(element.GameObject, toggleGO);
                        Toggle = toggleGO.GetComponentInChildren<Toggle>();
                        text = toggleGO.GetComponentInChildren<Text>();

						RectTransform rect = toggleGO.GetComponent<RectTransform>();
						FlexUIUtil.ExpandRect(rect);
                    }

                }
            }

        }
        
        
        public override void Update()
        {
            base.Update();
            
            if (toggleGO!= null && Toggle != null)
            {
                if (element.HasDirtyProperty(IS_ON))
                {
                    Toggle.isOn = bool.Parse(element.GetString(IS_ON));
                }

                if (element.HasDirtyProperty(GROUP_ID))
                {
                    int groupID = element.GetInt(GROUP_ID);
                    ModToggleGroup modToggleGroup = ModToggleGroup.GetGroupByGroupID(groupID);
                    if (modToggleGroup != null)
                    {
                        Toggle.group = modToggleGroup.ToggleGroup;
                    } else {
                        ModToggleGroup.RegGroupedToggle(groupID, this);
                    }

                }


                if (element.HasDirtyProperty(ON_VALUE_CHANGED))
                {

                    ToggleEventHandler handler = toggleGO.GetComponent<ToggleEventHandler>();
                    if (handler == null)
                    {
                        handler = toggleGO.AddComponent<ToggleEventHandler>();
                    }
                    handler.TargetToggle = element as TagToggle;

                    object value = element.GetProperty(ON_VALUE_CHANGED);
                    if (value is string)
                    {
                        handler.EventName = (string)value;
                    } else if (value is Action<TagToggle>)
                    {
                        handler.OnValueChanged = (Action<TagToggle>)value;
                        Toggle.onValueChanged.AddListener(handler.OnValueChangedHandler);
                    } else {
                        Debug.LogWarning("FlexUI: CtrlToggle: Handler for OnValueChanged does not match Action<TagToggle>.");
                    }
                }
            }
            if (toggleGO != null && text != null)
            {
                if (element.HasDirtyProperty(TEXT))
                {
                    text.text = element.GetString(TEXT, true);
                }
            }

            
        }


    }
}

