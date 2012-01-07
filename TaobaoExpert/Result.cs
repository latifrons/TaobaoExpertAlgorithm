using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaobaoExpert
{
	public class Result
	{
		// Fields (5) 

		private string algorithmName;
		private int cost;
		private HashSet<SellerCity> sellers;
		private long treecost;
		private HashSet<int> uniqSellers = new HashSet<int>();
		private long time;

		// Properties (4) 

		public string AlgorithmName
		{
			get { return algorithmName; }
			set { algorithmName = value; }
		}

		public int Cost
		{
			get { return cost; }
			set { cost = value; }
		}

		public HashSet<SellerCity> Sellers
		{
			get { return sellers; }
			set
			{
				sellers = value;
				GetUniqSeller();
			}
		}

		public long Treecost
		{
			get { return treecost; }
			set { treecost = value; }
		}

		public HashSet<int> UniqSellers
		{
			get { return uniqSellers; }
			set { uniqSellers = value; }
		}

		public long Time
		{
			get { return time; }
			set { time = value; }
		}

		// Methods (2) 

		// Public Methods (1) 

		public override string ToString()
		{
			return string.Format("AlgorithmName: {0},  Sellers: {1},  Unique Sellers: {2},  Cost: {3},  TreeCost: {4}", algorithmName, sellers.Count,UniqSellers.Count, cost,treecost);
		}
		// Private Methods (1) 

		private void GetUniqSeller()
		{
			UniqSellers.Clear();
			foreach (SellerCity sc in sellers)
			{
				UniqSellers.Add(sc.SellerID);
			}
		}
	}

}
