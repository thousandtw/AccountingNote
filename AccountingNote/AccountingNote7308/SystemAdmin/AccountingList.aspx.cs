using AccountingNote.dbSource;
using AccountingNote.Auth;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccountingNote.ORM.DBmodel;
using AccountingNote7308.Extensions;
using AccountingNote7308.Models;

namespace AccountingNote7308.SystemAdmin
{
    public partial class AccountingList : AdminPageBase
    {
        // 檢查是否已授權

        public override string[] RequiredRoles { get; set; } =
        new string[]
                {
                    StaticText.RoleName_Announting_FinanceClerk,
                    StaticText.RoleName_Announting_FinanceAdmin,
                    StaticText.RoleName_Announting_FinanceReviewer,
                };

        public UserInfoModel currentUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            currentUser = AuthManager.GetCurrentUser();

            if (currentUser.level == UserLevelEnum.Regular)
            {

                if (!this.CanEdit())
                    this.btnCreate.Visible = false;

            }

            var list = AccountingManager.GetAccountingList(currentUser.ID.ToGuid());

            if (list.Count > 0)
            {
                var PagedList = this.GetPagedDataTable(list);

                this.gvAccountingList.DataSource = PagedList;
                this.gvAccountingList.DataBind();

                this.ucPager_AccountingList.TotalSize = list.Count;
                this.ucPager_AccountingList.Bind();

                int inc = 0;
                int exp = 0;

                var dt1 = AccountingManager.GetIncome(currentUser.ID);
                var dt2 = AccountingManager.GetExpenses(currentUser.ID);
                var income = dt1.Rows[0]["AT"];
                var expense = dt2.Rows[0]["ATS"];

                if (income != System.DBNull.Value && expense != System.DBNull.Value)
                {
                    inc = (int)income;
                    exp = (int)expense;

                }
                else if (income == System.DBNull.Value)
                {
                    inc = 0;
                    exp = (int)expense;
                }
                else if (expense == System.DBNull.Value)
                {
                    inc = (int)income;
                    exp = 0;
                }

                int total = inc - exp;
                this.ltMsg.Text = $"小計{total}元";
            }
            else
            {
                this.gvAccountingList.Visible = false;
                this.plcNoData.Visible = true;
                this.ltMsg.Visible = false;
            }

        }

        private bool CanRead()
        {
            var currentUser = AuthManager.GetCurrentUser();

            // 檢查是否已授權
            var roles =
                new string[]
                {
                    StaticText.RoleName_Announting_FinanceClerk,
                    StaticText.RoleName_Announting_FinanceAdmin,
                    StaticText.RoleName_Announting_FinanceReviewer,
                };

            Guid CID = currentUser.ID.ToGuid();
            if (AuthManager.IsGrant(CID, roles))
                return true;
            else
                return false;
        }


        private bool CanEdit()
        {
            var currentUser = AuthManager.GetCurrentUser();

            // 檢查是否已授權
            var roles =
                new string[]
                {
                    StaticText.RoleName_Announting_FinanceClerk,
                    StaticText.RoleName_Announting_FinanceAdmin,
                };
            Guid CID = currentUser.ID.ToGuid();
            if (AuthManager.IsGrant(CID, roles))
                return true;
            else
                return false;
        }


        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/AccountingDetail.aspx");
        }
        protected void gvAccountingList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var row = e.Row;
            if (row.RowType == DataControlRowType.DataRow)
            {

                Label lbl = row.FindControl("lblActType") as Label;
                Image img = row.FindControl("imgCover") as Image;

                var rowData = row.DataItem as AccountingNote.ORM.DBmodel.Accounting;
                int actType = rowData.ActType;

                if (actType == 0)
                {

                    lbl.Text = "支出";
                    lbl.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lbl.Text = "收入";
                }

                if (!string.IsNullOrEmpty(rowData.CoverImage))
                {
                    img.ImageUrl = "/FileDownload/Accounting/" + rowData.CoverImage;
                }


            }
        }
        private int GetcurrentPage()
        {
            string pageText = Request.QueryString["Page"];

            if (string.IsNullOrWhiteSpace(pageText))
                return 1;

            int intPage;
            if (!int.TryParse(pageText, out intPage))
                return 1;

            if (intPage <= 0)
                return 1;

            return intPage;
        }

        private List<Accounting> GetPagedDataTable(List<Accounting> list)
        {
            int startindex = (this.GetcurrentPage() - 1) * 10;
            return list.Skip(startindex).Take(10).ToList();
        }


        //private DataTable GetPagedDataTable(DataTable dt)
        //{
        //    DataTable dtPaged = dt.Clone();

        //    int startindex = (this.GetcurrentPage() - 1) * 10;
        //    int endindex = (this.GetcurrentPage()) * 10;

        //    if (endindex > dt.Rows.Count)
        //        endindex = dt.Rows.Count;

        //    for (var i = startindex; i < endindex; i++)
        //    {
        //        DataRow dr = dt.Rows[i];
        //        var drNew = dtPaged.NewRow();

        //        foreach (DataColumn dc in dt.Columns)
        //        {
        //            drNew[dc.ColumnName] = dr[dc];
        //        }

        //        dtPaged.Rows.Add(drNew);
        //    }
        //    return dtPaged;
        //}
    }
}