using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using TaobaoSpider.BLL;
using TaobaoSpider.Model;
using Dapper;

namespace TaobaoExpert
{
	class Program
	{
		private static string sql = "select * from item";
//		private static string sql = "select * from item where uniqid !=0 limit 10000";
//		private static string sql = "select * from item where sellertaobaoid in (289785176,306293230)";

		static private int minPickupCount = 12;
		static private int stepPickupCount = 2;
		static private int maxPickupCount = 20;
		static private int maxRounds = 50;
		
		static StringBuilder sb = new StringBuilder();
		static Random r = new Random(1);
		static void Main(string[] args)
		{
			DbConnection conn = Database.GetConnection();
			var values = conn.Query<Item>(sql).ToList();
			List<Item> valuess = values.ToList();

			List<Alg> algs = new List<Alg>();
			algs.Add(new AlgRarestFirst(values));
			algs.Add(new AlgCoverSteiner(values));
			algs.Add(new AlgEnhancedSteiner(values));

			for (int i = minPickupCount; i <= maxPickupCount; i+=stepPickupCount)
			{
				RunSuite(valuess, algs, i, maxRounds);
			}
			Console.WriteLine("All done");
			Console.ReadKey();
		}
		static void RunSuite(List<Item> values, List<Alg> algs, int pickupCount, int rounds)
		{
			Console.WriteLine("pickup {0}",pickupCount);
			
			
			Dictionary<Alg,List<Result>> resultSets = new Dictionary<Alg,List<Result>>();
			foreach (var alg in algs)
			{
				resultSets[alg] = new List<Result>();
			}

			for (int i = 0; i < rounds; i++)
			{
				//random
				HashSet<long> requestItems = new HashSet<long>();

				int count = values.Count;
				for (int j = 0; j < pickupCount;)
				{
					Item it = values.ElementAt(r.Next(count));
					long d = it.UniqId == 0 ? it.TaobaoId : it.UniqId;
					if (!requestItems.Contains(d))
					{
						requestItems.Add(d);
						j++;
					}
				}
				//run every algorithm
				foreach (var alg in algs)
				{
					Console.WriteLine("Alg {0} Round {1}", alg.AlgorithmName, i);
					Result s = alg.DoAlg(requestItems.ToList());
					if (alg.Verify(requestItems, s))
					{
						resultSets[alg].Add(s);
					}
					else
					{
						throw new Exception(alg.AlgorithmName);
					}
				}
				Console.WriteLine("Round {0} Finished", i);

			}
			foreach (Alg alg in algs)
			{
				//flush to data
				double[] avgs = Average(resultSets[alg]);
				string log = string.Format("{0} {1} {2} {3} {4} {5}" + Environment.NewLine,
										pickupCount, avgs[0], avgs[1], avgs[2], avgs[3], avgs[4]);
				File.AppendAllText(alg.AlgorithmName + ".txt", log);
				
			}
			
			
		}
		static double[] Average(List<Result> results)
		{
			int cost = 0;
			int sellerCount = 0;
			int uniqSellerCount = 0;
			long treeCost = 0;
			long time = 0;
			int size = results.Count;

			foreach (var result in results)
			{
				Console.WriteLine(result);
				cost += result.Cost;
				treeCost += result.Treecost;
				sellerCount += result.Sellers.Count;
				uniqSellerCount += result.UniqSellers.Count;
				time += result.Time;
			}
			return new double[]
			       	{
			       		(double)cost/size,
						(double)treeCost/size,
						(double)sellerCount/size,
						(double)uniqSellerCount/size,
						(double)time/size,
			       	};
		}

		static void RunAlg(Alg alg, HashSet<long> requestItems)
		{
			Result s = alg.DoAlg(requestItems.ToList());
			sb.Append(s);
			sb.Append("\nVerify: ");
			sb.Append(alg.Verify(requestItems, s));
			sb.Append(Environment.NewLine);
		}
	}
}
