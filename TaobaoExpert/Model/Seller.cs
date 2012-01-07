using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaobaoSpider.Model
{
	public class Seller
	{
		private int sellerId;
		private long taobaoID;
		private int? credit;
		private DateTime? startTime;
		private double rmatch;
		private double pmatch;
		private double rservice;
		private double pservice;
		private double rspeed;
		private double pspeed;
		private double? refunddays;
		private double? refundrate;
		private double? complaint;
		private int? penalty;
		private double? goodrate;
		private bool isTmall;
		private bool? pprotect;
		private bool? psevendays;
//		private bool? pcharge;
		private bool? preal;
		private bool? pinvoice;

		/// <summary>
		/// 卖家数据库ID
		/// </summary>
		public int SellerId
		{
			get { return sellerId; }
			set { sellerId = value; }
		}

		/// <summary>
		/// 卖家淘宝ID（纯数字）
		/// </summary>
		public long TaobaoId
		{
			get { return taobaoID; }
			set { taobaoID = value; }
		}

		/// <summary>
		/// 卖家信用
		/// </summary>
		public int? Credit
		{
			get { return credit; }
			set { credit = value; }
		}

		/// <summary>
		/// 开店日期
		/// </summary>
		public DateTime? StartTime
		{
			get { return startTime; }
			set { startTime = value; }
		}

		/// <summary>
		/// 宝贝与描述相符分
		/// </summary>
		public double Rmatch
		{
			get { return rmatch; }
			set { rmatch = value; }
		}

		/// <summary>
		/// 宝贝与描述相符分 与同行业平均水平高低
		/// </summary>
		public double Pmatch
		{
			get { return pmatch; }
			set { pmatch = value; }
		}

		/// <summary>
		/// 卖家的服务态度分
		/// </summary>
		public double Rservice
		{
			get { return rservice; }
			set { rservice = value; }
		}

		/// <summary>
		/// 卖家的服务态度分 与同行业平均水平高低
		/// </summary>
		public double Pservice
		{
			get { return pservice; }
			set { pservice = value; }
		}

		/// <summary>
		/// 卖家的发货速度
		/// </summary>
		public double Rspeed
		{
			get { return rspeed; }
			set { rspeed = value; }
		}

		/// <summary>
		/// 卖家的发货速度 与同行业平均水平高低
		/// </summary>
		public double Pspeed
		{
			get { return pspeed; }
			set { pspeed = value; }
		}

		/// <summary>
		/// 平均退款速度（天）
		/// </summary>
		public double? Refunddays
		{
			get { return refunddays; }
			set { refunddays = value; }
		}

		/// <summary>
		/// 近30天退款率
		/// </summary>
		public double? Refundrate
		{
			get { return refundrate; }
			set { refundrate = value; }
		}

		/// <summary>
		/// 近30天投诉率
		/// </summary>
		public double? Complaint
		{
			get { return complaint; }
			set { complaint = value; }
		}

		/// <summary>
		/// 近30天处罚数
		/// </summary>
		public int? Penalty
		{
			get { return penalty; }
			set { penalty = value; }
		}

		/// <summary>
		/// 好评率
		/// </summary>
		public double? Goodrate
		{
			get { return goodrate; }
			set { goodrate = value; }
		}

		/// <summary>
		/// 是否是淘宝商城
		/// </summary>
		public bool IsTmall
		{
			get { return isTmall; }
			set { isTmall = value; }
		}

		/// <summary>
		/// 是否参加消费者保障
		/// </summary>
		public bool? Pprotect
		{
			get { return pprotect; }
			set { pprotect = value; }
		}

		/// <summary>
		/// 是否参加七天退款
		/// </summary>
		public bool? Psevendays
		{
			get { return psevendays; }
			set { psevendays = value; }
		}

//		/// <summary>
//		/// 是否
//		/// </summary>
//		public bool? Pcharge
//		{
//			get { return pcharge; }
//			set { pcharge = value; }
//		}

		/// <summary>
		/// 是否参加正品保证
		/// </summary>
		public bool? Preal
		{
			get { return preal; }
			set { preal = value; }
		}

		/// <summary>
		/// 是否参加提供发票
		/// </summary>
		public bool? Pinvoice
		{
			get { return pinvoice; }
			set { pinvoice = value; }
		}

		public override string ToString()
		{
			return string.Format("SellerId: {0}, TaobaoId: {1}, Credit: {2}, StartTime: {3}, Rmatch: {4}, Pmatch: {5}, Rservice: {6}, Pservice: {7}, Rspeed: {8}, Pspeed: {9}, Refunddays: {10}, Refundrate: {11}, Complaint: {12}, Penalty: {13}, Goodrate: {14}, IsTmall: {15}, Pprotect: {16}, Psevendays: {17}, Preal: {18}, Pinvoice: {19}", sellerId, taobaoID, credit, startTime, rmatch, pmatch, rservice, pservice, rspeed, pspeed, refunddays, refundrate, complaint, penalty, goodrate, isTmall, pprotect, psevendays, preal, pinvoice);
		}
	}
}
