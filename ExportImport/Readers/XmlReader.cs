namespace Skyline.DataMiner.Utils.ExportImport.Readers
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Text;
	using System.Xml.Serialization;

	/// <summary>
	/// This class will read out a XML file and convert it into a list of the specified type of row.
	/// </summary>
	/// <typeparam name="T">Type of the data row.</typeparam>
	public class XmlReader<T> : Reader<T> where T : class, new()
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="XmlReader{T}"></see> class.
		/// </summary>
		public XmlReader(string fullPath) : base(fullPath)
		{
		}

		/// <inheritdoc cref="Reader{T}.Read"/>
		public override List<T> Read()
		{
			string text = String.Join(Environment.NewLine, GetFileData());

			return new XmlSerializer(typeof(List<T>)).Deserialize(
				new MemoryStream(Encoding.Default.GetBytes(text))) as List<T>;
		}
	}
}