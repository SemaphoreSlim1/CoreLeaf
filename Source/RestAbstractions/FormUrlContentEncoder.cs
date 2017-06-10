using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;

namespace RestAbstractions
{
    public class FormUrlContentEncoder : IContentEncoder
    {
        public HttpContent Encode<T>(T content)
        {
            if (content is IEnumerable<KeyValuePair<string, string>> castedContent)
            {
                //maybe we got lucky and our content was already what we needed. 
                //No need to convert, just go!
                return new FormUrlEncodedContent(castedContent);
            }

            var propertyInfos = content.GetType().GetTypeInfo().DeclaredProperties;

            var formVars = new List<KeyValuePair<string, string>>();

            foreach (var propertyInfo in propertyInfos)
            {
                if (propertyInfo.CanRead)
                {
                    var value = propertyInfo.GetValue(content)?.ToString() ?? string.Empty;

                    if (string.IsNullOrWhiteSpace(value))
                    { continue; } //don't add a property whose value is null or empty

                    var name = propertyInfo.Name;
                    var kvp = new KeyValuePair<string, string>(name, value);

                    formVars.Add(kvp);
                }
            }

            return new FormUrlEncodedContent(formVars);
        }
    }
}
