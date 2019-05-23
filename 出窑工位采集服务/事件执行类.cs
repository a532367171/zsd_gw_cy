﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using 出窑工位采集服务.common;

namespace 出窑工位采集服务
{
   public class 事件执行类
    {
       public void WriteLog1(string old_value, string new_value, string middle_value)
       {
           try
           {
               #region 保旧值
               IniFileReference _iniFile = new IniFileReference(AppDomain.CurrentDomain.BaseDirectory + "Geometry.ini");
               _iniFile.IniWriteValue("SYSDNSection", "local_old_value", Convert.ToString(new_value));
               _iniFile.IniWriteValue("SYSDNSection", "local_middle_value", Convert.ToString(middle_value));
               _iniFile = null;
               #endregion

               #region 工位判断

               string Manufacture = ConfigurationManager.AppSettings["生产企业"];


               //string UUID = System.Guid.NewGuid().ToString("N");
               int i = 0;

               string strD="";

               if (old_value == "126" && new_value == "94")
               {
                   string str = "INSERT INTO CmdGongWei (cGWid,cGWlineCode,cManufacture) VALUES ('01','01','" + Manufacture + "')";
                   strD=str;
               }
               if (old_value == "93" && new_value == "125")
               {
                   string str = "INSERT INTO CmdGongWei (cGWid,cGWlineCode,cManufacture) VALUES ('13','01','" + Manufacture + "')";
                   strD = str;
               }
               if (old_value == "91" && new_value == "123")
               {
                   string str = "INSERT INTO CmdGongWei (cGWid,cGWlineCode,cManufacture) VALUES ('13','02','" + Manufacture + "')";
                   strD=str;
               }
               if (old_value == "87" && new_value == "119")
               {
                   string str = "INSERT INTO CmdGongWei (cGWid,cGWlineCode,cManufacture) VALUES ('13','03','" + Manufacture + "')";
                   strD=str;
               }
               if (old_value == "79" && new_value == "111")
               {
                   string str = "INSERT INTO CmdGongWei (cGWid,cGWlineCode,cManufacture) VALUES ('13','04','" + Manufacture + "')";
                   strD=str;
                  
               }
               try
               {
                   if (strD!="")
                   {
                       i = DbUtils.ExecuteNonQuerySp(strD);

                   }
               }
               catch (Exception ex)
               {
                   #region 错误日记
                   AppLog.WriteErr(ex.Message);
                   #endregion

               }
               finally 
               {
                   #region 变化日记
                   AppLog.Write(System.Convert.ToString(Convert.ToInt32(old_value), 2).PadLeft(7, '0') + "--" + System.Convert.ToString(Convert.ToInt32(new_value), 2).PadLeft(7, '0') + "--插入标识：" + Convert.ToString(i) + "--" + strD);              
                   #endregion     
               } 

               #endregion

                    
           }
           catch (Exception ex)
           {

               #region 错误日记
               AppLog.WriteErr(ex.Message);
               #endregion
           }

       

       }

    }
}
