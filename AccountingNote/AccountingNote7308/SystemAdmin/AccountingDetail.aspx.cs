using AccountingNote.dbSource;
using AccountingNote.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccountingNote7308.Extensions;
using AccountingNote.ORM.DBmodel;
using System.IO;
using AccountingNote7308.Helpers;
using AccountingNote7308.Models;

namespace AccountingNote7308.SystemAdmin
{
    public partial class AccountingDetail : AdminPageBase
    {
        public override string[] RequiredRoles { get; set; } =
            new string[]
            {
                StaticText.RoleName_Announting_FinanceClerk,
                StaticText.RoleName_Announting_FinanceAdmin,
            };

        protected void Page_Load(object sender, EventArgs e)
        {
            var currentUser = AuthManager.GetCurrentUser();

            if (!this.IsPostBack)
            {

                if (this.Request.QueryString["ID"] == null)
                {
                    this.btnDelete.Visible = false;
                }
                else
                {
                    this.btnDelete.Visible = true;
                    string idText = this.Request.QueryString["ID"];
                    int id;
                    if (int.TryParse(idText, out id))
                    {
                        var accounting = AccountingManager.GetAccounting(id, currentUser.ID.ToGuid());


                        if (accounting == null)
                        {
                            this.ltMsg.Text = "記帳不存在";
                            this.btnSave.Visible = false;
                            this.btnDelete.Visible = false;
                        }
                        else
                        {
                            this.ddIActType.SelectedValue = accounting.ActType.ToString();
                            this.txtAmount.Text = accounting.Amount.ToString();
                            this.txtCaption.Text = accounting.Caption.ToString();
                            this.txtDesc.Text = accounting.Body.ToString();
                        }
                    }
                    else
                    {
                        this.ltMsg.Text = "需要使用者的ID";
                        this.btnSave.Visible = false;
                        this.btnDelete.Visible = false;
                    }

                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> msgList = new List<string>();
            if (!this.CheckInput(out msgList))
            {
                this.ltMsg.Text = string.Join("<br/>", msgList);
                return;
            }

            UserInfoModel currentUser = AuthManager.GetCurrentUser();

            if (currentUser == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            string userID = currentUser.ID;
            string actTypeText = this.ddIActType.SelectedValue;
            string amountText = this.txtAmount.Text;
            //string caption = this.txtCaption.Text;
            //string body = this.txtDesc.Text;

            int amount = Convert.ToInt32(amountText);
            int actType = Convert.ToInt32(actTypeText);

            string idText = this.Request.QueryString["ID"];

            //AccountingManager.CreateAccounting(userID, caption, amount, actType, body);
            Accounting accounting = new Accounting()
            {
                UserID = userID.ToGuid(),
                ActType = actType,
                Amount = amount,
                Caption = this.txtCaption.Text,
                Body = this.txtDesc.Text
            };

            // 假如有上傳檔案，就寫入檔名
            if (this.fileCover.HasFile &&
                FileHelper.ValidFileUpload(this.fileCover, out List<string> tempList))
            {
                string saveFileName = FileHelper.GetNewFileName(this.fileCover);
                string filePath = Path.Combine(this.GetSaveFolderPath(), saveFileName);
                this.fileCover.SaveAs(filePath);

                accounting.CoverImage = saveFileName;
            }


            if (string.IsNullOrWhiteSpace(idText))
            {
                
                AccountingManager.CreateAccounting(accounting);

            }
            else
            {
                int id;
                if (int.TryParse(idText, out id))
                {
                    accounting.ID = id;
                    AccountingManager.UpdaateAccounting(accounting);
                }
            }

            Response.Redirect("/SystemAdmin/AccountingList.aspx");
        }

        private bool CheckInput(out List<string> errorMsgList)
        {
            List<string> msglist = new List<string>();


            if (this.ddIActType.SelectedValue != "0" &&
                this.ddIActType.SelectedValue != "1")
            {
                msglist.Add("類別必須是1或0");
            }

            if (string.IsNullOrWhiteSpace(this.txtAmount.Text))
                msglist.Add("必須是金額");

            else
            {
                int tempInt;
                if (!int.TryParse(this.txtAmount.Text, out tempInt))
                {
                    msglist.Add("金額必須是數字");
                }

                if (tempInt < 0 || tempInt > 1000000)
                {
                    msglist.Add("金額應在0至1,000,000之間");
                }
            }

            errorMsgList = msglist;

            if (msglist.Count == 0)
                return true;
            else
                return false;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idText = this.Request.QueryString["ID"];
            if (string.IsNullOrWhiteSpace(idText))
                return;
            int id;
            if (int.TryParse(idText, out id))
            {
                AccountingManager.DeleteAccounting(id);
            }

            Response.Redirect("/SystemAdmin/AccountingList.aspx");
        }

        private string GetSaveFolderPath()
        {
            return Server.MapPath("~/FileDownload/Accounting");
        }
    }
}