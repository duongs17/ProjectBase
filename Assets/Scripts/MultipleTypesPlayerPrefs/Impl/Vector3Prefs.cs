using System;
using UnityEngine;

namespace MultipleTypesPlayerPrefs
{
	public class Vector3Prefs : PrefsBase<Vector3>
	{
		public Vector3Prefs(string key, Vector3 defaultValue) : base(key, defaultValue)
		{
		}

		protected override Vector3 GetValueWhenHasKey()
		{
			var result = _defaultValue;
			try
			{
				var vectorStr = GetString().Split('|');
				result.x = Convert.ToSingle(vectorStr[0]);
				result.y = Convert.ToSingle(vectorStr[1]);
				result.z = Convert.ToSingle(vectorStr[2]);
			}
			catch (Exception e)
			{
				Debug.LogError("Key: " + _internalKey + " . Error: " + e);
			}

			return result;
		}

		public override IPrefs<Vector3> SetValue(Vector3 value)
		{
#if UNITY_2018_1_OR_NEWER
			SetString(string.Join("|", new[] {value.x, value.y, value.z}));
#else
			SetString(string.Join("|", new[] {value.x.ToString("R"), value.y.ToString("R"), value.z.ToString("R")}));
#endif
			return this;
		}
	}
}
