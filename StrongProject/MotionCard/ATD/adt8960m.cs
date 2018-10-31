using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;


namespace DEMO
{
    class adt8960m
    {
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_initial();
				/***************************初始化卡******************************
				功能：初始化运动控制卡
				(1)返回值>0时，表示adt8960卡的数量。如果为3，则下面的可用卡号分别为0、1、2；
				(2)返回值=0时，说明没有安装adt8960卡；
				(3)返回值<0时，-1表示没有安装端口驱动程序，-2表示PCI桥故障。
				*****************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_set_pulse_mode(Int32  cardno,Int32  axis,Int32  value,Int32 logic,Int32 dir_logic);
				/********************设置输出脉冲的工作方式***********************
				功能：
					设置输出脉冲的工作方式
				参数：
					cardno	   卡号
					axis		轴号(1-6)
					value 	   0： 脉冲+脉冲方式     1：脉冲+方向方式
					logic	   0：	正逻辑脉冲		 1：	负逻辑脉冲
					dir-logic  0：方向输出信号正逻辑 1：方向输出信号负逻辑
				返回值	       0：正确				 1：错误
				默认模式为：脉冲+方向方式，正逻辑脉冲，方向输出信号正逻辑
				*****************************************************************/
				
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_set_limit_mode(Int32  cardno,Int32  axis,Int32  v1,Int32  v2,Int32  logic);
				/*************设定正/负方向限位输入nLMT信号的模式设定*************
				功能：
					设定正/负方向限位输入nLMT信号的模式
				参数：
					cardno	    卡号
					axis		轴号(1-6)
					v1          0：正限位有效       1：正限位无效
					v2          0：负限位有效       1：负限位无效
					logic       0：低电平有效       1：高电平有效
				返回值			0：正确				1：错误 
				默认模式为：正限位有效，负限位有效，低电平有效
				注意： 限位信号不能设置成有效/无效。
				*****************************************************************/
				
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_set_stop0_mode(Int32  cardno,Int32  axis,Int32  v,Int32  logic);
				/********************设定stop0输入信号的模式**********************
				功能：设置stop0信号的有效/无效和逻辑电平
				参数：
					cardno	   卡号
					axis		轴号(1-6)
					v		    0：无效				1：有效
					logic		0：低电平停止		1：高电平停止
				返回值	        0：正确				1：错误
				默认模式为：：无效，低电平停止
				*****************************************************************/
				
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_set_stop1_mode(Int32  cardno,Int32  axis,Int32  v,Int32  logic);
				/*********************设定stop1输入信号的模式*********************
				功能：设置stop1信号的有效/无效和逻辑电平
				参数：
					cardno	   卡号
					axis		轴号(1-6)
					v		    0：无效				1：有效
					logic		0：低电平停止		1：高电平停止
				返回值	        0：正确				1：错误
				默认模式为：：无效，低电平停止
				*****************************************************************/
				
				//----------------------------------------------------//
				//               驱动状态检查函数                     //
				//----------------------------------------------------//
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_get_status(Int32  cardno,Int32  axis, out Int32  v);
				/*************************获取各轴的驱动状态**********************
				功能：
					获取单轴的驱动状态
				参数：
					cardno	   卡号
					axis		轴号(1-6)
					v			驱动状态的指针
								0：驱动结束        非0：value为两个字节长度的值
				返回值			0：正确				 1：错误
				*****************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_get_inp_status(Int32  cardno,out Int32  v);
				/************************获取插补的驱动状态***********************
				功能：
					获取插补运动的驱动状态
				参数：
					cardno	   卡号
					v		    插补状态的指针
								0：插补结束			1：正在插补
				返回值			0：正确				1：错误
				*****************************************************************/
				
				//----------------------------------------------------//
				//               运动参数设定函数                     //
				//----------------------------------------------------//
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_set_acc(Int32  cardno, Int32  axis,Int32  add);
				/**************************加速度设定*****************************
				功能：
					设定加速度的值
				参数：
					cardno	    卡号
					axis		轴号(1-6)
					add         (1-64000)
				硬件版本1        加速度＝add/125 
				返回值			0：正确				1：错误
				*****************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_set_startv(Int32  cardno, Int32  axis,Int32  speed);
				/*************************设定初始速度****************************
				功能：
					初始速度设定
				参数：
					cardno	    卡号
					axis		轴号(1-6)
					speed       范围(1-2M)
				返回值			0：正确					1：错误 
				*****************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_set_speed(Int32  cardno, Int32  axis,Int32  speed);
				/***************************设定驱动速度**************************
				功能：
					驱动速度的设定
				参数：
					cardno	    卡号
					axis		轴号(1-6)
					speed       范围(1-2M)
				返回值			0：正确					1：错误 
				*****************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_set_command_pos(Int32  cardno, Int32  axis,Int32  pos);
				/*************************设定逻辑位置****************************
				功能：
					逻辑位置设定
				参数：
					cardno	    卡号
					axis		轴号(1-6)
					pos         范围(-2147483648～+2147483647)
				返回值			0：正确					1：错误 
				*****************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_set_actual_pos(Int32  cardno, Int32  axis,Int32  pos);
				/**************************设定实际位置***************************
				功能：
					实际位置设定
				参数：
					cardno	    卡号
					axis		轴号(1-6)
					pos         范围(-2147483648～+2147483647)
				返回值			0：正确					1：错误 
				*****************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_set_symmetry_speed(Int32  cardno,Int32  axis,Int32  lspd,Int32  hspd,double tacc,Int32  vacc,Int32  mode);
				/**************************设定加速度************************
				功能:	设定加速度的值
				参数:
					cardno       卡号
					axis         轴号(1-6)
					lspd         起步速度
				    hspd         驱动速度
					tacc         加速时间
					mode         加速模式（0:梯形,1:S型）
					vacc         加速度变化率
				返回值           0:正确          1:错误  
				******************************************************************/
				
