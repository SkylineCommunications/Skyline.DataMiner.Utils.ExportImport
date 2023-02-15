namespace Skyline.DataMiner.Utils.ExportImport.Writers
{
	using System.Collections.Generic;
	using System.IO;
	using System.Text;
	using System.Xml.Serialization;

	/// <summary>
	/// This class will convert the list of data into a XML file.
	/// </summary>
	/// <typeparam name="T">Type of the data row.</typeparam>
	public class XmlWriter<T> : Writer<T> where T : class, new()
	{
		public XmlWriter(string fullPath) : base(fullPath)
		{
		}

		/// <inheritdoc cref="Writer{T}.Write"/>
		public override void Write(List<T> data)
		{
			using (StreamWriter sw = new StreamWriter(new FileStream(FullPath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite), Encoding.Default))
			{
				new XmlSerializer(typeof(List<T>)).Serialize(sw, data);
			}
		}
	}
}