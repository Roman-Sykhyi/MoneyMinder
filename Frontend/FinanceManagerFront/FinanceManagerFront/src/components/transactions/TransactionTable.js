import React, { useEffect } from 'react';
import s from './TransactionTableStyles.module.css'
import { useState } from "react";
import AddTransaction from './AddTransactionModal';
import { apiClient } from '../../client';
import { useSelector } from 'react-redux';
import { useDispatch } from 'react-redux';
import { selectTransaction } from '../../features/lastTransaction';
import TransactionService from '../../services/TransactionService';
import './Table.css'
import { useFetching } from '../../hooks/useFetching';

export default function RenderTransactions() {
    let dispatch = useDispatch();
    const [transactions, setTransactionsList] = useState([]);
    const wallet = useSelector((state) => state.currentWallet.value);
    const category = useSelector((state) => state.currentCategory.value)
    useEffect(() => {
        fetchTransactions()
    }, [wallet, category]);

    const [Delete] = useFetching(async (id) => {
        const res = await TransactionService.deleteTranasction(id);
        if(res.status === 200 ) {
            dispatch(selectTransaction(res.data));
            fetchTransactions();
        }
    })

    const fetchTransactions = async function () {
        if (wallet.id === -1)
            return;
        const t = await TransactionService.getTransactions(wallet, category)
        if(t.length !== 0) {
             t.reverse();
        }
        setTransactionsList(t);
    }

    const getDateString = function (dateTime) {
        let date = new Date(dateTime);
        return date.getDate() + '.' + (date.getMonth() + 1) + '.' + date.getFullYear() + ' '
            + date.getHours() + ':' + date.getMinutes() + ':' + date.getSeconds();
    }

    return (
        <React.Fragment>
            <div className={s.container}>
                <h1>Transactions History</h1>
                <div className="tableFixHead">
                    <table className={'table table-striped'} aria-labelledby="tabelLabel">
                        <thead>
                            <tr>
                                <th scope='col'>Type</th>
                                <th scope='col'>Amount</th>
                                <th scope='col'>Date</th>
                                <th scope='col'>Delete</th>
                            </tr>
                        </thead>
                        <tbody>
                            {transactions.length !== 0 && transactions.map(transaction =>
                                <tr key={transaction.id}>
                                    <td className="align-middle">{transaction.amount < 0 ? '-' : '+'}</td>
                                    <td className="align-middle">{Math.abs(transaction.amount)}</td>
                                    <td className="align-middle">{getDateString(transaction.creationTime)}</td>
                                    <td><div className="d-inline-block">
                                        <button
                                            type="button"
                                            className="btn btn-danger text-left"
                                            onClick={async () => {
                                                Delete(transaction.id)

                                            }}
                                        >
                                            X
                                        </button>
                                    </div></td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </div>

            </div>
        </React.Fragment>
    );
}