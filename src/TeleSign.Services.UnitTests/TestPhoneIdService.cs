//-----------------------------------------------------------------------
// <copyright file="TestPhoneIdService.cs" company="TeleSign Corporation">
//     Copyright (c) TeleSign Corporation 2012.
// </copyright>
// <license>MIT</license>
// <author>Zentaro Kavanagh</author>
//-----------------------------------------------------------------------

namespace TeleSign.Services.UnitTests
{
    using System;
    using System.Net.Http;
    using NUnit.Framework;
    using TeleSign.Services.PhoneId;

    [TestFixture]
    public class TestPhoneIdService : BaseServiceTest
    {
        private PhoneIdService CreateService(
                    HttpMessageHandler webRequester = null, 
                    IPhoneIdResponseParser responseParser = null)
        {
            if (webRequester == null)
            {
                webRequester = new FakeWebRequester();
            }

            if (responseParser == null)
            {
                responseParser = new FakeResponseParser();
            }

            return new PhoneIdService(
                        this.GetConfiguration(), 
                        webRequester, 
                        responseParser);
        }

        [Test]
        public void TestStandardRejectsNullNumber()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => this.CreateService().StandardLookupAsync(null));
        }

        [Test]
        public void TestContactRejectsNullNumber()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => this.CreateService().ContactLookupAsync(null));
        }

        [Test]
        public void TestScoreRejectsNullNumber()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => this.CreateService().ScoreLookupAsync(null));
        }

        [Test]
        public void TestStandardRejectsEmptyNumber()
        {
            Assert.ThrowsAsync<ArgumentException>(() => this.CreateService().StandardLookupRawAsync(string.Empty));
        }

        [Test]
        public void TestContactRejectsEmptyNumber()
        {
            Assert.ThrowsAsync<ArgumentException>(() => this.CreateService().ContactLookupAsync(string.Empty));
        }

        [Test]
        public void TestScoreRejectsEmptyNumber()
        {
            Assert.ThrowsAsync<ArgumentException>(() => this.CreateService().ScoreLookupAsync(string.Empty));
        }

        [Test]
        public void TestStandardRejectsNumberWithNoDigits()
        {
            Assert.ThrowsAsync<ArgumentException>(() => this.CreateService().StandardLookupAsync("X+#$?"));
        }

        [Test]
        public void TestContactRejectsNumberWithNoDigits()
        {
            Assert.ThrowsAsync<ArgumentException>(() => this.CreateService().ContactLookupAsync("X+#$?"));
        }

        [Test]
        public void TestScoreRejectsNumberWithNoDigits()
        {
            Assert.ThrowsAsync<ArgumentException>(() => this.CreateService().ScoreLookupAsync("X+#$?"));
        }

        [Test]
        public void TestPhoneIdStandardWrapsParserErrors()
        {
            string message = "My exception message";
            IPhoneIdResponseParser parser = new FakeResponseParser()
            {
                ExpectedException = new Exception(message),
            };

            PhoneIdService service = this.CreateService(null, parser);

            Assert.ThrowsAsync<ResponseParseException>(() => service.StandardLookupAsync("15555555555"));
        }

        [Test]
        public void TestPhoneIdContactWrapsParserErrors()
        {
            string message = "My exception message";
            IPhoneIdResponseParser parser = new FakeResponseParser()
            {
                ExpectedException = new Exception(message),
            };

            PhoneIdService service = this.CreateService(null, parser);

            Assert.ThrowsAsync<ResponseParseException>(() => service.ContactLookupAsync("15555555555"));
        }

        [Test]
        public void TestPhoneIdScoreWrapsParserErrors()
        {
            string message = "My exception message";
            IPhoneIdResponseParser parser = new FakeResponseParser()
            {
                ExpectedException = new Exception(message),
            };

            PhoneIdService service = this.CreateService(null, parser);

            Assert.ThrowsAsync<ResponseParseException>(() => service.ScoreLookupAsync("15555555555"));
        }
    }
}