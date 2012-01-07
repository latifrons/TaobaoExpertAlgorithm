using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TaobaoSpider.Model;

namespace TaobaoExpert
{
	class AlgRarestFirst : Alg
	{
		public AlgRarestFirst(List<Item> items) : base(items)
		{
			this.algorithmName = "RarestFirst";
		}

		public override Result DoAlg(List<long> requestItems)
		{
			List<List<SellerCity>> itemsSellers = new List<List<SellerCity>>();
			//3 find the minimum List<SellerCity> count
			int minSize = int.MaxValue;
			long arare = 0;
			foreach (long item in requestItems)
			{
				List<SellerCity> sellers = maps2[item];
				itemsSellers.Add(sellers);
				if (sellers.Count < minSize)
				{
					minSize = sellers.Count;
					arare = item;
				}
			}
			//4

			SellerCity iStar = null;
			int argMinRi = int.MaxValue;
			foreach (SellerCity i in maps2[arare])
			{
				//5
				int Ri = int.MinValue;
				foreach (long a in requestItems)
				{
					if (a != arare)
					{
						//6
						int Ria = GetMinDistance(i, maps2[a]);
						Ri = Math.Max(Ri, Ria);
					}
				}
				if (Ri < argMinRi)
				{
					argMinRi = Ri;
					iStar = i;
				}
			}
			if (iStar == null)
			{
				return null;
			}

			HashSet<SellerCity> ss = new HashSet<SellerCity>();
			ss.Add(iStar);
			foreach (long a in requestItems)
			{
				ss.Add(GetPath(iStar, maps2[a]));
			}
			//calculate cost
			int cost = CalcCost(ss);
			return new Result
			       	{
			       		Cost = cost,
						Sellers = ss,
						AlgorithmName = this.algorithmName
			       	};
		}
	}
}
