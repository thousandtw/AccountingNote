using AccountingNote.Auth;
using AccountingNote.dbSource;
using AccountingNote.ORM.DBmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote7308.SystemAdmin
{
    public partial class UserAuth : AdminPageBase
    {
        public override UserLevelEnum RequiredLevel { get; set; } = UserLevelEnum.Admin;

        protected void Page_Load(object sender, EventArgs e)
        {
            var currentUser = AuthManager.GetCurrentUser();
            if (currentUser.level != UserLevelEnum.Admin)
            {
                Response.Redirect("UserInfo.aspx");
                return;
            }

            if (!this.IsPostBack)                           // 可能是按鈕跳回本頁，所以要判斷 postback
            {
                string userIDText = Request.QueryString["ID"];

                if (string.IsNullOrWhiteSpace(userIDText))
                    return;

                Guid userID = Guid.Parse(userIDText);
                var mUser = UserInfoManager.GetUserInfo(userID);

                if (mUser == null)                             // 如果帳號不存在，導至使用者管理
                {
                    Response.Redirect("UserList.aspx");
                    return;
                }

                this.ltAccount.Text = mUser.Account;


                this.ckbRoleList.DataSource = RoleManager.GetRoleList();
                this.ckbRoleList.DataBind();

                // 讀取現有的角色
                List<Role> list = RoleManager.GetUserRoleList(userID);
                this.rptRoleList.DataSource = list;
                this.rptRoleList.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> willSaveIDList = new List<string>();

            foreach (ListItem li in this.ckbRoleList.Items)
            {
                if (li.Selected)
                    willSaveIDList.Add(li.Value);
            }

            if (willSaveIDList.Count == 0)
                return;


            var roleList =
                willSaveIDList.Select(obj => Guid.Parse(obj)).ToArray();

            string userIDText = Request.QueryString["ID"];
            Guid userID = Guid.Parse(userIDText);

            AccountingNote.Auth.AuthManager.MapUserAndRole(userID, roleList);
        }

        protected void rptRoleList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
               e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string userIDText = Request.QueryString["ID"];

                if (string.IsNullOrWhiteSpace(userIDText))
                    return;

                Guid userID = Guid.Parse(userIDText);
                var mUser = UserInfoManager.GetUserInfo(userID);

                if (mUser == null)                             // 如果帳號不存在，導至使用者管理
                {
                    Response.Redirect("UserList.aspx");
                    return;
                }

                if (e.CommandName == "DeleteRole")
                {
                    string roleIDText = e.CommandArgument as string;
                    Guid roleID = Guid.Parse(roleIDText);

                    RoleManager.DeleteUserRole(userID, roleID);
                    Response.Redirect(Request.RawUrl);
                }
            }
        }
    }
}