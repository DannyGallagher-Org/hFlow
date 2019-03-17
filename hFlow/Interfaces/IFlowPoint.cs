using RSG;

namespace hFlow.Interfaces
{
    public interface IFlowPoint
    {
        IPromise FlowPromise();
        void DrawEditorView();
    }
}