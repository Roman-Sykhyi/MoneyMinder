import React, { useEffect } from 'react';
import { useState } from "react";
import { useDispatch } from 'react-redux';
import { selectWallet } from '../../features/currentWallet';
import AddWalletModal from './AddWalletModal';
import './style/WalletList.css';
import { useSelector } from 'react-redux';
import DeleteWalletModal from './DeleteWalletModal';
import WalletService from '../../services/WalletService';
import Dropdown from 'react-bootstrap/Dropdown';
import MoneyTransferModal from '../money/MoneyTransferModal';



export default function WalletList() {

    const [wallets, setWalletList] = useState([]);

    const dispatch = useDispatch();

    const wallet = useSelector((state) => state.currentWallet.value)
    const shouldUpdate = useSelector((state) => state.shouldUpdate.value)
    const lastTransaction = useSelector((state) => state.lastTransaction.value);


    const handleSelect = (e) => {
        let currentWallet = wallets.find(w => w.id == e);
        dispatch(selectWallet(currentWallet));
    };

    useEffect(() => {
        getWallets();
        renderDropdown();
    }, [shouldUpdate, lastTransaction]);


    const renderDropdown = function () {
        return (
            <Dropdown onSelect={handleSelect}>
                <Dropdown.Toggle variant="primary" id="dropdown-basic">
                    {wallet.name}
                </Dropdown.Toggle>

                <Dropdown.Menu >
                    {wallets.map(wallet => <Dropdown.Item key={wallet.id} eventKey={wallet.id}>{wallet.name}</Dropdown.Item>)}
                </Dropdown.Menu>
            </Dropdown>
        )
    }

    const getWallets = async function () {
        let newWallets = await WalletService.getWallets()
        if (newWallets) {
            setWalletList(newWallets);
            if (wallet.id === -1) {
                dispatch(selectWallet(newWallets[0]));
            } else {
                dispatch(selectWallet(newWallets.find(w => w.id === wallet.id)));
            }

        }

    }

    return (
        <div className='wallet-controls'>
            {renderDropdown()}
            <AddWalletModal wallets={wallets} setWallets={setWalletList} />
            <DeleteWalletModal wallets={wallets} setWallets={setWalletList} />
            <div className='transfer-button'>
                <MoneyTransferModal wallets={wallets} />
            </div>
        </div>

    );



}