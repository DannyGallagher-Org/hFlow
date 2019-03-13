using hFlowRuntime.Interfaces;
using RSG;
using UnityEngine;

namespace hFlowRuntime.CustomFlowTypes
{
    public class FlowInterstitialCleanUp : AbstractFlowPoint, IFlowPoint
    {
//        public GameObject MainParentObject;

        public IPromise FlowPromise()
        {
//            if(!MainParentObject)
//                MainParentObject = gameObject;
//            
//            Destroy(MainParentObject);
            return Promise.Resolved();
        }

        public void DrawEditorView()
        {
            GUI.color = Color.red;
            GUILayout.Label("OLD NOT USED");
        }
    }
}