using System.Net.Http;

namespace CoreLeaf.Net
{
    public interface IContentEncoder
    {
        HttpContent Encode<T>(T content);
    }
}
