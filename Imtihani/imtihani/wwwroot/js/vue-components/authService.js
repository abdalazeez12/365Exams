var authservice = {
    login: function (ReturnUrl) {
        var data = $('#login-form').serialize();
        axios.post("/api/auth/login", data)
            .then(response => {
                localStorage.setItem("authtoken", response.data.token);
                var type = response.data.type;
                if (ReturnUrl != null && ReturnUrl != '') {
                    window.location.href = ReturnUrl;
                } else {
                    if (type == 3 || type == 1)
                        window.location.href = "/StudentProfile";
                    else if (type == 5)
                        window.location.href = "/TeacherProfile";
                }
            })
            .catch(error => {
                swal(
                    'Ops !',
                    'Your Email OR Password are inCorrect' ,
                    'error'
                )
            });
        return false;
    },
    register: function () {
        var data = $('#register-form').serialize();
        axios.post("/api/auth/register", data)
            .then(response => {
                window.location.href = "/StudentProfile";
            })
            .catch(error => {
                swal(
                    'Ops !',
                    'Something Went Wrong Please Try Again',
                    'error'
                )            });
        return false;
    },
    submitschoolform: function () {
        var data = $('#school-form').serialize();
        axios.post("/api/auth/registerschool", data)
            .then(response => {
                alert("Success");
            })
            .catch(error => {
                alert(error);
            });
        return false;
    },
    submitteacherform: function () {
        var data = $('#teacher-form').serialize();
        axios.post("/api/auth/registerteacher", data)
            .then(response => {
                alert("Success");
            })
            .catch(error => {
                alert(error);
            });
        return false;
    }
}