using System;
using System.Collections.Generic;
using UnityEngine;

#if !UNITY_2018_1_OR_NEWER
using System.Linq;
#endif

namespace MultipleTypesPlayerPrefs
{
	public class FloatListPrefs : PrefsBase<List<float>>
	{
		public FloatListPrefs(string key, List<float> defaultValue) : base(key, defaultValue)
		{
		}

		protected override List<float> GetValueWhenHasKey()
		{
			var result = _defaultValue;
			try {
				var tmpResult = new List<float>();
				foreach (var floatStr in GetString().Split('|'))
				{
					tmpResult.Add(Convert.ToSingle(floatStr));
				}
				result = tmpResult;
			}
			catch (Exception e)
			{
				Debug.LogError("Key: " + _internalKey + " . Error: " + e);
			}

			return result;
		}

		public override IPrefs<List<float>> SetValue(List<float> value)
		{
#if UNITY_2018_1_OR_NEWER
			SetString(string.Join("|", value));
#else
			SetString(string.Join("|", value.Select(_ => _.ToString("R")).ToArray()));
#endif
			return this;
		}
	}
}
