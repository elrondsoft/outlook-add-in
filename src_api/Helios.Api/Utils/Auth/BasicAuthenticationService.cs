using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using Helios.Api.EFContext;

namespace Helios.Api.Utils.Auth
{
    public class BasicAuthenticationService
    {
        private readonly HeliosDbContext _context;

        public BasicAuthenticationService(HeliosDbContext context)
        {
            _context = context;
        }

        public bool ThrowIfNotAuthorized(AuthenticationHeaderValue authHeader)
        {
            if (authHeader == null || authHeader.Parameter == null)
                throw new Exception("No secure header present.");

            return ThrowIfNotAuthorized(authHeader.Parameter);
        }

        private bool ThrowIfNotAuthorized(string authHeader)
        {
            if (authHeader != null)
            {
                var encoding = Encoding.GetEncoding("iso-8859-1");
                var usernamePassword = encoding.GetString(Convert.FromBase64String(authHeader));
                // username:password = stuart:stuart

                var seperatorIndex = usernamePassword.IndexOf(':');

                var username = usernamePassword.Substring(0, seperatorIndex);
                var password = usernamePassword.Substring(seperatorIndex + 1);

                var passwordHash = new MD5(password);

                if (CheckIfUserExists(username, passwordHash.ToString()))
                    return true;
                throw new Exception("User or password you provided are invalid");
            }
            throw new Exception("The authorization header is either empty or isn't Basic.");
        }

        private bool CheckIfUserExists(string username, string password)
        {
            var result = _context.Users.FirstOrDefault(_ => _.HeliosLogin == username && _.HeliosPassword == password);
            if (result != null)
                return true;
            return false;
        }
    }
}
