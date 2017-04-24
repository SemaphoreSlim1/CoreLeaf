using CoreLeaf.Configuration;
using CoreLeaf.Encryption;
using CoreLeaf.Net;
using Microsoft.Extensions.Configuration;
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

        public LoginClient(IConfiguration config, IRestClient client, Func<byte[], IBlowfish> blowfishFactory)
            : base(config, client)
        {
            _blowfishFactory = blowfishFactory;
        }

        public async Task<LoginResponse> LoginAsync(string encryptionToken, CancellationToken cancelToken)
        {
            var apiKey = _config[ConfigurationKeys.NissanApiKey];
            var userName = _config[ConfigurationKeys.UserName];
            var password = _config[ConfigurationKeys.Password];

            var crypt = _blowfishFactory(Encoding.ASCII.GetBytes(encryptionToken));
            
            var paddedPassword = crypt.ApplyPkcs5Padding(password);
            var passwordBytes = Encoding.ASCII.GetBytes(paddedPassword);


            var base64EncryptedBytes = crypt.Encrypt_ECB(passwordBytes);
            var base64EncryptedString = Convert.ToBase64String(base64EncryptedBytes);


            var bodyArgs = new List<KeyValuePair<string, string>>();
            bodyArgs.Add(new KeyValuePair<string, string>("UserId", userName));
            bodyArgs.Add(new KeyValuePair<string, string>("cartype", ""));
            bodyArgs.Add(new KeyValuePair<string, string>("custom_sessionid", ""));
            bodyArgs.Add(new KeyValuePair<string, string>("initial_app_strings", apiKey));
            bodyArgs.Add(new KeyValuePair<string, string>("tz", string.Empty));
            bodyArgs.Add(new KeyValuePair<string, string>("lg", "en-US"));
            bodyArgs.Add(new KeyValuePair<string, string>("DCMID", string.Empty));
            bodyArgs.Add(new KeyValuePair<string, string>("VIN", string.Empty));
            bodyArgs.Add(new KeyValuePair<string, string>("RegionCode", "NNA"));
            bodyArgs.Add(new KeyValuePair<string, string>("Password", base64EncryptedString));

            var route = _config[ConfigurationKeys.LoginRoute];
            var response = await PostAsync<LoginResponse>(route, bodyArgs, cancelToken);

            return response;
        }
    }
}
