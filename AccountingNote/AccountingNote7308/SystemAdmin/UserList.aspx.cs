using AccountingNote.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccountingNote.dbSource;
using System.Data;

namespace AccountingNote7308.SystemAdmin
{
    public partial class UserList : AdminPageBase
    {
        public override UserLevelEnum RequiredLevel { get; set; } = UserLevelEnum.Admin;

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

            string uid = currentUser.ID;
            var dt = UserInfoManager.GetUserInfoList_Order();

            if (dt.Rows.Count > 0)
            {
                var dtPaged = this.GetPagedDataTable(dt);

                this.GV_UserList.DataSource = dtPaged;
                this.GV_UserList.DataBind();

                this.ucPager_UserList.TotalSize = dt.Rows.Count;
                this.ucPager_UserList.Bind();
            }
            else
            {
                this.GV_UserList.Visible = false;
                this.plc_noUser.Visible = true;
            }
        }
        protected void addUserbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/UserDetail.aspx");
        }
        protected void GV_UserList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var row = e.Row;

            if (row.RowType == DataControlRowType.DataRow)
            {
                Literal ltl = row.FindControl("ltlLevel") as Literal;

                var dr = row.DataItem as DataRowView;
                int userlevel = dr.Row.Field<int>("UserLevel");

                if (userlevel == 0)
                    ltl.Text = "管理者";
                else
                    ltl.Text = "一般會員";
            }
        }
        private int GetcurrentPage()
        {
            string txtpage = Request.QueryString["Page"];

            if (string.IsNullOrWhiteSpace(txtpage))
                return 1;

            int intPage;
            if (!int.TryParse(txtpage, out intPage))
                return 1;

            if (intPage <= 0)
                return 1;

            return intPage;
        }
        private DataTable GetPagedDataTable(DataTable dt)
        {
            DataTable dtPaged = dt.Clone();

            int startindex = (this.GetcurrentPage() - 1) * 10;
            int endindex = (this.GetcurrentPage()) * 10;

            if (endindex > dt.Rows.Count)
                endindex = dt.Rows.Count;

            for (var i = startindex; i < endindex; i++)
            {
                DataRow dr = dt.Rows[i];
                var drNew = dtPaged.NewRow();

                foreach (DataColumn dc in dt.Columns)
                {
                    drNew[dc.ColumnName] = dr[dc];
                }

                dtPaged.Rows.Add(drNew);
            }
            return dtPaged;
        }

    }
}