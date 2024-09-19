namespace Authentication.Business.Constants
{
    public static class UserMessages
    {
        /* Response Messages */
        public static string AddedUser = "User has been successfully added.";
        public static string DeletedUser = "User has been successfully deleted.";
        public static string UpdatedUser = "User has been successfully updated.";
        public static string GetUser = "User has been successfully retrieved.";
        public static string GetUsers = "Users have been successfully retrieved.";

        /* NotFound Messages */
        public static string NotFoundById = "User not found.";
        public static string NotFound = "Users not found.";

        /* Validation Messages */
        public static string NotEmptyIdentityNumber = "Identity Number cannot be empty.";
        public static string InvalidIdentityNumberLength = "Identity Number must be 11 characters long.";

        public static string NotEmptyFirstName = "First name cannot be empty.";
        public static string InvalidFirstNameMinLength = "First name must be at least 2 characters.";
        public static string InvalidFirstNameMaxLength = "First name can be at most 100 characters.";

        public static string NotEmptyLastName = "Last name cannot be empty.";
        public static string InvalidLastNameMinLength = "Last name must be at least 2 characters.";
        public static string InvalidLastNameMaxLength = "Last name can be at most 100 characters.";

        public static string NotEmptyUserName = "Username cannot be empty.";
        public static string InvalidUserNameMinLength = "Username must be at least 2 characters.";
        public static string InvalidUserNameMaxLength = "Username can be at most 100 characters.";

        public static string NotEmptyEmail = "Email address cannot be empty.";
        public static string InvalidEmailFormat = "Invalid email address format.";
        public static string InvalidEmailMaxLength = "Email address can be at most 100 characters.";

        public static string NotEmptyPhoneNumber = "Phone number cannot be empty.";
        public static string InvalidPhoneNumberLength = "Phone number must be 15 characters long.";

        public static string NotEmptyPassword = "Password cannot be empty.";
        public static string NotEmptyOldPassword = "Old password cannot be empty.";
        public static string NotEmptyNewPassword = "New password cannot be empty.";
        public static string NotEmptyControlNewPassword = "Confirm new password cannot be empty.";
        public static string PasswordContainDigit = "Password must contain at least 1 digit.";
        public static string PasswordContainUpperCase = "Password must contain at least 1 uppercase letter.";
        public static string PasswordContainLowerCase = "Password must contain at least 1 lowercase letter.";
        public static string PasswordContainSpecialCharacter = "Password must contain at least 1 special character.";
        public static string InvalidPasswordMinLength = "Password must be at least 8 characters long.";
        public static string InvalidPasswordMaxLength = "Password can be at most 25 characters long.";

        /* Exists Messages */
        public static string ExistsUserName = "The entered username is already in use.";
        public static string ExistsEmail = "The entered email address is already in use.";
        public static string ExistsPhone = "The entered phone number is already in use.";
    }
}
