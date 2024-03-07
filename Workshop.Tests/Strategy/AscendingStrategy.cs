namespace Workshop.Tests.Strategy
{
    // Concrete Strategies implement the algorithm while following the base
    // Strategy interface. The interface makes them interchangeable in the
    // Context.
    class AscendingStrategy : IStrategy
    {
        public void DoAlgorithm(List<string> data)
        {
            data.Sort();
        }
    }
}