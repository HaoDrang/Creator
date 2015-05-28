using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorSetter : MonoBehaviour 
{
	protected Image target = null;

	void Awake()
	{
		target = GetComponent<Image> ();
	}

	virtual public void SetColor(LevelConfig conf)
	{
		if (conf == null) {
			return;
		}
		if (target != null) {
			target.color = conf.mColors [Random.Range (0, conf.mColors.Length)];
		} else {
			Debug.LogWarning("This Game Object Do not have an image component :" + gameObject.name);
		}
	}
}
