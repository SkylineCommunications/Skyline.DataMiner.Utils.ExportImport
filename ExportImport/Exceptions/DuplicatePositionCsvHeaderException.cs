namespace Skyline.DataMiner.Utils.ExportImport.Exceptions
{
	using System;
	using System.Runtime.Serialization;

	using Skyline.DataMiner.Utils.ExportImport.Attributes;

	[Serializable]
	public class DuplicatePositionCsvHeaderException : DuplicateCsvHeaderException
	{
		public DuplicatePositionCsvHeaderException()
		{
		}

		public DuplicatePositionCsvHeaderException(string message) : base(message)
		{
		}

		public DuplicatePositionCsvHeaderException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected DuplicatePositionCsvHeaderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		internal new static DuplicatePositionCsvHeaderException From(CsvHeaderAttribute attr, Type @class)
		{
			string message = $"Duplicate position '{attr.Header}' in class '{@class.Name}'.";
			return new DuplicatePositionCsvHeaderException(message);
		}
	}
}