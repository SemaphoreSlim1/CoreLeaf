using CoreLeaf.Console;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CoreLeaf.Tests.Console
{
    public class ColorPreserverTests
    {
        [Theory]
        [InlineData(ConsoleColor.Black, ConsoleColor.Red)]
        public void ColorPreserver_Dispose_RestoresBackground(ConsoleColor originalColor, ConsoleColor newColor)
        {
            //arrange
            var consoleMock = new Mock<IConsole>();
            consoleMock.SetupAllProperties();
            var console = consoleMock.Object;

            //initialze the original colors
            console.Background = originalColor;

            //"preserve" the colors
            var cp = new ColorPreserver(console);

            //set the new color
            console.Background = newColor;

            //act
            cp.Dispose();

            //assert
            //colors should be back to their original colors
            Assert.Equal(originalColor, console.Background);
        }
    }
}
