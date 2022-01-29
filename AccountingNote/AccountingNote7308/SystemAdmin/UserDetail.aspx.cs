using AccountingNote.dbSource;
using AccountingNote.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote7308.SystemAdmin
{
    public partial class UserDetail : AdminPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthManager.Islogined())
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            var currentUser = AuthManager.GetCurrentUser();

            if (currentUser == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            if (!this.IsPostBack)
            {
                if (this.Request.QueryString["UID"] == null)
                {
                    this.Deletebtn.Visible = false;
                    this.pwdbtn.Visible = false;
                    this.pwdLabel.Text =
                        "預設密碼為12345，帳號建立後請變更密碼";
                }
                else
                {
                    this.Deletebtn.Visible = true;
                    string uid = this.Request.QueryString["UID"];

                    var drAccounting = UserInfoManager.GetUserInfobyUID(uid);

                    if (drAccounting == null)
                    {
                        this.LitMsg.Text = "使用者不存在";
                        this.Savebtn.Visible = false;
                        this.Deletebtn.Visible = false;
                    }
                    else
                    {
                        this.acctxtbox.Text = drAccounting["Account"].ToString();
                        this.acctxtbox.Enabled = false;
                        this.nametxtbox.Text = drAccounting["Name"].ToString();
                        this.emailtxtbox.Text = drAccounting["Email"].ToString();
                        this.ddl_Level.Text = drAccounting["UserLevel"].ToString();
                        this.ddl_Level.Enabled = false;
                        this.timeLabel.Text = drAccounting["CreateDate"].ToString();
                    }
                }
            }
        }
        protected void Deletebtn_Click(object sender, EventArgs e)
        {
            var currentUser = AuthManager.GetCurrentUser();

            string uidtxt = this.Request.QueryString["UID"];
            string uid = currentUser.ID;

            if (string.IsNullOrWhiteSpace(uid))
            { return; }
            else
            {
                if (string.Compare(uid, uidtxt, true) == 0)
                {
                    UserInfoManager.DeleteUser(uid);
                    this.Session.Clear();
                    Response.Redirect("/Login.aspx");
                }
                else
                {
                    this.LitMsg.Visible = true;
                    this.LitMsg.Text = "<span style='color:red'>只能修改現在登入的帳號</span>";
                    return;
                }
            }

        }
        protected void Savebtn_Click(object sender, EventArgs e)
        {
            List<string> msgList = new List<string>();
            if (!CheckInput(out msgList))
            {
                this.LitMsg.Text = string.Join("<br/>", msgList);
                return;
            }
            else
            { this.LitMsg.Visible = false; }

            var currentUser = AuthManager.GetCurrentUser();

            string uid = currentUser.ID;
            string txtUserLevel = this.ddl_Level.SelectedValue;
            string acctxt = this.acctxtbox.Text;
            string nametxt = this.nametxtbox.Text;
            string emailtxt = this.emailtxtbox.Text;

            int userlevel = Convert.ToInt32(txtUserLevel);

            string uidtxt = this.Request.QueryString["UID"];

            if (string.IsNullOrWhiteSpace(uidtxt))
            {
                List<string> allUser = new List<string>();
                allUser = UserInfoManager.GetAllAccountList();

                foreach (string listuser in allUser)
                {
                    if (string.Compare(listuser, acctxt, true) == 0)
                    {
                        this.LitMsg.Visible = true;
                        this.LitMsg.Text = "<span style='color:red'>帳號已存在</span>";
                        return;
                    }
                }
                string pwd = "12345";
                UserInfoManager.CreateUser(acctxt, nametxt, emailtxt, userlevel, pwd);
            }
            else
            {
                if (string.Compare(uid, uidtxt, true) == 0)
                {
                    UserInfoManager.UpdateUser(uidtxt, nametxt, emailtxt);
                }
                else
                {
                    this.LitMsg.Visible = true;
                    this.LitMsg.Text = "<span style='color:red'>只能修改現在登入的帳號</span>";
                    return;
                }
            }

            Response.Redirect("/SystemAdmin/UserList.aspx");
        }
        private bool CheckInput(out List<string> errorMsgList)
        {
            List<string> msgList = new List<string>();

            if (this.ddl_Level.SelectedValue != "0" &&
                this.ddl_Level.SelectedValue != "1")
            {
                msgList.Add("<span style='color:red'>必須是管理者或一般會員</span>");
            }
            if (string.IsNullOrWhiteSpace(this.acctxtbox.Text))
            {
                msgList.Add("<span style='color:red'>必須是帳號名稱</span>");
            }
            if (string.IsNullOrWhiteSpace(this.nametxtbox.Text))
            {
                msgList.Add("<span style='color:red'>必須是名稱</span>");
            }
            if (string.IsNullOrWhiteSpace(this.emailtxtbox.Text))
            {
                msgList.Add("<span style='color:red'>必須填入Email</span>");
            }
            errorMsgList = msgList;

            if (msgList.Count == 0)
                return true;
            else
                return false;
        }
        protected void returnbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/UserList.aspx");
        }
        protected void pwdbtn_Click(object sender, EventArgs e)
        {
            var currentUser = AuthManager.GetCurrentUser();

            string uid = currentUser.ID;

            Response.Redirect($"UserPassword.aspx?UID={uid}");
        }
    }
}