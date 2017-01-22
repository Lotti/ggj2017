using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MySerializer
{
	public static Dictionary<string,object> ToDictionary(this Vector3 myVector)
	{
		Dictionary<string, object> appDict = new Dictionary<string, object>();

		appDict.Add("x", myVector.x);
		appDict.Add("y", myVector.y);
		appDict.Add("z", myVector.z);

		return appDict;
	}

	public static Dictionary<string, object> ToDictionary(this Quaternion myQuaternion)
	{
		Dictionary<string, object> appDict = new Dictionary<string, object>();

		appDict.Add("x", myQuaternion.x);
		appDict.Add("y", myQuaternion.y);
		appDict.Add("z", myQuaternion.z);
		appDict.Add("w", myQuaternion.w);

		return appDict;
	}

	public static List<object> ToList(this Vector3 myVector)
	{
		List<object> appDict = new List<object>();

		appDict.Add(myVector.x);
		appDict.Add(myVector.y);
		appDict.Add(myVector.z);

		return appDict;
	}

	public static List<object> ToList(this Quaternion myQuaternion)
	{
		List<object> appDict = new List<object>();

		appDict.Add(myQuaternion.x);
		appDict.Add(myQuaternion.y);
		appDict.Add(myQuaternion.z);
		appDict.Add(myQuaternion.w);

		return appDict;
	}


	public static Vector3 FromDictionary(this Vector3 myVector, Dictionary<string, object> appDict)
	{
		myVector.x = System.Convert.ToSingle(appDict["x"]);
		myVector.y = System.Convert.ToSingle(appDict["y"]);
		myVector.z = System.Convert.ToSingle(appDict["z"]);

		return myVector;
	}

	public static Quaternion FromDictionary(this Quaternion myQuaternion, Dictionary<string, object> appDict)
	{
		myQuaternion.x = System.Convert.ToSingle(appDict["x"]);
		myQuaternion.y = System.Convert.ToSingle(appDict["y"]);
		myQuaternion.z = System.Convert.ToSingle(appDict["z"]);
		myQuaternion.w = System.Convert.ToSingle(appDict["w"]);

		return myQuaternion;
	}

	public static Vector3 FromList(this Vector3 myVector, List<object> appDict)
	{
		myVector.x = System.Convert.ToSingle(appDict[0]);
		myVector.y = System.Convert.ToSingle(appDict[1]);
		myVector.z = System.Convert.ToSingle(appDict[2]);

		return myVector;
	}

	public static Quaternion FromList(this Quaternion myQuaternion, List<object> appDict)
	{
		myQuaternion.x = System.Convert.ToSingle(appDict[0]);
		myQuaternion.y = System.Convert.ToSingle(appDict[1]);
		myQuaternion.z = System.Convert.ToSingle(appDict[2]);
		myQuaternion.w = System.Convert.ToSingle(appDict[3]);

		return myQuaternion;
	}
}