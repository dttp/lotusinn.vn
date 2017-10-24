angular.module('alertService', [])
    .factory('$alert', function ($timeout) {
        function show(alerts, type, title, body) {
            var id = new Date().getTime();
            alerts.push({
                id: id,
                type: type,
                title: title,
                body: body
            });
            $("html, body").animate({ scrollTop: 0 }, "slow");

            $timeout(function() {
                for (var i = 0; i < alerts.length; i ++) {
                    if (alerts[i].id === id) {
                        alerts.splice(i, 1);
                        break;
                    }
                }
            }, 5000);
        };

        function showSuccess(alerts, msg) {
            show(alerts, 'success', 'Success!', msg);
        }

        function showError(alerts, msg) {
            show(alerts, 'error', 'Error!', msg);
        }

        function showInfo(alerts, msg) {
            show(alert, 'info', 'Info!', msg);
        }

        function showWarning(alerts, msg) {
            show(alert, 'warning', 'Warning!', msg);
        }

        return {
            show: show,
            success: showSuccess,
            error: showError,
            info: showInfo,
            warning: showWarning
        }
    });