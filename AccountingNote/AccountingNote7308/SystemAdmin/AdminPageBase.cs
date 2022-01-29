using AccountingNote.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingNote7308.SystemAdmin
{
    public class AdminPageBase :System.Web.UI.Page
    {
        /// <summary> 需要的等級 </summary>
        public virtual UserLevelEnum RequiredLevel { get; set; } = UserLevelEnum.Regular;

        /// <summary> 需要的角色 </summary>
        public virtual string[] RequiredRoles { get; set; } = null;
    }
}