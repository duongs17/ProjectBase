using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MultipleTypesPlayerPrefs
{
	public static class PlayerPrefsHelper
	{
		public static string Serialize<T>(T obj)
		{
			var bf = new BinaryFormatter();
			var ms = new MemoryStream();
			bf.Serialize(ms, obj);
			return Convert.ToBase64String(ms.GetBuffer());
		}

		public static T Deserialize<T>(string str)
		{
			var bf = new BinaryFormatter();
			var ms = new MemoryStream(Convert.FromBase64String(str));
			return (T)bf.Deserialize(ms);
		}
	}
}
