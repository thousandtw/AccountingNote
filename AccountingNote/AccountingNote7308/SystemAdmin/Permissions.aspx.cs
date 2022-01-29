using AccountingNote.dbSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote7308.SystemAdmin
{
    public partial class Permissions : AdminPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var list = UserInfoManager.GetUserInfoList();
            this.gvList.DataSource = list;
            this.gvList.DataBind();
        }
    }
}
