using AccountingNote.Auth;
using AccountingNote7308.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote7308.SystemAdmin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
                     //Page_Init比page_load更早
        protected void Page_Init(object sender, EventArgs e)
        {

            if (!AuthManager.Islogined())                             // 如果未登入，導至登入頁
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            var currentUser = AuthManager.GetCurrentUser();            // 如果帳號不存在，導至登入頁

            if (currentUser == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }
            this.ValidateLevelAndRole(currentUser);
        }

        /// <summary> 管理者 / 角色授權檢查 </summary>
        /// <param name="currentUser"></param>
        private void ValidateLevelAndRole(UserInfoModel currentUser)
        {
            if (!(this.Page is AdminPageBase))
            {
                return;
            }

            var adminPage = (AdminPageBase)this.Page;
            if (adminPage.RequiredLevel == UserLevelEnum.Regular)
            {
                // 如果是管理者，不做角色驗證
                if (currentUser.level == UserLevelEnum.Admin)
                    return;

                Guid CID = currentUser.ID.ToGuid();
                if (!AuthManager.IsGrant(CID, adminPage.RequiredRoles))
                {
                    Response.Redirect("UserInfo.aspx");
                    return;
                }
            }
        }
    }
}