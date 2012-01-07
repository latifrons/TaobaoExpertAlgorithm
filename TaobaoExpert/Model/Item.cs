using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaobaoSpider.Model
{
	public class Item
	{
		private int itemID;
		private long uniqID;
		private long taobaoID;
		private string name;
		private double price;
		private double freight;
		private string location;
		private int sellerTaobaoID;
		private string urlLink;
		private int recentDeal;

		public int ItemId
		{
			get { return itemID; }
			set { itemID = value; }
		}

		public long UniqId
		{
			get { return uniqID; }
			set { uniqID = value; }
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public double Price
		{
			get { return price; }
			set { price = value; }
		}

		public double Freight
		{
			get { return freight; }
			set { freight = value; }
		}

		public string Location
		{
			get { return location; }
			set { location = value; }
		}

		public int SellerTaobaoId
		{
			get { return sellerTaobaoID; }
			set { sellerTaobaoID = value; }
		}

		public string UrlLink
		{
			get { return urlLink; }
			set { urlLink = value; }
		}

		public int RecentDeal
		{
			get { return recentDeal; }
			set { recentDeal = value; }
		}

		public long TaobaoId
		{
			get { return taobaoID; }
			set { taobaoID = value; }
		}

		public override string ToString()
		{
			return string.Format("ItemId: {0}, UniqId: {1}, Name: {2}, Price: {3}, Freight: {4}, Location: {5}, SellerTaobaoId: {6}, UrlLink: {7}, RecentDeal: {8}", itemID, uniqID, name, price, freight, location, sellerTaobaoID, urlLink, recentDeal);
		}
	}
}
