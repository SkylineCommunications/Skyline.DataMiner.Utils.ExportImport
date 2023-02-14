namespace Skyline.DataMiner.Utils.ExportImport.Factories
{
	using System;
	using System.IO;

	using Skyline.DataMiner.Utils.ExportImport.Writers;

	public static class WriterFactory
	{
		/// <summary>
		/// Gets the correct file writer.
		/// </summary>
		/// <param name="fullPath">The full file path.</param>
		/// <exception cref="ArgumentException">Path is empty.</exception>
		/// <exception cref="ArgumentNullException">Path is Null.</exception>
		/// <exception cref="NotSupportedException">The extension isn't supported.</exception>
		public static Writer<T> GetWriter<T>(string fullPath) where T : class, new()
		{
			if (fullPath == null)
			{
				throw new ArgumentNullException(nameof(fullPath));
			}

			if (String.IsNullOrWhiteSpace(fullPath))
			{
				throw new ArgumentException("fullPath can't be empty.");
			}

			string extension = Path.GetExtension(fullPath);

			switch (extension)
			{
				case ".csv":
					return new CsvWriter<T>(fullPath);

				case ".json":
					return new JsonWriter<T>(fullPath);

				case ".xml":
					return new XmlWriter<T>(fullPath);

				default:
					throw new NotSupportedException($"This extension ({extension}) isn't supported.");
			}
		}
	}
}