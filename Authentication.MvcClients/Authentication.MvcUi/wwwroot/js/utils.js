export function showAlert(type, title, message, time) {
    const alertTypes = { success: 'success', error: 'danger', info: 'info', warning: 'warning' };
    const alertIcons = { success: 'bx bxs-check-circle', error: 'bx bxs-error', info: 'bx bxs-info-circle', warning: 'bx bxs-error' };
    if ($(`.alert-fixed:contains(${message})`).length > 0) {
        return;
    }
    const alertId = 'alert-' + new Date().getTime();
    const alertHtml = `
        <div id="${alertId}" class="bg-light alert border-0 border-start border-5 border-${alertTypes[type]} alert-dismissible fade show py-3 alert-fixed">
            <div class="d-flex align-items-center">
                <div class="text-${alertTypes[type]} fs-35"><i class="${alertIcons[type]}"></i></div>
                <div class="ms-3">
                    <h6 class="mb-1 text-${alertTypes[type]}">${title}</h6>
                    <div class="fs-12">${message}</div>
                </div>
            </div>
            <button type="button" class="btn-close fs-12 p-1 me-3 mt-3" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
    `;
    const alertContainer = $('#alert-container');
    alertContainer.append(alertHtml);
    repositionAlerts();
    setTimeout(() => {
        $('#' + alertId).alert('close').on('closed.bs.alert', function () {
            $(this).remove();
            repositionAlerts();
        });
    }, time);
}

function repositionAlerts() {
    const alertContainer = $('#alert-container');
    const alerts = alertContainer.children('.alert');
    let offset = 20;
    alerts.each(function () {
        $(this).css('top', offset + 'px');
        offset += $(this).outerHeight() + 10;
    });
}

export async function handleAsync(callback) {
    try {
        return await callback();
    } catch (error) {
        showAlert('error', 'Error!', 'An error occurred while processing your request. Please try again later.', 3000);
    }
}

export default function ajaxRequest(url, method, data) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: url,
            method: method,
            data: data,
            success: function (response) {
                resolve(response);
            },
            error: function (error) {
                reject(error);
            }
        });
    });
}

export function serializeFormToJson(formSelector) {
    var formData = $(formSelector).serializeArray();
    var formDataObject = {};
    $.each(formData, function (index, element) {
        formDataObject[element.name] = element.value;
    });
    var jsonData = formDataObject;
    return jsonData;
}