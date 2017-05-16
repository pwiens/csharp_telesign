//-----------------------------------------------------------------------
// <copyright file="IWebRequester.cs" company="TeleSign Corporation">
//     Copyright (c) TeleSign Corporation 2012.
// </copyright>
// <license>MIT</license>
// <author>Zentaro Kavanagh</author>
//-----------------------------------------------------------------------

namespace TeleSign.Services
{
    using System.Net;
    using System.Net.Http;

    /// <summary>
    /// Trivial abstraction to the interaction with the service/internet.
    /// Useful for replacing for testing or other interception/tracing purposes.
    /// </summary>
    public interface IWebRequester
    {
        /// <summary>
        /// Given a web request - reads the response and returns it all
        /// as a string.
        /// </summary>
        /// <param name="response"></param>
        /// <returns>The response as a string.</returns>
        string ReadResponseAsString(HttpResponseMessage response);
    }
}
