using AccountingNote.dbSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingNote7308.Handlers
{
    /// <summary>
    /// CreateAccountingNote 的摘要描述
    /// </summary>
    /// 
    public class CreateAccountingNote : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.HttpMethod != "POST")
            {
                this.ProcessError(context, "POST ONLY.");
                return;
            }

            //取得使用者輸入值
            string caption = context.Request.Form["Caption"];
            string amountText = context.Request.Form["Amount"];
            string actTypeText = context.Request.Form["ActType"];
            string body = context.Request.Form["Body"];

            //資料庫id
            string id = "3e2f9779-ad5d-4369-b9ec-ec96f013417c";

            //必填檢查
            if (string.IsNullOrWhiteSpace(caption) || string.IsNullOrWhiteSpace(amountText) || string.IsNullOrWhiteSpace(actTypeText))
            {
                this.ProcessError(context, "caption, amount, actType is required.");
                return;
            }

            //轉型
            int tempAmount, tempActType;
            if (!int.TryParse(amountText, out tempAmount) || !int.TryParse(actTypeText, out tempActType))
            {
                this.ProcessError(context, "Amount, ActType should be a integer");
                return;
            }

            //建立流水帳
            AccountingManager.CreateAccounting(id, caption, tempAmount, tempActType, body);

            context.Response.ContentType = "text/plain";
            context.Response.Write("ok");

        }

        private void ProcessError(HttpContext context, string msg)
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "text/plain";
            context.Response.Write(msg);
            context.Response.End();
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