using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingNote.Auth
{
    public class UserInfoModel
    {
        //public Guid ID { get; set; }
        public string ID { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int UserLevel { get; set; }
        public string MobilePhone { get; set; }

        public UserLevelEnum level
        {
            get
            {
                return (UserLevelEnum)UserLevel;
            }
        }

        public Guid UserGuid
        {
            get
            {
                if (Guid.TryParse(this.ID, out Guid tempGuid))
                    return tempGuid;
                else
                    return Guid.Empty;
            }
        }

    }
}
