using UnityEngine;

namespace hFlowRuntime
{
    public class FlowInterstitial : MonoBehaviour
    {
        public bool CleanOnComplete = true;
        [SerializeField] public MonoBehaviour[] FlowPoints = new MonoBehaviour[0];
        public bool PlayOnAwake;

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
            FlowUtility.GetFlowSequence(FlowPoints)
                .Finally(() =>
                {
                    if (CleanOnComplete)
                        Destroy(gameObject);
                });
        }
    }
}