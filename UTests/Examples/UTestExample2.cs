using Godot;
using System.Collections;

namespace UTests
{
    public sealed partial class UTestExample2 : Node
    {
        [UTest]
        private void ExampleTest6()
        {
            Assert.IsTrue(false, "ExampleTest6 valid fail");
        }

        [UTest]
        private IEnumerator ExampleTest7()
        {
            Assert.IsTrue(false, "ExampleTest7 valid fail");
            
            yield return null;

            Assert.IsTrue(false, "ExampleTest7 invalid fail");
        }
    }
}