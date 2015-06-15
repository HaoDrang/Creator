
using System;
using System.Collections.Generic;

namespace FrameWork
{
	public class TFactory<T>
	{
		private IDictionary<System.Type, Func<T>> mContainer = null;

		public TFactory ()
		{
			mContainer = new Dictionary<System.Type, Func<T>> ();
		}

		public void Register<TReg> (Func<T> creator = null) where TReg : T
		{
			if (mContainer.ContainsKey (typeof(TReg))) {
				mContainer [typeof(TReg)] = creator;
			} else {
				mContainer.Add (typeof(TReg), creator);
			}
		}

		public void UnRegister (System.Type type)
		{
			mContainer.Remove (type);
		}

		public TGet Generate<TGet> () where TGet : T
		{
			Func<T> creator = null;
			mContainer.TryGetValue (typeof(TGet), out creator);
			if (creator == null) {
				return (TGet)creator ();
			} else {
				return (TGet)System.Activator.CreateInstance (typeof(TGet));
			}
		}
	}
}

