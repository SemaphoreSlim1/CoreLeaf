using CoreLeaf.Encryption;
using LeafStandard.NissanApi;
using RestAbstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLeaf.NissanApi.Login
{
    public class LoginClient : NissanClient, ILoginClient
    {
        private readonly Func<byte[], IBlowfish> _blowfishFactory;
        private readonly string _loginRoute;

        public LoginClient(IRestClient client, string loginRoute, string apiKey, Func<byte[], IBlowfish> blowfishFactory)
            : base(client, apiKey)
        {
            _blowfishFactory = blowfishFactory;
            _loginRoute = loginRoute;
        }

        public async Task<LoginResponse> LoginAsync(string encryptionToken,
            string userName, string password, CancellationToken cancelToken)
        {
            var crypt = _blowfishFactory(Encoding.ASCII.GetBytes(encryptionToken));
            
            var paddedPassword = crypt.ApplyPkcs5Padding(password);
            var passwordBytes = Encoding.ASCII.GetBytes(paddedPassword);

            var base64EncryptedBytes = crypt.Encrypt_ECB(passwordBytes);
            var base64EncryptedString = Convert.ToBase64String(base64EncryptedBytes);

            var args = new BodyArgs();
            args.Add("UserId", userName);
            args.Add("cartype");
            args.Add("custom_sessionid");
            args.Add("initial_app_strings", _apiKey);
            args.Add("tz");
            args.Add("lg", "en-US");
            args.Add("DCMID");
            args.Add("VIN"); ;
            args.Add("RegionCode", "NNA");
            args.Add("Password", base64EncryptedString);
            
            var response = await PostAsync<LoginResponse>(_loginRoute, args, cancelToken);

            return response;
        }
    }
}
