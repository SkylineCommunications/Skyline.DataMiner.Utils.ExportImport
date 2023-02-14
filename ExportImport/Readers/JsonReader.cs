namespace Skyline.DataMiner.Utils.ExportImport.Readers
{
	using System;
	using System.Collections.Generic;

	using Newtonsoft.Json;

	public class JsonReader<T> : Reader<T> where T : class, new()
	{
		public JsonReader(string fullPath) : base(fullPath)
		{
		}

		public override List<T> Read()
		{
			string text = String.Join(Environment.NewLine, GetFileData());

			return JsonConvert.DeserializeObject<List<T>>(text);
		}
	}
}