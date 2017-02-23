﻿using System;
using System.Collections.Generic;
using System.Net;

namespace TeleSign.Services.AutoVerify
{
    /// <summary>
    /// AutoVerify is a secure, lightweight SDK that integrates a frictionless user verification process into existing native mobile applications.
    /// </summary>
    public class RawAutoVerifyService: TeleSignService
    {
        private const string AUTOVERIFY_STATUS_RESOURCE = "/v1/mobile/verification/status/{0}";

        public RawAutoVerifyService(TeleSignServiceConfiguration configuration) :base(configuration, null) { }

        /// <summary>
        /// Retrieves the verification result for an AutoVerify transaction by external_id.To ensure a secure verification flow you must check the status using TeleSign's servers on your backend. Do not rely on the SDK alone to indicate a successful verification.See<a href="https://developer.telesign.com/docs/auto-verify-sdk#section-obtaining-verification-status"> for detailed API documentation</a>.
        /// </summary>
        /// <param name="externalId"></param>
        /// <param name="statusParams"></param>
        /// <returns></returns>
        public TSResponse StatusRaw(string externalId, Dictionary<String, String> statusParams = null) {
            if (null == statusParams)
                statusParams = new Dictionary<string, string>();

            statusParams.Add("external_id", externalId);

            string resourceName = string.Format(AUTOVERIFY_STATUS_RESOURCE, externalId);

            WebRequest request = this.ConstructWebRequest(resourceName, "GET", statusParams, AuthenticationMethod.HmacSha256);

            return this.WebRequester.ReadTeleSignResponse(request);
        }
    }
}