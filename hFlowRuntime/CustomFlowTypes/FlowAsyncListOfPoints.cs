using System.Linq;
using hFlowRuntime.Interfaces;
using RSG;
using UnityEngine;

namespace hFlowRuntime.CustomFlowTypes
{
    public class FlowAsyncListOfPoints : AbstractFlowPoint, IFlowPoint
    {
        public FlowInterstitial FlowInterstitial;

        public IPromise FlowPromise()
        {
            return Promise.All(FlowInterstitial.FlowPoints.Cast<IFlowPoint>().Select(fp => fp.FlowPromise()))
                // ReSharper disable once ObjectCreationAsStatement
                .Catch(e => new UnityException(e.Message));
        }

        public void DrawEditorView()
        {
            GUI.color = new Color(0.45f, 0.64f, 1f);
            GUILayout.Label("Async List", GUILayout.Width(85));
            GUILayout.BeginVertical();
            if (FlowInterstitial.FlowPoints != null)
                foreach (var flowPoint in FlowInterstitial.FlowPoints)
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