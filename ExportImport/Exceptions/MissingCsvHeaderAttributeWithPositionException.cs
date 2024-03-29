﻿namespace Skyline.DataMiner.Utils.ExportImport.Exceptions
{
	using System;
	using System.Reflection;
	using System.Runtime.Serialization;

	/// <summary>
	/// Represents an error where CSV header attributes are missing.
	/// </summary>
	[Serializable]
	public class MissingCsvHeaderAttributeWithPositionException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MissingCsvHeaderAttributeWithPositionException"></see> class.
		/// </summary>
		public MissingCsvHeaderAttributeWithPositionException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MissingCsvHeaderAttributeWithPositionException"></see> class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public MissingCsvHeaderAttributeWithPositionException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MissingCsvHeaderAttributeWithPositionException"></see> class with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public MissingCsvHeaderAttributeWithPositionException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MissingCsvHeaderAttributeWithPositionException"></see> class with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="StreamingContext"></see> that contains contextual information about the source or destination.</param>
		/// <exception cref="ArgumentNullException">The <paramref name="info">info</paramref> parameter is null.</exception>
		/// <exception cref="SerializationException">The class name is null or <see cref="System.Exception.HResult"></see> is zero (0).</exception>
		protected MissingCsvHeaderAttributeWithPositionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		internal static MissingCsvHeaderAttributeWithPositionException FromPropertyInfo(PropertyInfo property)
		{
			string message = $"Property '{property.Name}' doesn't have a CsvHeaderAttribute with position.";
			return new MissingCsvHeaderAttributeWithPositionException(message);
		}
	}
}