				//----------------------------------------------------//
				//               运动参数检查函数                     //
				//----------------------------------------------------//
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_get_command_pos(Int32  cardno,Int32  axis,out Int32  pos);
				/**************************获取逻辑位置****************************
				功能：
					获取各轴的逻辑位置
				参数：
					cardno	    卡号
					axis		轴号(1-6)
					pos         逻辑位置的指针
				返回值			0：正确					1：错误 
				******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_get_actual_pos(Int32  cardno,Int32  axis, out Int32  pos);
				/*************************获取实际位置****************************
				功能：
					获取各轴的实际位置
				参数：
					cardno	    卡号
					axis		轴号(1-6)
					pos         实际位置的指针
				返回值			0：正确					1：错误 
				*****************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_get_speed(Int32  cardno,Int32  axis,out Int32  speed);
				/**************************获取驱动速度***************************
				功能：
					获取各轴的当前驱动速度
				参数：
					cardno	    卡号
					axis		轴号(1-6)
					speed       当前驱动速度的指针
				返回值			0：正确					1：错误 
				*****************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_get_out(Int32  cardno, Int32  number);
				/***************************获取输出状态***************************
				功能:获取输出状态
				参数:
					cardno       卡号
					number       端口号
				返回值:指定端口的当前状态,-1表示参数错误    
				******************************************************************/
				
