using System;
using hFlow.Interfaces;
using RSG;
using UnityEngine;
using UnityEngine.Events;

namespace hFlow.CustomFlowTypes
{
    public class FlowCallUnityEvent : AbstractFlowPoint, IFlowPoint
    {
        public UnityEvent Event;

        public IPromise FlowPromise()
        {
            Event?.Invoke();
            return Promise.Resolved();
        }

        public void DrawEditorView()
        {
            try
            {
                GUI.color = new Color(0.48f, 0.78f, 1f);
                GUILayout.Label($"Call UnityEvent. {Event.GetPersistentMethodName(0)}");
            }
            catch (Exception e)
            {
                GUI.color = new Color(1f, 0.13f, 0.17f);
                GUILayout.Label($"Call UnityEvent. !Error!");
                Console.WriteLine(e);
            }
        }
    }
}