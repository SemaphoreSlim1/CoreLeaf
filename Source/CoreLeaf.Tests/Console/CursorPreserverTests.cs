using CoreLeaf.Console;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CoreLeaf.Tests.Console
{
    public class CursorPreserverTests
    {
        [Theory]
        [InlineData(1,2)]
        public void CursorPreserver_Dispose_RestoresLeft(int originalLeft, int newLeft)
        {
            //arrange
            var consoleMock = new Mock<IConsole>();
            consoleMock.SetupAllProperties();
            var console = consoleMock.Object;

            //initialize the position
            console.CursorLeft = originalLeft;

            //"preserve" the position
            var cp = new CursorPreserver(console);

            //alter the position
            console.CursorLeft = newLeft;

            //act
            cp.Dispose();

            //assert
            //left should be set back to the original value
            Assert.Equal(originalLeft, console.CursorLeft);
        }

        [Theory]
        [InlineData(1, 2)]
        public void CursorPreserver_Dispose_RestoresTop(int originalTop, int newTop)
        {
            //arrange
            var consoleMock = new Mock<IConsole>();
            consoleMock.SetupAllProperties();
            var console = consoleMock.Object;

            //initialize the position
            console.CursorTop = originalTop;

            //"preserve" the position
            var cp = new CursorPreserver(console);

            //alter the position
            console.CursorTop = newTop;

            //act
            cp.Dispose();

            //assert
            //left should be set back to the original value
            Assert.Equal(originalTop, console.CursorTop);
        }
    }
}
