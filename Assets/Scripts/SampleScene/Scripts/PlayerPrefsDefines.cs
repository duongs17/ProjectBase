using System;
using System.Collections.Generic;
using UnityEngine;

namespace MultipleTypesPlayerPrefs
{
	public static class PlayerPrefsDefines
	{
		/*
		 * Define your playerPrefs Here!
		 */
		public static readonly IPrefs<bool>                    BoolPrefs = new BoolPrefs("bool_prefs", false);
		public static readonly IPrefs<DateTime>                DateTimePrefs = new DateTimePrefs("date_time_prefs", DateTime.MinValue);
		public static readonly IPrefs<TestType>                EnumPrefs = new EnumPrefs<TestType>("enum_prefs", TestType.Type1);
		public static readonly IPrefs<List<float>>             FloatListPrefs = new FloatListPrefs("float_list_prefs", new List<float>());
		public static readonly IPrefs<List<int>>               IntListPrefs = new IntListPrefs("int_list_prefs", new List<int>());
		public static readonly IPrefs<List<string>>            StringListPrefs = new StringListPrefs("string_list_prefs", new List<string>());
		public static readonly IPrefs<List<int>>               GenericList = new GenericListPrefs<int>("ge_list_prefs", new List<int>());
		public static readonly IPrefs<TestClass>               JsonPrefs = new JsonPrefs<TestClass>("json_prefs", null);
		public static readonly IPrefs<Vector3>                 Vector3Prefs = new Vector3Prefs("vector3_prefs", Vector3.zero);
		public static readonly IPrefs<Color>                   ColorPrefs = new ColorPrefs("color_prefs", Color.white);
		public static readonly IPrefs<long>                    LongPrefs = new LongPrefs("long_prefs", 0);
		public static readonly IPrefs<Dictionary<int, string>> IntStringDictPrefs = new DictionaryPrefs<int, string>("dictionary_prefs", null);
	}
}