				//----------------------------------------------------//
				//                   驱动函数                         //
				//----------------------------------------------------//
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_pmove(Int32  cardno,Int32  axis,Int32  pos);
				/*************************定量驱动***************************
				功能：
					单轴定量驱动
				参数：
					cardno 	   卡号
					axis		轴号(1-6)
					pulse 	   输出的脉冲数            范围（-268435455~+268435455）
								>0：正方向移动		   <0：负方向移动
				返回值	   0：正确				    1：错误
				**************************************************************/
				
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_continue_move(Int32  cardno,Int32  axis,Int32  dir);
				/*************************连续驱动***************************
				功能：
					单轴连续驱动
				参数：
				cardno 	   卡号
				axis		轴号(1-6)
				dir		   驱动的方向
						   0：正方向移动		   1：负方向移动
				返回值	   0：正确				   1：错误
				***************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_dec_stop(Int32  cardno,Int32  axis);
				/**************************驱动减速停止*************************
				功能：
					减速停止当前的驱动过程
				参数：
				cardno	   卡号
				axis		轴号(1-6)
				返回值	   0：正确				1：错误
				***************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_sudden_stop(Int32  cardno,Int32  axis);
				/***************************驱动立即停止************************
				功能：
					立即停止当前的驱动过程
				参数：
				cardno	   卡号
				axis		轴号(1-6)
				返回值	   0：正确				1：错误
				立即停止正在驱动中的脉冲输出，在加/减速驱动中也立即停止。
				******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_inp_move2(Int32  cardno,Int32  axis1,Int32  axis2,Int32  pulse1,Int32  pulse2);
				/****************************两轴直线插补*************************
				功能：
					两轴直线插补运动
				参数：
				cardno	   卡号
				axis1,axis2	    参与插补的轴号        
				pulse1,pulse2	移动的相对距离    范围（-8388608~+8388607）
				返回值	   0：正确			  	  1：错误
				******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_inp_move3(Int32  cardno,Int32  axis1,Int32  axis2,Int32  axis3,Int32  pulse1,Int32  pulse2,Int32  pulse3);
				/*****************************三轴直线插补**************************
				功能：
					三轴直线插补运动
				参数：
				cardno	   卡号
				axis1,axis2.axis3       参与插补的轴号        
				pulse1,pulse2,pulse3	移动的相对距离    范围（-8388608~+8388607）
				返回值	   0：正确			  	  1：错误
				******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_inp_move4(Int32  cardno,Int32  axis1,Int32  axis2,Int32  axis3,Int32  axis4,Int32  pulse1,Int32  pulse2,Int32  pulse3,Int32  pulse4);
				/***********************功能：四轴直线插补************************
				功能：
					四轴直线插补运动
				参数：
				cardno	   卡号
				axis1,axis2.axis3,axis4         参与插补的轴号        
				pulse1,pulse2,pulse3,pulse4 	移动的相对距离    范围（-8388608~+8388607）
				返回值	   0：正确			  	  1：错误
				******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_inp_move5(Int32  cardno,Int32  axis1,Int32  axis2,Int32  axis3,Int32  axis4,Int32  axis5,Int32  pulse1,Int32  pulse2,Int32  pulse3,Int32  pulse4,Int32  pulse5);
				/***********************功能：五轴直线插补************************
				功能：
					五轴直线插补运动
				参数：
				 cardno	   卡号
				axis1,axis2.axis3,axis4,axis5         参与插补的轴号        
				pulse1,pulse2,pulse3,pulse4,pulse5 	  移动的相对距离    范围（-8388608~+8388607）
				返回值	   0：正确			  	  1：错误
				******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_inp_move6(Int32  cardno,Int32  pulse1,Int32  pulse2,Int32  pulse3,Int32  pulse4,Int32  pulse5,Int32  pulse6);
				/***********************功能：六轴直线插补************************
				功能：
					六轴直线插补运动
				参数：
				cardno	   卡号
				pulse1,pulse2,pulse3,pulse4,pulse5,pulse6
				 	  移动的相对距离    范围（-8388608~+8388607）
				返回值	   0：正确			  	  1：错误
				******************************************************************/
				
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_get_lib_version(Int32  cardno);
				/************************************************
				功能：获取当前库版本号
				返回值为库版本的版本号
				************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Single  adt8960_get_hardware_ver(Int32  cardno);
				/***************************获取硬件版本***************************
				功能：
					硬件版本的获取
				参数：
					cardno       卡号
				返回值       硬件版本     
				******************************************************************/
				
				
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_read_bit(Int32  cardno,Int32  number);
				/**************************输入点的读取****************************
				功能：读取输入点
				参数：
					cardno	    卡号
					number      输入点(0-31)
				返回值			0：低电平			1：高电平      -1：错误
				*******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_write_bit(Int32  cardno,Int32  number,Int32  value);
				/***************************输出点的读取*****************************
				功能：
					读取输出点
				参数：
					cardno	    卡号
					number      输出点(0-15)
					value       0: 低电平           1: 高电平
				返回值			0：正确				1：错误
				********************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_set_suddenstop_mode(Int32  cardno,Int32  v,Int32  logic);
				/****************************硬件停止模式**************************
				功能：
					硬件停止模式设置
				参数：
					cardno       卡号
					v            0：无效             1：有效
					logic        0：低电平有效       1：高电平有效
				返回值			 0：正确			 1：错误
				硬件停止信号固定使用J2端子板33引脚(IN31)
				******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_set_io_mode(Int32  cardno,Int32  v1,Int32  v2);
				/***************************设定输入输出******************************
				功能：
					设定输入输出
				 参数： 
					v1		    0:前面8个点定义为输入 1:前面的8个点定义为输出
				
					v2		    0:后面8个点定义为输入 1:后面的8个点定义为输出
				
				  返回值		0:正确				  1:错误
				
				 注：当IO点作为输出点用的时候且能同时读到输入状态
				*********************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_get_delay_status(Int32  cardno);
				/**************************获取延时状态******************************
				功能：
					获取延时状态
				参数：
					cardno       卡号
				返回值			0：延时结束          1：延时进行中
				********************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_set_delay_time(Int32  cardno,Int32  time);
				/***************************设定延时时间******************************
				功能：
					设定延时时间
				参数：
					cardno       卡号
					time          延时时间
				返回值			0：正确				1：错误
				时间单位为1/8us
				*********************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_get_stopdata(Int32  cardno,Int32  axis,out Int32 value);
				/*************************获取各轴的错误停止信息*******************
				功能：获取各轴的错误停止信息
				参数：
				cardno	   卡号
				axis		轴号（1-4）
				value	   停止信息的指针
				0：无错误              
				非0：有限位或是STOP停止信号触发.
				通过低四位表示触发脉冲停止的信号类型,正限位:D0,负限位:D1,STOP0:D2,STOP1:D3,
				例如:value值为1时,正限位
				value值为2时,负限位
				value值为4时,STOP0
				value值为8时,STOP1
				也可能组合出现,value为3（1+2）时,正负限位都触发等等
				
				  返回值         0：正确          1：错误
				******************************************************************/
				
				
				//*********************************************//
				//               复合驱动类                    //
				//*********************************************//
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_symmetry_relative_move(Int32  cardno, Int32  axis, Int32  pulse, Int32  lspd ,Int32  hspd, double tacc, Int32  vacc, Int32  mode);
				/********************************************************
				*功能:单轴直线插补相对运动，参照当前位置,以加速进行定量移动
				*参数:
				      cardno-卡号
					  axis---轴号
					  pulse--脉冲
					  lspd---低速
					  hspd---高速
				      tacc---加速时间(单位:秒)
					  vacc---加速度变化率
				      mode---模式（0:梯形，1:S曲线）
				返回值         0：正确          1：错误
				*********************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_symmetry_absolute_move(Int32  cardno, Int32  axis, Int32  pulse, Int32  lspd ,Int32  hspd, double tacc, Int32  vacc, Int32  mode)  ;
				/*********************************************************
				*功能:单轴直线插补绝对运动，参照零点位置,以加速进行定量移动
				*参数:
				      cardno-卡号 
					  axis---轴号
					  pulse--脉冲
					  lspd---低速
					  hspd---高速
				      tacc---加速时间(单位:秒)
					  vacc---加速度变化率
				      mode---模式（0:梯形，1:S曲线）
				返回值         0：正确          1：错误
				**********************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_symmetry_relative_line2(Int32  cardno, Int32  axis1, Int32  axis2, Int32  pulse1, Int32  pulse2, Int32  lspd ,Int32  hspd, double tacc, Int32  vacc, Int32  mode)  ;
				/**********************************************************
				*功能:二轴直线插补相对运动，参照当前位置,以加速进行直线插补
				*参数:
				      cardno-卡号
					  axis1---轴号1
					  axis2---轴号2	
					  pulse1--脉冲1
					  pulse2--脉冲2
					  lspd---低速
					  hspd---高速
				      tacc---加速时间(单位:秒)
					  vacc---加速度变化率
				      mode---模式（0:梯形，1:S曲线）
				返回值         0：正确          1：错误
				***********************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_symmetry_absolute_line2(Int32  cardno, Int32  axis1, Int32  axis2, Int32  pulse1, Int32  pulse2, Int32  lspd ,Int32  hspd, double tacc, Int32  vacc, Int32  mode);
				/***********************************************************
				*功能:二轴直线插补绝对运动，参照零点位置,以加速进行直线插补
				*参数:
				      cardno-卡号
					  axis1---轴号1
					  axis2---轴号2	
					  pulse1--脉冲1
					  pulse2--脉冲2
					  lspd---低速
					  hspd---高速
				      tacc---加速时间(单位:秒)
					  vacc---加速度变化率
				      mode---模式（0:梯形，1:S曲线）
				返回值         0：正确          1：错误
				************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_symmetry_relative_line3(Int32  cardno, Int32  axis1, Int32  axis2, Int32  axis3, Int32  pulse1, Int32  pulse2, Int32  pulse3, Int32  lspd ,Int32  hspd, double tacc, Int32  vacc, Int32  mode)  ;
				/************************************************************
				*功能:三轴直线插补相对运动,参照当前位置,以加速进行直线插补
				*参数:
				      cardno-卡号
					  axis1---轴号1
					  axis2---轴号2	
					  axis3---轴号3	
					  pulse1--脉冲1
					  pulse2--脉冲2
					  pulse3--脉冲3
					  lspd---低速
					  hspd---高速
				      tacc---加速时间(单位:秒)
					  vacc---加速度变化率
				      mode---模式（0:梯形，1:S曲线）
				返回值         0：正确          1：错误
				***************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_symmetry_absolute_line3(Int32  cardno, Int32  axis1, Int32  axis2, Int32  axis3, Int32  pulse1, Int32  pulse2, Int32  pulse3, Int32  lspd ,Int32  hspd, double tacc, Int32  vacc, Int32  mode)  ;
				/**************************************************************
				功能:三轴直线插补绝对运动，参照零点位置,以加速进行直线插补
				参数:
				      cardno-卡号
					  axis1---轴号1
					  axis2---轴号2	
					  axis3---轴号3
					  pulse1--脉冲1
					  pulse2--脉冲2
					  pulse3--脉冲3
					  lspd---低速
					  hspd---高速
				      tacc---加速时间(单位:秒)
					  vacc---加速度变化率
				      mode---模式（0:梯形，1:S曲线）
				返回值         0：正确          1：错误
				****************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_symmetry_relative_line4(Int32  cardno,Int32  axis1, Int32  axis2, Int32  axis3,Int32  axis4,Int32  pulse1, Int32  pulse2, Int32  pulse3,  Int32  pulse4,Int32  lspd ,Int32  hspd, double tacc, Int32  vacc, Int32  mode);  
				/****************************************************************
				*功能:四轴直线插补相对运动,参照当前位置,以加速进行直线插补
				*参数:
				      cardno-卡号	  
					  axis1---轴号1
					  axis2---轴号2	
					  axis3---轴号3
					  axis4---轴号4
					  pulse1--脉冲1
					  pulse2--脉冲2
					  pulse3--脉冲3
					  pulse4--脉冲4
					  lspd---低速
					  hspd---高速
				      tacc---加速时间(单位:秒)
					  vacc---加速度变化率
				      mode---模式（0:梯形，1:S曲线）
				****************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_symmetry_absolute_line4(Int32  cardno, Int32  axis1, Int32  axis2, Int32  axis3,Int32  axis4,Int32  pulse1, Int32  pulse2, Int32  pulse3, Int32  pulse4,Int32  lspd ,Int32  hspd, double tacc, Int32  vacc, Int32  mode);
				/***************************************************************
				*功能:四轴直线插补绝对运动,参照零点位置,以加减速进行直线插补
				*参数:
				      cardno-卡号	 
					  axis1---轴号1
					  axis2---轴号2	
					  axis3---轴号3
					  axis4---轴号4
					  pulse1--脉冲1
					  pulse2--脉冲2
					  pulse3--脉冲3
					  pulse4--脉冲4
					  lspd---低速
					  hspd---高速
				      tacc---加速时间(单位:秒)
					  vacc---加速度变化率
				      mode---模式（0:梯形，1:S曲线）
				****************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_symmetry_relative_line5(Int32  cardno,Int32  axis1, Int32  axis2, Int32  axis3,Int32  axis4,Int32  axis5,Int32  pulse1, Int32  pulse2, Int32  pulse3,  Int32  pulse4,Int32  pulse5,Int32  lspd ,Int32  hspd, double tacc, Int32  vacc, Int32  mode);  
				/*****************五轴直线插补相对运动****************
				*功能:参照当前位置,以加减速进行直线插补
				*参数:
				      cardno-卡号	  
					  axis1---轴号1
					  axis2---轴号2	
					  axis3---轴号3
					  axis4---轴号4
					  axis5---轴号5
					  pulse1--脉冲1
					  pulse2--脉冲2
					  pulse3--脉冲3
					  pulse4--脉冲4
					  pulse5--脉冲5
					  lspd---低速
					  hspd---高速
				      tacc---加速时间(单位:秒)
					  vacc---加速度变化率
				      mode---模式（0:梯形，1:S曲线）
				******************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_symmetry_absolute_line5(Int32  cardno, Int32  axis1, Int32  axis2, Int32  axis3,Int32  axis4,Int32  axis5,Int32  pulse1, Int32  pulse2, Int32  pulse3, Int32  pulse4,Int32  pulse5,Int32  lspd ,Int32  hspd, double tacc, Int32  vacc, Int32  mode);  
				/*****************五轴对称直线插补绝对运动****************
				*功能:参照零点位置,以对称加减速进行直线插补
				*参数:
				      cardno-卡号	 
					  axis1---轴号1
					  axis2---轴号2	
					  axis3---轴号3
					  axis4---轴号4
					  axis5---轴号5
					  pulse1--脉冲1
					  pulse2--脉冲2
					  pulse3--脉冲3
					  pulse4--脉冲4
					  pulse5--脉冲5
					  lspd---低速
					  hspd---高速
				      tacc---加速时间(单位:秒)
					  vacc---加速度变化率
				      mode---模式（0:梯形，1:S曲线）
				******************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_symmetry_relative_line6(Int32  cardno,Int32  pulse1, Int32  pulse2, Int32  pulse3,  Int32  pulse4,Int32  pulse5,  Int32  pulse6,Int32  lspd ,Int32  hspd, double tacc, Int32  vacc, Int32  mode);  
				/*****************六轴直线插补相对运动****************
				*功能:参照当前位置,以加减速进行直线插补
				*参数:
				      cardno-卡号	
					  pulse1--脉冲1
					  pulse2--脉冲2
					  pulse3--脉冲3
					  pulse4--脉冲4
					  pulse5--脉冲5
					  pulse6--脉冲6
					  lspd---低速
					  hspd---高速
				      tacc---加速时间(单位:秒)
					  vacc---加速度变化率
				      mode---模式（0:梯形，1:S曲线）
				******************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_symmetry_absolute_line6(Int32  cardno,Int32  pulse1, Int32  pulse2, Int32  pulse3, Int32  pulse4,Int32  pulse5, Int32  pulse6,Int32  lspd ,Int32  hspd, double tacc, Int32  vacc, Int32  mode) ; 
				/*****************六轴对称直线插补绝对运动****************
				*功能:参照零点位置,以对称加减速进行直线插补
				*参数:
				      cardno-卡号	 
					  pulse1--脉冲1
					  pulse2--脉冲2
					  pulse3--脉冲3
					  pulse4--脉冲4
					  pulse5--脉冲5
					  pulse6--脉冲6
					  lspd---低速
					  hspd---高速
				      tacc---加速时间(单位:秒)
					  vacc---加速度变化率
				      mode---模式（0:梯形，1:S曲线）
				******************************************************/
				
