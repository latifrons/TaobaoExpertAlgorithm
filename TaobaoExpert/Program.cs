using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TaobaoSpider.BLL;
using TaobaoSpider.Model;
using Dapper;

namespace TaobaoExpert
{
	class Program
	{
		//private static string sql = "select * from item order by taobaoid limit 1000";
		private static string sql = "select * from item where uniqid !=0 order by taobaoid";
		static void Main(string[] args)
		{
//			SellerCity sc1 = new SellerCity(10,"a");
//			SellerCity sc2 = new SellerCity(10, "a");
//			Debug.Assert(sc1.Equals(sc2));
//			MultiValueDictionary<SellerCity,int> cd =new MultiValueDictionary<SellerCity, int>();
//			cd.Add(sc1,44);
//			List<int> b = cd[sc2];
//			Debug.Assert(b[0] == 44);
//			return;

			DbConnection conn = Database.GetConnection();
			var values = conn.Query<Item>(sql).ToList();

			//random
			HashSet<long> requestItems = new HashSet<long>();
			Random r = new Random();
			int count = values.Count;
			for (int i = 0 ; i < 100;i++)
			{
				Item it = values.ElementAt(r.Next(count));
				long d = it.UniqId == 0 ? it.TaobaoId : it.UniqId;
				requestItems.Add(d);
			}
//			requestItems.Add(-266824210L);
//			requestItems.Add(-167456081L);
//			requestItems.Add(-1860892874L);

			Console.WriteLine(values.Count);
			Console.WriteLine(requestItems.Count);

			{
				Alg alg = new AlgRarestFirst(values);
				Result s = alg.DoAlg(requestItems.ToList());
				Console.WriteLine(s);
			}
			{
				Alg alg = new AlgCoverSteiner(values);
				Result s = alg.DoAlg(requestItems.ToList());
				Console.WriteLine(s);
			}
			Console.ReadKey();
		}

	}
}
