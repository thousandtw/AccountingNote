using AccountingNote.dbSource;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccountingNote;

namespace AccountingNote7308.SystemAdmin
{
    public partial class UserPassword : AdminPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ltlCheckInput.Text = string.Empty;
            this.ltlMsg.Text = string.Empty;
            this.ltlMsg2.Text = string.Empty;

            if (!IsPostBack)
            {
                if (this.Session["UserLoginInfo"] == null)
                {
                    Response.Redirect("/Login.aspx");
                    return;
                }

                string account = this.Session["UserLoginInfo"] as string;
                DataRow dr = UserInfoManager.GetUserInfobyAccount(account);

                if (dr == null)
                {
                    this.Session["UserLoginInfo"] = null;
                    Response.Redirect("/Login.aspx");
                    return;
                }

                this.ltAccount.Text = dr["Account"].ToString();
            }
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            string account = this.Session["UserLoginInfo"] as string;
            DataRow dr = UserInfoManager.GetUserInfobyAccount(account);

            string dbPWD = dr["PWD"].ToString();
            string iptPWD = txbPWD.Text;
            string iptNewPWD = txbNewPWD.Text;
            string iptNewPWD_Check = txbNewPWD_Check.Text;

            List<string> msgList = new List<string>();
            if (!CheckInput(out msgList))
            {
                this.ltlCheckInput.Text = string.Join("<br/>", msgList);
                return;
            }
            else
            {
                if (dbPWD == iptPWD)
                {
                    this.txbPWD.Text = dr["PWD"].ToString();
                }
                else
                {
                    this.ltlMsg.Text = "<span style='color:red'>原密碼輸入錯誤，請重新確認。</span>";
                    return;
                }

                if (iptNewPWD == iptNewPWD_Check)
                {
                    UserInfoManager.UpdatePWD(dr["Account"].ToString(), iptNewPWD_Check);
                    //this.Session["UserLoginInfo"] = null;
                    Response.Redirect("UserList.aspx");
                    return;
                }
                else
                {
                    this.ltlMsg2.Text = "<span style='color:red'>新密碼輸入不一致，請重新確認。</span>";
                    return;
                }
            }            
        }

        private bool CheckInput(out List<string> errorMsgList)
        {
            List<string> msgList = new List<string>();

            if (string.IsNullOrWhiteSpace(this.txbPWD.Text.Trim()) || string.IsNullOrEmpty(this.txbPWD.Text.Trim()))
            {
                msgList.Add("<span style='color:red'>請輸入原密碼。</span>");
            }
            if (string.IsNullOrWhiteSpace(this.txbNewPWD.Text.Trim()) || string.IsNullOrEmpty(this.txbNewPWD.Text.Trim()))
            {
                msgList.Add("<span style='color:red'>請輸入新密碼。</span>");
            }
            if (string.IsNullOrWhiteSpace(this.txbNewPWD_Check.Text.Trim()) || string.IsNullOrEmpty(this.txbNewPWD_Check.Text.Trim()))
            {
                msgList.Add("<span style='color:red'>請輸入確認密碼。</span>");
            }
            if(this.txbPWD.Text.Length < 8)
            {
                msgList.Add("<span style='color:red'>請確認原密碼長度 8 ~ 16 碼。</span>");
            }
            if (this.txbNewPWD.Text.Length < 8)
            {
                msgList.Add("<span style='color:red'>請確認新密碼長度 8 ~ 16 碼。</span>");
            }
            if (this.txbNewPWD_Check.Text.Length < 8)
            {
                msgList.Add("<span style='color:red'>請確認新密碼長度 8 ~ 16 碼。</span>");
            }
            errorMsgList = msgList;

            if (msgList.Count == 0)
                return true;
            else
                return false;
        }
    }
}