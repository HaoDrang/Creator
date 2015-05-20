using System;
using System.Collections.Generic;
using UnityEngine;

public class Property
{
	const string defaultFileName = "Properties";

	static Dictionary<string, string> mPropDic = null;

	static public bool LoadDictionary (string value)
	{
		byte[] bytes = null;
		
		if (string.IsNullOrEmpty(value))
		{
			TextAsset asset = Resources.Load<TextAsset>(defaultFileName);
			if (asset != null) bytes = asset.bytes;
		}
		else
		{			
			TextAsset asset = Resources.Load<TextAsset>(value);
			if (asset != null) bytes = asset.bytes;
		}
		
		if (bytes != null)
		{
			ReadProperties(bytes);
			return true;
		}

		return false;
	}

	static void ReadProperties (byte[] bytes)
	{
		ByteReader reader = new ByteReader(bytes);
		mPropDic = reader.ReadDictionary();
	}

	//-----------------------------------------------------------
	public static string GetString(string key)
	{
		string ret;
		mPropDic.TryGetValue(key, out ret);
		return ret;
	}

	public static bool GetBool(string key)
	{
		string temp = default(string);
		mPropDic.TryGetValue(key, out temp);
		byte ret = 0;
		byte.TryParse(temp, out ret);
		return ret > 0;
	}

	public static byte GetByte(string key)
	{
		string temp = default(string);
		mPropDic.TryGetValue(key, out temp);
		byte ret = 0;
		byte.TryParse(temp, out ret);
		return ret;
	}

	public static sbyte GetSbyte(string key)
	{
		string temp;
		mPropDic.TryGetValue(key, out temp);
		sbyte ret = 0;
		sbyte.TryParse(temp, out ret);
		return ret;
	}

	public static short GetShort(string key)
	{
		string temp = default(string);
		mPropDic.TryGetValue(key, out temp);
		short ret = 0;
		short.TryParse(temp, out ret);
		return ret;
	}

	public static ushort GetUshort(string key)
	{
		string temp = default(string);
		mPropDic.TryGetValue(key, out temp);
		ushort ret = 0;
		ushort.TryParse(temp, out ret);
		return ret;
	}

	public static int GetInt(string key)
	{
		string temp = default(string);
		mPropDic.TryGetValue(key, out temp);
		int ret = 0;
		int.TryParse(temp, out ret);
		return ret;
	}

	public static uint GetUint(string key)
	{
		string temp = default(string);
		mPropDic.TryGetValue(key, out temp);
		uint ret = 0;
		uint.TryParse(temp, out ret);
		return ret;
	}

	public static long GetLong(string key)
	{
		string temp = default(string);
		mPropDic.TryGetValue(key, out temp);
		long ret = 0;
		long.TryParse(temp, out ret);
		return ret;
	}

	public static ulong GetUlong(string key)
	{
		string temp = default(string);
		mPropDic.TryGetValue(key,out temp);
		ulong ret = 0;
		if (!string.IsNullOrEmpty(temp)) {
			ulong.TryParse(temp, out ret);
		}
		return ret;
	}

	public static float GetFloat(string key)
	{
		string temp = default(string);
		mPropDic.TryGetValue(key,out temp);
		float ret = 0;
		if (!string.IsNullOrEmpty(temp)) {
			float.TryParse(temp, out ret);
		}
		return ret;
	}

	public static double GetDouble(string key)
	{
		string temp = default(string);
		mPropDic.TryGetValue(key,out temp);
		double ret = 0;
		if (!string.IsNullOrEmpty(temp)) {
			double.TryParse(temp, out ret);
		}
		return ret;
	}

	public static Color GetColor(string key)
	{
		string temp = default(string);
		mPropDic.TryGetValue(key,out temp);
		Color ret = default(Color);
		if (!string.IsNullOrEmpty(temp)) {
			string[] split = temp.Split(',');
			if (split.Length == 4) {
				float f;
				float.TryParse(split[0], out f);
				ret.r = f / 255f;
				f = 0;
				float.TryParse(split[1], out f);
				ret.g = f / 255f;
				f = 0;
				float.TryParse(split[2], out f);
				ret.b = f / 255f;
				f = 0;
				float.TryParse(split[3], out f);
				ret.a = f / 255f;
			}
		}
		return ret;
	}

	public static int[] GetIntArray(string key)
	{
		string temp = default(string);
		mPropDic.TryGetValue(key,out temp);
		int[] ret = null;
		if (!string.IsNullOrEmpty(temp)) {
			string[] split = temp.Split(',');
			ret = new int[split.Length];
			for (int i = 0; i < split.Length; i++) {
				int.TryParse(split[i], out ret[i]);
			}
		}
		return ret;
	}

	public static Rect GetRect(string key)
	{
		string temp = default(string);
		Rect ret = default(Rect);
		mPropDic.TryGetValue(key,out temp);
		if (!string.IsNullOrEmpty(temp)) {
		string[] split = temp.Split(',');
			if (split.Length == 4) {
				ret = new Rect();
				float f = 0f;
				float.TryParse(split[0], out f);
				ret.x = f;
				f = 0;
				float.TryParse(split[1], out f);
				ret.y = f;
				f = 0;
				float.TryParse(split[2], out f);
				ret.width = f;
				f = 0;
				float.TryParse(split[3], out f);
				ret.height = f;
			}
		}
		return ret;
	}

	public static Vector2 GetVector2(string key)
	{
		string temp = default(string);
		Vector2 ret = default(Vector2);
		mPropDic.TryGetValue(key,out temp);
		if (!string.IsNullOrEmpty(temp)) {
			string[] split = temp.Split(',');
			if (split.Length == 2) {
				ret = new Vector2();
				float f = 0f;
				float.TryParse(split[0], out f);
				ret.x = f;
				f = 0;
				float.TryParse(split[1], out f);
				ret.y = f;
			}
		}

		return ret;
	}

	public static Vector3 GetVector3(string key)
	{
		string temp = default(string);
		Vector3 ret = default(Vector3);
		mPropDic.TryGetValue(key,out temp);
		if (!string.IsNullOrEmpty(temp)) {
			string[] split = temp.Split(',');
			if (split.Length == 3) {
				ret = new Vector3();
				float f = 0f;
				float.TryParse(split[0], out f);
				ret.x = f;
				f = 0;
				float.TryParse(split[1], out f);
				ret.y = f;
				f = 0;
				float.TryParse(split[2], out f);
				ret.z = f;
			}
		}
		return ret;
	}
}

