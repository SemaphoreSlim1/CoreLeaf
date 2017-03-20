using CoreLeaf.Console;
using CoreLeaf.Console.ColorPreservation;
using Moq;
using System;
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
            var cp = new ForegroundBackgroundPreserver(console);

            //set the new color
            console.Background = newColor;

            //act
            cp.Dispose();

            //assert
            //colors should be back to their original colors
            Assert.Equal(originalColor, console.Background);
        }

        [Theory]
        [InlineData(ConsoleColor.Black, ConsoleColor.Red)]
        public void ColorPreserver_Dispose_RestoresForeground(ConsoleColor originalColor, ConsoleColor newColor)
        {
            //arrange
            var consoleMock = new Mock<IConsole>();
            consoleMock.SetupAllProperties();
            var console = consoleMock.Object;

            //initialze the original colors
            console.Foreground = originalColor;

            //"preserve" the colors
            var cp = new ForegroundBackgroundPreserver(console);

            //set the new color
            console.Foreground = newColor;

            //act
            cp.Dispose();

            //assert
            //colors should be back to their original colors
            Assert.Equal(originalColor, console.Foreground);
        }
    }
}
