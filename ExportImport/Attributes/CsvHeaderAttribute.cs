namespace Skyline.DataMiner.Utils.ExportImport.Attributes
{
	using System;

	[AttributeUsage(AttributeTargets.Property)]
	public sealed class CsvHeaderAttribute : Attribute
	{
		public CsvHeaderAttribute(string headerName)
		{
			Header = headerName;
			Position = UInt16.MaxValue;
		}

		public CsvHeaderAttribute(ushort index)
		{
			Position = index;
		}

		public string Header { get; }

		public ushort Position { get; }
	}
}