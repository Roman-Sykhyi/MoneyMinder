import Login from "../Pages/Login";
import Register from "../Pages/Register";

export const routesArray = [
    {path: '/login', component: Login, exact: true},
    {path: '/register', component: Register, exact: true},
    {path: '*', component: Register, exact: true}
];