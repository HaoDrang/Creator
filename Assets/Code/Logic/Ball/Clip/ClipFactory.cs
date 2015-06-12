using System;
using System.Collections.Generic;
using System.Reflection;
using FrameWork;
namespace Game.Logic.Clip
{
	public class ClipFactory
	{
		private Dictionary<System.Type, ActClip> _container = null;

		public ClipFactory ()
		{
			_container = new Dictionary<System.Type, ActClip> ();
		}

		public void Register (System.Type type, ActClip entity)
		{
			if (_container.ContainsKey (type)) {
				_container [type] = entity;
			}
		}

		public void Register<T>(Func<T> constructor)
		{

		}

		public void UnRegister (System.Type type)
		{
			if (_container.ContainsKey (type)) {
				_container.Remove (type);
			}
		}

		public ActClip Generate (System.Type type)
		{
			if (_container.ContainsKey (type)) {
				return _container [type];
			}
			return default(ActClip);
		}

		public T Generate<T> () where T: ActClip
		{
			System.Type tp = typeof(T);
			if (_container.ContainsKey (tp)) {

			}
			return default(T);
		}
	}
}

