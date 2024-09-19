import AuthenticationModule from '../modules/authenticationModule.js';
import { serializeFormToJson } from '../utils.js';

$(document).ready(async function () {
    const authenticationModule = new AuthenticationModule();

    $(document).on('click', '#btn-login', async function () {
        var formData = serializeFormToJson('form[name="loginForm"]');
        await authenticationModule.login(formData);
    });

    $(document).on('keypress', 'form[name="loginForm"] #password', function (e) {
        if (e.which === 13) {
            $('#btn-login').trigger("click");
        }
    });

    $(document).on('click', '#btn-forgot-password', async function () {
        var formData = serializeFormToJson('form[name="forgotPasswordForm"]');
        await authenticationModule.forgotPassword(formData);
    });

    $(document).on('keypress', 'form[name="forgotPasswordForm"] #email', function (e) {
        if (e.which === 13) {
            $('#btn-forgot-password').trigger("click");
        }
    });

    $(document).on('click', '#btn-reset-password', async function () {
        var formData = serializeFormToJson('form[name="resetPasswordForm"]');
        await authenticationModule.resetPassword(formData);
    });

    $(document).on('keypress', 'form[name="resetPasswordForm"] #controlNewPassword', function (e) {
        if (e.which === 13) {
            $('#btn-reset-password').trigger("click");
        }
    });

    $(document).on('click', '#btn-register', async function () {
        var formData = serializeFormToJson('form[name="registerForm"]');
        await authenticationModule.register(formData);
    });

    $(document).on('keypress', 'form[name="registerForm"] #controlPassword', function (e) {
        if (e.which === 13) {
            $('#btn-register').trigger("click");
        }
    });

    $(document).on('change', '#forgot-password', function () {
        if ($(this).is(':checked')) {
            window.location.href = '/Authentication/ForgotPassword';
        }
    });

    $('form[name="forgotPasswordForm"] #phone, form[name="registerForm"] #phone').mask('(000) 000 00-00');
});