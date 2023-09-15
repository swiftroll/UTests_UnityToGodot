using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace UTests
{
    public abstract class UTestsRunnerBase
    {
        protected readonly List<Tuple<Node, MethodInfo>> _testMethods;

        protected int _currentIndex = 0;
        protected int _successfulNum = 0;
        protected int _failedNum = 0;

        public bool IsFinished => _currentIndex >= _testMethods.Count;

        public UTestsRunnerBase(List<Tuple<Node, MethodInfo>> testMethods)
        {
            _testMethods = testMethods;
        }

        public void Update()
        {
            if (IsFinished)
                return;

            //iterate through all test methods
            while (_currentIndex < _testMethods.Count)
            {
                bool result = RunTest();
                if (!result)
                    break;
            }
        }

        protected abstract bool RunTest();

        public abstract void LogResults();
    }
}