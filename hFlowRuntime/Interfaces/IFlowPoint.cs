using RSG;

namespace hFlowRuntime.Interfaces
{
    public interface IFlowPoint
    {
        IPromise FlowPromise();
        void DrawEditorView();
    }
}