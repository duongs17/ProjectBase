namespace MultipleTypesPlayerPrefs
{
	public interface IPrefs<T>
	{
		T GetValue();
		IPrefs<T> SetValue(T value);
		void Save();
		/// <summary>
		/// reset to defaultValue
		/// </summary>
		void Reset();
		void DeleteKey();
	}

	public abstract class PrefsBase<T> : IPrefs<T>
	{
		protected readonly string _internalKey;
		protected readonly T _defaultValue;

		protected PrefsBase(string key, T defaultValue)
		{
			_internalKey = string.Format("{0}_{1}", Config.PEFIX_FMT, key);
			_defaultValue = defaultValue;
		}

		public T GetValue()
		{
			if (HasKey())
			{
				return GetValueWhenHasKey();
			}

			return _defaultValue;
		}

		protected abstract T GetValueWhenHasKey();

		public abstract IPrefs<T> SetValue(T value);

		public void Reset()
		{
			SetValue(_defaultValue);
		}

		public void Save()
		{
#if !USE_EDITOR_PREFS
			UnityEngine.PlayerPrefs.Save();
#endif
		}

		private bool HasKey()
		{
#if USE_EDITOR_PREFS && UNITY_EDITOR
			return UnityEditor.EditorPrefs.HasKey(_internalKey);
#else
			return UnityEngine.PlayerPrefs.HasKey(_internalKey);
#endif
		}

		public void DeleteKey()
		{
#if USE_EDITOR_PREFS && UNITY_EDITOR
			UnityEditor.EditorPrefs.DeleteKey(_internalKey);
#else
			UnityEngine.PlayerPrefs.DeleteKey(_internalKey);
#endif
		}

		public static void DeleteAll()
		{
#if USE_EDITOR_PREFS && UNITY_EDITOR
			UnityEditor.EditorPrefs.DeleteAll();
#else
			UnityEngine.PlayerPrefs.DeleteAll();
#endif
		}

		protected int GetInt()
		{
#if USE_EDITOR_PREFS && UNITY_EDITOR
			return UnityEditor.EditorPrefs.GetInt(_internalKey);
#else
			return UnityEngine.PlayerPrefs.GetInt(_internalKey);
#endif
		}

		protected float GetFloat()
		{
#if USE_EDITOR_PREFS && UNITY_EDITOR
			return UnityEditor.EditorPrefs.GetFloat(_internalKey);
#else
			return UnityEngine.PlayerPrefs.GetFloat(_internalKey);
#endif
		}

		protected string GetString()
		{
#if USE_EDITOR_PREFS && UNITY_EDITOR
			return UnityEditor.EditorPrefs.GetString(_internalKey);
#else
			return UnityEngine.PlayerPrefs.GetString(_internalKey);
#endif
		}

		protected void SetInt(int value)
		{
#if USE_EDITOR_PREFS && UNITY_EDITOR
			UnityEditor.EditorPrefs.SetInt(_internalKey, value);
#else
			UnityEngine.PlayerPrefs.SetInt(_internalKey, value);
#endif
		}

		protected void SetFloat(float value)
		{
#if USE_EDITOR_PREFS && UNITY_EDITOR
			UnityEditor.EditorPrefs.SetFloat(_internalKey, value);
#else
			UnityEngine.PlayerPrefs.SetFloat(_internalKey, value);
#endif
		}

		protected void SetString(string value)
		{
#if USE_EDITOR_PREFS && UNITY_EDITOR
			UnityEditor.EditorPrefs.SetString(_internalKey, value);
#else
			UnityEngine.PlayerPrefs.SetString(_internalKey, value);
#endif
		}
	}
}
