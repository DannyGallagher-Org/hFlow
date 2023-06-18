using UnityEngine;

namespace hFlow
{
    public class FlowInterstitial : MonoBehaviour
    {[SerializeField] public MonoBehaviour[] FlowPoints = new MonoBehaviour[0];
        public bool DebugFlowInterstitial;
        public bool PlayOnAwake;
        
        public bool CleanOnComplete = true;

        private void Awake()
        {
            if (PlayOnAwake)
                Invoke();
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public void Invoke()
        {
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
            DoFlow();
        }

        private void DoFlow()
        {
            if(DebugFlowInterstitial) 
                Debug.Log("debugfp: - DoFlow() -");
            
            FlowUtility.GetFlowSequence(FlowPoints, DebugFlowInterstitial)
                .Finally(() =>
                {
                    if (CleanOnComplete)
                        Destroy(gameObject);
                });
        }
    }
}