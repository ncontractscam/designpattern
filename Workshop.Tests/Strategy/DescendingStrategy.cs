namespace Workshop.Tests.Strategy
{
    class DescendingStrategy : IStrategy
    {
        public void DoAlgorithm(List<string> data)
        {
            data.Sort();
            data.Reverse();
        }
    }
}