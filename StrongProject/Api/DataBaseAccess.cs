using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
namespace StrongProject
{
	public class DataBaseAccess
	{// <summary>
	 /// 数据连接通道
	 /// </summary>
		public OleDbConnection tag_Connection;
		/// <summary>
		/// 表名
		/// </summary>
		public string tag_tableName = "TestResult";
		/// <summary>
		/// 删除
		/// </summary>
		public int tag_isDelect;

		/// <summary>
		/// 
		/// </summary>
		public int tag_count;
		/// <summary>
		/// 实例化,数据库名，暂时不用
		/// </summary>
		/// <param name="DataName"></param>
		public DataBaseAccess(string DataName)
		{
			try
			{
				// string strConn = "Provider=SQLOLEDB;Data Source=127.0.0.1;Integrated Security=SSPI;Initial Catalog=BALA;";
				string path = Path.Combine(Application.StartupPath, "Strong.accdb");
				string strConn = "Provider=MS Access Database;uid=Admin ;Data Source=" + path;
				tag_Connection = new OleDbConnection(strConn);
				if (tag_Connection != null)
				{
					tag_Connection.Open();
				}
			}
			catch (Exception ex)
			{

			}

		}
		/// <summary>
		/// 查询日志结果
		/// </summary>
		/// <param name="sn"></param>
		/// <param name="WORKORDER"></param>
		/// <param name="INFO"></param>
		/// <param name="begin"></param>
		/// <param name="end"></param>
		/// <returns></returns>
		public DataTable SelectWorkOrderResult(string sn, string describe)
		{
			try
			{

				string snStr = null;
				if (sn != "")
				{
					snStr = "  sn like '%" + sn + "%' and ";
				}
				string strdescribe = null;
				if (describe != "")
				{
					strdescribe = "  describeInfo  like '%" + describe + "%' and ";
				}
				string sql = "select * FROM  WorkOrder where " + snStr + strdescribe + "ID > 0";
				if (strdescribe == null && snStr == null)
				{
					sql = "select * FROM  WorkOrder";
				}
				OleDbCommand cmd = tag_Connection.CreateCommand();
				cmd.CommandText = sql;
				DataTable dt = new DataTable();
				OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
				adapter.Fill(dt);
				return dt;
			}
			catch (Exception ex)
			{

				return null;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public DataTable SelectWorkOrderResult(string state)
		{
			try
			{
				string idstate = null;
				if (state != "")
				{
					idstate = "  state = '1'";
				}

				string sql = "select * FROM  WorkOrder where " + idstate;

				OleDbCommand cmd = tag_Connection.CreateCommand();
				cmd.CommandText = sql;
				DataTable dt = new DataTable();
				OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
				adapter.Fill(dt);
				return dt;
			}
			catch (Exception ex)
			{

				return null;
			}
		}
		/// <summary>
		/// 查询日志结果
		/// </summary>
		/// <param name="sn"></param>
		/// <param name="WORKORDER"></param>
		/// <param name="INFO"></param>
		/// <param name="begin"></param>
		/// <param name="end"></param>
		/// <returns></returns>
		public DataTable SelectAlarmResult(string Operate, string test_station_name, DateTime beginDateTime, DateTime EndDateTime, string info, string sn, string ErrId)
		{
			try
			{

				string beginbdatetime = beginDateTime.Year.ToString("00") + beginDateTime.Month.ToString("00") + beginDateTime.Day.ToString("00") + beginDateTime.Hour.ToString("00") + beginDateTime.Minute.ToString("00") + beginDateTime.Second.ToString("00");
				string enddatetime = EndDateTime.Year.ToString("00") + EndDateTime.Month.ToString("00") + EndDateTime.Day.ToString("00") + EndDateTime.Hour.ToString("00") + EndDateTime.Minute.ToString("00") + EndDateTime.Second.ToString("00");

				string str_Operate = null;
				if (Operate != "")
				{
					str_Operate = " Operate like '%" + Operate + "%' and";
				}

				string station_name = null;
				if (test_station_name != "")
				{
					station_name = " test_station_name like '%" + test_station_name + "%' and";
				}
				string str_info = null;
				if (info != "")
				{
					str_info = " info like '%" + info + "%' and";
				}
				string snStr = null;
				if (sn != "")
				{
					snStr = "  sn like '%" + sn + "%' and ";
				}
				string strErrId = null;
				if (ErrId != "")
				{
					strErrId = "  ErrId  '%" + ErrId + "%' and ";
				}
				string sql = "select * FROM " + "alarm where " + str_Operate + info + station_name + snStr + strErrId + "  createDate >= '" + beginbdatetime + "'   and createDate <= '" + enddatetime + "' ";
				OleDbCommand cmd = tag_Connection.CreateCommand();
				cmd.CommandText = sql;
				DataTable dt = new DataTable();
				OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
				adapter.Fill(dt);
				return dt;
			}
			catch (Exception ex)
			{

				return null;
			}
		}

		/// <summary>
		/// 查询日志结果 0报警，1是一般提示信息，2以上是工位步骤信息
		/// </summary>
		/// <param name="sn"></param>
		/// <param name="WORKORDER"></param>
		/// <param name="INFO"></param>
		/// <param name="begin"></param>
		/// <param name="end"></param>
		/// <returns></returns>
		public DataTable SelectAlarmResult(string ErrId)
		{
			try
			{


				string sql = "select top 15 createDate,info FROM alarm  where ErrId= '" + ErrId + "'" + "order by  id desc";
				OleDbCommand cmd = tag_Connection.CreateCommand();
				cmd.CommandText = sql;
				DataTable dt = new DataTable();
				OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
				adapter.Fill(dt);
				return dt;
			}
			catch (Exception ex)
			{

				return null;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="test_station_name"></param>
		/// <param name="beginDateTime"></param>
		/// <param name="EndDateTime"></param>
		/// <param name="sn"></param>
		/// <param name="NgOk"></param>
		/// <returns></returns>
		public DataTable SelectProducePdcaResult(string test_station_name, DateTime beginDateTime, DateTime EndDateTime, string NgOk, string sn)
		{
			try
			{

				string beginbdatetime = beginDateTime.Year.ToString("00") + beginDateTime.Month.ToString("00") + beginDateTime.Day.ToString("00") + beginDateTime.Hour.ToString("00") + beginDateTime.Minute.ToString("00") + beginDateTime.Second.ToString("00");
				string enddatetime = EndDateTime.Year.ToString("00") + EndDateTime.Month.ToString("00") + EndDateTime.Day.ToString("00") + EndDateTime.Hour.ToString("00") + EndDateTime.Minute.ToString("00") + EndDateTime.Second.ToString("00");

				string station_name = null;
				if (test_station_name != "")
				{
					station_name = " test_station_name like '%" + test_station_name + "%' and";
				}

				string snStr = null;
				if (sn != "")
				{
					snStr = "  sn like '%" + sn + "%' and ";
				}
				string strNgOk = null;
				if (NgOk != "")
				{
					strNgOk = "  result NgOk '%" + NgOk + "%' and ";
				}
				string sql = "select * FROM " + "produce_pdca where " + station_name + snStr + strNgOk + "  start_time >= '" + beginbdatetime + "'   and start_time <= '" + enddatetime + "' ";
				OleDbCommand cmd = tag_Connection.CreateCommand();
				cmd.CommandText = sql;
				DataTable dt = new DataTable();
				OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
				adapter.Fill(dt);
				return dt;
			}
			catch (Exception ex)
			{

				return null;
			}
		}


		/// <summary>
		/// 数据库打开
		/// </summary>
		public void SqllitDataBase_Open()
		{
			if (tag_Connection.State != ConnectionState.Closed)
			{
				tag_Connection.Open();
			}
		}
		/// <summary>
		/// 数据库关闭
		/// </summary>
		public void SqllitDataBase_Close()
		{
			if (tag_Connection != null)
			{
				tag_Connection.Close();
			}
		}
		/// <summary>
		/// 清楚表日志，删除30天以前的数据
		/// </summary>
		public void ClearTestResultTable()
		{
			try
			{
				DateTime curTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
				DateTime delcurTime = curTime.AddDays(-30);



				string sql = "delete from TestResult where cstr(BTime) <'" + delcurTime + "'";

				//  string sql = "delete  from " + "TestResult";
				OleDbCommand cmd = tag_Connection.CreateCommand();
				cmd.CommandText = sql;
				DataTable dt = new DataTable();
				OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
				adapter.Fill(dt);
				tag_count = 0;
			}
			catch (Exception ex)
			{

				return;
			}
		}

		/// <summary>
		/// 插入一条数据
		/// </summary>
		/// <param name="sn">sn号</param>
		/// <param name="VhAdd">V+</param>
		/// <param name="VhSub">v-</param>
		/// <param name="Result">结果</param>
		/// <param name="color"></param>
		/// <param name="begin">开始时间</param>
		/// <param name="end">结束时间</param>
		/// <returns></returns>
		public bool InsertAlarmResult(string workOrder, string ErrID, string info, string Operate)
		{
			try
			{

				string beginbdatetime = "'" + DateTime.Now.Year.ToString("00") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + "-" + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00") + "'";
				string sql = "insert into alarm(WorkOrder,ErrID,Info,Operate,createDate) values('" + workOrder + "','" + ErrID + "','" + info + "','" + Operate + "',  " + beginbdatetime + ")";
				OleDbCommand cmd = tag_Connection.CreateCommand();
				cmd.CommandText = sql;
				int res = cmd.ExecuteNonQuery();
				return true;
			}
			catch (Exception ex)
			{


				return false;
			}



		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="workOrder"></param>
		/// <returns></returns>
		public bool InsertworkOrderResult(string workOrder)
		{
			try
			{

				ClearTestResultTable();

				string sql = "insert into box( alarm) values('" + workOrder + "')";
				OleDbCommand cmd = tag_Connection.CreateCommand();
				cmd.CommandText = sql;
				int res = cmd.ExecuteNonQuery();
				tag_count++;
				return true;
			}
			catch (Exception ex)
			{


				return false;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="describeInfo"></param>
		/// <param name="SN"></param>
		/// <param name="box_country"></param>
		/// <param name="box_colour"></param>
		/// <param name="box_size"></param>
		/// <param name="FB1_material_sn"></param>
		/// <param name="FB1_material_country"></param>
		/// <param name="FB2_material_sn"></param>
		/// <param name="FB2_material_country"></param>
		/// <param name="FB3_material_sn"></param>
		/// <param name="FB3_material_country"></param>
		/// <param name="FB4_material_sn"></param>
		/// <param name="FB4_material_country"></param>
		/// <param name="AT_material_channel"></param>
		/// <param name="AT_material_channel_country"></param>
		/// <returns></returns>
		public bool InsertWorkOrder(string describeInfo,
		 string SN,
		 string box_country,
		 string box_colour,
		 string box_size,
		 string box_material_country1,
		 string box_material_country2,
		 string box_material_country3,
		 string box_material_country4,
		 string box_material_country5,
		 string box_material_country6,
		 string FB1_material_sn,
		 string FB1_material_country,
		 string FB2_material_sn,
		 string FB2_material_country,
		 string FB3_material_sn,
		 string FB3_material_country,
		 string FB4_material_sn,
		 string FB4_material_country,
			string AT_material_leftChannel_country,
		 string AT_material_centreChannel_country,
		 string AT_material_RightChannel_country)
		{
			try
			{
				string strdescribeInfo = "'" + describeInfo + "',"; ;
				string strSN = "'" + SN + "',";
				string tag_state = "'0'";
				string strbox_country = "'" + box_country + "',";
				string strbox_colour = "'" + box_colour + "',";
				string strbox_size = "'" + box_size + "',";
				string strbox_material_country1 = "'" + box_material_country1 + "',";
				string strbox_material_country2 = "'" + box_material_country2 + "',";
				string strbox_material_country3 = "'" + box_material_country3 + "',";
				string strbox_material_country4 = "'" + box_material_country4 + "',";
				string strbox_material_country5 = "'" + box_material_country5 + "',";
				string strbox_material_country6 = "'" + box_material_country6 + "',";



				string strFB1_material_sn = "'" + FB1_material_sn + "',";
				string strFB1_material_country = "'" + FB1_material_country + "',";
				string strFB2_material_sn = "'" + FB2_material_sn + "',";
				string strFB2_material_country = "'" + FB2_material_country + "',";
				string strFB3_material_sn = "'" + FB3_material_sn + "',";
				string strFB3_material_country = "'" + FB3_material_country + "',";
				string strFB4_material_sn = "'" + FB4_material_sn + "',";
				string strFB4_material_country = "'" + FB4_material_country + "',";
				string strAT_material_leftChannel_country = "AT_material_leftChannel_country='" + AT_material_leftChannel_country + "' ,";
				string strAT_material_centreChannel_country = "AT_material_centreChannel_country='" + AT_material_centreChannel_country + "',";
				string strAT_material_RightChannel_country = "AT_material_RightChannel_country='" + AT_material_RightChannel_country + "'";

				string sql = "insert into WorkOrder(describeInfo,SN,state,box_country,box_colour,box_size,box_material_country1,box_material_country2,box_material_country3,box_material_country4,box_material_country5,box_material_country6,FB1_material_sn,FB1_material_country,FB2_material_sn,FB2_material_country,FB3_material_sn,FB3_material_country,FB4_material_sn,FB4_material_country,AT_material_channel,AT_material_channel_country) values("
				  + strdescribeInfo + strSN + tag_state + strbox_country + strbox_colour + strbox_size + strbox_material_country1 + strbox_material_country2 + strbox_material_country3 + strbox_material_country4 + strbox_material_country5 + strbox_material_country6 + strFB1_material_sn + strFB1_material_country + strFB2_material_sn + strFB2_material_country + strFB3_material_sn + strFB3_material_country + strFB4_material_sn + strFB4_material_country + strAT_material_leftChannel_country + strAT_material_centreChannel_country + strAT_material_RightChannel_country + ")";
				OleDbCommand cmd = tag_Connection.CreateCommand();
				cmd.CommandText = sql;
				int res = cmd.ExecuteNonQuery();
				tag_count++;
				return true;
			}
			catch (Exception ex)
			{


				return false;
			}
			return true;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="describeInfo"></param>
		/// <param name="SN"></param>
		/// <param name="box_country"></param>
		/// <param name="box_colour"></param>
		/// <param name="box_size"></param>
		/// <param name="FB1_material_sn"></param>
		/// <param name="FB1_material_country"></param>
		/// <param name="FB2_material_sn"></param>
		/// <param name="FB2_material_country"></param>
		/// <param name="FB3_material_sn"></param>
		/// <param name="FB3_material_country"></param>
		/// <param name="FB4_material_sn"></param>
		/// <param name="FB4_material_country"></param>
		/// <param name="AT_material_channel"></param>
		/// <param name="AT_material_channel_country"></param>
		/// <returns></returns>
		public bool alterWorkOrder(string describeInfo,
		 string SN,
		 string box_country,
		 string box_colour,
		 string box_size,
		 string box_material_country1,
		 string box_material_country2,
		 string box_material_country3,
		 string box_material_country4,
		 string box_material_country5,
		 string box_material_country6,
		 string FB1_material_sn,
		 string FB1_material_country,
		 string FB2_material_sn,
		 string FB2_material_country,
		 string FB3_material_sn,
		 string FB3_material_country,
		 string FB4_material_sn,
		 string FB4_material_country,
		 string AT_material_leftChannel_country,
		 string AT_material_centreChannel_country,
		 string AT_material_RightChannel_country,
			string id)
		{
			try
			{
				string strdescribeInfo = " describeInfo='" + describeInfo + "',"; ;
				string strSN = "SN='" + SN + "',";
				string strbox_country = "box_country='" + box_country + "',";
				string strbox_colour = "box_colour='" + box_colour + "',";
				string strbox_size = "box_size='" + box_size + "',";
				string strbox_material_country1 = "box_material_country1='" + box_material_country1 + "',";
				string strbox_material_country2 = "box_material_country2='" + box_material_country2 + "',";
				string strbox_material_country3 = "box_material_country3='" + box_material_country3 + "',";
				string strbox_material_country4 = "box_material_country4='" + box_material_country4 + "',";
				string strbox_material_country5 = "box_material_country5='" + box_material_country5 + "',";
				string strbox_material_country6 = "box_material_country6='" + box_material_country6 + "',";



				string strFB1_material_sn = "FB1_material_sn='" + FB1_material_sn + "',";
				string strFB1_material_country = "FB1_material_country='" + FB1_material_country + "',";
				string strFB2_material_sn = "FB2_material_sn='" + FB2_material_sn + "',";
				string strFB2_material_country = "FB2_material_country='" + FB2_material_country + "',";
				string strFB3_material_sn = "FB3_material_sn='" + FB3_material_sn + "',";
				string strFB3_material_country = "FB3_material_country='" + FB3_material_country + "',";
				string strFB4_material_sn = "FB4_material_sn='" + FB4_material_sn + "',";
				string strFB4_material_country = "FB4_material_country='" + FB4_material_country + "',";
				string strAT_material_leftChannel_country = "AT_material_leftChannel_country='" + AT_material_leftChannel_country + "' ,";
				string strAT_material_centreChannel_country = "AT_material_centreChannel_country='" + AT_material_centreChannel_country + "',";
				string strAT_material_RightChannel_country = "AT_material_RightChannel_country='" + AT_material_RightChannel_country + "'";
				string strid = "ID=" + id + "";

				//  UPDATE Person SET FirstName = 'Fred' WHERE LastName = 'Wilson'

				string sql = "UPDATE WorkOrder SET " + strdescribeInfo + strSN + strbox_country +
					strbox_colour + strbox_size + strbox_material_country1 + strbox_material_country2 +
					strbox_material_country3 + strbox_material_country4 + strbox_material_country5 +
					strbox_material_country6 + strFB1_material_sn + strFB1_material_country + strFB2_material_sn +
					strFB2_material_country + strFB3_material_sn + strFB3_material_country + strFB4_material_sn +
					strFB4_material_country + strAT_material_leftChannel_country + strAT_material_centreChannel_country + strAT_material_RightChannel_country + "    where " + strid;
				OleDbCommand cmd = tag_Connection.CreateCommand();
				cmd.CommandText = sql;
				int res = cmd.ExecuteNonQuery();
				tag_count++;
				return true;
			}
			catch (Exception ex)
			{


				return false;
			}
			return true;
		}
		public bool deleteWorkOrder(string id)
		{
			try
			{

				string strid = "ID=" + id + "";

				//  UPDATE Person SET FirstName = 'Fred' WHERE LastName = 'Wilson'

				string sql = "delete from WorkOrder  where  " + strid;
				OleDbCommand cmd = tag_Connection.CreateCommand();
				cmd.CommandText = sql;
				int res = cmd.ExecuteNonQuery();
				tag_count++;
				return true;
			}
			catch (Exception ex)
			{


				return false;
			}
			return true;

		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="describeInfo"></param>
		/// <param name="SN"></param>
		/// <param name="box_country"></param>
		/// <param name="box_colour"></param>
		/// <param name="box_size"></param>
		/// <param name="FB1_material_sn"></param>
		/// <param name="FB1_material_country"></param>
		/// <param name="FB2_material_sn"></param>
		/// <param name="FB2_material_country"></param>
		/// <param name="FB3_material_sn"></param>
		/// <param name="FB3_material_country"></param>
		/// <param name="FB4_material_sn"></param>
		/// <param name="FB4_material_country"></param>
		/// <param name="AT_material_channel"></param>
		/// <param name="AT_material_channel_country"></param>
		/// <returns></returns>
		public bool alterCurrWorkOrder(string workOrderId,
			string state)
		{
			try
			{
				string strstate = " state='" + state + "'";

				string strworkOrderId = "sn='" + workOrderId + "'";

				string sql1 = "UPDATE WorkOrder SET state ='0' where state = '1'";

				string sql = "UPDATE WorkOrder SET " + strstate + "  where " + strworkOrderId;
				OleDbCommand cmd = tag_Connection.CreateCommand();
				cmd.CommandText = sql1;
				int res = cmd.ExecuteNonQuery();
				cmd.CommandText = sql;
				res = cmd.ExecuteNonQuery();
				tag_count++;
				return true;
			}
			catch (Exception ex)
			{


				return false;
			}
			return true;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sn"> 手机SN</param>
		/// <param name="station_id">工位ID</param>
		/// <param name="product">产品</param>
		/// <param name="test_station_name">工位名</param>
		/// <param name="result">结果</param>
		/// <param name="start_time">开始时间</param>
		/// <param name="stop_time">结束时间</param>
		/// <param name="mac_address">PC地址</param>
		/// <param name="operate">操作员</param>
		/// <param name="workOrder">工单</param>
		/// <param name="submit">提交结果</param>
		/// <param name="toolssn">载具SN</param>
		/// <returns></returns>
		public bool InsertProducePdca(string sn,
				 string station_id,
				 string product,
				 string test_station_name,
				 string result,
				 string start_time,
				 string stop_time,
				 string mac_address,
				 string operate,
				 string workOrder,
				 string submit,
				  string toolsn)
		{
			try
			{

				string strsn = " '" + sn + "',";
				string strstation_id = " '" + station_id + "',";
				string strproduct = " '" + product + "',";
				string strtest_station_name = " '" + test_station_name + "',";
				string strresult = " '" + result + "',";
				string strstart_time = " '" + start_time + "',";
				string strstop_time = " '" + stop_time + "',";
				string strmac_address = " '" + mac_address + "',";
				string stroperate = " '" + operate + "',";
				string strworkOrder = " '" + workOrder + "',";
				string strsubmit = " '" + submit + "',";
				string strtoolssn = " '" + toolsn + "'";
				string sql = "insert into produce_pdca( sn, station_id, product,test_station_name,result,start_time, stop_time,mac_address,operate,workOrder,submit,toolssntoolSn) values(" + strsn +
				strstation_id +
				strproduct +
				strtest_station_name +
				strresult +
				strstart_time +
				strstop_time +
				strmac_address +
				stroperate +
				strworkOrder +
				strsubmit +
				toolsn + ")";
				OleDbCommand cmd = tag_Connection.CreateCommand();
				cmd.CommandText = sql;
				int res = cmd.ExecuteNonQuery();
				tag_count++;
				return true;
			}
			catch (Exception ex)
			{


				return false;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="toolSn"></param>
		/// <returns></returns>
		public DataTable SelectProducePdca(string toolSn, string start_time)
		{
			try
			{

				string strtoolSn = null;
				string strstart_time = null;

				if (toolSn != "")
				{
					strtoolSn = " toolSn = '" + toolSn + "' and";
				}
				if (toolSn != "")
				{
					strstart_time = " start_time = '" + start_time + "'";
				}
				string sql = "select * FROM " + "produce_pdca where " + strtoolSn + strstart_time;
				OleDbCommand cmd = tag_Connection.CreateCommand();
				cmd.CommandText = sql;
				DataTable dt = new DataTable();
				OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
				adapter.Fill(dt);
				return dt;
			}
			catch (Exception ex)
			{

				return null;
			}
		}
		/// <summary>
		/// 插入治具，SN
		/// </summary>
		/// <param name="workOrder"></param>
		/// <returns></returns>
		public bool InsertToolIphoneSn(string toolSn,
			string iphoneSn)
		{
			try
			{

				string striphoneSn = " '" + iphoneSn + "',";
				string strtoolSn = " '" + toolSn + "'";
				string sql = "insert into tool( iphoneSn,toolSn) values(" + striphoneSn + strtoolSn + ")";
				OleDbCommand cmd = tag_Connection.CreateCommand();
				cmd.CommandText = sql;
				int res = cmd.ExecuteNonQuery();
				return true;
			}
			catch (Exception ex)
			{


				return false;
			}
		}
		/// <summary>
		/// 修改治具绑定手机号 
		/// </summary>
		/// <param name="workOrderId"></param>
		/// <param name="state"></param>
		/// <returns></returns>
		public bool alterToolIphoneSn(string toolSn,
			string iphoneSn)
		{
			try
			{
				string striphoneSn = " iphoneSn='" + iphoneSn + "'";
				string strtoolSn = "toolSn='" + toolSn + "'";
				string sql1 = "UPDATE tool SET state ='0' where toolSn = '1'";
				string sql = "UPDATE tool SET " + striphoneSn + "  where " + strtoolSn;
				OleDbCommand cmd = tag_Connection.CreateCommand();
				cmd.CommandText = sql;
				int res = cmd.ExecuteNonQuery();

				if (res == 0)
				{
					InsertToolIphoneSn(toolSn, iphoneSn);
				}

				return true;
			}
			catch (Exception ex)
			{


				return false;
			}
			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="toolSn"></param>
		/// <returns></returns>
		public DataTable SelectToolIphoneSn(string toolSn)
		{
			try
			{


				string strtoolSn = null;
				if (toolSn != "")
				{
					strtoolSn = " toolSn = '" + toolSn + "'";
				}

				string sql = "select * FROM " + "tool where " + strtoolSn;
				OleDbCommand cmd = tag_Connection.CreateCommand();
				cmd.CommandText = sql;
				DataTable dt = new DataTable();
				OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
				adapter.Fill(dt);
				return dt;
			}
			catch (Exception ex)
			{

				return null;
			}
		}


		/// <summary>
		/// 插入Tray
		/// </summary>
		/// <param name="workOrder"></param>
		/// <returns></returns>
		public bool InsertTraySn(string traySN,
			string Index)
		{
			try
			{

				string strtraySN = " '" + traySN + "',";
				string strIndex = " '" + Index + "'";
				string sql = "insert into tray( traySN,Index) values(" + strtraySN + strIndex + ")";
				OleDbCommand cmd = tag_Connection.CreateCommand();
				cmd.CommandText = sql;
				int res = cmd.ExecuteNonQuery();
				if (res > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception ex)
			{


				return false;
			}
		}// <summary>
		 /// 修改Tray 
		 /// </summary>
		 /// <param name="workOrderId"></param>
		 /// <param name="state"></param>
		 /// <returns></returns>
		public bool alterTraySn(string traySN,
			string Index)
		{
			try
			{
				string strtraySN = " [traySN] = '" + traySN + "'";
				string strIndex = " [Index] ='" + Index + "'";
				string sql = "UPDATE [tray] SET " + strIndex + "  where " + strtraySN;
				OleDbCommand cmd = tag_Connection.CreateCommand();
				cmd.CommandText = sql;
				int res = cmd.ExecuteNonQuery();

				if (res == 0)
				{
					InsertTraySn(traySN, Index);
				}

				return true;
			}
			catch (Exception ex)
			{


				return false;
			}

		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="toolSn"></param>
		/// <returns></returns>
		public DataTable SelectTraySn(string traySN)
		{
			try
			{
				string strtraySN = null;
				if (traySN != "")
				{
					strtraySN = "where traySN = '" + traySN + "'";
				}

				string sql = "select * FROM " + "tray  " + strtraySN;
				OleDbCommand cmd = tag_Connection.CreateCommand();
				cmd.CommandText = sql;
				DataTable dt = new DataTable();
				OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
				adapter.Fill(dt);
				return dt;
			}
			catch (Exception ex)
			{

				return null;
			}
		}
	}

}




