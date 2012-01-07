using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TaobaoSpider.Model;

namespace TaobaoExpert
{
	class AlgCoverSteiner:Alg
	{
		Random r = new Random(1);
		public AlgCoverSteiner(List<Item> items) : base(items)
		{
			this.AlgorithmName = "CoverSteiner";
		}

		public override Result DoAlg(List<long> requestItems)
		{
			long start = Environment.TickCount;
			//greedycover
			HashSet<SellerCity> x0 = GreedyCover(new HashSet<long>(requestItems));
			//steinertree
			long a;
			HashSet<SellerCity> x_ = SteinerTree(x0,out a);
			long end = Environment.TickCount;
			//calculate cost
			int cost = CalcCost(x_);
			return new Result
			{
				Cost = cost,
				Sellers = x_,
				AlgorithmName = this.AlgorithmName,
				Treecost = a,
				Time = end-start
			};

		}

		private HashSet<SellerCity> SteinerTree(HashSet<SellerCity> x0, out long treecost)
		{
			HashSet<Tuple<SellerCity, SellerCity>> connection = new HashSet<Tuple<SellerCity, SellerCity>>();
			treecost = 0;
			HashSet<SellerCity> xSteiner = new HashSet<SellerCity>(sellerCities);
			xSteiner.ExceptWith(x0);

			HashSet<SellerCity> xSteinerExcept = new HashSet<SellerCity>(x0);
			HashSet<SellerCity> x_ = new HashSet<SellerCity>();
			x_.Add(PickRandom(x0));
			xSteinerExcept.ExceptWith(x_);
			while (xSteinerExcept.Count != 0)
			{
//				Console.WriteLine(xSteinerExcept.Count);
				int minDistance = int.MaxValue;
				SellerCity v = null;
				foreach (SellerCity sc in xSteinerExcept)
				{
					int dis = GetMinDistance(sc, x_);
					if (dis < minDistance)
					{
						minDistance = dis;
						v = sc;
					}
				}

				SellerCity p = GetPath(v, x_);
				if (p != null)
				{
					x_.Add(p);
					x_.Add(v);
					//connection cost
					var t1 = new Tuple<SellerCity, SellerCity>(p,v);
					var t2 = new Tuple<SellerCity, SellerCity>(v, p);
					if (!connection.Contains(t1) && !connection.Contains(t2))
					{
						connection.Add(t1);
						connection.Add(t2);
						treecost += GetDistance(v,p);
					}
				}
				else
				{
					Debug.Assert(false);
					return null;
				}

				xSteinerExcept.Clear();
				xSteinerExcept.UnionWith(x0);
				xSteinerExcept.ExceptWith(x_);
			}
			return x_;
		}

		private SellerCity PickRandom(HashSet<SellerCity> x0)
		{
			int count = x0.Count;
			return x0.ElementAt(r.Next(count));
		}

		private HashSet<SellerCity> GreedyCover(HashSet<long> requestItems)
		{
			//greedy set cover alg
			HashSet<SellerCity> sellerSet = new HashSet<SellerCity>();
			//1
			HashSet<long> covered = new HashSet<long>();
			//2
			while (!covered.SetEquals(requestItems))
			{
				HashSet<long> remains = new HashSet<long>(requestItems);
				remains.ExceptWith(covered);
				//check cost-effective
				int minRemain = int.MaxValue;
				SellerCity minSellerCity = null;
				foreach (SellerCity sc in sellerCities)
				{
					HashSet<long> sellerRemainItems = new HashSet<long>(remains);
					sellerRemainItems.ExceptWith(maps[sc]);
					if (sellerRemainItems.Count < minRemain)
					{
						minRemain = sellerRemainItems.Count;
						minSellerCity = sc;
					}
				}
				Debug.Assert(minSellerCity != null);
				sellerSet.Add(minSellerCity);
				HashSet<long> sellerProvide = new HashSet<long>(maps[minSellerCity]);
				sellerProvide.IntersectWith(requestItems);
				covered.UnionWith(sellerProvide);
			}
			return sellerSet;
		}
	}
}
