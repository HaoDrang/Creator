namespace Game.Logic
{
	public class RowFactory
	{
		const string RowPrefabName = "RowPrefab";

		private LevelConfig mConfig = null;

		public RowFactory(LevelConfig conf)
		{
			mConfig = conf;
		}

		public virtual Row Generate()//+++
		{
			UnityEngine.GameObject rowObj = PrefabMgr.Ins.CreateCopy (RowPrefabName);
			return rowObj.GetComponent<Row>();
		}
	}
}

