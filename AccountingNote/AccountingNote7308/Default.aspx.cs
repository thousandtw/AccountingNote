using AccountingNote.dbSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote7308
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            
            var dt1 = AccountingManager.GetStartData();
            var dt2 = AccountingManager.GetEndData();
            var dt3 = AccountingManager.GetDataCount();
            var dt4 = UserInfoManager.GetUser();

            var datacount = dt3.Rows[0]["Amount"].ToString();
            var usercount = dt4.Rows[0]["Name"].ToString();

            this.firstdatalb.Text = dt1.Rows[0]["CreateDate"].ToString();
            this.enddatalb.Text = dt2.Rows[0]["CreateDate"].ToString();
            this.datacountlb.Text = $"共{datacount}筆";
            this.userlb.Text = $"共{usercount}人";
        }
    }
}