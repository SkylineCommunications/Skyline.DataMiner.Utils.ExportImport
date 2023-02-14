﻿namespace Skyline.DataMiner.Utils.ExportImport.Exceptions
{
	using System;
	using System.Reflection;
	using System.Runtime.Serialization;

	[Serializable]
	public class MissingCsvHeaderAttributeWithPositionException : Exception
	{
		public MissingCsvHeaderAttributeWithPositionException()
		{
		}

		public MissingCsvHeaderAttributeWithPositionException(string message) : base(message)
		{
		}

		public MissingCsvHeaderAttributeWithPositionException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected MissingCsvHeaderAttributeWithPositionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public static MissingCsvHeaderAttributeWithPositionException FromPropertyInfo(PropertyInfo property)
		{
			string message = $"Property '{property.Name}' doesn't have a CsvHeaderAttribute with position.";
			return new MissingCsvHeaderAttributeWithPositionException(message);
		}
	}
}