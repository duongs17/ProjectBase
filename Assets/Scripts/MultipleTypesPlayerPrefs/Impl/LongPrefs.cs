using System;
using UnityEngine;

namespace MultipleTypesPlayerPrefs
{
	public class LongPrefs : PrefsBase<long>
	{
		public LongPrefs(string key, long defaultValue = 0) : base(key, defaultValue)
		{
		}

		protected override long GetValueWhenHasKey()
		{
			var str = GetString();
			try
			{
				var bytes = Convert.FromBase64String(str);
				return BitConverter.ToInt64(bytes, 0);
			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}

			return _defaultValue;
		}

		public override IPrefs<long> SetValue(long value)
		{
			var bytes = BitConverter.GetBytes(value);
			SetString(Convert.ToBase64String(bytes));
			return this;
		}
	}
}
