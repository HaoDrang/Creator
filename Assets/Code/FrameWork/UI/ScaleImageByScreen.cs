/*Scale the image to the screen
 * 4-16-2015 by hao
 */
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class ScaleImageByScreen : MonoBehaviour 
{
	[SerializeField]
	FitScreenType fitType = FitScreenType.Horizontal;

	void Awake () {
		FitScreen ();
	}

	void Update()
	{
		FitScreen ();
	}

	void FitScreen(){
		RectTransform rcTrans = GetComponent<RectTransform> ();
		Rect rect = rcTrans.rect;
		float originImgWidth 	= rect.width;
		float originImgHeight	= rect.height;
		float imgWidth 	= 0f;
		float imgHeight = 0f;
		float screenWidth = Screen.width;
		float screenHeight = Screen.height;

		switch (fitType) {
		case FitScreenType.Horizontal:
			imgWidth = screenWidth;
			imgHeight = imgWidth * originImgHeight / originImgWidth;
			break;
		case FitScreenType.Vertical:
			imgHeight = screenHeight;
			imgWidth = imgHeight * originImgWidth / originImgHeight;
			break;
		default:
			break;
		}
		rcTrans.sizeDelta = new Vector2 (imgWidth,imgHeight);
	}
}

public enum FitScreenType
{
	Vertical,
	Horizontal,
}