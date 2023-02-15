namespace Skyline.DataMiner.Utils.ExportImport.Writers
{
	using System.Collections.Generic;

	using Newtonsoft.Json;

	/// <summary>
	/// This class will convert the list of data into a JSON file.
	/// </summary>
	/// <typeparam name="T">Type of the data row.</typeparam>
	public class JsonWriter<T> : Writer<T> where T : class, new()
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="JsonWriter{T}"></see> class.
		/// </summary>
		public JsonWriter(string fullPath) : base(fullPath)
		{
		}

		/// <inheritdoc cref="Writer{T}.Write"/>
		public override void Write(List<T> data)
		{
			string text = JsonConvert.SerializeObject(data);

			SetFileData(text);
		}
	}
}