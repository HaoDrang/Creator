
using System.Collections.Generic;

public class ArrayTool
{
	public static void Shuffle<T>(T[] list)
	{
		T temp = default(T);
		int swapIdx = 0;
		for (int i = 0; i < list.Length; i++) {
			swapIdx = UnityEngine.Random.Range (0, list.Length);
			temp = list [i];
			
			list [i] = list [swapIdx];
			list [swapIdx] = temp;
		}
	}

	public static void Shuffle<T>(List<T> list)
	{
		T temp = default(T);
		int swapIdx = 0;
		for (int i = 0; i < list.Count; i++) {
			swapIdx = UnityEngine.Random.Range (0, list.Count);
			temp = list [i];
			
			list [i] = list [swapIdx];
			list [swapIdx] = temp;
		}
	}
}