				//----------------------------------------------------//
				//                   2008.10.6                        //
				//----------------------------------------------------//
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_set_acac(Int32  cardno,Int32  axis,Int32  value);
				/*******************功能：加/减速度的变化率设定********************
				cardno	   卡号
				axis		轴号
				value	    K值（1-65535）
				实际变化率  1000000/k
				返回值	   0：正确				1：错误
				******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_set_ad_mode(Int32  cardno,Int32  axis,Int32  mode);
				/*********************功能：加/减速方式的设定**********************
				cardno	   卡号
				axis		轴号（1-4）
				mode	   0：直线加/减速			1：S曲线加/减速
				返回值	   0：正确					1：错误
				默认模式为 : 直线加/减速
				******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_get_ad(Int32  cardno,Int32  axis,out Int32  ad);
				/*******************功能：获取各轴的当前加速度*********************
				cardno	   卡号
				axis		轴号
				ad		   当前加速度的指针
				返回值		0：正确			1：错误
				数据的单位和驱动加速度设定数值A一样
				******************************************************************/
				
				//----------------------------------------------------//
				//                   外部信号驱动                     //
				//----------------------------------------------------//
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_manual_continue(Int32  cardno, Int32  axis);
				/*******************功能：外部信号连续驱动*********************
				cardno	   卡号
				axis		轴号
				返回值		0：正确			1：错误
				说明:(1)发出连续脉冲，但驱动没有立即进行，需要等到外部信号电平发生变化
					 (2)可以使用普通按钮,也可以接手轮
				******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_manual_pmove(Int32  cardno, Int32  axis, Int32  pos);
				/*******************功能：外部信号定量驱动*********************
				cardno	   卡号
				axis		轴号
				返回值		0：正确			1：错误
				说明:(1)发出定量脉冲，但驱动没有立即进行，需要等到外部信号电平发生变化
					 (2)可以使用普通按钮,也可以接手轮
				******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_manual_disable(Int32  cardno, Int32  axis);
				/***********************关闭外部信号驱动使能***********************
				功能:关闭外部信号驱动使能
				参数：
					cardno      卡号
					axis        轴号(1-6)
				返回值         0：正确          1：错误
				******************************************************************/
				
