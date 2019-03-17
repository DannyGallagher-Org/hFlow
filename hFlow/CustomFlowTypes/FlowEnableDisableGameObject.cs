using System;
using hFlow.Interfaces;
using RSG;
using UnityEngine;

namespace hFlow.CustomFlowTypes
{
    public class FlowEnableDisableGameObject : AbstractFlowPoint, IFlowPoint
    {
        public bool Enable;
        public GameObject GameObject;

        public IPromise FlowPromise()
        {
            GameObject.SetActive(Enable);
            return Promise.Resolved();
        }

        public void DrawEditorView()
        {
            try
            {
                GUI.color = Color.white;
                var enableOrDisable = Enable ? "Enable" : "Disable";
                GUILayout.Label($"{enableOrDisable} {GameObject.name}");
            }
            catch (Exception e)
            {
                GUI.color = new Color(1f, 0.03f, 0f);
                var enableOrDisable = Enable ? "Enable" : "Disable";
                GUILayout.Label($"{enableOrDisable} Error!");
                Console.WriteLine(e);
            }
        }
    }
}