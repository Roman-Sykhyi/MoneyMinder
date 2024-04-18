import { apiClient } from "../client";


export default class TransactionService {

    static getCurrentDateTime() {
        let dateTime = new Date();
        let offset = dateTime.getTimezoneOffset();
        
        dateTime.setMinutes(dateTime.getMinutes() - offset);
        
        return dateTime.toISOString();
    };

    static async getTransactions(wallet, category) {
        return await apiClient.get('transactions/all/' + wallet.id + '/' + category.id)
            .then(res => res.data);
    }

    static async addTransaction(transToSend) {
        await apiClient.post('transactions', transToSend)
            .then(res => res);

    }

    static async deleteTranasction(id) {
        return await apiClient.delete('transactions/' + id)
            .then(res => res);
    }

    static CreateTransaction(wallet) {
        return {
            amount: 20,
            category: null,
            walletId: wallet.id,
            creationTime: this.getCurrentDateTime()
        }
    }

    static async createPutTransaction(wallet, transSum, categoryId) {
        let newTransaction = this.CreateTransaction(wallet);
        newTransaction.amount = transSum;
        newTransaction.category = categoryId;
        await this.addTransaction(newTransaction);
        return newTransaction;
    }

    static async createSpentTransaction(wallet, transSum, categoryId) {
        let newTransaction = this.CreateTransaction(wallet);
        newTransaction.amount = -transSum;
        newTransaction.category = categoryId;
        await this.addTransaction(newTransaction);
        return newTransaction;
    }
}