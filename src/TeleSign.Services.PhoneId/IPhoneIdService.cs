//-----------------------------------------------------------------------
// <copyright file="PhoneIdService.cs" company="TeleSign Corporation">
//     Copyright (c) TeleSign Corporation 2012.
// </copyright>
// <license>MIT</license>
// <author>Zentaro Kavanagh</author>
//-----------------------------------------------------------------------

namespace TeleSign.Services.PhoneId
{
    using System.Threading.Tasks;

    public interface IPhoneIdService : ITeleSignService
    {
        /// <summary>
        /// Performs a PhoneId Standard lookup on a phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number to lookup.</param>
        /// <returns>
        /// A StandardPhoneIdResponse object containing both status of the transaction
        /// and the resulting data (if successful).
        /// </returns>
        Task<PhoneIdStandardResponse> StandardLookupAsync(string phoneNumber);

        /// <summary>
        /// Performs a PhoneID Contact lookup on a phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number to lookup.</param>
        /// <returns>
        /// A ContactPhoneIdResponse object containing both status of the transaction
        /// and the resulting data (if successful).
        /// </returns>
        Task<PhoneIdContactResponse> ContactLookupAsync(string phoneNumber);

        /// <summary>
        /// Performs a PhoneID Score lookup on a phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number to lookup.</param>
        /// <returns>
        /// A ScorePhoneIdResponse object containing both status of the transaction
        /// and the resulting data (if successful).
        /// </returns>
        Task<PhoneIdScoreResponse> ScoreLookupAsync(string phoneNumber);

        /// <summary>
        /// Performs a PhoneID Live lookup on a phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number to lookup.</param>
        /// <returns>
        /// A PhoneIdLiveResponse object containing both status of the transaction
        /// and the resulting data (if successful).
        /// </returns>
        Task<PhoneIdLiveResponse> LiveLookupAsync(string phoneNumber);
    }
}
