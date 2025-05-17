﻿using Tudormobile.Strava.Model;

namespace Tudormobile.Strava.Api
{
    /// <summary>
    /// Strava API Error. Encapsulated the errors that may be returned from the API.
    /// </summary>
    public class ApiError
    {
        /// <summary>
        /// Error Message.
        /// </summary>
        public string Message { get; init; }

        /// <summary>
        /// Error exception.
        /// </summary>
        public Exception Exception { get; init; }

        /// <summary>
        /// Strava Fault
        /// </summary>
        public Fault Fault { get; init; }

        /// <summary>
        /// Create and initialize a new instance.
        /// </summary>
        /// <param name="message">Error message</param>
        /// <remarks>
        /// The exception property will contain an generic excetion with the same error message.
        /// </remarks>
        public ApiError(string? message = null) : this(message ?? "Unknown Strava API Error.", new Exception(message)) { }

        /// <summary>
        /// Create and initialize a new instance.
        /// </summary>
        /// <param name="exception">Exception that caused the error.</param>
        /// <remarks>
        /// The Message property will contain the exception message.
        /// </remarks>
        public ApiError(Exception exception) : this(exception.Message, exception) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fault"></param>
        public ApiError(Fault fault) : this(fault.Message) { Fault = fault; }

        /// <summary>
        /// Create and initialize a new instance.
        /// </summary>
        /// <param name="messge">Error message.</param>
        /// <param name="error">Error exception.</param>
        public ApiError(string messge, Exception error)
        {
            Message = messge;
            Exception = error;
            Fault = new Fault();
        }
    }
}
