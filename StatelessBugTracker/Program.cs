using System;

namespace StatelessBugTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            var bug = new Bug("Hello World");

            Console.WriteLine($"Current State: {bug.CurrentState}");

            bug.Assign("Timothy");
            Console.WriteLine($"Current State: {bug.CurrentState}");
            Console.WriteLine($"Current Assignee: {bug.Assignee}");

            bug.Defer();
            Console.WriteLine($"Current State: {bug.CurrentState}");
            Console.WriteLine($"Current Assignee: {bug.Assignee}");

            bug.Assign("Xing");
            Console.WriteLine($"Current State: {bug.CurrentState}");
            Console.WriteLine($"Current Assignee: {bug.Assignee}");

            bug.Close();
            Console.WriteLine($"Current State: {bug.CurrentState}");
        }
    }
}
