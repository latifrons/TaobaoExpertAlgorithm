using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using TaobaoSpider.Model;

namespace TaobaoExpert
{
	abstract class Alg
	{
		protected string algorithmName;
		protected HashSet<Item> items;
		protected HashSet<SellerCity> sellerCities;
		//sellertaobaoid, location -> uniqid/taobaoid
		protected MultiValueDictionary<SellerCity, long> maps = new MultiValueDictionary<SellerCity, long>();
		//uniqid/taobaoid -> sellertaobaoid, location
		protected MultiValueDictionary<long, SellerCity> maps2 = new MultiValueDictionary<long, SellerCity>();
		
		public Alg(IEnumerable<Item> items)
		{
			this.items = new HashSet<Item>(items);
			this.sellerCities = new HashSet<SellerCity>();
			foreach (Item i in items)
			{
				SellerCity t = new SellerCity(i.SellerTaobaoId, i.Location);
				
				long id = i.UniqId == 0 ? i.TaobaoId : i.UniqId;
				sellerCities.Add(t);
				maps.Add(t, id);
				maps2.Add(id, t);
			}
		}
		public abstract Result DoAlg(List<long> requestItems);

		public int CalcCost(HashSet<SellerCity> ss)
		{
			int dis = 0;
			StringBuilder sb = new StringBuilder();
			foreach (SellerCity s1 in ss)
			{
				foreach (SellerCity s2 in ss)
				{
					if (s1 != s2)
					{
						sb.Append(s1).Append(' ');
						sb.Append(s2).Append(' ');
						sb.Append(GetDistance(s1, s2)).Append('\n');
						dis += GetDistance(s1, s2);
					}
				}
			}
			File.AppendAllText("a.txt", sb.ToString());
			return dis / 2;
		}

		public int GetDistance(SellerCity s1, SellerCity s2)
		{
			if (s1.SellerID == s2.SellerID)
			{
				return Constants.SAME_SELLER_DIFF_CITY;
			}
			else
			{
				return Constants.DIFF_SELLER;
			}
		}

		public int GetMinDistance(SellerCity seller, IEnumerable<SellerCity> sellers)
		{
			int minD = int.MaxValue;
			if (sellers == null)
			{
				return minD;
			}
			foreach (var s in sellers)
			{
				minD = Math.Min(minD, GetDistance(seller, s));
			}
			return minD;
		}

		public SellerCity GetPath(SellerCity src, IEnumerable<SellerCity> tars)
		{
			int minD = int.MaxValue;
			SellerCity tar = null;

			foreach (var s in tars)
			{
				if (src.SellerID == s.SellerID)
				{
					minD = Constants.SAME_SELLER_DIFF_CITY;
					tar = s;
				}
				else
				{
					if (minD > Constants.DIFF_SELLER)
					{
						minD = Constants.DIFF_SELLER;
						tar = s;
					}
				}
			}
			return tar;
		}
	}
	public class SellerCity
	{
		private readonly Tuple<int, string> t;

		public int SellerID
		{
			get { return t.Item1; }
		}
		public string City
		{
			get { return t.Item2; }
		}

		public SellerCity(int i, string s)
		{
			t = new Tuple<int, string>(i,s);
		}
		public override bool Equals(object obj)
		{
			if (!(obj is SellerCity))
			{
				return false;
			}
			return t.Equals(((SellerCity)obj).t);
		}

		public override int GetHashCode()
		{
			return (t != null ? t.GetHashCode() : 0);
		}

		public override string ToString()
		{
			return t.ToString();
		}
	}
}
