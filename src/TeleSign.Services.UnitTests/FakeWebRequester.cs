//-----------------------------------------------------------------------
// <copyright file="FakeWebRequester.cs" company="TeleSign Corporation">
//     Copyright (c) TeleSign Corporation 2012.
// </copyright>
// <license>MIT</license>
// <author>Zentaro Kavanagh</author>
//-----------------------------------------------------------------------

namespace TeleSign.Services.UnitTests
{
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using TeleSign.Services;

    /// <summary>
    /// Implements the most trivial implementation of IWebRequester.
    /// It implements ReadResponseAsString by simply returning
    /// a response string that has been provided by calling SetResponse.
    /// </summary>
    public class FakeWebRequester : DelegatingHandler
    {
        /// <summary>
        /// The response that will be returned.
        /// </summary>
        private string response;

        public FakeWebRequester() : base(new HttpClientHandler())
        {
            this.response = string.Empty;
        }

        /// <summary>
        /// Set the response string. Every call to ReadResponseAsString will
        /// return this string.
        /// </summary>
        /// <param name="response">The response you want from ReadResponseAsString.</param>
        public void SetResponse(string response)
        {
            this.response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(this.response, Encoding.UTF8)
                });
        }
    }
}
