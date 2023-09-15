using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UTests
{
	public sealed partial class UTestsRunner : Node
	{
		[Export] private Node[] _testNodes;
		
		private UTestsRunnerPlain _plain;
		private UTestsRunnerCoroutine _coroutine;

		private bool _isFinished;

		public override void _Ready()
		{
			Init(_testNodes.ToList());
		}

		public override void _Process(double delta)
		{
			UTestTime.DeltaTime = delta;

			if (_isFinished)
				return;

			if (!_plain.IsFinished)
			{
				_plain.Update();
			}
			else if (_plain.IsFinished && !_coroutine.IsFinished)
			{
				_coroutine.Update();
			}
			else if (_plain.IsFinished && _coroutine.IsFinished)
			{
				GD.Print($"---");
				_plain.LogResults();
				_coroutine.LogResults();
				_isFinished = true;
			}
		}

		private void Init(List<Node> nodes)
		{
			List<Tuple<Node, MethodInfo>> plainTestMethods = new();
			List<Tuple<Node, MethodInfo>> coroutineTestMethods = new();

			for (int i = 0; i < nodes.Count; i++)
			{
				//find all methods with attribute
				var methodsPlain = nodes[i]
					.GetType()
					.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
					.Where(m => m.GetCustomAttributes(typeof(UTest), false).Length > 0)
					.Where(m => m.ReturnType == typeof(void));

				//add theem to list of plain methods
				foreach (var method in methodsPlain)
				{
					plainTestMethods.Add(new(nodes[i], method));
				}

				//find all methods with attribute
				var methodsCoroutine = nodes[i]
					.GetType()
					.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
					.Where(m => m.GetCustomAttributes(typeof(UTest), false).Length > 0)
					.Where(m => m.ReturnType == typeof(IEnumerator));

				//add theem to list of coroutine methods
				foreach (var method in methodsCoroutine)
				{
					coroutineTestMethods.Add(new(nodes[i], method));
				}
			}

			_plain = new(plainTestMethods);
			_coroutine = new(coroutineTestMethods);
		}
	}
}
