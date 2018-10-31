using System;
using System.Threading;

namespace StrongProject
{
	public class DVPModbusPlc
	{
		public JSerialPort tag_SerialPort;
		/** 获取串口缓冲区中的字节数  
     *  
     *    
     *  @Crc_buf: UINT   
     *  @BuffLen:    
     *  @see:      
 */
		public UInt16 crc_ccitt(byte[] Crc_buf, int BuffLen)
		{
			UInt16 Crc_RetrunValue = 0xffff;
			byte i = 0;
			byte j = 0;
			while (BuffLen-- > 0)
			{
				Crc_RetrunValue ^= Crc_buf[i++];
				j = 8;
				do
				{
					if ((Crc_RetrunValue & 0x01) != 0)
					{
						Crc_RetrunValue = (UInt16)((Crc_RetrunValue >> 1) ^ 0xA001);
					}
					else
					{
						Crc_RetrunValue = (UInt16)(Crc_RetrunValue >> 1);
					}
					--j;

				} while (j != 0);
			}
			return Crc_RetrunValue;

		}

		/** crc_CrcCheck  
            *  
            *    
            *  @Crc_buf: UINT   
            *  @BuffLen:    
            *  @see:      
        */
		public bool crc_CrcCheck(byte[] Crc_buf, int BuffLen)
		{
			UInt16 Crc_RetrunValue = crc_ccitt(Crc_buf, BuffLen - 2);

			if (BuffLen < 4)
				return false;
			UInt16 crc_ = BitConverter.ToUInt16(Crc_buf, BuffLen - 2);
			return (crc_ == Crc_RetrunValue);

		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"></param>
		/// <param name="begin"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public byte LRC(byte[] data, int begin, int count)
		{
			byte lrc = 0;
			for (int i = begin; i < begin + count; i++)
			{
				lrc += data[i];
			}
			return (byte)-lrc;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"></param>
		/// <param name="begin"></param>
		/// <param name="count"></param>
		/// <param name="total"></param>
		/// <returns></returns>
		public string taidaTostr(byte[] data, int begin, int count, int total)
		{
			char lrc = (char)0;
			string ret = null;
			for (int i = 0; i < begin; i++)
			{
				lrc = (char)data[i];
				ret = ret + lrc;

			}
			for (int i = begin; i < begin + count; i++)
			{

				if (data[i] / 16 < 10)
				{
					char hex = (char)(48 + data[i] / 16);
					ret = ret + hex;
				}
				else
				{
					char hex = (char)('A' + data[i] / 16 - 10);
					ret = ret + hex;
				}
				if (data[i] % 16 < 10)
				{
					char hex = (char)(48 + data[i] % 16);
					ret = ret + hex;
				}
				else
				{
					char hex = (char)('A' + data[i] % 16 - 10);
					ret = ret + hex;
				}
			}
			for (int i = begin + count; i < total; i++)
			{
				lrc = (char)data[i];
				ret = ret + lrc;
			}
			return ret;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public int taidaToByte(string str, byte[] Receive, int ReceiveLen)
		{
			int len = str.Length;
			int j = 0;
			int bufferLen = (len - 2) / 2;
			if (len < 4)
			{
				return 0;
			}

			if (str[0] == ':')
			{
				while (j < bufferLen)
				{
					if (str[j * 2 + 1] <= 'z' && str[j * 2 + 1] >= 'a')
					{
						Receive[j] = (byte)((str[j * 2 + 1] - 'a' + 10) * 16);
					}
					if (Receive[j * 2 + 1] <= 'Z' && str[j * 2 + 1] >= 'A')
					{
						Receive[j] = (byte)((str[j * 2 + 1] - 'A' + 10) * 16);
					}
					if (str[j * 2 + 1] <= '9' && str[j * 2 + 1] >= '0')
					{
						Receive[j] = (byte)((str[j * 2 + 1] - '0') * 16);
					}

					if (str[j * 2 + 2] <= 'z' && str[j * 2 + 2] >= 'a')
					{
						Receive[j] = (byte)(Receive[j] + (str[j * 2 + 2] - 'a' + 10));
					}
					if (str[j * 2 + 2] <= 'Z' && str[j * 2 + 2] >= 'A')
					{
						Receive[j] = (byte)(Receive[j] + (str[j * 2 + 2] - 'A' + 10));
					}
					if (str[j * 2 + 2] <= '9' && str[j * 2 + 2] >= '0')
					{
						Receive[j] = (byte)(Receive[j] + (str[j * 2 + 2] - '0'));
					}
					j++;
				}

			}
			return j;
		}
		/** 获取串口缓冲区中的字节数  
            *  
            *    
            *  @return: bool    是否发送成功 
            *  @pData:     发送的数据， 
            *  @length:      发送的长度 
        */
		public bool WriteData(byte[] pData, int length)
		{
			try
			{
				tag_SerialPort.tag_SerialPort.Write(pData, 0, length);
				return true;
			}
			catch
			{
				return false;
			}


		}
		/** 获取串口缓冲区中的字节数  
            *  
            *    
            *  @return: int    读取字节的长度
            *  @pData:     读取的数据的数据， 
            *  @length:      发送的长度 
        */
		public int ReadData(byte[] pData, int pDataLen)
		{
			try
			{
				return tag_SerialPort.tag_SerialPort.Read(pData, 0, pDataLen);
			}
			catch
			{
				return 0;
			}

		}
		/** 获取串口缓冲区中的字节数  
            *  
            *    
            *  @return: int    读取字节的长度
            *  @pData:     读取的数据的数据， 
            *  @length:      发送的长度 
        */
		public UInt16 swapInt16(UInt16 value)
		{
			byte[] buffer = BitConverter.GetBytes(value);
			byte h = 0;
			h = buffer[0];
			buffer[0] = buffer[1];
			buffer[1] = h;
			UInt16 ret = BitConverter.ToUInt16(buffer, 0);
			return ret;
		}
		/** * 函数名 CreateReadModbusData
             * 描述: 创建一个MODBUS数据 
             *  本函数提供直接根据DCB参数设置串口参数  
             *  @param:  unsigned short address  
             *  @param:  unsigned short command 命令ID  
             *  @return: int  返回的值    
        **/
		public int CreateReadModbusDataTud(UInt16 address, byte CommandId, UInt16 count, byte[] Receive, int ReceiveLen)
		{
			byte[] buufer = new byte[256];//{0x01,0x01};
			int index = 0;
			buufer[index++] = 0x02;


			UInt16 crc = 0;
			int readlen = 0;
			UInt16 DvpBeginAddress = address;
			buufer[index++] = CommandId;

			byte[] temp = BitConverter.GetBytes(DvpBeginAddress);
			buufer[index++] = temp[1];
			buufer[index++] = temp[0];

			temp = BitConverter.GetBytes(count);
			buufer[index++] = temp[1];
			buufer[index++] = temp[0];






			crc = crc_ccitt(buufer, 6);
			temp = BitConverter.GetBytes(crc);
			buufer[index++] = temp[0];
			buufer[index++] = temp[1];



			if (WriteData(buufer, index))
			{
				Thread.Sleep(500);
				readlen = ReadData(Receive, ReceiveLen);
			}
			if (crc_CrcCheck(Receive, readlen))
			{
				return readlen;
			}
			return 0;
		}
		/** * 函数名 CreateReadModbusData
           * 描述: 创建一个MODBUS数据 
           *  本函数提供直接根据DCB参数设置串口参数  
           *  @param:  unsigned short address  
           *  @param:  unsigned short command 命令ID  
           *  @return: int  返回的值    
      **/
		public int CreateReadModbusData(UInt16 address, byte CommandId, UInt16 count, byte[] Receive, int ReceiveLen)
		{
			byte[] buufer = new byte[256];//{0x01,0x01};
			int index = 0;
			buufer[index++] = 0x3a;
			buufer[index++] = 0x02;

			UInt16 crc = 0;
			int readlen = 0;
			UInt16 DvpBeginAddress = address;
			buufer[index++] = CommandId;

			byte[] temp = BitConverter.GetBytes(DvpBeginAddress);
			buufer[index++] = temp[1];
			buufer[index++] = temp[0];

			temp = BitConverter.GetBytes(count);
			buufer[index++] = temp[1];
			buufer[index++] = temp[0];

			buufer[index] = LRC(buufer, 1, index - 1);
			index++;
			buufer[index++] = (byte)'\r';
			buufer[index] = (byte)'\n';

			string sendstr = taidaTostr(buufer, 1, index - 2, index + 1);

			tag_SerialPort.tag_SerialPort.Write(sendstr);

			Thread.Sleep(500);
			string r = tag_SerialPort.tag_SerialPort.ReadLine();

			readlen = taidaToByte(r, Receive, ReceiveLen);



			return readlen;
		}
		/**  * 函数名 CreateSetModbusData
             * 描述: 创建一个MODBUS数据 
             *  
             *  本函数提供直接根据DCB参数设置串口参数  
             *  @param:  unsigned short address  
             *  @param:  unsigned short command 命令ID  
             *  @return: int  返回的值    
             */
		public int CreateSetModbusDataTud(UInt16 address, byte CommandId, UInt16 var, byte[] Receive, int ReceiveLen)
		{
			byte[] buufer = new byte[256];
			int index = 0;
			UInt16 crc = 0;
			int readlen = 0;
			UInt16 DvpBeginAddress = address;
			buufer[index++] = 0x00;
			buufer[index++] = CommandId;

			byte[] temp = BitConverter.GetBytes(DvpBeginAddress);
			buufer[index++] = temp[1];
			buufer[index++] = temp[0];

			temp = BitConverter.GetBytes(var);
			buufer[index++] = temp[1];
			buufer[index++] = temp[0];

			temp = BitConverter.GetBytes(crc);
			buufer[index++] = temp[0];
			buufer[index++] = temp[1];

			if (WriteData(buufer, 8))
			{
				Thread.Sleep(500);
				readlen = ReadData(Receive, ReceiveLen);

			}
			if (crc_CrcCheck(Receive, readlen))
			{
				return readlen;
			}
			return 0;
		}
		/**  * 函数名 CreateSetModbusData
            * 描述: 创建一个MODBUS数据 
            *  
            *  本函数提供直接根据DCB参数设置串口参数  
            *  @param:  unsigned short address  
            *  @param:  unsigned short command 命令ID  
            *  @return: int  返回的值    
            */
		public int CreateSetModbusData(UInt16 address, byte CommandId, UInt16 var, byte[] Receive, int ReceiveLen)
		{
			byte[] buufer = new byte[256];
			int index = 0;
			UInt16 crc = 0;
			int readlen = 0;
			UInt16 DvpBeginAddress = address;
			buufer[index++] = 0x3a;
			buufer[index++] = CommandId;

			byte[] temp = BitConverter.GetBytes(DvpBeginAddress);
			buufer[index++] = temp[1];
			buufer[index++] = temp[0];

			temp = BitConverter.GetBytes(var);
			buufer[index++] = temp[1];
			buufer[index++] = temp[0];

			buufer[index] = LRC(buufer, 1, index - 1);
			index++;

			buufer[index++] = (byte)'\r';
			buufer[index] = (byte)'\n';

			string sendstr = taidaTostr(buufer, 1, index - 2, index + 1);
			tag_SerialPort.tag_SerialPort.Write(sendstr);
			Thread.Sleep(500);
			string r = tag_SerialPort.tag_SerialPort.ReadLine();

			readlen = taidaToByte(r, Receive, ReceiveLen);

			return 0;
		}
		/** 
            * 函数名 ReadCoilStatus
            * 描述: Read Coil Status 
             *   可读的寄存器 S,Y,M,T,C
             *  本函数提供直接根据DCB参数设置串口参数  
             *  @param:  unsigned short address  
             *  @param:  unsigned short count 地址数  
             *  @return: int  返回的值    
        **/
		public int ReadCoilStatus(UInt16 address, UInt16 count, byte[] Receive, int ReceiveLen)
		{
			return CreateReadModbusData(address, 0x01, count, Receive, ReceiveLen);
		}
		/**
            * 函数名 ReadInputStatus
            * 描述:Read Input Status 
             *   可读的寄存器 S,X,Y,M,T,C
             *  本函数提供直接根据DCB参数设置串口参数  
             *  @param:  unsigned short address  
             *  @param:  unsigned short count 地址数  
             *  @return: int  返回的值    
        **/
		public int ReadInputStatus(UInt16 address, UInt16 count, byte[] Receive, int ReceiveLen)
		{
			return CreateReadModbusData(address, 0x02, count, Receive, ReceiveLen);

		}
		/**
            * 函数名 ReadHoldingRegisters
            * 描述:Read Holding Registers
             *   可读的寄存器 T,C,D,R
             *  本函数提供直接根据DCB参数设置串口参数  
             *  @param:  unsigned short address  
             *  @param:  unsigned short count 地址数  
             *  @return: int  返回的值    
        **/
		public int ReadHoldingRegisters(UInt16 address, UInt16 count, byte[] Receive, int ReceiveLen)
		{
			return CreateReadModbusData(address, 0x03, count, Receive, ReceiveLen);
		}
		/**
            * 函数名 ForceSingleCoil
            * 描述: Force Single Coil,
            *   S,Y,M,T,C
            *  本函数提供直接根据DCB参数设置串口参数  
            *  @param:  unsigned short address  
            *  @param:  unsigned short count 地址数  
            *  @return: int  返回的值    
        **/
		public int ForceSingleCoil(UInt16 address, UInt16 var, byte[] Receive, int ReceiveLen)
		{
			return CreateSetModbusData(address, 0x05, var, Receive, ReceiveLen);
		}
		/**
            * 函数名 ForceSingleCoil
            * 描述: Force Single Coil,
            *   S,Y,M,T,C
            *  05命令
            *  @param:  unsigned short address  
            *  @param:  unsigned short count 地址数  
            *  @return: int  返回的值    
        **/
		public bool ForceSingleCoil(UInt16 address, UInt16 var)
		{
			byte[] buffer = new byte[8];
			if (CreateSetModbusData(address, 0x05, var, buffer, 8) > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/**
            * 函数名 PresetSingleRegister
            * 描述: PresetSingleRegister,
            *   T,C,D
            *  本函数提供直接根据DCB参数设置串口参数  
            *  @param:  unsigned short address  
            *  @param:  unsigned short count 地址数  
            *  @return: int  返回的值    
        **/
		public int PresetSingleRegister(UInt16 address, UInt16 var, byte[] Receive, int ReceiveLen)
		{
			return CreateSetModbusData(address, 0x06, var, Receive, ReceiveLen);
		}
		/**
            * 函数名 PresetSingleRegister
            * 描述: PresetSingleRegister,
            *   T,C,D
            *  06命令 
            *  @param:  unsigned short address  
            *  @param:  unsigned short count 地址数  
            *  @return: int  返回的值    
        **/
		public bool PresetSingleRegister(UInt16 address, UInt16 var)
		{
			byte[] buffer = new byte[256];
			if (CreateSetModbusData(address, 0x06, var, buffer, 256) > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}



		/// <summary>
		///  描述: 寄存器D（0-10）的值
		///      @param: int n   (0-10)( D0..D10)
		///    @return: int  返回Dn的值    

		/// </summary>
		/// <param name="n"></param>
		/// <returns></returns>

		public int GetStatusD(UInt16 n)
		{
			byte[] Receive = new byte[256];
			int ReceiveLen = 255;
			int ret = 0;
			UInt16 address = (UInt16)(0x1000 + n);


			if (ReadHoldingRegisters(address, 1, Receive, ReceiveLen) > 0)
			{
				ret = BitConverter.ToUInt16(Receive, 3);
				return ret;
			}
			return 0;
		}

		/// <summary>
		///  描述: 设置寄存器D（0-10）的值
		///      @param: int 0x1000 + n   (0-10)( D0..D10)
		///    @return: int  返回Dn的值    
		/// </summary>
		/// <param name="n"></param>
		/// <returns></returns>
		public int SetStatusD(UInt16 n, UInt16 var1)
		{
			byte[] Receive = new byte[256];
			int ReceiveLen = 255;
			int ret = 0;
			UInt16 address = (UInt16)(0x1000 + n);
			PresetSingleRegister(address, var1);
			return 0;
		}
	}
}
