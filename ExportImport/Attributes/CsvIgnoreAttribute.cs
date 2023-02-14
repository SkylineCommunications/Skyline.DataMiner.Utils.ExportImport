namespace Skyline.DataMiner.Utils.ExportImport.Attributes
{
	using System;

	[AttributeUsage(AttributeTargets.Property)]
	public sealed class CsvIgnoreAttribute : Attribute
	{
		public CsvIgnoreAttribute()
		{
		}
	}
}