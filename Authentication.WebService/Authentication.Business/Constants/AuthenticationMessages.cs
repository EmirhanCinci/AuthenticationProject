namespace Authentication.Business.Constants
{
    public static class AuthenticationMessages
    {  
        /* Success Messages */
        public static string SuccessLogin = "Login was successful.";
        public static string SuccessChangePassword = "Your password has been successfully changed.";
        public static string SuccessCreateToken = "Token has been successfully created.";
        public static string SuccessDeleteToken = "Token has been successfully deleted.";
        public static string SuccessForgotPassword = "Password reset request was successful. Please check your email inbox.";
        public static string SuccessResetPassword = "Password has been successfully reset. You can now log in with your new password.";
        public static string SuccessRegister = "Your registration has been successfully completed. You can log in to the system by entering your information.";

        /* Wrong Messages */
        public static string WrongLogin = "Login information is incorrect.";

        /* NotFound Messsages */
        public static string RefreshTokenNotFound = "Refresh Token was not found.";
        public static string InformationNotFound = "Information was not found.";

        /* Validation Messages */
        public static string TryControlNewPassword = "Please double-check your current password.";
        public static string NotEqualsNewPasswords = "The new passwords you entered do not match.";
        public static string NotEqualsPasswords = "The passwords you entered do not match.";
        public static string InvalidForgotPasswordRequest = "Your password reset request either does not exist or is not valid.";
        public static string ForgotPasswordRequestExists = "A password reset request already exists. Please check your email inbox.";
        public static string NotEmptyLogin = "Login information cannot be empty.";
        public static string NotEmptyRefreshToken = "Refresh Token cannot be empty.";
        public static string NotEmptyResetPasswordCode = "Password reset code cannot be empty.";
        public static string LastThreePasswordException = "Your new password cannot be the same as any of your last three passwords.";
    }
}
