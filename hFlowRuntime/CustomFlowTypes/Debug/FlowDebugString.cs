using System.Collections;
using hFlowRuntime.Interfaces;
using RSG;
using UnityEngine;

namespace hFlowRuntime.CustomFlowTypes.Debug
{
    public class FlowDebugString : AbstractFlowPoint, IFlowPoint
    {
        public string DebugString;
        public float Delay;

        public IPromise FlowPromise()
        {
            var promise = new Promise();

            StartCoroutine(DebugAfterDelay(promise));

            return promise;
        }

        public void DrawEditorView()
        {
            GUI.color = Color.yellow;
            GUILayout.Label($"{DebugString}");
        }

        private IEnumerator DebugAfterDelay(IPendingPromise promise)
        {
            yield return new WaitForSeconds(Delay);
            UnityEngine.Debug.Log(DebugString);
            promise.Resolve();
        }
    }
}