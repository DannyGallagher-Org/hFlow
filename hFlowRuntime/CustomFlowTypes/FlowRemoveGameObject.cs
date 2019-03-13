using hFlowRuntime.Interfaces;
using RSG;
using UnityEngine;

namespace hFlowRuntime.CustomFlowTypes
{
    public class FlowRemoveGameObject : AbstractFlowPoint, IFlowPoint
    {
        public GameObject GameObject;

        public IPromise FlowPromise()
        {
            if (GameObject)
                Destroy(GameObject);

            return Promise.Resolved();
        }

        public void DrawEditorView()
        {
            GUI.color = new Color(1f, 0.39f, 0.31f);
            GUILayout.Label($"Destroy {GameObject}");
        }
    }
}