				//----------------------------------------------------//
				//                   位置锁存                        //
				//----------------------------------------------------//
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_set_lock_position(Int32  cardno, Int32  axis,Int32  mode,Int32  regi,Int32  logical);
				/****************************位置锁存设置函数**********************
				功能:设置到位信号功能,锁定所有轴的逻辑位置和实际位置
				参数:
					axis—参照轴
					mode—锁存模式    |0:无效
									  |1:有效
					regi—计数器模式  |0:逻辑位置
									  |1:实际位置 
					logical—电平信号 |0:上升沿 
								      |1:下降沿
				返回值         0：正确          1：错误
				说明:使用指定轴axis的IN信号作为触发信号						  
				*******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_get_lock_status(Int32  cardno, Int32  axis, out Int32  v);
				/*************************获取锁存状态***********************
				功能:获取锁存操作的状态
				参数:
					cardno      卡号
					axis         轴号(1-6)
					v           0|未执行锁存操作
							    1|执行过锁存操作
				返回值          0：正确					    1：错误
				说明:利用该函数可以捕捉位置锁存是否执行		
				******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_get_lock_position(Int32  cardno,Int32  axis,out Int32  pos);
				/**************************获取锁定的位置**************************
				功能:获取锁定的位置
				参数:
					cardno      卡号
					axis         轴号(1-6)
					pos         锁存的位置
				返回值         0：正确          1：错误
				******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_clr_lock_status(Int32  cardno, Int32  axis);
				/**************************清除锁定**************************
				功能:清除锁定
				参数:
					cardno      卡号
					axis         轴号(1-6)
				返回值         0：正确          1：错误
				******************************************************************/
				
