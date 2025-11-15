namespace MultipleTypesPlayerPrefs
{
	public class BoolPrefs : PrefsBase<bool>
	{
		public BoolPrefs(string key, bool defaultValue) : base(key, defaultValue)
		{
		}

		protected override bool GetValueWhenHasKey()
		{
			return GetInt() != 0;
		}

		public override IPrefs<bool> SetValue(bool value)
		{
			SetInt(value ? 1 : 0);
			return this;
		}
	}
}
