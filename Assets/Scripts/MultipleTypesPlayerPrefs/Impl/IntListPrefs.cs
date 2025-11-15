using System;
using System.Collections.Generic;
using UnityEngine;

#if !UNITY_2018_1_OR_NEWER
using System.Linq;
#endif

namespace MultipleTypesPlayerPrefs
{
	public class IntListPrefs : PrefsBase<List<int>>
	{
		public IntListPrefs(string key, List<int> defaultValue) : base(key, defaultValue)
		{
		}

		protected override List<int> GetValueWhenHasKey()
		{
			var result = _defaultValue;
			try {
				var tmpResult = new List<int>();
				string[] intStrArray = GetString().Split('|');
				foreach (var intStr in intStrArray)
				{
					tmpResult.Add(Convert.ToInt32(intStr));
				}
				result = tmpResult;
			}
			catch (Exception e)
			{
				Debug.LogError("Key: " + _internalKey + " . Error: " + e);
			}

			return result;
		}

		public override IPrefs<List<int>> SetValue(List<int> value)
		{
#if UNITY_2018_1_OR_NEWER
			SetString(string.Join("|", value));
#else
			SetString(string.Join("|", value.Select(_ => _.ToString()).ToArray()));
#endif
			return this;
		}
	}
}
