using UnityEngine;

namespace MultipleTypesPlayerPrefs
{
	public class ColorPrefs : PrefsBase<Color>
	{
		public ColorPrefs(string key, Color defaultValue) : base(key, defaultValue)
		{
		}

		protected override Color GetValueWhenHasKey()
		{
			var htmlString = GetString();
			Color retColor;
			if (ColorUtility.TryParseHtmlString(htmlString, out retColor))
			{
				return retColor;
			}

			return _defaultValue;
		}

		public override IPrefs<Color> SetValue(Color value)
		{
			var htmlString = "#" + ColorUtility.ToHtmlStringRGBA(value);
			SetString(htmlString);
			return this;
		}
	}
}
