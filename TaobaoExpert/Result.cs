using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaobaoExpert
{
	public class Result
	{
		private HashSet<SellerCity> sellers;
		private string algorithmName;

		public override string ToString()
		{
			return string.Format("AlgorithmName: {0},  Sellers: {1},  Cost: {2}",algorithmName, sellers.Count , cost);
		}

		private int cost;

		public HashSet<SellerCity> Sellers
		{
			get { return sellers; }
			set { sellers = value; }
		}

		public int Cost
		{
			get { return cost; }
			set { cost = value; }
		}

		public string AlgorithmName
		{
			get { return algorithmName; }
			set { algorithmName = value; }
		}
	}

}
