using Authentication.MvcUi.Models.Items;

namespace Authentication.MvcUi.Models.Requests
{
    public class UserRoleRequest
    {
        public class UserRolePostAndPutRequest
        {
            public long UserId { get; set; }
            public List<int> RoleIdList { get; set; } = new List<int>();
        }

        public class UserRoleFilterRequest : BasePaginateFilterRequest
        {
            public long? UserId { get; set; }
            public int? RoleId { get; set; }
        }
    }
}