				//----------------------------------------------------//
				//                   硬件缓存                        //
				//----------------------------------------------------//
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_fifo_inp_move1(Int32  cardno,Int32  axis1,Int32  pulse1,Int32  speed);
				/**************************单轴缓存**************************
				功能:单轴缓存
				参数:
					cardno      卡号
					axis1        轴号(1-6)
					pulse1       缓存的脉冲
					speed        缓存的速度
				返回值         0：正确          1：错误
				说明:共有2048个缓存空间，每条单轴缓存指令占用3个空间，可缓存682条指令
				******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_fifo_inp_move2(Int32  cardno,Int32  axis1,Int32  axis2,Int32  pulse1,Int32  pulse2,Int32  speed);
				/**************************两轴缓存**************************
				功能:两轴缓存
				参数:
					cardno      卡号
					axis1        轴号(1-6)
					axis2        轴号(1-6)
					pulse1       缓存的脉冲数
					pulse2       缓存的脉冲数
					speed        缓存的速度
				返回值         0：正确          1：错误
				说明:共有2048个缓存空间，每条两轴缓存指令占用4个空间，可缓存512条指令
				******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_fifo_inp_move3(Int32  cardno,Int32  axis1,Int32  axis2,Int32  axis3,Int32  pulse1,Int32  pulse2,Int32  pulse3,Int32  speed);
				/**************************三轴缓存**************************
				功能:三轴缓存
				参数:
					cardno      卡号
					axis1        轴号(1-6)
					axis2        轴号(1-6)
					axis3        轴号(1-6)
					pulse1       缓存的脉冲数
					pulse2       缓存的脉冲数
					pulse3       缓存的脉冲数
					speed        缓存的速度
				返回值         0：正确          1：错误
				说明:共有2048个缓存空间，每条三轴缓存指令占用5个空间，可缓存409条指令
				******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_fifo_inp_move4(Int32  cardno,Int32  axis1,Int32  axis2,Int32  axis3,Int32  axis4,Int32  pulse1,Int32  pulse2,Int32  pulse3,Int32  pulse4,Int32  speed);
				/**************************四轴缓存**************************
				功能:四轴缓存
				参数:
					cardno      卡号
					axis1        轴号(1-6)
					axis2        轴号(1-6)
					axis3        轴号(1-6)
					axis4        轴号(1-6)
					pulse1       缓存的脉冲数
					pulse2       缓存的脉冲数
					pulse3       缓存的脉冲数
					pulse4       缓存的脉冲数
					speed        缓存的速度
				返回值         0：正确          1：错误
				说明:共有2048个缓存空间，每条四轴缓存指令占用6个空间，可缓存341条指令
				******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_fifo_inp_move5(Int32  cardno,Int32  axis1,Int32  axis2,Int32  axis3,Int32  axis4,Int32  axis5,Int32  pulse1,Int32  pulse2,Int32  pulse3,Int32  pulse4,Int32  pulse5,Int32  speed);
				/**************************五轴缓存**************************
				功能:五轴缓存
				参数:
					cardno      卡号
					axis1        轴号(1-6)
					axis2        轴号(1-6)
					axis3        轴号(1-6)
					axis4        轴号(1-6)
					axis5        轴号(1-6)
					pulse1       缓存的脉冲数
					pulse2       缓存的脉冲数
					pulse3       缓存的脉冲数
					pulse4       缓存的脉冲数
					pulse5       缓存的脉冲数
					speed        缓存的速度
				返回值         0：正确          1：错误
				说明:共有2048个缓存空间，每条五轴缓存指令占用7个空间，可缓存292条指令
				******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_fifo_inp_move6(Int32  cardno,Int32  pulse1,Int32  pulse2,Int32  pulse3,Int32  pulse4,Int32  pulse5,Int32  pulse6,Int32  speed);
				/**************************六轴缓存**************************
				功能:六轴缓存
				参数:
					cardno      卡号
					pulse1       缓存的脉冲数
					pulse2       缓存的脉冲数
					pulse3       缓存的脉冲数
					pulse4       缓存的脉冲数
					pulse5       缓存的脉冲数
					pulse6       缓存的脉冲数
					speed        缓存的速度
				返回值         0：正确          1：错误
				说明:共有2048个缓存空间，每条六轴缓存指令占用8个空间，可缓存256条指令
				******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_reset_fifo(Int32  cardno);
				/**************************重设缓存**************************
				功能:清除缓存
				参数:
					cardno      卡号
				返回值         0：正确          1：错误
				******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_read_fifo_count(Int32  cardno,out Int32  value);
				/**************************读取缓存数**********************
				功能:读取缓存数，存放进去的指令还剩多少条未执行
				参数:
					cardno      卡号
				返回值         0：正确          1：错误
				******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_read_fifo_empty(Int32  cardno);
				/**************************读取缓存状态**********************
				功能:读取缓存是否为空
				参数:
					cardno      卡号
				返回值         0：非空          1：空
				******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_read_fifo_full(Int32  cardno);
				/**************************读取缓存状态**********************
				功能:读取缓存是否满了，满了之后将不能再存数据
				参数:
					cardno      卡号
				返回值         0：未满          1：满
				******************************************************************/
				
