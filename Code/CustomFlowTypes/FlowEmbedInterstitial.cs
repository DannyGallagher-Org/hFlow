using hFlow.Interfaces;
using RSG;
using UnityEngine;

namespace hFlow.CustomFlowTypes
{
    public class FlowEmbedInterstitial : AbstractFlowPoint, IFlowPoint
    {
        public FlowInterstitial FlowInterstitial;

        public IPromise FlowPromise()
        {
            return FlowUtility.GetFlowSequence(FlowInterstitial.FlowPoints, false);
        }

        public void DrawEditorView()
        {
            GUI.color = Color.green;
            GUILayout.Label("Embeded", GUILayout.Width(85));
            GUILayout.BeginVertical();
            if (FlowInterstitial?.FlowPoints != null)
                foreach (var flowPoint in FlowInterstitial?.FlowPoints)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(4);

                    (flowPoint as IFlowPoint)?.DrawEditorView();

                    GUILayout.EndHorizontal();
                }

            GUILayout.EndVertical();
        }
    }
}