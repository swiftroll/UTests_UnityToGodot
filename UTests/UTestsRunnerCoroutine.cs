using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace UTests
{
    public sealed class UTestsRunnerCoroutine : UTestsRunnerBase
    {
        private IEnumerator _currentCoroutine;
        private Tuple<Node, MethodInfo> _currentMethodTuple;

        public UTestsRunnerCoroutine(List<Tuple<Node, MethodInfo>> testMethods) : base(testMethods) { }

        protected override bool RunTest()
        {
            if (_currentCoroutine == null)
            {
                _currentMethodTuple = _testMethods[_currentIndex];
                GD.Print($"-");
                GD.Print($"Starting coroutine test: {_currentMethodTuple.Item1.GetType().Name}->{_currentMethodTuple.Item2.Name}");

                _currentCoroutine = (IEnumerator)_currentMethodTuple.Item2.Invoke(_currentMethodTuple.Item1, null);
            }

            try
            {
                bool isRunning = _currentCoroutine.MoveNext();

                if (isRunning)
                    return false;
            }
            catch (Exception)
            {
                _currentCoroutine = null;
                _failedNum++;
                _currentIndex++;
                GD.PrintErr($"Failed coroutine test: {_currentMethodTuple.Item1.GetType().Name}->{_currentMethodTuple.Item2.Name}");
                throw;
            }

            _currentCoroutine = null;
            _successfulNum++;
            _currentIndex++;
            GD.Print($"Successful coroutine test: {_currentMethodTuple.Item1.GetType().Name}->{_currentMethodTuple.Item2.Name}");

            return true;
        }

        public override void LogResults()
        {
            GD.Print($"{_testMethods.Count} coroutine tests have finished, successful:{_successfulNum}, failed:{_failedNum}");
        }
    }
}