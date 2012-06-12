using System.Collections.Generic;
using LebroITSolutions.ChatModel;
using LeBroITSolutions.ServiceStack.Interfaces.ServiceInterface.ServiceModel;

namespace LeBroITSolutionsServiceInterface.ServiceResponses
{
    /// <summary>
    /// Definition of the Response
    /// </summary>
    public class ErrorMessageResponse
    {
        /// <summary>
        /// Gets or sets the error message result.
        /// </summary>
        /// <value>
        /// The error message result.
        /// </value>
        public ErrorMessage ErrorMessageResult { get; set; }

        /// <summary>
        /// Gets or sets the error message list result.
        /// </summary>
        /// <value>
        /// The error message list result.
        /// </value>
        public ErrorMessages ErrorMessageListResult { get; set; }

        /// <summary>
        /// Gets or sets the response status.
        /// </summary>
        /// <value>
        /// The response status.
        /// </value>
        public ResponseStatus ResponseStatus { get; set; }
    }
}
