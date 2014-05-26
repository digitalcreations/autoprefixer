﻿using System;

namespace Autoprefixer
{
    /// <summary>
	/// The exception that is thrown when a cleaning of asset code by Autoprefixer fails
	/// </summary>
    public sealed class AutoprefixerException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the BundleTransformer.Autoprefixer.Cleaners.AutoprefixerException class 
		/// with a specified error message
		/// </summary>
		/// <param name="message">The message that describes the error</param>
		public AutoprefixerException(string message)
			: base(message)
		{ }

		/// <summary>
		/// Initializes a new instance of the BundleTransformer.Autoprefixer.Cleaners.AutoprefixerException class 
		/// with a specified error message and a reference to the inner exception that is the cause of this exception
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception</param>
		/// <param name="innerException">The exception that is the cause of the current exception</param>
		public AutoprefixerException(string message, Exception innerException)
			: base(message, innerException)
		{ }
	}
}