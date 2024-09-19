using Infrastructure.Extensions;
using System.Globalization;

namespace Authentication.Business.Utilities.Email
{
    public class EmailTemplate
    {
        public static string ChangePasswordMessage = "Your password has been changed in the Authentication system. If this was not done by you, please contact the system administrator. If you are aware of this, please disregard this message.";
        public static string ChangePasswordTitle = "Password Change Process";
        public static string EmailLabel = "Email Address";
        public static string ForgotPasswordMessage = "Please click the link below to reset your password.";
        public static string ForgotPasswordTitle = "Password Reset";
        public static string Greeting = "Hello {0} {1},";
        public static string LoginDetails = "Your login details are listed below.";
        public static string PasswordLabel = "Password:";
        public static string PasswordTitle = "System Registration and Password";
        public static string Regards = "Best regards,";
        public static string ResetPasswordLinkText = "Password Reset Link";
        public static string Team = "Authentication IT Team";
        public static string UsernameLabel = "Username:";
        public static string WelcomeMessage = "Welcome to the Authentication system! You can log in using your username or email address.";
        public static string RegisterTitle = "System Registration";

        public static string RegisterEmailTemplate(string firstName, string lastName)
        {
            string bodyHtml =
            $@"
            <!DOCTYPE html>
            <html lang='{CultureInfo.CurrentCulture.TwoLetterISOLanguageName}'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>{RegisterTitle}</title>
            </head>
            <body>
                <h2>{string.Format(Greeting, firstName.ToTitleCase(), lastName.ToUpper())}</h2>
                <p>{WelcomeMessage}</p>
                <p>{Regards}</p>
                <p><strong>{Team}</strong></p>
            </body>
            </html>
        ";
            return bodyHtml;
        }

        public static string PasswordEmailTemplate(string firstName, string lastName, string userName, string email, string password)
        {
            string bodyHtml =
            $@"
            <!DOCTYPE html>
            <html lang='{CultureInfo.CurrentCulture.TwoLetterISOLanguageName}'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>{PasswordTitle}</title>
            </head>
            <body>
                <h2>{string.Format(Greeting, firstName.ToTitleCase(), lastName.ToUpper())}</h2>
                <p>{WelcomeMessage}</p>
                <p>{LoginDetails}</p>
                <ul>
                    <li><strong>{UsernameLabel}</strong> {userName}</li>
                    <li><strong>{EmailLabel}</strong> {email}</li>
                    <li><strong>{PasswordLabel}</strong> {password}</li>
                </ul>
                <p>{Regards}</p>
                <p><strong>{Team}</strong></p>
            </body>
            </html>
        ";
            return bodyHtml;
        }

        public static string ChangePasswordEmailTemplate(string firstName, string lastName)
        {
            string bodyHtml =
            $@"
            <!DOCTYPE html>
            <html lang='{CultureInfo.CurrentCulture.TwoLetterISOLanguageName}'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>{ChangePasswordTitle}</title>
            </head>
            <body>
                <p>{string.Format(Greeting, firstName.ToTitleCase(), lastName.ToUpper())}</p>
                <p>{ChangePasswordMessage}</p>
                <p>{Regards}</p>
                <p><strong>{Team}</strong></p>
            </body>
            </html>
        ";
            return bodyHtml;
        }

        public static string ForgotPasswordEmailTemplate(string firstName, string lastName, string resetCode)
        {
            string bodyHtml =
            $@"
            <!DOCTYPE html>
            <html lang='{CultureInfo.CurrentCulture.TwoLetterISOLanguageName}'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>{ForgotPasswordTitle}</title>
            </head>
            <body>
                <p>{string.Format(Greeting, firstName.ToTitleCase(), lastName.ToUpper())}</p>
                <p>{ForgotPasswordMessage}</p>
                <p><a href='{resetCode}'>{ResetPasswordLinkText}</a></p>
                <p>{Regards}</p>
                <p><strong>{Team}</strong></p>
            </body>
            </html>
        ";
            return bodyHtml;
        }
    }
}
