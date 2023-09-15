using Godot;
using System.Collections;

namespace UTests
{
    public sealed partial class UTestExample1 : Node
    {
        [UTest]
        private void ExampleTest1()
        {
            Assert.IsTrue(1 == 1);
        }

        [UTest]
        private void ExampleTest2()
        {
            Assert.IsTrue(1 == 1);
            Assert.IsTrue(1 == 2, "ExampleTest2 valid fail");
        }

        [UTest]
        private IEnumerator ExampleTest3()
        {
            Assert.IsTrue(1 == 1);

            yield break;
        }

        [UTest]
        private IEnumerator ExampleTest4()
        {
            double timer = 0f;
            while (timer < 1f)
            {
                GD.Print($"ExampleTest4 timer={timer}");
                timer += UTestTime.DeltaTime;
                yield return null;
            }    

            Assert.IsTrue(1 == 2, "ExampleTest4 valid fail");

            yield break;
        }

        [UTest]
        private IEnumerator ExampleTest5()
        {
            GD.Print($"ExampleTest5 waiting for user to press Space key");

            while (!Input.IsKeyPressed(Key.Space))
                yield return null;

            Assert.IsTrue(true);

            yield break;
        }
    }
}