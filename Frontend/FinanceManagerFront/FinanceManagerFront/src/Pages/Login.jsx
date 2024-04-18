import React, { useContext } from "react";
import { AuthContext } from "../context";
import "../App.css";
import LoginService from "../API/LoginService";
import { useFetching } from "../hooks/useFetching";
import { Link } from "react-router-dom";

const Login = () => {
    const { setIsAuth } = useContext(AuthContext);

    const [LogIn, Loading, Error] = useFetching(async (data) => {
        const res = await LoginService.LogIn(data);

        const token = res.data;

        if (res.status === 200) {
            localStorage.setItem("user_token", token);
            setIsAuth(true);
        }
    });

    const handleSubmit = async (event) => {
        //Prevent page reload
        event.preventDefault();

        var { email, pass } = document.forms[0];

        const data = { email: email.value, password: pass.value };

        LogIn(data);
    };

    // JSX code for login form
    const renderForm = (
        <div className="form">
            <form onSubmit={handleSubmit}>
                <div className="input-container form-group">
                    <label>Email </label>
                    <input
                        type="email"
                        name="email"
                        className="form-control"
                        placeholder="user@example.com"
                        pattern="[^@\s]+@[^@\s]+\.[^@\s]+"
                        required
                    />
                </div>
                <div className="input-container form-group">
                    <label>Password </label>
                    <input
                        type="password"
                        name="pass"
                        className="form-control"
                        placeholder="password"
                        minLength="6"
                        required
                    />
                    <small className="form-text text-muted">
                        Password must contain a minimum of 1 upper case letter
                        and special character
                    </small>
                    {Error && (
                        <div class="alert alert-danger" role="alert">
                            Incorrect email or password. You can register{" "}
                            <Link to="/register" className="alert-link">
                                on this link
                            </Link>
                            .
                        </div>
                    )}
                </div>
                {Loading ? (
                    <div class="d-flex justify-content-center">
                        <div class="spinner-border text-info" role="status">
                            <span class="visually-hidden">Loading...</span>
                        </div>
                    </div>
                ) : (
                    <div className="button-container">
                        <input type="submit" value="Log In" />
                    </div>
                )}
            </form>
        </div>
    );

    return (
        <div className="app">
            <div className="login-form">
                <div className="title">Log In</div>
                {renderForm}
            </div>
        </div>
    );
};

export default Login;
