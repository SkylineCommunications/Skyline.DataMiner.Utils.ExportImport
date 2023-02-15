namespace Skyline.DataMiner.Utils.ExportImport.Exceptions
{
	using System;
	using System.Runtime.Serialization;

	using Skyline.DataMiner.Utils.ExportImport.Attributes;

	[Serializable]
	public class DuplicateHeaderCsvHeaderException : DuplicateCsvHeaderException
	{
		public DuplicateHeaderCsvHeaderException()
		{
		}

		public DuplicateHeaderCsvHeaderException(string message) : base(message)
		{
		}

		public DuplicateHeaderCsvHeaderException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected DuplicateHeaderCsvHeaderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		internal new static DuplicateHeaderCsvHeaderException From(CsvHeaderAttribute attr, Type @class)
		{
			string message = $"Duplicate header '{attr.Header}' in class '{@class.Name}'.";
			return new DuplicateHeaderCsvHeaderException(message);
		}
	}
}