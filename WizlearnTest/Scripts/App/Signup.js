jQuery(document).ready(function () {
    jQuery('#dob').val(my_date_format(new Date()));
    jQuery('#dob').datepicker({ dateFormat: "dd/M/yy" })
    LoadMainTypes();
    function LoadMainTypes() {
        jQuery.ajax({
            type: "GET",
            url: "/Signup/LoadGender",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("gender");
                        jQuery("#gender").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            for (i = 0; i < result.data.length; i++) {
                                option.text = result.data[i].Text;
                                option.value = result.data[i].Value;
                                options.push(option.outerHTML);
                            }
                        } else {
                            option.text = "Select Type";
                            option.value = "";
                            options.push(option.outerHTML);
                        }
                        select.insertAdjacentHTML('beforeEnd', options.join('\n'));
                    } else {
                        setError(result.msg);
                    }
                } else {
                    Logout();
                }
            }
        })
    }
    jQuery(".btn-signup-data").click(function (evt) {
        evt.preventDefault();
        var Firstname = jQuery("#fname").val();
        var LastName = jQuery("#lname").val();
        var FullName = jQuery("#fullname").val();
        var Mobile = jQuery("#mobile").val();
        var Address = jQuery("#address").val();
        var Gender = jQuery("#gender").val();
        var Dob = jQuery("#dob").val();
        var Password = jQuery("#password").val();
        var Email = jQuery("#email").val();

        if (Firstname == "") {
            setInfoMsg("Please Enter First Name");
            return;
        }
        if (LastName == "") {
            setInfoMsg("Please Enter Last Name");
            return;
        }
        if (Mobile == "") {
            setInfoMsg("Please Enter Mobile");
            return;
        }
        if (Address == "") {
            setInfoMsg("Please Enter Address");
            return;
        }
        if (Gender == "") {
            setInfoMsg("Please Select Gender");
            return;
        }
        if (Dob == "") {
            setInfoMsg("Please Select DOB");
            return;
        }
        if (Email == "") {
            setInfoMsg("Please Select Email");
            return;
        }
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/Signup/UserSignup",
            data: {
                Firstname: Firstname, LastName: LastName, FullName: FullName, Mobile: Mobile, Address: Address,
                Gender: Gender, Dob: Dob, Password: Password, Email: Email
            },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        EmptyFields();
                        setSuccesssMsg(result.message);
                    } else {
                        setError(result.message);
                    }
                } else {
                    //Logout();
                }
            }
        });
    });
    $('#email').focusout(function () {
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/Signup/ValidateEmail",
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

        var email = jQuery("#email").val();
        if (isValidEmailAddress(email) == false) {
            setInfoMsg("Invalid Email");
            jQuery("#email").val("");
            return;
        }
    });
    function EmptyFields() {
        jQuery("#fname").val("");
        jQuery("#lname").val("");
        jQuery("#fullname").val("");
        jQuery("#mobile").val("");
        jQuery("#address").val("");
        jQuery("#gender").val("");
        jQuery("#dob").val("");
        jQuery("#password").val("");
        jQuery("#cpass").val("");
        jQuery("#email").val("");
    }

    //cpass
    $('#cpass').focusout(function () {
        
        var password = jQuery("#password").val();
        var epass = jQuery("#cpass").val();
        if (password != epass)
        {
            setInfoMsg("Password Confirmation Failed");
            jQuery("#password").val("");
            jQuery("#cpass").val("");
            return;
        }
    });
    $('#password').focusout(function () {
            jQuery("#cpass").val("");
    });
    $('#mobile').focusout(function () {
        var mob = jQuery("#mobile").val();
        var regex = new RegExp(/^\+?[0-9(),.-]+$/);
        if (mob.match(regex)==false) {
            setInfoMsg("Invalid Mobile");
            return;
            if (mob.length>10) {
                setInfoMsg("Invalid Mobile");
                return;
            }
        }
    });
    function isValidEmailAddress(emailAddress) {
        var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;
        return pattern.test(emailAddress);
    };
    $('#dob').focusout(function () {
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/Signup/ValidateDOB",
            data: { DOB: jQuery("#dob").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        //True
                    } else {
                        setInfoMsg(result.message);
                           jQuery('#dob').val(my_date_format(new Date()));
                           jQuery('#dob').datepicker({ dateFormat: "dd/M/yy" });
                        return;
                    }
                } else {
                    // Logout();
                }
            }
        });
    });
});