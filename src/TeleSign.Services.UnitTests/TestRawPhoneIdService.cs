//-----------------------------------------------------------------------
// <copyright file="TestPhoneIdRawService.cs" company="TeleSign Corporation">
//     Copyright (c) TeleSign Corporation 2012.
// </copyright>
// <license>MIT</license>
// <author>Zentaro Kavanagh</author>
//-----------------------------------------------------------------------

namespace TeleSign.Services.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using TeleSign.Services.PhoneId;

    [TestFixture] 
    public class TestRawPhoneIdService : BaseServiceTest
    {
        private RawPhoneIdService CreateService(HttpMessageHandler webRequester = null)
        {
            if (webRequester == null)
            {
                webRequester = new FakeWebRequester();
            }

            return new RawPhoneIdService(this.GetConfiguration(), webRequester);
        }

        private string CreateDefaultQueryString()
        {
            return this.CreateQueryString(new Dictionary<string, string>()
            {
                { "ucid", "othr" }
            });
        }

        [Test]
        public void TestStandardRejectsNullNumber()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() =>
                this.CreateService().StandardLookupRawAsync(null)
            );
        }

        [Test]
        public void TestContactRejectsNullNumber()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() =>
                this.CreateService().ContactLookupRawAsync(null)
            );
        }

        [Test]
        public void TestScoreRejectsNullNumber()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() =>
                this.CreateService().ScoreLookupRawAsync(null)
            );
        }

        [Test]
        public void TestLiveRejectsNullNumber()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() =>
                this.CreateService().LiveLookupRawAsync(null)
            );
        }

        [Test]
        public void TestStandardRejectsEmptyNumber()
        {
            Assert.ThrowsAsync<ArgumentException>(() =>
                this.CreateService().StandardLookupRawAsync(string.Empty)
            );
        }

        [Test]
        public void TestContactRejectsEmptyNumber()
        {
            Assert.ThrowsAsync<ArgumentException>(() =>
                this.CreateService().ContactLookupRawAsync(string.Empty)
            );
        }

        [Test]
        public void TestScoreRejectsEmptyNumber()
        {
            Assert.ThrowsAsync<ArgumentException>(() =>
                this.CreateService().ScoreLookupRawAsync(string.Empty)
            );
        }

        [Test]
        public void TestLiveRejectsEmptyNumber()
        {
            Assert.ThrowsAsync<ArgumentException>(() =>
                this.CreateService().LiveLookupRawAsync(string.Empty)
            );
        }

        [Test]
        public void TestStandardRejectsNumberWithNoDigits()
        {
            Assert.ThrowsAsync<ArgumentException>(() =>
                this.CreateService().StandardLookupRawAsync("X+#$?")
            );
        }

        [Test]
        public void TestContactRejectsNumberWithNoDigits()
        {
            Assert.ThrowsAsync<ArgumentException>(() =>
                this.CreateService().ContactLookupRawAsync("X+#$?")
            );
        }

        [Test]
        public void TestScoreRejectsNumberWithNoDigits()
        {
            Assert.ThrowsAsync<ArgumentException>(() =>
                this.CreateService().ScoreLookupRawAsync("X+#$?")
            );
        }

        [Test]
        public void TestLiveRejectsNumberWithNoDigits()
        {
            Assert.ThrowsAsync<ArgumentException>(() =>
                this.CreateService().LiveLookupRawAsync("X+#$?")
            );
        }

        [Test]
        public async Task TestStandardDoesNotRejectCleanableNumbers()
        {
            await this.CreateService().StandardLookupRawAsync("+61 (08) 9791-1234");
        }

        [Test]
        public async Task TestContactDoesNotRejectCleanableNumbers()
        {
            await this.CreateService().ContactLookupRawAsync("+61 (08) 1111-1234");
        }

        [Test]
        public async Task TestScoreDoesNotRejectCleanableNumbers()
        {
            await this.CreateService().ScoreLookupRawAsync("+61 (08) 1111-1234");
        }

        [Test]
        public async Task TestLiveDoesNotRejectCleanableNumbers()
        {
            await this.CreateService().LiveLookupRawAsync("+61 (08) 1111-1234");
        }

        [Test]
        public async Task TestStandardWebRequest()
        {
            SerializingWebRequester requester = new SerializingWebRequester(
                HttpMethod.Get,
                "/v1/phoneid/standard/61811111234",
                null,
                this.CreateDefaultQueryString());

            string expectedResponse = requester.ConstructSerializedString();

            string actualResponse = await this.CreateService(requester).StandardLookupRawAsync("61811111234");

            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task TestContactWebRequest()
        {
            SerializingWebRequester requester = new SerializingWebRequester(
                HttpMethod.Get,
                "/v1/phoneid/contact/61811111234",
                null,
                this.CreateDefaultQueryString());
            string expectedResponse = requester.ConstructSerializedString();

            string actualResponse = await this.CreateService(requester).ContactLookupRawAsync("61811111234");

            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task TestScoreWebRequest()
        {
            SerializingWebRequester requester = new SerializingWebRequester(
                HttpMethod.Get,
                "/v1/phoneid/score/61811111234",
                null,
                this.CreateDefaultQueryString());

            string expectedResponse = requester.ConstructSerializedString();

            string actualResponse = await this.CreateService(requester).ScoreLookupRawAsync("61811111234");

            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task TestLiveWebRequest()
        {
            SerializingWebRequester requester = new SerializingWebRequester(
                HttpMethod.Get,
                "/v1/phoneid/live/61811111234",
                null,
                this.CreateDefaultQueryString());

            string expectedResponse = requester.ConstructSerializedString();

            string actualResponse = await this.CreateService(requester).LiveLookupRawAsync("61811111234");

            Assert.AreEqual(expectedResponse, actualResponse);
        }
    }
}
