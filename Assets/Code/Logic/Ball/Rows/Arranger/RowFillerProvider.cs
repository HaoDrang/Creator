using System;
using System.Collections.Generic;
namespace Game.Logic
{
	public class RowFillerProvider
	{
		Dictionary<System.Type, IRowBallFiller> _container = null;
		public RowFillerProvider ()
		{
			_container = new Dictionary<Type, IRowBallFiller> ();
			
			Init ();
		}
		
		void Init ()
		{
			Register (typeof(RandomRowFiller), new RandomRowFiller());
		}
		
		void Register (Type type, IRowBallFiller filler)
		{
			if (_container.ContainsKey (type)) {
				_container [type] = filler;
			} else {
				_container.Add(type, filler);
			}
		}
		
		public T GetFiller<T> () where T : IRowBallFiller
		{
			if (_container.ContainsKey(typeof(T))) {
				return (T)_container[typeof(T)];
			}
			return default(T);
		}
		
		public object GetFiller(System.Type t)
		{
			if (_container.ContainsKey(t)) {
				return _container[t];
			}
			return default(object);
		}
	}
}

