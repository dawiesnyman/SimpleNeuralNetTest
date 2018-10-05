namespace NeuralNetTest.Nodes
{
    public interface IInputNode : IBaseNode
    {
        double Weight { get; set; }
        double Output { get; }
        double Input { get; set; }

    }
}
