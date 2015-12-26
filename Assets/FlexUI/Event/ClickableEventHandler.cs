using System;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace FlexUI
{
    public class ClickableEventHandler:BaseInteractableEventHandler, IPointerClickHandler
    {
        public Action OnClick;
        
        public Action<BaseEventData> OnClickWithEventData;
        
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (OnClick != null)
            {
                OnClick();
            }
            if (OnClickWithEventData != null)
            {
                OnClickWithEventData(eventData);
            }
            if (!string.IsNullOrEmpty(EventName))
            {
                DefaultClickMessageDispather(eventData);
            }
        }
        
        
        void OnDestroy()
        {
            OnClick = null;
            OnClickWithEventData = null;
        }
    }
}
