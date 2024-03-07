using System;
using System.Collections.Generic;
using Workshop.Tests.Strategy;

namespace Workshop.Tests
{    
    public class Tests
    {
        [Test]
        public void TestAscending()
        {
            // The client code picks a concrete strategy and passes it to the
            // context. The client should be aware of the differences between
            // strategies in order to make the right choice.
            var input = new List<string> { "a", "e", "b", "c", "d" };
            var context = new Context(input);

            Console.WriteLine("Client: Strategy is set to normal sorting.");
            context.SetStrategy(new AscendingStrategy());
            var result = context.ExecuteAndDisplay();
            Assert.That(result, Is.EqualTo("a,b,c,d,e"));
        }

        [Test] 
        public void TestDescending()
        {
            var input = new List<string> { "a", "e", "b", "c", "d" };
            var context = new Context(input);

            context.SetStrategy(new DescendingStrategy());
            var result = context.ExecuteAndDisplay();
            Assert.That(result, Is.EqualTo("e,d,c,b,a"));
        }

        [Test]
        public void TestBoth()
        {
            var input = new List<string> { "a", "e", "b", "c", "d" };
            var context = new Context(input);

            context.SetStrategy(new DescendingStrategy());
            var result = context.ExecuteAndDisplay();
            Assert.That(result, Is.EqualTo("e,d,c,b,a"));

            context.SetStrategy(new AscendingStrategy());
            result = context.ExecuteAndDisplay();
            Assert.That(result, Is.EqualTo("a,b,c,d,e"));
        }
    }
}