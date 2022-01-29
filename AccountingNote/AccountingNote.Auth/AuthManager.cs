using AccountingNote.dbSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AccountingNote.Auth
{
    public class AuthManager
    {
        public static bool Islogined()
        {
            if (HttpContext.Current.Session["UserLoginInfo"] == null)
                return false;
            else
                return true;
        }
        public static UserInfoModel GetCurrentUser()
        {
            string account = HttpContext.Current.Session["UserLoginInfo"] as string;

            if (account == null)
            {
                return null;
            }

            var userInfo = UserInfoManager.GetUserInfobyAccount_ORM(account);

            if (userInfo == null)
            {
                HttpContext.Current.Session["UserLoginInfo"] = null;
                return null;
            }

           
            UserInfoModel model = new UserInfoModel();
            model.ID = userInfo.ID.ToString();
            model.Account = userInfo.Account;
            model.Name = userInfo.Name;
            model.Email = userInfo.Email;
            model.MobilePhone = userInfo.MobilePhone;
            model.UserLevel = userInfo.UserLevel;
            return model;
        }
        public static void Logout()
        {
            HttpContext.Current.Session["UserLoginInfo"] = null;
        }
        public static bool tryLogin(string account, string pwd, out string errorMsg)
        {
            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(pwd))
            {
                errorMsg = "請輸入帳號和密碼。";
                return false;
            }

            var dr = UserInfoManager.GetUserInfobyAccount(account);

            if (dr == null)
            {
                errorMsg = "帳號輸入錯誤。";
                return false;
            }

            if (string.Compare(dr["Account"].ToString(), account) == 0 &&
                string.Compare(dr["PWD"].ToString(), pwd) == 0)
            {
                HttpContext.Current.Session["UserLoginInfo"] = dr["Account"].ToString();
                errorMsg = string.Empty;
                return true;
            }
            else
            {
                errorMsg = "登入失敗，請重新確認帳號與密碼。";
                return false;
            }
        }

        /// <summary> 儲存角色對應 </summary>
        /// <param name="userID"></param>
        /// <param name="roleIDs"></param>
        public static void MapUserAndRole(Guid userID, Guid[] roleIDs)
        {
            RoleManager.MappingUserAndRole(userID, roleIDs);
        }

        /// <summary> 是否被授權 </summary>
        /// <param name="userID"></param>
        /// <param name="roleIDs"></param>
        /// <returns></returns>
        public static bool IsGrant(Guid userID, Guid[] roleIDs)
        {
            return RoleManager.IsGrant(userID, roleIDs);
        }

        /// <summary> 是否被授權 </summary>
        /// <param name="userID"></param>
        /// <param name="roleNames"></param>
        /// <returns></returns>
        public static bool IsGrant(Guid userID, string[] roleNames)
        {
            if (roleNames == null)
                return true;

            List<Guid> roleIDs = new List<Guid>();

            foreach (string roleName in roleNames)
            {
                var role = RoleManager.GetRoleByName(roleName);
                if (role == null)
                    continue;
                roleIDs.Add(role.ID);
            }

            return RoleManager.IsGrant(userID, roleIDs.ToArray());
        }
    }
}
