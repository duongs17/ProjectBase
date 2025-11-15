using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MultipleTypesPlayerPrefs
{
	public class StringListPrefs : PrefsBase<List<string>>
	{
		public StringListPrefs(string key, List<string> defaultValue) : base(key, defaultValue)
		{
		}

		protected override List<string> GetValueWhenHasKey()
		{
			var result = _defaultValue;
			try {
				result = GetString().Split('|').ToList();
			}
			catch (Exception e)
			{
				Debug.LogError("Key: " + _internalKey + " . Error: " + e);
			}

			return result;
		}

		public override IPrefs<List<string>> SetValue(List<string> value)
		{
#if UNITY_2018_1_OR_NEWER
			SetString(string.Join("|", value));
#else
			SetString(string.Join("|", value.ToArray()));
#endif
			return this;
		}
	}
}
