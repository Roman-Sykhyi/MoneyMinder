import React, { Component } from 'react';
import { useSelector } from 'react-redux';
import './style/WalletBalance.css';


export default function WalletBalance() {
    const wallet = useSelector((state) => state.currentWallet.value);
    return (
        <div id='balance-wraper'>
            <div id='balance-body'>
                <div className='balance'>Balance</div>
                <div className='balance balance-value'>{wallet.totalSum}</div>
            </div>
        </div>

    );
}

