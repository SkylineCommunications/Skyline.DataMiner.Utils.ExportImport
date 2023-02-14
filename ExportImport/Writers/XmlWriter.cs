namespace Skyline.DataMiner.Utils.ExportImport.Writers
{
	using System.Collections.Generic;
	using System.IO;
	using System.Text;
	using System.Xml.Serialization;

	public class XmlWriter<T> : Writer<T> where T : class, new()
	{
		public XmlWriter(string fullPath) : base(fullPath)
		{
		}

		public override void Write(List<T> data)
		{
			using (StreamWriter sw = new StreamWriter(new FileStream(fullPath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite), Encoding.Default))
			{
				new XmlSerializer(typeof(List<T>)).Serialize(sw, data);
			}
		}
	}
}