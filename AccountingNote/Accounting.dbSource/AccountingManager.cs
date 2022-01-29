using AccountingNote.dbSource;
using AccountingNote.ORM.DBmodel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AccountingNote.dbSource
{
    public class AccountingManager
    {
        public static DataTable GetStartData()
        {
            string ConnStr = dbHelper.Getconnectionstring();
            string dbCommand =
                @"SELECT top 1 * 
                  FROM Accounting
                  order by CreateDate asc
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
        public static DataTable GetEndData()
        {
            string ConnStr = dbHelper.Getconnectionstring();
            string dbCommand =
                @"SELECT top 1 * 
                  FROM Accounting
                  order by CreateDate desc
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
        public static DataTable GetDataCount()
        {
            string ConnStr = dbHelper.Getconnectionstring();
            string dbCommand =
                @"SELECT COUNT(Amount) as Amount
                  FROM Accounting
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
        public static DataTable GetIncome(string userid)
        {
            string ConnStr = dbHelper.Getconnectionstring();
            string dbCommand =
                @"SELECT SUM (Amount) AS 'AT'
                  FROM  [Accounting]
                  WHERE [ActType] = 1
                  AND   [UserID] = @userid
                 ";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@userid", userid));

            try
            {
                return dbHelper.ReadDataTable(ConnStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
                return null;
            }
        }
        public static DataTable GetExpenses(string userid)
        {
            string ConnStr = dbHelper.Getconnectionstring();
            string dbCommand =
                @"SELECT SUM (Amount) AS 'ATS'
                  FROM  [Accounting]
                  WHERE [ActType] = 0
                  AND   [UserID] = @userid
                 ";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@userid", userid));

            try
            {
                return dbHelper.ReadDataTable(ConnStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
                return null;
            }
        }


        public static void CreateAccounting(string userID, string caption, int amount, int actType, string body)
        {
            //值的驗證
            if (amount < 0 || amount > 1000000)
                throw new ArgumentException("Amount must between 0 and 1,000,000 .");

            if (actType < 0 || actType > 1)
                throw new ArgumentException("actType must be 0 or 1.");

            string bodyColumnSQL = "";
            string bodyValueSQL = "";
            if (!string.IsNullOrWhiteSpace(body))                          //驗證body
            {
                bodyColumnSQL = ", Body";
                bodyValueSQL = ", @Body";
            }

            string connStr = dbHelper.Getconnectionstring();               //新增body
            string dbCommand =
            $@" INSERT INTO [dbo].[Accounting]
                (
               UserID
              ,Caption
              ,Amount
              ,ActType
              ,CreateDate
              {bodyColumnSQL}                                         
              )
              VALUES 
              (
              @userID
             ,@caption
             ,@amount
             ,@actType
             ,@createDate
              {bodyValueSQL}
               ) ";

            // connect db & execute
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))
                {
                    List<SqlParameter> paralist = new List<SqlParameter>();
                    paralist.Add(new SqlParameter("@userID", userID));
                    paralist.Add(new SqlParameter("@caption", caption));
                    paralist.Add(new SqlParameter("@amount", amount));
                    paralist.Add(new SqlParameter("@actType", actType));
                    paralist.Add(new SqlParameter("@createDate", DateTime.Now));

                    if (!string.IsNullOrWhiteSpace(body))
                        paralist.Add(new SqlParameter("@body", body));
                    dbHelper.CreateData(connStr, dbCommand, paralist);

                    comm.Parameters.AddWithValue("@userID", userID);
                    comm.Parameters.AddWithValue("@caption", caption);
                    comm.Parameters.AddWithValue("@amount", amount);
                    comm.Parameters.AddWithValue("@actType", actType);
                    comm.Parameters.AddWithValue("@createDate", DateTime.Now);

                    if (!string.IsNullOrWhiteSpace(body))
                        comm.Parameters.AddWithValue("@body", body);

                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        Logger.Writelog(ex);
                    }

                }
            }

        }

        //public static DataTable GetAccountingList(string userID)
        //{
        //    string ConnStr = dbHelper.Getconnectionstring();
        //    string dbCommand =
        //        @"SELECT 
        //             ID, 
        //             Caption,
        //             Amount,
        //             ActType,
        //             CreateDate
        //          FROM Accounting
        //          WHERE  [UserID] = @userid
        //          ORDER BY CreateDate DESC
        //         ";

        //    List<SqlParameter> list = new List<SqlParameter>();
        //    list.Add(new SqlParameter("@userID", userID));


        //    using (SqlConnection conn = new SqlConnection(ConnStr))
        //    {
        //        using (SqlCommand comm = new SqlCommand(dbCommand, conn))
        //        {
        //            try
        //            {
        //                return dbHelper.ReadDataTable(ConnStr, dbCommand, list);
        //            }
        //            catch (Exception ex)
        //            {
        //                Logger.Writelog(ex);
        //                return null;
        //            }
        //        }

        //    }
        //}



        //public static DataRow GetAccounting(int id, string userID)
        //{

        //    string ConnStr = dbHelper.Getconnectionstring();
        //    string dbCommand =
        //        $@"SELECT 
        //             ID, 
        //             Caption,
        //             Amount,
        //             ActType,
        //             CreateDate,
        //             Body
        //           FROM Accounting
        //           WHERE id = @id AND UserID = @userID";

        //    using (SqlConnection conn = new SqlConnection(ConnStr))
        //    {
        //        using (SqlCommand comm = new SqlCommand(dbCommand, conn))
        //        {
        //            comm.Parameters.AddWithValue("@id", id);
        //            comm.Parameters.AddWithValue("@userID", userID);

        //            try
        //            {
        //                conn.Open();
        //                var reader = comm.ExecuteReader();

        //                DataTable dt = new DataTable();
        //                dt.Load(reader);

        //                if (dt.Rows.Count == 0)
        //                    return null;
        //                return dt.Rows[0];
        //            }
        //            catch (Exception ex)
        //            {
        //                Logger.Writelog(ex);
        //                return null;

        //            }
        //        }

        //    }

        //}








        //public static bool UpdaateAccounting(int ID, string userID, string caption, int amount, int actType, string body)
        //{

        //    if (amount < 0 || amount > 1000000)
        //        throw new ArgumentException("Amount must between 0 and 1,000,000 .");

        //    if (actType < 0 || actType > 1)
        //        throw new ArgumentException("ActType must be 0 or 1.");

        //    string ConnStr = dbHelper.Getconnectionstring();
        //    string dbCommand =
        //        $@" UPDATE [Accounting]
        //           SET
        //              UserID     =@userID
        //             ,Caption    =@caption
        //             ,Amount     =@amount
        //             ,ActType    =@actType
        //             ,CreateDate =createDate
        //             ,Body       =@body
        //           WHERE
        //               ID=@id";


        //    using (SqlConnection conn = new SqlConnection(ConnStr))
        //    {
        //        using (SqlCommand comm = new SqlCommand(dbCommand, conn))
        //        {
        //            comm.Parameters.AddWithValue("@userID", userID);
        //            comm.Parameters.AddWithValue("@caption", caption);
        //            comm.Parameters.AddWithValue("@amount", amount);
        //            comm.Parameters.AddWithValue("@actType", actType);
        //            comm.Parameters.AddWithValue("@createDate", DateTime.Now);
        //            comm.Parameters.AddWithValue("@body", body);
        //            comm.Parameters.AddWithValue("@id", ID);

        //            try
        //            {
        //                conn.Open();
        //                int effectRows = comm.ExecuteNonQuery();
        //                //異動資料
        //                if (effectRows == 1)
        //                    return true;
        //                else
        //                    return false;
        //            }
        //            catch (Exception ex)
        //            {
        //                Logger.Writelog(ex);
        //                return false;
        //            }
        //        }

        //    }

        //}



        //public static void DeleteAccounting(int ID)
        //{
        //    string ConnStr = dbHelper.Getconnectionstring();
        //    string dbCommand =
        //        $@" DELETE [Accounting]

        //           WHERE ID= @id";



        //    using (SqlConnection conn = new SqlConnection(ConnStr))
        //    {
        //        using (SqlCommand comm = new SqlCommand(dbCommand, conn))
        //        {

        //            comm.Parameters.AddWithValue("@id", ID);

        //            try
        //            {
        //                conn.Open();
        //                comm.ExecuteNonQuery();
        //            }
        //            catch (Exception ex)
        //            {
        //                Logger.Writelog(ex);
        //            }
        //        }
        //    }
        //}


        //public static void CreateAccounting(string userID, string caption, int amount, int actType, string body)
        //{
        //    //值的驗證
        //    if (amount < 0 || amount > 1000000)
        //        throw new ArgumentException("Amount must between 0 and 1,000,000 .");

        //    if (actType < 0 || actType > 1)
        //        throw new ArgumentException("actType must be 0 or 1.");

        //    string bodyColumnSQL = ""; 
        //    string bodyValueSQL = "";
        //    if (!string.IsNullOrWhiteSpace(body))                          //驗證body
        //    {
        //        bodyColumnSQL = ", Body";
        //        bodyValueSQL = ", @Body";
        //    }

        //    string connStr = dbHelper.Getconnectionstring();               //新增body
        //    string dbCommand =
        //    $@" INSERT INTO [dbo].[Accounting]
        //        (
        //       UserID
        //      ,Caption
        //      ,Amount
        //      ,ActType
        //      ,CreateDate
        //      {bodyColumnSQL}                                         
        //      )
        //      VALUES 
        //      (
        //      @userID
        //     ,@caption
        //     ,@amount
        //     ,@actType
        //     ,@createDate
        //      {bodyValueSQL}
        //       ) ";

        //    // connect db & execute
        //    using (SqlConnection conn = new SqlConnection(connStr))
        //    {
        //        using (SqlCommand comm = new SqlCommand(dbCommand, conn))
        //        {
        //            List<SqlParameter> paralist = new List<SqlParameter>();
        //            paralist.Add(new SqlParameter("@userID", userID));
        //            paralist.Add(new SqlParameter("@caption", caption));
        //            paralist.Add(new SqlParameter("@amount", amount));
        //            paralist.Add(new SqlParameter("@actType", actType));
        //            paralist.Add(new SqlParameter("@createDate", DateTime.Now));

        //            if (!string.IsNullOrWhiteSpace(body))
        //            paralist.Add(new SqlParameter("@body", body));
        //            dbHelper.CreateData(connStr, dbCommand, paralist);

        //            //comm.Parameters.AddWithValue("@userID", userID);
        //            //comm.Parameters.AddWithValue("@caption", caption);
        //            //comm.Parameters.AddWithValue("@amount", amount);
        //            //comm.Parameters.AddWithValue("@actType", actType);
        //            //comm.Parameters.AddWithValue("@createDate", DateTime.Now);

        //            //if (!string.IsNullOrWhiteSpace(body))
        //            //    comm.Parameters.AddWithValue("@body", body);

        //            //try
        //            //{
        //            //    conn.Open();
        //            //    comm.ExecuteNonQuery();

        //            //}
        //            //catch (Exception ex)
        //            //{
        //            //    Logger.WriteLog(ex);
        //            //}

        //        }
        //    }

        //}


        public static List<Accounting> GetAccountingList(Guid userID)
        {
            try
            {
                using(ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Accountings
                         where item.UserID == userID
                         select item);
                    var list = query.ToList();
                    return list;
                }
            }
            catch(Exception ex)
            {
                Logger.Writelog(ex);
                return null;
            }
        }

        public static ORM.DBmodel.Accounting GetAccounting(int id, Guid userID)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Accountings
                         where item.ID == id && item.UserID ==userID
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

        public static void CreateAccounting(Accounting accounting)
        {
            if (accounting.Amount< 0 || accounting.Amount > 1000000)
                throw new ArgumentException("Amount must between 0 and 1,000,000 .");

            if (accounting.ActType< 0 || accounting.ActType > 1)
                throw new ArgumentException("actType must be 0 or 1.");

            try
            {
                using (ContextModel context = new ContextModel())
                {
                    accounting.CreateDate = DateTime.Now;
                    context.Accountings.Add(accounting);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
               
            }
        }

        public static bool UpdaateAccounting(Accounting accounting)
        {
            if (accounting.Amount < 0 || accounting.Amount > 1000000)
                throw new ArgumentException("Amount must between 0 and 1,000,000 .");

            if (accounting.ActType < 0 || accounting.ActType > 1)
                throw new ArgumentException("actType must be 0 or 1.");

            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var dbObject =
                    context.Accountings.Where(obj => obj.ID == accounting.ID).FirstOrDefault();

                    if(dbObject != null)
                    {
                        dbObject.Caption = accounting.Caption;
                        dbObject.Body = accounting.Body;
                        dbObject.Amount = accounting.Amount;
                        dbObject.ActType = accounting.ActType;
                        dbObject.CoverImage = accounting.CoverImage;
                    }
                   
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

        public static void DeleteAccounting(int ID)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var dbObject =
                    context.Accountings.Where(obj => obj.ID == ID).FirstOrDefault();

                    if (dbObject != null)
                    {
                        context.Accountings.Remove(dbObject);
                        context.SaveChanges();
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);
              
            }
        }

    }
}
