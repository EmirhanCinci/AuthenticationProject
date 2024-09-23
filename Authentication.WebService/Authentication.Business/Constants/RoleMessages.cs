namespace Authentication.Business.Constants
{
    public static class RoleMessages
    {
        /* Response Messages */
        public static string AddedRole = "Role has been successfully added.";
        public static string DeletedRole = "Role has been successfully deleted.";
        public static string UpdatedRole = "Role has been successfully updated.";
        public static string GetRole = "Role has been successfully retrieved.";
        public static string GetRoles = "Roles have been successfully retrieved.";

        /* NotFound Messages */
        public static string NotFoundById = "Role not found.";
        public static string NotFound = "Roles not found.";

        /* Validation Messages */
        public static string NotEmptyName = "Role name cannot be empty.";
        public static string InvalidNameMinLength = "Role name must be at least 2 characters.";
        public static string InvalidNameMaxLength = "Role name can be at most 100 characters.";

        /* Exists Messages */
        public static string ExistsName = "The entered role name is already in use.";
    }
}
