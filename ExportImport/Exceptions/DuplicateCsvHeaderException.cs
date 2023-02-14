namespace Skyline.DataMiner.Utils.ExportImport.Exceptions
{
	using System;
	using System.Runtime.Serialization;

	using Skyline.DataMiner.Utils.ExportImport.Attributes;

	[Serializable]
	public class DuplicateCsvHeaderException : Exception
	{
		public DuplicateCsvHeaderException()
		{
		}

		public DuplicateCsvHeaderException(string message) : base(message)
		{
		}

		public DuplicateCsvHeaderException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected DuplicateCsvHeaderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public static DuplicateCsvHeaderException From(CsvHeaderAttribute attr, Type @class)
		{
			string message = $"Duplicate attribute values in class '{@class.Name}'";
			return new DuplicateCsvHeaderException(message);
		}
	}
}