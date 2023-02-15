namespace Skyline.DataMiner.Utils.ExportImport.Attributes
{
	using System;

	/// <summary>
	/// Specify either a name or position for each property.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class CsvHeaderAttribute : Attribute
	{
		/// <summary>
		/// Specify the name of the header.
		/// </summary>
		/// <param name="headerName">Name of the header.</param>
		public CsvHeaderAttribute(string headerName)
		{
			Header = headerName;
			Position = UInt16.MaxValue;
		}

		/// <summary>
		/// Specify the position of the column.
		/// </summary>
		/// <param name="index">Column position.</param>
		public CsvHeaderAttribute(ushort index)
		{
			Position = index;
		}

		/// <summary>
		/// Gets the header name of the column.
		/// </summary>
		public string Header { get; }

		/// <summary>
		/// Gets the position of the column.
		/// </summary>
		public ushort Position { get; }
	}
}