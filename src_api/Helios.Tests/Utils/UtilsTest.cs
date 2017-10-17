using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Helios.Api.Domain.Extensions;
using Helios.Api.Utils.Encryption.Providers;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using RestSharp.Extensions.MonoHttp;

namespace Helios.Tests.Utils
{
    [TestFixture]
    public sealed class UtilsTest
    {
        [Test]
        public void EncryptString__ShouldWork()
        {
            var originalString = "123";
            var key = "E546C8DF278CD5931069B522E695D4F2";

            var encryptString = AesStringEncryptor.EncryptString(originalString, key);
            var decryptedString = AesStringEncryptor.DecryptString(encryptString, key);

            Assert.AreEqual(originalString, decryptedString);
        }

        [Test]
        public void ExtractTextFromHtml_ExpectedBehavior()
        {
            var html = "<html>\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\r\n<meta name=\"Generator\" content=\"Microsoft Exchange Server\">\r\n<!-- converted from rtf -->\r\n<style><!-- .EmailQuote { margin-left: 1pt; padding-left: 4pt; border-left: #800000 2px solid; } --></style>\r\n</head>\r\n<body>\r\n<font face=\"Calibri\" size=\"2\"><span style=\"font-size:11pt;\">\r\n<div>test-task-226</div>\r\n<div>&nbsp;</div>\r\n</span></font>\r\n</body>\r\n</html>\r\n";
            var plainText = html.HtmlToPlainText();

            Console.WriteLine($"plainText = {plainText}");
        }
    }
}
