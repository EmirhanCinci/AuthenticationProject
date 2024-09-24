using Authentication.MvcUi.Models.Items;

namespace Authentication.MvcUi.Models.Requests
{
    public class RoleRequest
    {
        public class RolePostRequest
        {
            public string Name { get; set; } = string.Empty;
        }

        public class RolePutRequest
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        public class RoleFilterRequest : BaseFilterRequest
        {

        }
    }
}
