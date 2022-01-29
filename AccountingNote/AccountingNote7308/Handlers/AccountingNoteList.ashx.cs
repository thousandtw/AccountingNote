using AccountingNote.dbSource;
using AccountingNote.ORM.DBmodel;
using AccountingNote7308.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AccountingNote7308.Handlers                                //API:兩個程式互動介面
{
    /// <summary>
    /// AccountingNoteList 的摘要描述
    /// </summary>
    public class AccountingNoteList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string account = context.Request.QueryString["Account"];

            if (string.IsNullOrWhiteSpace(account))
            {
                context.Response.StatusCode = 404;
                context.Response.End();
                return;
            }

            var dr = UserInfoManager.GetUserInfobyAccount(account);

            if (dr==null)
            {
                context.Response.StatusCode = 404;
                context.Response.End();
                return;
            }

            string userID = dr["ID"].ToString();
            Guid userGUID = userID.ToGuid();
            List<Accounting> sourceList = AccountingManager.GetAccountingList(userGUID);

            //DataTable dataTable = AccountingManager.GetAccountingList(userID);

            //List<AccountingNoteViewModel> list = new List<AccountingNoteViewModel>();
            //foreach (DataRow drAccounting in dataTable.Rows)
            //{
            //    AccountingNoteViewModel model = new AccountingNoteViewModel()
            //    {
            //        ID = drAccounting["ID"].ToString(),
            //        Caption = drAccounting["Caption"].ToString(),
            //        Amount = drAccounting.Field<int>("Amount"),
            //        ActType = (drAccounting.Field<int>("ActType") == 0) ? "支出" : "收入",
            //        CreateDate = drAccounting.Field<DateTime>("CreateDate").ToString("yyyy-MM-dd")
            //    };
            //    list.Add(model);
            //}

            List<AccountingNoteViewModel> list = 
             sourceList.Select(obj => new AccountingNoteViewModel()
            {
                ID = obj.ID,
                Caption = obj.Caption,
                Amount = obj.Amount,
                ActType = (obj.ActType== 0) ? "支出" : "收入",
                CreateDate = obj.CreateDate.ToString("yyyy-MM-dd")
            }).ToList();

            string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(list);

            context.Response.ContentType = "application/json";
            context.Response.Write(jsonText);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}