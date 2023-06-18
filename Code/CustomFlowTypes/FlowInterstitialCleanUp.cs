using hFlow.Interfaces;
using RSG;
using UnityEngine;

namespace hFlow.CustomFlowTypes
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