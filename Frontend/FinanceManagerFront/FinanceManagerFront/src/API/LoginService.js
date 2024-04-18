import axios from "axios";

export default class LoginService {
    static async LogIn(data) {
        const response = await axios.post(
            process.env.REACT_APP_API_URL + "account/login",
            data
        );

        return response;
    }
}
