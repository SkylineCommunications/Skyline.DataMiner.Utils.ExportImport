namespace Skyline.DataMiner.Utils.ExportImport.Exceptions
{
	using System;
	using System.Runtime.Serialization;

	/// <summary>
	/// Represents an error where an invalid amount of columns are detected.
	/// </summary>
	[Serializable]
	public class InvalidColumnCountException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InvalidColumnCountException"></see> class.
		/// </summary>
		public InvalidColumnCountException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InvalidColumnCountException"></see> class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public InvalidColumnCountException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InvalidColumnCountException"></see> class with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public InvalidColumnCountException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InvalidColumnCountException"></see> class with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="StreamingContext"></see> that contains contextual information about the source or destination.</param>
		/// <exception cref="ArgumentNullException">The <paramref name="info">info</paramref> parameter is null.</exception>
		/// <exception cref="SerializationException">The class name is null or <see cref="System.Exception.HResult"></see> is zero (0).</exception>
		protected InvalidColumnCountException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		internal static InvalidColumnCountException From(int columnCount, int rowNumber, int expectedCount)
		{
			string message = $"Invalid Column Count ({columnCount}) for row {rowNumber}. Expected Column Count: {expectedCount}";
			return new InvalidColumnCountException(message);
		}
	}
}