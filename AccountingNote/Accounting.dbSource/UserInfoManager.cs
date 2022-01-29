using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using AccountingNote.ORM.DBmodel;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace AccountingNote.dbSource
{
    public class UserInfoManager
    {
        public static List<UserInfo> GetUserInfoList()
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query = context.UserInfoes;
                    var list = query.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
                return null;
            }
        }

        public static UserInfo GetUserInfo (Guid id)
        {
            try 
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                      (from item in context.UserInfoes
                       where item.ID == id
                       select item);

                    var obj = query.FirstOrDefault();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
                return null;
            }
        }

        public static void SendAutomatedEmail(string elb)
        {
            MailMessage mail = new MailMessage();
            //前面是發信email後面是顯示的名稱
            mail.From = new MailAddress("thousandsones@gmail.com", "1000");

            //收信者email
            mail.To.Add(elb);

            //設定優先權
            mail.Priority = MailPriority.Normal;

            //標題
            mail.Subject = "TakeAwalk火車訂票系統-忘記密碼確認信";

            //內容
            mail.Body = "認證碼:9267434351";

            //內容使用html
            //mail.IsBodyHtml = true;

            //設定gmail的smtp (這是google的)
            SmtpClient MySmtp = new SmtpClient("smtp.gmail.com", 587);

            //您在gmail的帳號密碼
            MySmtp.Credentials = new System.Net.NetworkCredential("thousandsones@gmail.com", "h0916916145");

            //開啟ssl
            MySmtp.EnableSsl = true;

            //發送郵件
            MySmtp.Send(mail);

            //放掉宣告出來的MySmtp
            MySmtp = null;

            //放掉宣告出來的mail
            mail.Dispose();
        }




        public static DataRow GetUserInfobyAccount(string account)
        {
            string connectionstring = dbHelper.Getconnectionstring();
            string dbCommandstring = @"SELECT [ID], [Account], [PWD],
                                              [Name], [Email], [UserLevel],
                                              [CreateDate]
                                       FROM   [UserInfo]
                                       WHERE  [Account] = @account";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@account", account));

            try
            {
                return dbHelper.ReadDataRow(connectionstring, dbCommandstring, list);
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
                return null;
            }
        }

        public static UserInfo GetUserInfobyAccount_ORM(string account)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.UserInfoes
                         where item.Account == account
                         select item);

                    var obj = query.FirstOrDefault();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
                return null;
            }
        }

        public static bool trySearch(string account, string email, out string errorMsg)
        {
            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(email))
            {
                errorMsg = "請輸入帳號和信箱。";
                return false;
            }

            var userInfo = GetUserInfobyAccount_ORM(account);

            if (userInfo == null)
            {
                errorMsg = "帳號輸入錯誤。";
                return false;
            }
            if (string.Compare(userInfo.Account, account) == 0 &&
                string.Compare(userInfo.Email, email) == 0)
            {
                HttpContext.Current.Session["UserLoginInfo"] = userInfo.Account;
                errorMsg = string.Empty;
                return true;
            }
            else
            {
                errorMsg = "請重新確認帳號與信箱。";
                return false;
            }

        }

        public static DataRow GetUserInfobyUID(string uid)
        {
            string connectionstring = dbHelper.Getconnectionstring();
            string dbCommandstring = @"SELECT [ID], [Account], [PWD],
                                              [Name], [Email], [UserLevel],
                                              [CreateDate]
                                       FROM   [UserInfo]
                                       WHERE  [ID] = @uid";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@uid", uid));

            try
            {
                return dbHelper.ReadDataRow(connectionstring, dbCommandstring, list);
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
                return null;
            }
        }
        public static DataTable GetUserInfoList_Order()
        {
            string connectionstring = dbHelper.Getconnectionstring();
            string dbCommandstring = @"SELECT [Account], [Name], [Email], [UserLevel],
                                              [CreateDate], [ID]
                                       FROM   [UserInfo]
                                       ORDER BY [CreateDate] DESC";

            List<SqlParameter> list = new List<SqlParameter>();

            try
            {
                return dbHelper.ReadDataTable(connectionstring, dbCommandstring, list);
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
                return null;
            }
        }
        public static DataTable GetAllAccount()
        {
            string connectionstring = dbHelper.Getconnectionstring();
            string dbCommandstring = @"SELECT [Account] FROM [UserInfo]";

            try
            {
                return dbHelper.GetDataTable(connectionstring, dbCommandstring);
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
                return null;
            }
        }
        public static void DeleteUser(string uid)
        {
            string connectionstring = dbHelper.Getconnectionstring();
            string dbCommandstring = @"DELETE FROM [UserInfo]
                                       WHERE       [ID] = @uid";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@uid", uid));

            try
            {
                int effectRowsCnt = dbHelper.ModifyData(connectionstring, dbCommandstring, list);
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
            }
        }
        public static void CreateUser(string account, string name, string email, int userlevel, string pwd)
        {
            if (userlevel != 0 && userlevel != 1)
                throw new ArgumentException("必須是管理者或一般會員.");

            string connectionstring = dbHelper.Getconnectionstring();
            string dbCommandstring = @"INSERT INTO [UserInfo]
                                                  ([Account], [Name], [PWD],
                                                   [Email], [UserLevel], [CreateDate])
                                       VALUES     (@account, @name, @pwd,
                                                   @email, @userlevel, @date)";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@Account", account));
            list.Add(new SqlParameter("@name", name));
            list.Add(new SqlParameter("@email", email));
            list.Add(new SqlParameter("@userlevel", userlevel));
            list.Add(new SqlParameter("@date", DateTime.Now));
            list.Add(new SqlParameter("@pwd", pwd));

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(dbCommandstring, connection))
                {
                    try
                    {
                        dbHelper.CreateData(connectionstring, dbCommandstring, list);
                    }
                    catch (Exception ex)
                    {
                        Logger.Writelog(ex);
                    }
                }
            }
        }
        public static bool UpdateUser(string uid, string name, string email)
        {
            string connectionstring = dbHelper.Getconnectionstring();
            string dbCommandstring = @"UPDATE [UserInfo]
                                       SET    [Name] = @name,
                                              [Email] = @email,
                                              [CreateDate] = @date
                                       WHERE  [ID] = @uid";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@uid", uid));
            list.Add(new SqlParameter("@name", name));
            list.Add(new SqlParameter("@email", email));
            list.Add(new SqlParameter("@date", DateTime.Now));

            try
            {
                int effectRowsCnt = dbHelper.ModifyData(connectionstring, dbCommandstring, list);

                if (effectRowsCnt == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
                return false;
            }
        }
        public static bool UpdatePWD(string Account, string PWD)
        {
            string connStr = dbHelper.Getconnectionstring();
            string dbCommand =
                $@"UPDATE [UserInfo]
                   SET
                      PWD = @pwd
                  WHERE
                      Account = @account
                ";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))
                {
                    comm.Parameters.AddWithValue("@account", Account);
                    comm.Parameters.AddWithValue("@pwd", PWD);

                    try
                    {
                        conn.Open();
                        int effectRows = comm.ExecuteNonQuery();

                        if (effectRows == 1)
                            return true;
                        else
                            return false;
                    }
                    catch (Exception ex)
                    {
                        Logger.Writelog(ex);
                        return false;
                    }
                }
            }
        }
        public static bool UpdatePWDS(Guid ID, string PWD)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query = (from item in context.UserInfoes
                                 where item.ID == ID
                                 select item).FirstOrDefault();
                    query.PWD = PWD;
                    context.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
                return false;
            }
        }
        public static List<string> GetAllAccountList()
        {
            DataTable dt = GetAllAccount();

            List<string> acclist = new List<string>();

            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    acclist.Add(dr[dc].ToString());
                }
            }
            return acclist;
        }
        public static DataTable GetUser()
        {
            string ConnStr = dbHelper.Getconnectionstring();
            string dbCommand =
                @"SELECT COUNT(Name) as Name
                  FROM UserInfo
                 ";

            try
            {
                return dbHelper.GetDataTable(ConnStr, dbCommand);
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
                return null;
            }
        }

    }
}
