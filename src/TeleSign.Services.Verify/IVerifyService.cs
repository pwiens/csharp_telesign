//-----------------------------------------------------------------------
// <copyright file="VerifyService.cs" company="TeleSign Corporation">
//     Copyright (c) TeleSign Corporation 2012.
// </copyright>
// <license>MIT</license>
// <author>Zentaro Kavanagh</author>
//-----------------------------------------------------------------------

namespace TeleSign.Services.Verify
{
    using System.Threading.Tasks;

    public interface IVerifyService : ITeleSignService
    {
        /// <summary>
        /// Initiates a TeleSign Verify transaction using SMS. The SMS will use the default
        /// US English message template and the API will generate the code for you.
        /// </summary>
        /// <param name="phoneNumber">The phone number to send the sms to.</param>
        /// <returns>
        /// A VerifyResponse object with the status and returned information
        /// for the transaction.
        /// </returns>
        Task<VerifyResponse> SendSmsAsync(string phoneNumber);

        /// <summary>
        /// Initiates a TeleSign Verify transaction using SMS. The SMS will use the default
        /// US English message template and the code you provide.
        /// </summary>
        /// <param name="phoneNumber">The phone number to send the sms to.</param>
        /// <param name="verifyCode">
        /// The code to send to the user. When null a code will
        /// be generated for you.
        /// </param>
        /// <returns>
        /// A VerifyResponse object with the status and returned information
        /// for the transaction.
        /// </returns>
        Task<VerifyResponse> SendSmsAsync(string phoneNumber, string verifyCode);

        /// <summary>
        /// Initiates a TeleSign Verify transaction using SMS. The SMS will use the default
        /// message template and the code you provide.
        /// </summary>
        /// <param name="phoneNumber">The phone number to send the sms to.</param>
        /// <param name="verifyCode">
        /// The code to send to the user. When null a code will
        /// be generated for you.
        /// </param>
        /// <param name="language">
        /// The language that the message should be in.
        /// TODO: Details about  language string format.
        /// </param>
        /// <returns>
        /// A VerifyResponse object with the status and returned information
        /// for the transaction.
        /// </returns>
        Task<VerifyResponse> SendSmsAsync(string phoneNumber, string verifyCode, string language);

        /// <summary>
        /// Initiates a TeleSign Verify transaction using SMS. The SMS will use the default
        /// message template and the code you provide.
        /// </summary>
        /// <param name="phoneNumber">The phone number to send the sms to.</param>
        /// <param name="verifyCode">
        /// The code to send to the user. When null a code will
        /// be generated for you.
        /// </param>
        /// <param name="messageTemplate">
        /// A template for the message to be sent to the user. This string must be non-empty
        /// and contain the magic string $$CODE$$ which will be replaced by the
        /// verify code in the message.
        /// </param>
        /// <param name="language">
        /// The language that the message should be in. If a message template is specified
        /// language will be ignored.
        /// TODO: Details about  language string format.
        /// </param>
        /// <returns>
        /// A VerifyResponse object with the status and returned information
        /// for the transaction.
        /// </returns>
        Task<VerifyResponse> SendSmsAsync(string phoneNumber, string verifyCode, string messageTemplate, string language);

        /// <summary>
        /// The TeleSign Verify 2-Way SMS web service allows you to authenticate your users and verify user transactions via two-way Short Message Service (SMS) wireless communication. Verification requests are sent to user’s in a text message, and users return their verification responses by replying to the text message.
        /// </summary>
        /// <param name="phoneNumber">The phone number for the Verify Soft Token request, including country code</param>
        /// <param name="message">
        /// The text to display in the body of the text message. You must include the $$CODE$$ placeholder for the verification code somewhere in your message text. TeleSign automatically replaces it with a randomly-generated verification code
        /// </param>
        /// <param name="validityPeriod">
        /// This parameter allows you to place a time-limit on the verification. This provides an extra level of security by restricting the amount of time your end user has to respond (after which, TeleSign automatically rejects their response). Values are expressed as a natural number followed by a lower-case letter that represents the unit of measure. You can use 's' for seconds, 'm' for minutes, 'h' for hours, and 'd' for days
        /// </param>
        /// <param name="ucid">
        /// A string specifying one of the Use Case Codes
        /// </param>
        /// <returns>The raw JSON response from the REST API.</returns>
        Task<VerifyResponse> SendTwoWaySmsAsync(string phoneNumber, string message = null, string validityPeriod = "5m");

        /// <summary>
        /// Initiates a TeleSign Verify transaction via a voice call.
        /// </summary>
        /// <param name="phoneNumber">The phone number to call.</param>
        /// <param name="verifyCode">
        /// The code to send to the user. When null a code will
        /// be generated for you.
        /// </param>
        /// <param name="language">
        /// The language that the message should be in. This parameter is ignored if
        /// you supplied a message template.
        /// TODO: Details about language string format.
        /// </param>
        /// <returns>
        /// A VerifyResponse object with the status and returned information
        /// for the transaction.
        /// </returns>
        Task<VerifyResponse> InitiateCallAsync(string phoneNumber, string verifyCode = null, string language = "en");

        /// <summary>
        /// Initiates a TeleSign Verify transaction via a voice call.
        /// </summary>
        /// <param name="phoneNumber">The phone number to call.</param>
        /// <param name="verifyCode">
        /// The code to send to the user. When null a code will
        /// be generated for you.
        /// </param>
        /// <param name="language">
        /// The language that the message should be in. This parameter is ignored if
        /// you supplied a message template.
        /// </param>
        /// <returns>
        /// A VerifyResponse object with the status and returned information
        /// for the transaction.
        /// </returns>
        Task<VerifyResponse> InitiatePushAsync(string phoneNumber, string verifyCode = null);

        /// <summary>
        /// Validates the code provided by the user. After the code has been
        /// sent to the user, they enter the code to your website/application
        /// and you pass the code here to verify.
        /// </summary>
        /// <param name="referenceId">
        /// The reference id return in the VerifyResponse from
        /// a call to Sms or Call.
        /// </param>
        /// <param name="verifyCode">The code the user provided you to be verified.</param>
        /// <returns>
        /// A VerifyResponse object containing information about the status
        /// of the transactation and validity of the code.
        /// </returns>
        Task<VerifyResponse> ValidateCodeAsync(string referenceId, string verifyCode);

        /// <summary>
        /// Checks the status of TeleSign Verify transaction. At the underlying REST
        /// API layer this is the same call as ValidateCode (to the Status resource)
        /// but without supplying the code. This is useful to check the progress
        /// of the SMS or call prior to the user responding with the code.
        /// </summary>
        /// <param name="referenceId">
        /// The reference id return in the VerifyResponse from
        /// a call to Sms or Call.
        /// </param>
        /// <returns>
        /// A VerifyResponse object containing information about the status
        /// of the transactation
        /// </returns>
        Task<VerifyResponse> CheckStatusAsync(string referenceId);

        /// <summary>
        /// Validates whether a verifyCode is valid to use with the TeleSign API.
        /// Null is valid and indicates the API should generate the code itself,
        /// empty strings are not valid, any non-digit characters are not valid
        /// and leading zeros are not valid.
        /// </summary>
        /// <param name="verifyCode">The code to verify.</param>
        void ValidateCodeFormat(string verifyCode);
    }
}
