import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "../App.css";
import RegisterService from "../API/RegisterService";
import { useFetching } from "../hooks/useFetching";
const Register = () => {
    let navigate = useNavigate();

    const [Equals, setEquals] = useState(true);

    const [Register, Loading, Error] = useFetching(async (data) => {
        const res = await RegisterService.Register(data);
        console.log(res);
        if (res.status === 200) {
            navigate("/login");
        }
    });

    const handleSubmit = async (event) => {
        //Prevent page reload
        event.preventDefault();

        var { uname, email, pass, passconf } = document.forms[0];

        if (pass.value !== passconf.value) {
            setEquals(false);
        } else {
            setEquals(true);

            const data = {
                email: email.value,
                name: uname.value,
                password: pass.value,
                confirmpassword: passconf.value,
            };
            Register(data);
        }
    };

    // JSX code for login form
    const renderForm = (
        <div className="form">
            <form onSubmit={handleSubmit}>
                <div className="input-container form-group">
                    <label>Name </label>
                    <input
                        type="text"
                        className="form-control"
                        name="uname"
                        placeholder="username"
                        required
                    />
                </div>
                <div className="input-container form-group">
                    <label>Email </label>
                    <input
                        type="email"
                        name="email"
                        placeholder="user@example.com"
                        className="form-control"
                        pattern="[^@\s]+@[^@\s]+\.[^@\s]+"
                        required
                    />
                </div>
                <div className="input-container form-group">
                    <label>Password </label>
                    <input
                        type="password"
                        className="form-control"
                        placeholder="password"
                        name="pass"
                        pattern="^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#.$%^*_=+-]).{6,12}$"
                        minLength="6"
                        required
                    />
                    <small className="form-text text-muted">
                        Password must contain a minimum of 1 upper case letter
                        and special character (!@#.$%^*_=+-)
                    </small>
                </div>
                <div className="input-container form-group">
                    <label>Confirm Password </label>
                    <input
                        type="password"
                        name="passconf"
                        placeholder="password"
                        pattern="^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#.$%^*_=+-]).{6,12}$"
                        minLength="6"
                        className="form-control"
                        required
                    />
                    {!Equals && <div className="error">password not equal</div>}
                </div>
                {Error && (
                    <div class="alert alert-danger" role="alert">
                        Name or email are already taken!!!
                    </div>
                )}
                {Loading ? (
                    <div class="d-flex justify-content-center">
                        <div class="spinner-border text-info" role="status">
                            <span class="visually-hidden">Loading...</span>
                        </div>
                    </div>
                ) : (
                    <div className="button-container">
                        <input type="submit" value="Register" />
                    </div>
                )}
            </form>
        </div>
    );

    return (
        <div className="app">
            <div className="login-form">
                <div className="title">Registration</div>
                {renderForm}
            </div>
        </div>
    );
};

export default Register;
