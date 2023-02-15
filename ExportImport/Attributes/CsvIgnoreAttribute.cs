namespace Skyline.DataMiner.Utils.ExportImport.Attributes
{
	using System;

	/// <summary>
	/// Specify this when a property should be ignored.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class CsvIgnoreAttribute : Attribute
	{
		/// <summary>
		/// This will let the Reader/Writer ignore the property.
		/// </summary>
		public CsvIgnoreAttribute()
		{
		}
	}
}