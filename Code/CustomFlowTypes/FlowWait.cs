using System.Collections;
using hFlow.Interfaces;
using RSG;
using UnityEngine;

namespace hFlow.CustomFlowTypes
{
    public class FlowWait : AbstractFlowPoint, IFlowPoint
    {
        public float Wait;

        public IPromise FlowPromise()
        {
            var promise = new Promise();

            StartCoroutine(WaitForSeconds(promise));

            return promise;
        }

        public void DrawEditorView()
        {
            GUI.color = new Color(1f, 0.67f, 0.67f);
            GUILayout.Label($"Wait for {Wait}s");
        }

        private IEnumerator WaitForSeconds(IPendingPromise promise)
        {
            yield return new WaitForSeconds(Wait);
            promise.Resolve();
        }
    }
}