using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaobaoExpert
{
	public class MultiValueDictionary<T,K>
	{
		Dictionary<T,List<K>> dic = new Dictionary<T,List<K>>();

		public void Add(T t, K k)
		{
			List<K> ks;
			if (!dic.TryGetValue(t, out ks) || ks == null)
			{
				ks = new List<K>();
				dic[t] = ks;
			}
			ks.Add(k);
		}
		public bool Remove(T t)
		{
			return dic.Remove(t);
		}
		public bool Remove(T t, K k)
		{
			List<K> ks;
			if (!dic.TryGetValue(t, out ks) || ks == null)
			{
				return false;
			}
			bool result = ks.Remove(k);
			if (ks.Count == 0)
			{
				dic.Remove(t);
			}
			return result;
		}

		public List<K> this[T index]
		{
			get { return this.dic[index]; }
		} 
	}
}
