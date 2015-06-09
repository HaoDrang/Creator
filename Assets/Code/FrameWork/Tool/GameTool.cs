using UnityEngine;

namespace FrameWork.Utils
{
	public class GameTool
	{
		public static GameObject SearchChild(Transform parent, string targetName)
		{
			int childNum = parent.childCount;
			for (int i = 0; i < childNum; i++) 
			{
				Transform child = parent.GetChild(i);
				if (child != null) 
				{
					if (child.name == targetName) 
					{
						return child.gameObject;
					}
					else
					{
						GameObject subChild = SearchChild(child, targetName);
						if (subChild != null) {
							return subChild;
						}
					}
				}
			}
			return null;
		}
	}
}

