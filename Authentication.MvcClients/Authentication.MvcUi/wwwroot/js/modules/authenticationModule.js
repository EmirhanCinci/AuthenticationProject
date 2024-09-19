import AuthenticationService from '../services/authenticationService.js';
import { showAlert, handleAsync } from '../utils.js';

export default class AuthenticationModule {
    constructor() {
        this.authenticationService = AuthenticationService;
    }

    async login(loginRequest) {
        if (!loginRequest.userNameOrEmail) {
            showAlert('warning', 'Warning!', 'Username or email address is required.', 3000);
            return;
        }
        if (!loginRequest.password) {
            showAlert('warning', 'Warning!', 'Password is required.', 3000);
            return;
        }
        return handleAsync(async () => {
            const response = await this.authenticationService.login(loginRequest);
            if (response.isSuccessful) {
                window.location.href = response.redirectUrl;
            } else {
                for (var i = 0; i < response.errorMessages.length; i++) {
                    showAlert('error', response.statusMessage, response.errorMessages[i], 5000);
                }
            }
        });
    }

    async forgotPassword(forgotPasswordRequest) {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!forgotPasswordRequest.userName) {
            showAlert('warning', 'Warning!', 'Username cannot be empty.', 3000);
            return;
        }
        if (!forgotPasswordRequest.phone) {
            showAlert('warning', 'Warning!', 'Phone number cannot be empty.', 3000);
            return;
        }
        if (forgotPasswordRequest.phone.length != 15) {
            showAlert('warning', 'Warning!', 'Please enter a valid phone number in the correct format. Example: (555) 555 55-55', 3000);
            return;
        }
        if (!forgotPasswordRequest.email) {
            showAlert('warning', 'Warning!', 'Email address cannot be empty.', 3000);
            return;
        }
        if (!emailRegex.test(forgotPasswordRequest.email)) {
            showAlert('warning', 'Warning!', 'Please enter a valid email address.', 3000);
            return;
        }
        return handleAsync(async () => {
            const response = await this.authenticationService.forgotPassword(forgotPasswordRequest);
            if (response.isSuccessful) {
                showAlert('success', 'Success!', response.statusMessage, 5000);
            } else {
                for (var i = 0; i < response.errorMessages.length; i++) {
                    showAlert('error', response.statusMessage, response.errorMessages[i], 5000);
                }
            }
        });
    }

    async resetPassword(resetPasswordRequest) {
        const uppercasePattern = /[A-Z]/;
        const lowercasePattern = /[a-z]/;
        const digitPattern = /\d/;
        const specialCharPattern = /[!@#$%^&*(),.?":{}|<>]/;
        const datePattern = /\b\d{4}\b/;
        if (!resetPasswordRequest.code) {
            showAlert('warning', 'Warning!', 'A valid password reset code was not found.', 3000);
            return;
        }
        if (!resetPasswordRequest.newPassword) {
            showAlert('warning', 'Warning!', 'Please set a new password.', 3000);
            return;
        }
        if (resetPasswordRequest.newPassword.length < 8 || resetPasswordRequest.newPassword.length > 25) {
            showAlert('warning', 'Warning!', 'The length of the new password must be at least 8 and at most 25 characters.', 3000);
            return;
        }
        if (!uppercasePattern.test(resetPasswordRequest.newPassword) || !lowercasePattern.test(resetPasswordRequest.newPassword) || !digitPattern.test(resetPasswordRequest.newPassword) || !specialCharPattern.test(resetPasswordRequest.newPassword)) {
            showAlert('warning', 'Warning!', 'Your password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.', 3000);
            return;
        }
        if (datePattern.test(resetPasswordRequest.newPassword)) {
            showAlert('warning', 'Warning!', 'Your password should not contain date formats.', 3000);
            return;
        }
        if (!resetPasswordRequest.controlNewPassword) {
            showAlert('warning', 'Warning!', 'Please confirm your new password.', 3000);
            return;
        }
        if (resetPasswordRequest.newPassword != resetPasswordRequest.controlNewPassword) {
            showAlert('warning', 'Warning!', 'Passwords do not match. Please check again.', 3000);
            return;
        }
        return handleAsync(async () => {
            const response = await this.authenticationService.resetPassword(resetPasswordRequest);
            if (response.isSuccessful) {
                showAlert('success', 'Success!', response.statusMessage, 5000);
            } else {
                for (var i = 0; i < response.errorMessages.length; i++) {
                    showAlert('error', response.statusMessage, response.errorMessages[i], 5000);
                }
            }
        });
    }

    async register(registerRequest) {
        const uppercasePattern = /[A-Z]/;
        const lowercasePattern = /[a-z]/;
        const digitPattern = /\d/;
        const specialCharPattern = /[!@#$%^&*(),.?":{}|<>]/;
        const datePattern = /\b\d{4}\b/;
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!registerRequest.userName) {
            showAlert('warning', 'Warning!', 'Please set a username.', 3000);
            return;
        }
        if (!registerRequest.firstName) {
            showAlert('warning', 'Warning!', 'Please enter your first name.', 3000);
            return;
        }
        if (!registerRequest.lastName) {
            showAlert('warning', 'Warning!', 'Please enter your last name.', 3000);
            return;
        }
        if (!registerRequest.email) {
            showAlert('warning', 'Warning!', 'Please enter an email address.', 3000);
            return;
        }
        if (!emailRegex.test(registerRequest.email)) {
            showAlert('warning', 'Warning!', 'Please enter a valid email address.', 3000);
            return;
        }
        if (registerRequest.phone) {
            if (registerRequest.phone.length != 15) {
                showAlert('warning', 'Warning!', 'Please enter a valid phone number in the correct format. Example: (555) 555 55-55', 3000);
                return;
            }
        }
        if (!registerRequest.password) {
            showAlert('warning', 'Warning!', 'Please set a password.', 3000);
            return;
        }
        if (registerRequest.password.length < 8 || registerRequest.password.length > 25) {
            showAlert('warning', 'Warning!', 'The length of the password must be at least 8 and at most 25 characters.');
            return;
        }
        if (!uppercasePattern.test(registerRequest.password) || !lowercasePattern.test(registerRequest.password) || !digitPattern.test(registerRequest.password) || !specialCharPattern.test(registerRequest.password)) {
            showAlert('warning', 'Warning!', 'Your password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.', 3000);
            return;
        }
        if (datePattern.test(registerRequest.password)) {
            showAlert('warning', 'Warning!', 'Your password should not contain date formats.', 3000);
            return;
        }
        if (!registerRequest.controlPassword) {
            showAlert('warning', 'Warning!', 'Please confirm your new password.', 3000);
            return;
        }
        if (registerRequest.password != registerRequest.controlPassword) {
            showAlert('warning', 'Warning!', 'Passwords do not match. Please check again.', 3000);
            return;
        }
        return handleAsync(async () => {
            const response = await this.authenticationService.register(registerRequest);
            if (response.isSuccessful) {
                showAlert('success', 'Success!', response.statusMessage, 5000);
            } else {
                for (var i = 0; i < response.errorMessages.length; i++) {
                    showAlert('error', response.statusMessage, response.errorMessages[i], 5000);
                }
            }
        });
    }
}