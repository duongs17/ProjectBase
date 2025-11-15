using UnityEngine;

namespace MultipleTypesPlayerPrefs
{
	public class JsonPrefs<T> : PrefsBase<T>
	{
		public JsonPrefs(string key, T defaultValue) : base(key, defaultValue)
		{
		}

		protected override T GetValueWhenHasKey()
		{
			string str = GetString();
			return string.IsNullOrEmpty(str) ? _defaultValue : JsonUtility.FromJson<T>(str);
		}

		public override IPrefs<T> SetValue(T value)
		{
			SetString(JsonUtility.ToJson(value));
			return this;
		}
	}
}
