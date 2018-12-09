using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedPoint.Models.Users_Permissions_Models
{
    public class GroupPermissions
    {
        public int Id { get; set; }

        public bool IsSuperAdmin { get; set; }
        public bool IsAdmin { get; set; }
        public bool CanWrite { get; set; }
        public bool CanView { get; set; }
        public bool CanSendLinks { get; set; }
        public bool CanAttachFiles { get; set; }
        public bool CanManageServers { get; set; }
        public bool CanManageChannels { get; set; }
    }
}