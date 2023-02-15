namespace Skyline.DataMiner.Utils.ExportImport.Writers
{
	using System.Collections.Generic;
	using System.IO;
	using System.Text;

	/// <summary>
	/// This class will convert the list of data into a file.
	/// </summary>
	/// <typeparam name="T">Type of the data row.</typeparam>
	public abstract class Writer<T> where T : class, new()
	{
		private protected readonly string FullPath;

		private protected Writer(string fullPath)
		{
			FullPath = fullPath;
		}

		/// <summary>
		/// Writes the specified data to the file.
		/// </summary>
		/// <param name="data">The data to write.</param>
		public abstract void Write(List<T> data);

		private protected void SetFileData(string text)
		{
			// Make sure it can be opened even if its used by another process
			// Write the file with encoding => used for chars like 'é'
			using (StreamWriter sw = new StreamWriter(new FileStream(FullPath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite), Encoding.Default))
			{
				sw.Write(text);
			}
		}
	}
}