using CoreLeaf.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CoreLeaf.Tests.Net
{
    public class FormUrlContentEncoderTests
    {
        public static IEnumerable<object[]> ObjectTestCases()
        {
            var testCases = new List<object[]>();            

            //handle dictionaries (the "easy" case)
            testCases.Add(new object[]
            {
                new Dictionary<string, string>
                {
                    ["Property1"] = "Hello",
                    ["Property2"] = "Unit Test"
                },
                "Property1=Hello&Property2=Unit+Test"
            });

            //handle objects when properties contain values
            testCases.Add(new object[] {
                new
                {
                    Property1 = "Hello",
                    Property2 = "Unit Test"
                },
                "Property1=Hello&Property2=Unit+Test"
            });

            //handle objects when properties are string empty
            testCases.Add(new object[] {
                new
                {
                    Property1 = "Hello",
                    Property2 = ""
                },
                "Property1=Hello"
            });

            //handle objects when properties are null
            testCases.Add(new object[] {
                new
                {
                    Property1 = "Hello",
                    Property2 = (object)null
                },
                "Property1=Hello"
            });

            return testCases;
        }

        [Theory]
        [MemberData(nameof(ObjectTestCases))]
        public async Task FormUrlContentEncoder_Encode_EncodesObjects(object content, string expectedString)
        {
            var encoder = new FormUrlContentEncoder();

            var encodedContent = encoder.Encode(content);

            var stringContent = await encodedContent.ReadAsStringAsync();

            Assert.Equal(expectedString, stringContent);
        }        
    }
}
