using System;

namespace StrongProject
{
	[Serializable]
	public class CardManager
	{
		public static int iControlCardCount = 1;//控制卡数量
		public static int iCountolCardTypeCount = 1;//控制卡类型数量
		public static int iMaxAxisCount = 100;//最大轴数
		public static string[] sCountCradName;  //卡类型数组
												////private Work _Worker = null;
												////public Work Worker
												////{
												//    get
												//    {
												//        return _Worker;
												//    }
												//}
												////
												//private bool _IsLinked = false;
												//public bool IsLinked
												//{
												//    get
												//    {
												//        return _IsLinked;
												//    }
												//}

		////
		//private ControlCardA _CardA = null;
		//public ControlCardA CardA
		//{
		//    get
		//    {
		//        return _CardA;
		//    }
		//}
		//private ControlCardB _CardB = null;
		//public ControlCardB CardB
		//{
		//    get
		//    {
		//        return _CardB;
		//    }
		//}
		//private ControlCardC _CardC = null;
		//public ControlCardC CardC
		//{
		//    get
		//    {
		//        return _CardC;
		//    }
		//}
		////
		//public event EventHandler ConnectChanged;
		//protected void OnConnectChanged()
		//{
		//    if (ConnectChanged != null)
		//    {
		//        ConnectChanged(this, null);
		//    }
		//}

		//public CardManager(Work worker)
		//{
		//    _Worker = worker;
		//    _CardA = new ControlCardA();
		//    _CardB = new ControlCardB();
		//    _CardC = new ControlCardC();
		//    //_MotionB = new MotionB();  

		//}

	}

	public class CardIOEvengArgs : EventArgs //System.EventArgs 是包含事件数据的类的基类。
	{


		public CardIOEvengArgs(int _type, int cardNum, ulong value)
		{
			_CardNum = cardNum;
			_Value = value;
			type = _type;
		}

		private int _CardNum = -1;
		public int CardNum
		{
			get { return _CardNum; }
			set { _CardNum = value; }
		}

		private ulong _Value = 0;
		public ulong Value
		{
			get { return _Value; }
			set { _Value = value; }
		}

		public int type = 0;
	}

	public class CardInputIOEvengArgs : EventArgs
	{
		public CardInputIOEvengArgs(int _type, int cardNum, ulong value)
		{
			_CardNum = cardNum;
			_Value = value;
			type = _type;
		}

		private int _CardNum = -1;
		public int CardNum
		{
			get { return _CardNum; }
			set { _CardNum = value; }
		}

		private ulong _Value = 0;
		public ulong Value
		{
			get { return _Value; }
			set { _Value = value; }
		}

		public int type = 0;
	}

	public class CardAxisSignalEvengArgs : EventArgs
	{

		public CardAxisSignalEvengArgs(int _type, int cardNum, int _axisNUm, ulong value)
		{
			_CardNum = cardNum;
			_Value = value;
			type = _type;
			axisNum = _axisNUm;
		}

		private int _CardNum = -1;
		public int CardNum
		{
			get { return _CardNum; }
			set { _CardNum = value; }
		}

		private ulong _Value = 0;
		public ulong Value
		{
			get { return _Value; }
			set { _Value = value; }
		}

		public int axisNum = 0;
		public int type = 0;

	}
	public class CardAxisPosEvengArgs : EventArgs
	{


		public CardAxisPosEvengArgs(int _type, int cardNum, int _AxisNum, double value)
		{
			_CardNum = cardNum;
			type = _type;
			_Value = value;
			AxisNum = _AxisNum;

		}

		public CardAxisPosEvengArgs(int cardNum, double value)
		{
			_CardNum = cardNum;
			_Value = value;
		}

		private int _CardNum = -1;
		public int CardNum
		{
			get { return _CardNum; }
			set { _CardNum = value; }
		}

		private double _Value = 0;
		public double Value
		{
			get { return _Value; }
			set { _Value = value; }
		}
		public int type = 0;
		public int AxisNum = 0;
	}
}
