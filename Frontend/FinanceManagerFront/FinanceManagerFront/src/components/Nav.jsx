import React, { useContext } from "react";
import { Link } from "react-router-dom";
import { AuthContext } from "../context";
import "bootstrap/dist/css/bootstrap.min.css";
import LoginService from "../API/LoginService";
const Nav = () => {
    const { isAuth, setIsAuth } = useContext(AuthContext);

    const logout = async (event) => {
        event.preventDefault();
        localStorage.removeItem("user_token");
        setIsAuth(false);
    };
    return isAuth ? (
        <nav className="navbar navbar-expand-sm bg-dark navbar-dark">
            <div className="container-fluid">
                <ul className="navbar-nav">
                    <li className="nav-item">
                        <Link className="nav-link " to="/">
                            Home
                        </Link>
                    </li>
                    <li className="nav-item">
                        <Link to="/login" onClick={logout} className="nav-link">
                            Log out
                        </Link>
                    </li>
                </ul>
            </div>
        </nav>
    ) : (
        <nav className="navbar navbar-expand-sm bg-dark navbar-dark">
            <div className="container-fluid">
                <ul className="navbar-nav">
                    <li className="nav-item">
                        <Link className="nav-link" to="/login">
                            Log In
                        </Link>
                    </li>
                    <li className="nav-item">
                        <Link className="nav-link" to="/register">
                            Register
                        </Link>
                    </li>
                </ul>
            </div>
        </nav>
    );
};

export default Nav;
