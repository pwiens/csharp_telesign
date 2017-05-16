//-----------------------------------------------------------------------
// <copyright file="TestVerifyService.cs" company="TeleSign Corporation">
//     Copyright (c) TeleSign Corporation 2012.
// </copyright>
// <license>MIT</license>
// <author>Zentaro Kavanagh</author>
//-----------------------------------------------------------------------

namespace TeleSign.Services.UnitTests
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using TeleSign.Services.Verify;

    [TestFixture]
    public class TestVerifyService : BaseServiceTest
    {
        private VerifyService CreateService(
                    HttpMessageHandler webRequester = null, 
                    IVerifyResponseParser responseParser = null)
        {
            if (webRequester == null)
            {
                webRequester = new FakeWebRequester();
            }

            if (responseParser == null)
            {
                responseParser = new FakeResponseParser();
            }

            return new VerifyService(
                        this.GetConfiguration(), 
                        webRequester, 
                        responseParser);
        }

        [Test]
        public void TestSmsRejectsNullNumber()
        {
            Assert.ThrowsAsync<ArgumentException>(() => this.CreateService().SendSmsAsync(null));
        }

        [Test]
        public void TestSmsRejectsEmptyNumber()
        {
            Assert.ThrowsAsync<ArgumentException>(() => this.CreateService().SendSmsAsync(string.Empty));
        }

        [Test]
        public void TestSmsRejectsNumberWithNoDigits()
        {
            Assert.ThrowsAsync<ArgumentException>(() => this.CreateService().SendSmsAsync("X+#$?"));
        }

        [Test]
        public async Task TestSmsDoesNotRejectCleanableNumbers()
        {
            await this.CreateService().SendSmsAsync("+61 (08) 1111-1234");
        }
    }
}