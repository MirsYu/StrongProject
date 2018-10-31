namespace StrongProject
{
	public enum PMCommond : uint
	{
		SetUPV1 = 0x01, //设定和读取喷码机参数组1
		SetUPV2 = 0x02, //设定和读取喷码机参数组2
		StartJet = 0x03, //启动墨线
		StopJet = 0x04, //停止墨线
		StartPrint = 0x05, //启动喷印
		StopPrint = 0x06, //停止喷印
		TriggerPrint = 0x07, //触发一次喷印
		GetPrinterStatus = 0x08, //读取喷码机状态参数
		GetJetStatus = 0x09, //读取喷码机墨线运行参数
		DownloadRemoteFieldData = 0x0b, //下载远程段数据
		SetKeyboardLockState = 0x0c, //设定键盘锁状态
		PrintTrigger = 0xf1, //电眼触发
		PrintGo = 0xf2, //开始喷印
		PrintEnd = 0xf3, //喷印结束
	}
	class JSerialPortFZ
	{
		static public byte[] TriggerPrintdata = { };
		static public byte[] Data = { 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, };

	}
}
