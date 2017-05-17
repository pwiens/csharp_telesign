//-----------------------------------------------------------------------
// <copyright file="TeleSignService.cs" company="TeleSign Corporation">
//     Copyright (c) TeleSign Corporation 2012.
// </copyright>
// <license>MIT</license>
// <author>Zentaro Kavanagh</author>
//-----------------------------------------------------------------------

namespace TeleSign.Services
{
    public interface ITeleSignService
    {
        /// <summary>
        /// Cleans up phone number strings by removing any non-digit characters from
        /// them. Will throw an exception if the resulting cleaned up string has
        /// no characters.
        /// </summary>
        /// <param name="phoneNumber">The input phone number.</param>
        /// <returns>The input phone number with all non-digit characters removed.</returns>
        string CleanupPhoneNumber(string phoneNumber);
    }
}
