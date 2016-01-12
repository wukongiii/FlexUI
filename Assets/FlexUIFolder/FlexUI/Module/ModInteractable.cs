using System;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace catwins.flexui
{
    [InterestedProperty(INTERACTABLE)]
    public class ModInteractable:BaseMod
    {
        public const string INTERACTABLE = "interactable";

        public const string ON_CLICK = "onclick";

        protected override void OnAdd()
        {

        }


        public override void Update()
        {
            LoadComponents();

            if (element.HasDirtyProperty(ON_CLICK))
            {
                ProcessOnClick();
            }
        }

        protected virtual void LoadComponents()
        {

        }

        protected virtual void ProcessOnClick()
        {
            AddEventHandler_OnClick(element.GameObject);
        }

        protected void AddEventHandler_OnClick(GameObject eventTarget)
        {
            ClickableEventHandler handler = eventTarget.GetComponent<ClickableEventHandler>();
            if (handler == null)
            {
                handler = eventTarget.AddComponent<ClickableEventHandler>();
            }
            handler.Element = element;
            handler.EventName = null;
            handler.OnClick = null;
            handler.OnClickWithEventData = null;
            
            object value = element.GetProperty(ON_CLICK);
            if (value is string)
            {
                handler.EventName = (string)value;
                
            } else if (value is Action)
            {
                handler.OnClick = element.GetProperty<Action>(ON_CLICK);
                
            } else if (value is Action<BaseEventData>)
            {
                handler.OnClickWithEventData = element.GetProperty<Action<BaseEventData>>(ON_CLICK);
            }
        }


    }

}
