namespace MultipleTypesPlayerPrefs
{
	public class FloatPrefs : PrefsBase<float>
	{
		public FloatPrefs(string key, float defaultValue) : base(key, defaultValue)
		{
		}

		protected override float GetValueWhenHasKey()
		{
			return GetFloat();
		}

		public override IPrefs<float> SetValue(float value)
		{
			SetFloat(value);
			return this;
		}
	}

	public class IntPrefs : PrefsBase<int>
	{
		public IntPrefs(string key, int defaultValue) : base(key, defaultValue)
		{
		}

		protected override int GetValueWhenHasKey()
		{
			return GetInt();
		}

		public override IPrefs<int> SetValue(int value)
		{
			SetInt(value);
			return this;
		}
	}

	public class StringPrefs : PrefsBase<string>
	{
		public StringPrefs(string key, string defaultValue) : base(key, defaultValue)
		{
		}

		protected override string GetValueWhenHasKey()
		{
			return GetString();
		}

		public override IPrefs<string> SetValue(string value)
		{
			SetString(value);
			return this;
		}
	}
}