				//**************************************************************************//
				//        *************       手动减速功能      *************               //
				//   首先需要设置手动减速模式                                               //
				//   当手动减速模式有效时，设置手动减速点pos1                               //
				//   设置原点偏移量pos2                                                     //
				//   设置手动减速点后的速度(低速) endspeed                                  //
				//   手动减速运动过程：运动到减速点pos1后自动减速到endspeed运行             //
				//   以速度endspeed搜索原点信号，原点信号需要外部信号触发(stop0的低电平触发)//
				//   当原点信号触发后，运动原点偏移量pos2 后立即停止                        //
				//**************************************************************************//
				[DllImport("adt8960m.dll")]
				public static extern Int32 adt8960_set_dec_mode(Int32  cardno,Int32  axis,Int32  mode);
				/**************************设置手动减速模式**************************
				功能:设置手动减速模式
				参数:
				cardno       卡号
				axis         轴号(1-6)
				mode         手动减速模式 0 无效      1 有效
				返回值         0：正确          1：错误
				******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32 adt8960_set_dec_pos1(Int32  cardno, Int32  axis,Int32  pos);
				/**************************设置手动减速点**************************
				功能:设置手动减速点
				参数:
				cardno       卡号
				axis         轴号(1-6)
				pos          减速点
				返回值         0：正确          1：错误
				说明：到了该减速点后自动减速到指定的低速运动，寻找原点的触发信号，
				如果没有找到触发信号，将以此速度一直运动下去，直到运动结束
				******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32 adt8960_set_dec_pos2(Int32  cardno, Int32  axis,Int32  pos);
				/**************************指定手动减速偏移量**************************
				功能:原点的偏移量
				参数:
				cardno       卡号
				axis         轴号(1-6)
				pos          剩余位置
				返回值         0：正确          1：错误
				说明：运动该偏移量需要外部信号触发，触发信号为stop0的低电平
				******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32 adt8960_clr_dec_status(Int32  cardno, Int32  axis);
				/**************************清除手动减速状态**************************
				功能:清除手动减速状态
				参数:
				cardno       卡号
				axis         轴号(1-6)
				返回值         0：正确          1：错误
				******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32 adt8960_get_dec_status(Int32  cardno,Int32  axis,out Int32  sta);
				/**************************获取手动减速状态**************************
				功能:获取手动减速状态
				参数:
				cardno       卡号
				axis         轴号(1-6)
				sta          减速状态
				0:正在搜索
				1:搜索完毕
				2:运动已经停止，减速点没有发现
				3: 没有达到实际的偏移
				4:搜索减速点过程中伺服关闭
				5:减速点已经发现，在向offset运动时没有到位（可能是限位触发等）
				返回值         0：正确          1：错误
				******************************************************************/
				[DllImport("adt8960m.dll")]
				public static extern Int32 adt8960_set_end_speed(Int32  cardno, Int32  axis,Int32  speed);
				/****************功能：设定拖尾速度**************
				功能:设置手动减速点后，低速寻找原点信号速度
				参数:
				cardno	    卡号
				axis		轴号(1-6)
				speed       范围(1-2M)
				返回值	    0：正确				1：错误 
				说明：如果在手动减速点pos1之后没有找到触发信号(减速原点信号)，将以此速度一直运动下去
				************************************************/
				
