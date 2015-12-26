using System;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace FlexUI
{
    public class BaseInteractableEventHandler:MonoBehaviour, IEventSystemHandler
    {
        public Element Element;

        public string EventName;

        protected virtual void DefaultClickMessageDispather(BaseEventData eventData)
        {
            if (!string.IsNullOrEmpty(EventName))
            {
                //replace to EventSystem or not?
                //refers to: http://docs.unity3d.com/Manual/MessagingSystem.html
                //refers to: http://docs.unity3d.com/ScriptReference/EventSystems.ExecuteEvents.ExecuteHierarchy.html
//                gameObject.SendMessageUpwards(EventName, eventData, SendMessageOptions.DontRequireReceiver);
                Element.document.DispatchEvent(EventName, Element, null);
                Element.document.DispatchEvent(Document.DOCUMENT_EVENT, Element, EventName);
            }

        }

        void OnEnable()
        {

        }
    }
}
