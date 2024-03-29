using System;
using System.Collections.Generic;
using System.Linq;
using hFlow.CustomFlowTypes;
using hFlow.Interfaces;
using RSG;
using UnityEngine;
using Object = UnityEngine.Object;

namespace hFlow
{
    public static class FlowUtility
    {
        public static IPromise GetFlowSequence(IEnumerable<MonoBehaviour> originalList, bool debugFlowInterstitial)
        {
            var flowPoints = (from flowPoint
                    in originalList
                where !((AbstractFlowPoint) flowPoint).Muted
                select flowPoint as AbstractFlowPoint).ToList();

            if (debugFlowInterstitial)
            {
                Debug.Log($" debugfp: found {flowPoints.Count} flow points.");
                foreach (var flowPoint in flowPoints)
                {
                    Debug.Log($"  debugfp: {flowPoint.name}");
                }
            }

            return Promise
                .Sequence(flowPoints.Select(fp => (Func<IPromise>) ((IFlowPoint) fp).FlowPromise))
                .Catch(exception =>
                {
                    Debug.LogError($"- Flow failed to complete msg [{exception.Source}] [{exception.Message}]");
                    Debug.LogError($"- Flow failed to complete target [{exception.Source}] [{exception.TargetSite}]");
                    Debug.LogError($"- Flow failed to complete trace [{exception.Source}] [{exception.StackTrace}]");
                });
        }

        public static IPromise GetFlowSequenceWithCleanup(IEnumerable<MonoBehaviour> originalList,
            GameObject gameObject)
        {
            var flowPoints = (from flowPoint
                    in originalList
                where !((AbstractFlowPoint) flowPoint).Muted
                select flowPoint as AbstractFlowPoint).ToList();

            return Promise
                .Sequence(flowPoints.Select(fp => (Func<IPromise>) ((IFlowPoint) fp).FlowPromise))
                .Catch(exception =>
                {
                    Debug.LogError($"- Flow failed to complete msg [{exception.Source}] [{exception.Message}]");
                    Debug.LogError($"- Flow failed to complete target [{exception.Source}] [{exception.TargetSite}]");
                    Debug.LogError($"- Flow failed to complete trace [{exception.Source}] [{exception.StackTrace}]");
                })
                .Finally(() =>
                {
                    Debug.Log($"Cleanup meh babeh {gameObject}");
                    Object.Destroy(gameObject);
                });
        }
    }
}