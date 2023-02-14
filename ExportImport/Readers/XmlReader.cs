namespace Skyline.DataMiner.Utils.ExportImport.Readers
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Text;
	using System.Xml.Serialization;

	public class XmlReader<T> : Reader<T> where T : class, new()
	{
		public XmlReader(string fullPath) : base(fullPath)
		{
		}

		public override List<T> Read()
		{
			string text = String.Join(Environment.NewLine, GetFileData());

			return new XmlSerializer(typeof(List<T>)).Deserialize(
				new MemoryStream(Encoding.Default.GetBytes(text))) as List<T>;
		}
	}
}