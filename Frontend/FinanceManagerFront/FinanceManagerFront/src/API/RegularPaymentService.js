import axios from "axios";
import { apiClient } from "../client";

export default class RegularPaymentService {
    static async GetAll() {
        const response = await apiClient.get(
            process.env.REACT_APP_API_URL + "payments"
        );
        return response;
    }

    static async Delete(id) {
        const response = await apiClient.delete(
            process.env.REACT_APP_API_URL + "deletepayment/" + id
        );
        return response;
    }

    static async GetAllInWallet(id) {
        const response = await apiClient.get(
            process.env.REACT_APP_API_URL + "paymentwallet/" + id
        );
        return response;
    }

    static async Add(data) {
        const response = await apiClient.post(
            process.env.REACT_APP_API_URL + "addpayment",
            data
        );

        return response;
    }
}
