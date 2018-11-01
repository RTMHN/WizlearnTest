jQuery(document).ready(function () {
    $('#email').focusout(function () {
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/Login/ValidateEmail",
            data: { Email: jQuery("#email").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        //True
                    } else {
                        setInfoMsg(result.message);
                        jQuery("#email").val("");
                        return;
                    }
                } else {
                    // Logout();
                }
            }
        });
    });
    jQuery(".btn-login-data").click(function (evt) {
        evt.preventDefault();
        var Password = jQuery("#password").val();
        var Email = jQuery("#email").val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/Login/LoginProcess",
            data: {Password: Password, Email: Email
            },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        //Reload Home Page
                        window.location.href = result.message;
                    } else {
                        setInfoMsg(result.message);
                        jQuery("#password").val("");
                        return;
                    }
                } else {
                    //Logout();
                }
            }
        });
    });
    jQuery(".btn-signup-data").click(function (evt) {
        window.location.href = "/Signup/Index";
       });
});