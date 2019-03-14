using System;
using hLevelsRuntime;
using RSG;
using UnityEngine;

namespace hFlowRuntime.CustomFlowTypes
{
    public class FlowLoadLevelFromHLevels : AbstractFlowPoint
    {
        public AdditiveScene Scene;

        public IPromise FlowPromise()
        {
            Scene.Load();
            return Promise.Resolved();
        }

        public void DrawEditorView()
        {
            try
            {
                GUI.color = new Color(1f, 0.62f, 0.75f);
                GUILayout.Label($"Load Scene. {Scene.name}");
            }
            catch (Exception e)
            {
                GUI.color = new Color(1f, 0.13f, 0.17f);
                GUILayout.Label("Load Scene. !Error!");
                Console.WriteLine(e);
            }
        }
    }
}