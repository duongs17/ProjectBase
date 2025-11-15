using System.Collections.Generic;

namespace MultipleTypesPlayerPrefs
{
	public class DictionaryPrefs<TKey, TValue> : PrefsBase<Dictionary<TKey, TValue>>
	{
		public DictionaryPrefs(string key, Dictionary<TKey, TValue> defaultValue) : base(key, defaultValue)
		{
		}

		protected override Dictionary<TKey, TValue> GetValueWhenHasKey()
		{
			string serizlizedDict = GetString();
			if (string.IsNullOrEmpty(serizlizedDict))
			{
				return _defaultValue;
			}


			return PlayerPrefsHelper.Deserialize<Dictionary<TKey, TValue>>(serizlizedDict);
		}

		public override IPrefs<Dictionary<TKey, TValue>> SetValue(Dictionary<TKey, TValue> value)
		{
			string serizlizedDict = PlayerPrefsHelper.Serialize(value);
			SetString(serizlizedDict);
			return this;
		}
	}
}
