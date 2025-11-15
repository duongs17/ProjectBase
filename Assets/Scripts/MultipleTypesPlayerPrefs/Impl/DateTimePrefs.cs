using System;

namespace MultipleTypesPlayerPrefs
{
	public class DateTimePrefs : PrefsBase<DateTime>
	{
		public DateTimePrefs(string key, DateTime defaultValue) : base(key, defaultValue)
		{
		}

		protected override DateTime GetValueWhenHasKey()
		{
			return new DateTime(Convert.ToInt64(GetString()));
		}

		public override IPrefs<DateTime> SetValue(DateTime value)
		{
			SetString(value.Ticks.ToString());
			return this;
		}
	}
}
