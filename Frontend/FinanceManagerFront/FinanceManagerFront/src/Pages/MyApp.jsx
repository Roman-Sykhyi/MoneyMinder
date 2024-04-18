import React, { useState } from "react";
import { BrowserRouter } from "react-router-dom";
import AppRouter from "../components/AppRouter";
import Nav from "../components/Nav";
import { AuthContext } from "../context";
import jwt from "jwt-decode";

const MyApp = () => {
    const [isAuth, setIsAuth] = useState(false);

    if (!isAuth) {
        const token = localStorage.getItem("user_token");
        if (token) {
            const data = jwt(token);
            const exp = new Date(0);
            exp.setUTCSeconds(data.exp);

            if (exp >= Date.now()) setIsAuth(true);
        }
    }

    return (
        <AuthContext.Provider
            value={{
                isAuth,
                setIsAuth: setIsAuth,
            }}
        >
            <BrowserRouter>
                <Nav />
                <AppRouter />
            </BrowserRouter>
        </AuthContext.Provider>
    );
};

export default MyApp;
