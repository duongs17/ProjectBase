using System;
using UnityEngine;

public enum TestType
{
	Type1 = 0,
	Type2 = 1,
	Type3 = 2
}

[Serializable]
public class TestClass
{
	[SerializeField]
	public string name;
	[SerializeField]
	public int age;
}