				//*********************************************//
				//               回原点                        //
				//*********************************************//
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_SetHomeMode_Ex(Int32  m_nCardNum,Int32  m_nAxisNum,Int32  m_nHomeDir, Int32  m_nStop0Active,Int32  m_nLimitActive,Int32  m_nStop1Active,
											Int32  m_nBackRange,Int32  m_nEncoderZRange,Int32  m_nOffset);
				///////////////////////////////////////////////////
				//功能：设置回零信号，步骤参数
				//参数：
				// 	Int32     m_nCardNum		//卡号
				// 	Int32     m_nAxisNum		//轴号
				// 	Int32     m_nHomeDir		//回零方向 0:负方向 1:正方向
				// 	Int32     m_nStop0Active	//stop0 有效电平设置；0：低电平停止	1：高电平停止
				// 	Int32     m_nLimitActive	//limit信号 有效电平设置；0：低电平停止	1：高电平停止
				// 	Int32     m_nStop1Active	//stop1 有效电平设置；0：低电平停止	1：高电平停止
				// 	Int32    m_nBackRange		//反向距离 >1
				// 	Int32    m_nEncoderZRange	//编码器Z相范围 >1
				// 	Int32    m_nOffset		//原点偏移量；==0不偏移，>0正方向偏移，<0负方向偏移	
				//返回值		0：正确					-1至-8：错误类型
				//错误信息提示
				//  -1   //参数1错误
				//  -2   //参数2错误
				//  -3   //参数3错误
				//  -4   //参数4错误
				//  -5   //参数5错误
				//  -6   //参数6错误
				//  -7   //参数7错误
				//  -8   //参数8错误
				////////////////////////////////////////////////////
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_SetHomeSpeed_Ex(Int32  m_nCardNum,Int32  m_nAxisNum,Int32  m_nStartSpeed,Int32  m_nSearchSpeed,Int32  m_nHomeSpeed,Int32  m_nAcc,Int32  m_nZPhaseSpeed);
				///////////////////////////////////////////////////
				//功能：回零速度参数
				//参数：
				// 	Int32     m_nCardNum		//卡号
				// 	Int32     m_nAxisNum		//轴号
				// 	Int32    m_nStartSpeed	//原点(STOP0)搜寻起始速度
				// 	Int32    m_nSearchSpeed	//原点搜寻速度
				// 	Int32    m_nHomeSpeed		//低速接近原点速度
				// 	Int32    m_nAcc			//回原点过程中的加速度
				// 	Int32    m_nZPhaseSpeed	//编码器Z相(STOP1)搜寻速度
				//返回值		0：正确					-1至-7：错误类型
				//错误信息提示
				//  -1   //参数1错误
				//  -2   //参数2错误
				//  -3   //参数3错误
				//  -4   //参数4错误
				//  -5   //参数5错误
				//  -6   //参数6错误
				//  -7   //参数7错误
				///////////////////////////////////////////////////
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_HomeProcess_Ex(Int32  m_nCardNum,Int32  m_nAxisNum);
				///////////////////////////////////////////////////
				//功能：启动回零
				//参数：
				// 	Int32     m_nCardNum		//卡号
				// 	Int32     m_nAxisNum		//轴号
				//返回值		0：正确					1：错误
				//说明	调用该函数时启动回零动作
				////////////////////////////////////////////////////
				[DllImport("adt8960m.dll")]
				public static extern Int32  adt8960_GetHomeStatus_Ex(Int32  m_nCardNum,Int32  m_nAxisNum);
				///////////////////////////////////////////////////
				//功能：获取回零状态
				//参数：
				// 	Int32     m_nCardNum		//卡号
				// 	Int32     m_nAxisNum		//轴号
				//返回值 0:回零成功;-1:参数1错误;-2:参数2错误;-3:回零未启动;
				//(1-10)执行的步骤1 :快速接近原点，搜索STOP0
				//   			  2 :检查STOP0是否找到
				//			      3 :反向退出原点
				//			      4 :检查反向退出原点是否完成
				//			      5 :低速接近原点，搜索STOP0
				//			      6 :检查STOP0搜索是否完成
				//			  	  7 :低速接近Z相，搜索STOP1.如果STOP1设置为-1，则跳过7,8两步。
				//			  	  8 :检查STOP1搜索是否完成
				//			  	  9 :原点偏移
				//			 	 10 :检查原点偏移
				//		   	   -100x:回零第x步出现异常，例如-1001表示回零第1步出现异常
				//			   -1020:回零被终止
				////////////////////////////////////////////////////
    }
}
