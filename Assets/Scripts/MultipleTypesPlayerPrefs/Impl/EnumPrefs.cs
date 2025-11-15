using System;
using UnityEngine;

namespace MultipleTypesPlayerPrefs
{
	public class EnumPrefs<TEnum> : PrefsBase<TEnum>
		where TEnum : struct, IConvertible
	{
		public EnumPrefs(string key, TEnum defaultValue) : base(key, defaultValue)
		{
		}

		protected override TEnum GetValueWhenHasKey()
		{
			var result = _defaultValue;
			try
			{
				result = (TEnum)Enum.ToObject(typeof(TEnum), GetInt());
			}
			catch (Exception e)
			{
				Debug.LogError("Key: " + _internalKey + " . Error: " + e);
			}

			return result;
		}

		public override IPrefs<TEnum> SetValue(TEnum value)
		{
			SetInt(Convert.ToInt32(value));
			return this;
		}
	}
}
