﻿namespace Skyline.DataMiner.Utils.ExportImport.Writers
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Text;

	using Skyline.DataMiner.Utils.ExportImport.Attributes;
	using Skyline.DataMiner.Utils.ExportImport.Exceptions;

	public class CsvWriter<T> : Writer<T> where T : class, new()
	{
		private string _separator;

		public CsvWriter(string fullPath) : base(fullPath)
		{
			_separator = ";";
		}

		/// <summary>
		/// Gets or sets the separator. Default this will be a comma.
		/// </summary>
		public string Separator
		{
			get => _separator;
			set
			{
				if (!String.IsNullOrWhiteSpace(value))
				{
					_separator = value;
				}
			}
		}

		/// <summary>
		/// When the list is empty, a file with only the headers will be created.
		/// </summary>
		/// <param name="data">Data to write to the CSV file.</param>
		public override void Write(List<T> data)
		{
			if (HasPositionHeaders())
			{
				WriteWithPositions(data);
			}
			else
			{
				WriteWithHeaders(data);
			}
		}

		private void WriteWithPositions(List<T> data)
		{
			// <Position, Getter>
			SortedDictionary<ushort, MethodInfo> props = GetPositionProperties();

			StringBuilder sb = new StringBuilder();

			foreach (var item in data)
			{
				List<string> items = new List<string>();

				foreach (var get in props.Values)
				{
					string value = Convert.ToString(get.Invoke(item, null));

					// Escape character in CSV is ". That's why we need to escape the " with another ".
					value = value.Replace("\"", "\"\"");

					items.Add(value);
				}

				sb.AppendLine(String.Join(Separator, items));
			}

			SetFileData(sb.ToString());
		}

		private void WriteWithHeaders(List<T> data)
		{
			// <HeaderName, Getter>
			Dictionary<string, MethodInfo> props = GetHeaderProperties();

			StringBuilder sb = new StringBuilder();
			sb.AppendLine(String.Join(Separator, props.Keys));

			foreach (var item in data)
			{
				List<string> items = new List<string>();

				foreach (var get in props.Values)
				{
					string value = Convert.ToString(get.Invoke(item, null));

					// Escape character in CSV is ". That's why we need to escape the " with another ".
					value = value.Replace("\"", "\"\"");

					items.Add(value);
				}

				sb.AppendLine(String.Join(Separator, items));
			}

			SetFileData(sb.ToString());
		}

		private static bool HasPositionHeaders()
		{
			var tempClass = typeof(T);
			var tempProps = tempClass.GetProperties();

			foreach (var tempProp in tempProps)
			{
				var attr = tempProp.GetCustomAttribute<CsvHeaderAttribute>();

				if (attr == null)
				{
					continue;
				}

				if (attr.Position != UInt16.MaxValue)
				{
					return true;
				}
			}

			return false;
		}

		private static Dictionary<string, MethodInfo> GetHeaderProperties()
		{
			var tempClass = typeof(T);
			var tempProps = tempClass.GetProperties();

			Dictionary<string, MethodInfo> props = new Dictionary<string, MethodInfo>();
			foreach (var tempProp in tempProps)
			{
				var attr = tempProp.GetCustomAttribute<CsvHeaderAttribute>();
				var ignore = tempProp.GetCustomAttribute<CsvIgnoreAttribute>();

				var name = attr == null ? tempProp.Name : attr.Header;

				if (name == null || ignore != null)
				{
					continue;
				}

				if (props.ContainsKey(name))
				{
					throw DuplicatePositionCsvHeaderException.From(attr, tempClass);
				}

				props.Add(name, tempProp.GetMethod);
			}

			return props;
		}

		private static SortedDictionary<ushort, MethodInfo> GetPositionProperties()
		{
			var tempClass = typeof(T);
			var tempProps = tempClass.GetProperties();

			SortedDictionary<ushort, MethodInfo> props = new SortedDictionary<ushort, MethodInfo>();
			foreach (var tempProp in tempProps)
			{
				var attr = tempProp.GetCustomAttribute<CsvHeaderAttribute>();
				var ignore = tempProp.GetCustomAttribute<CsvIgnoreAttribute>();

				if (ignore != null)
				{
					continue;
				}

				if (attr == null || attr.Position == UInt16.MaxValue)
				{
					// Missing CsvHeaderAttribute
					// Invalid CsvHeaderAttribute defined
					throw MissingCsvHeaderAttributeWithPositionException.FromPropertyInfo(tempProp);
				}

				if (props.ContainsKey(attr.Position))
				{
					throw DuplicatePositionCsvHeaderException.From(attr, tempClass);
				}

				props.Add(attr.Position, tempProp.GetMethod);
			}

			return props;
		}
	}
}