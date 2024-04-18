import { apiClient } from "../client";

export default class WalletService {
    static async getWallets() {
        return await apiClient.get(process.env.REACT_APP_API_URL + 'wallets')
            .then((response) => {
                if (response.status >= 200 && response.status <= 299) {
                    return response.data;
                } else {
                    throw Error(response.statusText);
                }
            }).catch((error) => {
                console.log(error);
                return null;
            });
    }
    static async getWallet(id) {
        return await apiClient.get(process.env.REACT_APP_API_URL + 'wallets/' + id)
            .then((response) => {
                if (response.status >= 200 && response.status <= 299) {
                    return response.data;
                } else {
                    throw Error(response.statusText);
                }
            }).catch((error) => {
                console.log(error);
                return null;
            });
    }

    static async addWallet(wallet) {
        return await apiClient.post(process.env.REACT_APP_API_URL + 'wallets', {
            name: wallet.name,
            totalSum: wallet.totalSum,
            transactions: wallet.transactions,
        }).then(response => response.data);
    }
    static async deleteWallet(walletId) {
        return await apiClient.delete(process.env.REACT_APP_API_URL + 'wallets/' + walletId, { data: { id: walletId } }).then(response => response.data);
    }
}