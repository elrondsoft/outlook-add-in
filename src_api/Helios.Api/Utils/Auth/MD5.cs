using System.Text;

namespace Helios.Api.Utils.Auth
{
    public class MD5
    {
        private readonly string _input;

        public MD5(string input)
        {
            _input = input;
        }

        public override string ToString()
        {
            return CreateMd5(_input);
        }

        private string CreateMd5(string input)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var inpuBytes = Encoding.ASCII.GetBytes(input);
                var hashBytes = md5.ComputeHash(inpuBytes);

                var sb = new StringBuilder();

                for (var i = 0; i < hashBytes.Length; i++)
                    sb.Append(hashBytes[i].ToString("X2"));
                return sb.ToString();
            }
        }
    }
}
