namespace Authentication.Business.Constants
{
    public static class UserRoleMessages
    {
        /* Response Messages */
        public static string AddedUserRole = "User Role has been successfully added.";
        public static string DeletedUserRole = "User Role has been successfully deleted.";
        public static string UpdatedUserRole = "User Role has been successfully updated.";
        public static string GetUserRole = "User Role has been successfully retrieved.";
        public static string GetUserRoles = "User Roles have been successfully retrieved.";

        /* NotFound Messages */
        public static string NotFoundById = "User Role not found.";
        public static string NotFound = "User Roles not found.";

        /* Validation Messages */
        public static string NotEmptyUserId = "User id cannot be empty.";
        public static string NotEmptyRoleId = "Role id cannot be empty.";
    }
}
