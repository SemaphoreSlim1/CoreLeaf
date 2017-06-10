using System.Net.Http;

namespace RestAbstractions
{
    public interface IContentEncoder
    {
        HttpContent Encode<T>(T content);
    }
}
