using Infrastructure.Model.Dtos.Implementations;
using Infrastructure.Model.Dtos.Interfaces;

namespace Authentication.Model.Dtos
{
    public class RoleDto
    {
        public class RoleGetDto : BasicDto<int>
        {

        }

        public class RolePostDto : IDto
        {
            public string Name { get; set; } = string.Empty;
        }

        public class RolePutDto : IDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        public class RoleFilterDto : BaseFilterDto
        {

        }
    }
}
