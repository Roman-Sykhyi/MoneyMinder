import axios from "axios";

export default class RegisterService {
    static async Register(data) {

        const response = await axios.post(process.env.REACT_APP_API_URL + 'account/register', data);
        return response;
    }
}
