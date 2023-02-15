namespace Skyline.DataMiner.Utils.ExportImport.Readers
{
	using System;
	using System.Collections.Generic;

	using Newtonsoft.Json;

	/// <summary>
	/// This class will read out a JSON file and convert it into a list of the specified type of row.
	/// </summary>
	/// <typeparam name="T">Type of the data row.</typeparam>
	public class JsonReader<T> : Reader<T> where T : class, new()
	{
		public JsonReader(string fullPath) : base(fullPath)
		{
		}

		/// <inheritdoc cref="Reader{T}.Read"/>
		public override List<T> Read()
		{
			string text = String.Join(Environment.NewLine, GetFileData());

			return JsonConvert.DeserializeObject<List<T>>(text);
		}
	}
}