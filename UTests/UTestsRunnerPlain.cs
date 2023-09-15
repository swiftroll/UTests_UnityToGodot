using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace UTests
{
    public sealed class UTestsRunnerPlain  : UTestsRunnerBase
    {
        public UTestsRunnerPlain(List<Tuple<Node, MethodInfo>> testMethods) : base(testMethods) { }

        protected override bool RunTest()
        {
            var methodTuple = _testMethods[_currentIndex];
            GD.Print($"-");
            GD.Print($"Starting plain test: {methodTuple.Item1.GetType().Name}->{methodTuple.Item2.Name}");

            try
            {
                methodTuple.Item2.Invoke(methodTuple.Item1, null);
            }
            catch (Exception)
            {
                _failedNum++;
                _currentIndex++;
                GD.PrintErr($"Failed plain test: {methodTuple.Item1.GetType().Name}->{methodTuple.Item2.Name}");
                throw;
            }

            _successfulNum++;
            _currentIndex++;
            GD.Print($"Successful plain test: {methodTuple.Item1.GetType().Name}->{methodTuple.Item2.Name}");

            return true;
        }

        public override void LogResults()
        {
            GD.Print($"{_testMethods.Count} plain tests have finished, successful:{_successfulNum}, failed:{_failedNum}");
        }
    }
}