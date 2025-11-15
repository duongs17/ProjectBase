using System.Collections.Generic;

namespace MultipleTypesPlayerPrefs
{
	public class GenericListPrefs<T> : PrefsBase<List<T>>
	{
		public GenericListPrefs(string key, List<T> defaultValue) : base(key, defaultValue)
		{
		}

		protected override List<T> GetValueWhenHasKey()
		{
			string serizlizedList = GetString();
			if (string.IsNullOrEmpty(serizlizedList))
			{
				return _defaultValue;
			}
			return PlayerPrefsHelper.Deserialize<List<T>> (serizlizedList);
		}

		public override IPrefs<List<T>> SetValue(List<T> value)
		{
			string serizlizedList = PlayerPrefsHelper.Serialize(value);
			SetString(serizlizedList);
			return this;
		}
	}
}
