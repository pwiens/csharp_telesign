//-----------------------------------------------------------------------
// <copyright file="SerializingWebRequester.cs" company="TeleSign Corporation">
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

    /// <summary>
    /// <para>
    /// A simple implemenetation of IWebRequester for testing purposes.
    /// It takes a select set of parameters from the web request
    /// and concatenates them into a key-value pair string set. This is
    /// used to validate that relevant inputs to the higher level
    /// API's made their way down to the WebRequest. This class does
    /// not make any network request.
    /// </para>
    /// <para>
    /// Also note - that what it returns is not valid JSON that the
    /// parser will not be able to process so this is only useful
    /// for testing the raw versions of the service.
    /// </para>
    /// </summary>
    public class SerializingWebRequester : DelegatingHandler
    {
        private HttpMethod method;
        private string localUriPath;
        private string contentType;
        private string queryString;

        public SerializingWebRequester(HttpMethod method, string localUriPath, string contentType, string queryString) : base(new HttpClientHandler())
        {
            this.method = method;
            this.localUriPath = localUriPath;
            this.contentType = contentType;
            this.queryString = queryString;
        }

        public string ConstructSerializedString()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("Method: {0}\r\n", DefaultIfNull(method?.Method));
            builder.AppendFormat("LocalUriPath: {0}\r\n", DefaultIfNull(localUriPath));
            builder.AppendFormat("ContentType: {0}\r\n", DefaultIfNull(contentType));
            builder.AppendFormat("QueryString: {0}\r\n", DefaultIfNull(queryString));

            return builder.ToString();
        }

        private static string DefaultIfNull(string value)
        {
            if (value == null)
            {
                return "<null>";
            }

            return value;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(this.ConstructSerializedString(), Encoding.UTF8, this.contentType)
                });
        }
    }
}
