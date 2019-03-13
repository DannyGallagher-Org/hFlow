using System;
using System.Collections;
using hFlowRuntime.Interfaces;
using RSG;
using UnityEngine;

namespace hFlowRuntime.CustomFlowTypes
{
    public class FlowInstantiatePrefab : AbstractFlowPoint, IFlowPoint
    {
        private GameObject _instance;
        public Transform Anchor;
        public bool BClean;
        public bool BPauseFlowForClean;
        public float CleanAfterSeconds;
        public GameObject Prefab;

        public IPromise FlowPromise()
        {
            var promise = new Promise();
            _instance = Instantiate(Prefab,
                Anchor != null ? Anchor.transform.position : transform.position,
                Anchor != null ? Anchor.transform.rotation : transform.rotation);

            if (BClean)
                StartCoroutine(DoCleanup(promise));

            if (!BPauseFlowForClean)
                promise.Resolve();

            return promise;
        }

        public void DrawEditorView()
        {
            try
            {
                GUI.color = new Color(1f, 0.93f, 0.44f);
                var text = $"Instaniate {Prefab.name}";
                if (Anchor != null)
                    text += $" on {Anchor.gameObject.name}";

                GUILayout.Label(text);
            }
            catch (Exception e)
            {
                GUI.color = Color.red;
                GUILayout.Label("Instaniate prefab has !Error.");
                Console.WriteLine(e);
            }
        }

        private IEnumerator DoCleanup(IPendingPromise promise)
        {
            yield return new WaitForSeconds(CleanAfterSeconds);

            if (BPauseFlowForClean)
                promise.Resolve();

            Destroy(_instance);
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}