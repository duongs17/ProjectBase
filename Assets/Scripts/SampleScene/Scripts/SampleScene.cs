using System;
using System.Collections.Generic;
using System.Linq;
using MultipleTypesPlayerPrefs;
using UnityEngine;

public class SampleScene : MonoBehaviour
{
	private void Awake()
	{
		PlayerPrefsDefines.BoolPrefs.SetValue(true).Save();
		PlayerPrefsDefines.DateTimePrefs.SetValue(DateTime.Now);
		PlayerPrefsDefines.IntStringDictPrefs.SetValue(new Dictionary<int, string> {{1, "one"}, {2, "two"}}).Save();
		PlayerPrefsDefines.EnumPrefs.SetValue(TestType.Type2).Save();
		PlayerPrefsDefines.FloatListPrefs.SetValue(new List<float> {1.2f, 2.0f, 3.4f}).Save();
		PlayerPrefsDefines.IntListPrefs.SetValue(new List<int> {1, 2, 3, 4}).Save();
		PlayerPrefsDefines.JsonPrefs.SetValue(new TestClass {name = "kan", age = 100}).Save();
		PlayerPrefsDefines.StringListPrefs.SetValue(new List<string> {"a", "b", "c"}).Save();
		PlayerPrefsDefines.Vector3Prefs.SetValue(new Vector3(10, 20, 44)).Save();
		PlayerPrefsDefines.GenericList.SetValue(new List<int> {4, 5, 6}).Save();
		PlayerPrefsDefines.ColorPrefs.SetValue(Color.gray).Save();
		PlayerPrefsDefines.LongPrefs.SetValue(long.MaxValue).Save();
	}
	private void Start()
	{
		Debug.Log($"BOOL: {PlayerPrefsDefines.BoolPrefs.GetValue()}");
		Debug.Log($"DATE TIME: {PlayerPrefsDefines.DateTimePrefs.GetValue()}");
		Debug.Log($"DICTIONARY: {PlayerPrefsDefines.IntStringDictPrefs.GetValue().DicToString()}");
		Debug.Log($"ENUM: {PlayerPrefsDefines.EnumPrefs.GetValue().ToString()}");
		Debug.Log($"FLOAT LIST: {PlayerPrefsDefines.FloatListPrefs.GetValue().ListToString()}");
		Debug.Log($"INT LIST: {PlayerPrefsDefines.IntListPrefs.GetValue().ListToString()}");
		Debug.Log($"JSON: {JsonUtility.ToJson(PlayerPrefsDefines.JsonPrefs.GetValue())}");
		Debug.Log($"STRING LIST: {PlayerPrefsDefines.StringListPrefs.GetValue().ListToString()}");
		Debug.Log($"VECTOR3: {PlayerPrefsDefines.Vector3Prefs.GetValue()}");
		Debug.Log($"GENERIC LIST: {PlayerPrefsDefines.GenericList.GetValue().ListToString()}");
		Debug.Log($"COLOR: {PlayerPrefsDefines.ColorPrefs.GetValue().ToString()}");
		Debug.Log($"LONG: {PlayerPrefsDefines.LongPrefs.GetValue()}");
	}
}

public static class StringConverter
{
	public static string DicToString(this Dictionary<int, string> dictionary)
	{
		var lines = dictionary.Select(kvp => kvp.Key + ": " + kvp.Value.ToString());
		return string.Join(", ", lines);
	}

	public static string ListToString<T>(this List<T> list)
	{
		return string.Join(", ", list);
	}
}