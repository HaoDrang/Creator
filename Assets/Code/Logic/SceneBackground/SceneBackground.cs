using UnityEngine;
using System.Collections;

public class SceneBackground : MonoBehaviour {
	[SerializeField]
	private BackgroundLayerBase[] backgrounds;
	public void BuildScene(LevelConfig conf)
	{
		for (int i = 0; i < (int)BackgroundLayer.LayerCount; i++) 
		{
			if (backgrounds[i] != null) {
				backgrounds[i].BuildLayer(conf);
			}

		}
	}
}

public enum BackgroundLayer
{
	Sky,
	Ground,
	Far,
	Near,
	Main,
	LayerCount,
}