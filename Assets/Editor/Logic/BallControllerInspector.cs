using UnityEngine;
using UnityEditor;

namespace Game.BallEditor
{
	[CustomEditor(typeof(Game.Logic.BallController))]
	public class BallControllerInspector : UnityEditor.Editor
	{
		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI ();
			GUILayout.Space (5f);
			GUILayout.BeginVertical ();
			if (GUILayout.Button("Fill Layers")) {
				FillLayers(target as Game.Logic.BallController);
			}
			GUILayout.Space (10f);

			AddLayers ();

			GUILayout.EndVertical ();
			
			if (GUI.changed)
				EditorUtility.SetDirty (target);
		} 

		void FillLayers (Game.Logic.BallController tar)
		{
			string layerName = "";
			GameObject targetLayer = null;
			UnityEngine.UI.Image[] images = new UnityEngine.UI.Image[(int)Game.Logic.BLR.LayerNum];
			for (int i = 0; i < (int)Game.Logic.BLR.LayerNum; i++) 
			{
				layerName = ((Game.Logic.BLR)i).ToString();
				targetLayer = FrameWork.Utils.GameTool.SearchChild(tar.transform, layerName);
				if (targetLayer != null) {
					images[i] = targetLayer.GetComponent<UnityEngine.UI.Image>();
				}
			}
			tar.SetLayerImgs (images);
		}

		void AddLayers ()
		{
			string layerName = "";
			for (int i = 0; i < (int)Game.Logic.BLR.LayerNum; i++) 
			{
				layerName = ((Game.Logic.BLR)i).ToString();
				if (GUILayout.Button("Add " + layerName +" Layer")) {
					AddSingleLayer(target as Game.Logic.BallController, layerName);
				}
			}
		}

		void AddSingleLayer (Game.Logic.BallController target, string layerName)
		{
			GameObject obj = FrameWork.Utils.GameTool.SearchChild(target.transform, layerName);
			if (obj != null) {
				Debug.LogError("The target Layer is already exists!  " + layerName);
				return;
			}
			obj = new GameObject (layerName);
			obj.transform.SetParent (target.transform);
			obj.transform.localPosition = Vector3.zero;
			obj.transform.localRotation = default(Quaternion);
			obj.transform.localScale = Vector3.one;
			obj.AddComponent<UnityEngine.UI.Image> ();

			ResortLayers ();
		}

		void ResortLayers ()
		{
			Game.Logic.BallController tar = target as Game.Logic.BallController;
			string layerName = "";
			GameObject targetLayer = null;

			UnityEngine.UI.Image[] images = new UnityEngine.UI.Image[(int)Game.Logic.BLR.LayerNum];

			int p = 0;

			for (int i = (int)Game.Logic.BLR.LayerNum - 1; i >= 0 ; i--) 
			{
				layerName = ((Game.Logic.BLR)i).ToString();
				targetLayer = FrameWork.Utils.GameTool.SearchChild(tar.transform, layerName);
				if (targetLayer != null) {
					targetLayer.transform.SetSiblingIndex(p++);
				}
			}

		}
	}
}
