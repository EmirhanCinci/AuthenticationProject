import ajaxRequest from '../utils.js';

export default class AuthenticationService {
    static login(loginRequest) {
        return ajaxRequest('/Authentication/Login', 'POST', loginRequest);
    }

    static forgotPassword(forgotPasswordRequest) {
        return ajaxRequest('/Authentication/ForgotPassword', 'POST', forgotPasswordRequest);
    }

    static resetPassword(resetPasswordRequest) {
        return ajaxRequest('/Authentication/ResetPassword', 'POST', resetPasswordRequest);
    }

    static register(registerRequest) {
        return ajaxRequest('/Authentication/Register', 'POST', registerRequest);
    }
}