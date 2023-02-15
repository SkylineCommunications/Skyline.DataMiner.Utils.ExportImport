namespace Skyline.DataMiner.Utils.ExportImport.Exceptions
{
	using System;
	using System.Runtime.Serialization;

	[Serializable]
	public class InvalidColumnCountException : Exception
	{
		public InvalidColumnCountException()
		{
		}

		public InvalidColumnCountException(string message) : base(message)
		{
		}

		public InvalidColumnCountException(string message, Exception innerException) : base(message, innerException)
		{
		}

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