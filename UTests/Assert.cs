using Godot;
using System;

namespace UTests
{
	public static class Assert
	{
		public static void IsTrue(bool val, string text = null)
		{
			Check(() => val, text);
		}

		public static void IsFalse(bool val, string text = null)
		{
			Check(() => !val, text);
		}

		public static void IsNull(object obj, string text = null)
		{
			Check(() => obj == null, text);
		}

		public static void IsNotNull(object obj, string text = null)
		{
			Check(() => obj != null, text);
		}

		private static void Check(Func<bool> func, string text)
		{
			if (text == null)
				text = "Assertion failed";

			if (!func.Invoke())
			{
				GD.PrintErr($"{text}");
				throw new AssertException(text);
			}
		}
	}
}
