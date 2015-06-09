using System;
using System.Collections.Generic;

namespace Game.Logic.Clip
{
	public class ClipFactory
	{
		private Dictionary<System.Type, ActClip> _container = null;

		public ClipFactory()
		{
			_container = new Dictionary<System.Type, ActClip> ();

			InternalRegister ();
		}

		public void Register(System.Type type, ActClip entity)
		{
			if (_container.ContainsKey(type)) {
				_container[type] = entity;
			}
		}

		public void UnRegister(System.Type type)
		{
			if (_container.ContainsKey(type)) {
				_container.Remove(type);
			}
		}

		public ActClip Generate(System.Type type)
		{
			if (_container.ContainsKey(type)) {
				return _container[type];
			}
			return default(ActClip);
		}

		private void InternalRegister ()
		{
//			Register (typeof(ActClip), new ActClip());
		}
	}
}

