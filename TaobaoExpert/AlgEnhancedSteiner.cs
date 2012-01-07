using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TaobaoSpider.Model;

namespace TaobaoExpert
{
	class AlgEnhancedSteiner:Alg
	{
		Random r = new Random(1);
		public static ulong Distance_D = 0;

		public AlgEnhancedSteiner(IEnumerable<Item> items) : base(items)
		{
			this.AlgorithmName = "EnhancedSteiner";
			ulong scount = (ulong)sellerCities.Count;
			Distance_D = scount*(scount - 1)/2;
		}

		public override Result DoAlg(List<long> requestItems)
		{
			//enhance graph to mapH and mapH2
			// using hashtable impl, do not actually build a graph
			//EnhanceGragh(requestItems);

			int minCost = int.MaxValue;
			Result minr = null;
//			for (int i = 0; i < requestItems.Count; i++)
			{
				//steinertree
				long a = 0;
				//HashSet<SellerCity> x_ = EnhancedSteinerTree(new HashSet<long>(requestItems),requestItems[i],out a);
				long start = Environment.TickCount;
				HashSet<SellerCity> x_ = EnhancedSteinerTree(new HashSet<long>(requestItems), requestItems[0], out a);
				long end = Environment.TickCount;

				//calculate cost
				int cost = CalcCost(x_);
				Result r = new Result
				           	{
				           		Cost = cost,
				           		Sellers = x_,
				           		AlgorithmName = this.AlgorithmName,
								Treecost = a,
								Time = end-start
				           	};
//				Console.WriteLine("Cost {1} : {0}",r ,i);
				if (cost < minCost)
				{
					minr = r;
					minCost = cost;
				}
			}
			
			return minr;
		}

		private HashSet<SellerCity> EnhancedSteinerTree(HashSet<long> x0,long root,out long treecost)
		{
			HashSet<Tuple<SellerCity,SellerCity> > connection = new HashSet<Tuple<SellerCity, SellerCity>>();
			treecost = 0;
			HashSet<SellerCity> xSteiner = new HashSet<SellerCity>(sellerCities);

			HashSet<long> xSteinerExcept = new HashSet<long>(x0);
			//divide two kinds of nodes
			HashSet<SellerCity> x_City = new HashSet<SellerCity>();
			HashSet<long> x_Item = new HashSet<long>();

			x_Item.Add(root);

			xSteinerExcept.ExceptWith(x_Item);
			while (xSteinerExcept.Count != 0)
			{
//				Console.WriteLine(xSteinerExcept.Count);
				//you must use long to support n(nodes)>32767
				ulong minDistance = UInt64.MaxValue;
				long v = -1;
				List<SellerCity> bridges = null;
				foreach (long item in xSteinerExcept)
				{
					List<SellerCity> bs;
					ulong dis = GetPathMinDistance(item, x_City, x_Item, out bs);
					if (dis < minDistance)
					{
						minDistance = dis;
						v = item;
						bridges = bs;
						if (dis == 0)
						{
							break;
						}
					}
				}
				//bridges must not be null
				Debug.Assert(bridges != null);
				x_City.UnionWith(bridges);
				x_Item.Add(v);

//				xSteinerExcept.Clear();
//				xSteinerExcept.UnionWith(x0);
				xSteinerExcept.ExceptWith(x_Item);

				//connection cost
				var t1 = new Tuple<SellerCity, SellerCity>(bridges[0], bridges[1]);
				var t2 = new Tuple<SellerCity, SellerCity>(bridges[1], bridges[0]);
				if (!connection.Contains(t1) && !connection.Contains(t2))
				{
					connection.Add(t1);
					connection.Add(t2);
					treecost += GetDistance(bridges[0], bridges[1]);
				}
				
			}
			return x_City;
		}

		private ulong GetPathMinDistance(long item, HashSet<SellerCity> xCities, 
			HashSet<long> xItems, out List<SellerCity> bridges)
		{
			List<SellerCity> supports = maps2[item];
			ulong minDis = UInt64.MaxValue;

			SellerCity bridge1 = null, bridge2 = null;
			foreach (var support in supports)
			{
				//check cities
				foreach (var city in xCities)
				{
					ulong dis = (ulong)GetDistance(support, city);
					if (dis < minDis)
					{
						minDis = dis;
						bridge1 = support;
						bridge2 = city;
					}
				}
				if (minDis > Distance_D)
				{
					//check item
					foreach (var i in xItems)
					{
						//HashSet<SellerCity> iSupport = new HashSet<SellerCity>(maps2[i]);
						var iSupports = maps2[i];
						foreach (var iSupport in iSupports)
						{
							ulong dis = (ulong)GetDistance(support, iSupport);
							dis += Distance_D;
							if (dis < minDis)
							{
								minDis = dis;
								bridge1 = support;
								bridge2 = iSupport;
								if (dis == 0)
								{
									break;
								}
							}
						}
					}
				}
				
			}
			Debug.Assert(bridge1 != null);
			Debug.Assert(bridge2 != null);
			bridges = new List<SellerCity>(2);
			bridges.Add(bridge1);
			bridges.Add(bridge2);
			return minDis;
		}

		private long PickRandom(HashSet<long> x0)
		{
			int count = x0.Count;
			return x0.ElementAt(r.Next(count));
		}
	}
}
