using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using Helios.Api.Utils.Encryption.Providers;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using RestSharp.Extensions.MonoHttp;

namespace Helios.Tests.Utils
{
    [TestFixture]
    public sealed class StringEncryptorTest
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
        public void QueryString_ShouldWork()
        {
            var qb = new QueryBuilder();
            qb.Add("key1","value1 asd");
            qb.Add("key2", "value2");
            var query = qb.ToQueryString().ToString().Substring(1);
            

            Console.WriteLine(query);
        }
    }
}
