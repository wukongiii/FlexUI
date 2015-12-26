#pragma warning disable 0219
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FlexUI
{
    public class EventHub
    {
       
        public delegate void Handler(object sender,object data);

        Dictionary<string, Handler> eventHandlers = new Dictionary<string, Handler>();

        public void AddEventListener(string eventName, Handler eventHandler)
        {

            if (eventHandlers.ContainsKey(eventName))
            {
                eventHandlers [eventName] += eventHandler;
            } else
            {
                eventHandlers [eventName] = eventHandler;
            }
        }

        public void RemoveEventListener(string eventName, Handler eventHandler)
        {
            if (!eventHandlers.ContainsKey(eventName))
            {
                return;
            }
            eventHandlers [eventName] -= eventHandler;
            if (eventHandlers [eventName] == null)
            {
                eventHandlers.Remove(eventName);
            }
        }

        public void RemoveAllEventListeners(string eventName)
        {
            if (!eventHandlers.ContainsKey(eventName))
            {
                return;
            }
            Handler handlers = eventHandlers [eventName];
            handlers = null;
			eventHandlers.Remove (eventName);
        }

        public void DispatchEvent(string eventName, object sender = null, object data = null)
        {
            if (!eventHandlers.ContainsKey(eventName))
            {
                return;
            }

            Handler handlers = eventHandlers [eventName];
            if (handlers == null)
            {
                eventHandlers.Remove(eventName);
                return;
            }
            for (int i = 0; i < handlers.GetInvocationList().GetLength(0); i++)
            {
                Handler eventHandler = (Handler)handlers.GetInvocationList() [i];
                try
                {
                    eventHandler(sender, data);
                } catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        
        }

        public void Clear()
        {
            foreach (string eventName in eventHandlers.Keys)
            {
                RemoveAllEventListeners(eventName);

            }
            eventHandlers.Clear();
        }

    }
}