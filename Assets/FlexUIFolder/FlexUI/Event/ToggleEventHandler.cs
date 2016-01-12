using System;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace catwins.flexui
{
    public class ToggleEventHandler:BaseInteractableEventHandler
    {
        public TagToggle TargetToggle;

        public Action<TagToggle> OnValueChanged;

        public void OnValueChangedHandler(bool value)
        {
            if (OnValueChanged != null)
            {
                OnValueChanged(TargetToggle);
            }
        }

        void OnDestroy()
        {
            TargetToggle = null;
            OnValueChanged = null;
        }
    }


}