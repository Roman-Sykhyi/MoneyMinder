import React from 'react';
import { useState } from "react";
import WalletService from '../../services/WalletService';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import { useDispatch } from 'react-redux';
import { selectWallet } from '../../features/currentWallet';
import './style/WalletList.css'
import TransactionService from '../../services/TransactionService';
import { selectTransaction } from '../../features/lastTransaction';

export default function AddWalletModal(props) {

    const [name, setName] = useState("");
    const [initAmout, setInitAmout] = useState(0);
    const [show, setShow] = useState(false);

    const dispatch = useDispatch();

    const handleClose = () => {
        setShow(false);
    };

    const handleShow = () => {
        setShow(true);
    };

    const handleAdd = async () => {
        if (name.length < 2) {
            window.alert('Wallet\'s name lenght must be greater than 1 character');
            return;
        }

        var regExp = /[a-zA-Z]/g;
        if (!regExp.test(name)) {
            window.alert('Wallet\'s name  must contain letter');
            return
        }
        let newWallet = await WalletService.addWallet(createWallet());
        let transaction = await TransactionService.createPutTransaction(newWallet, initAmout);

        setInitAmout(0);
        setName('');
        props.setWallets([...props.wallets, newWallet])
        dispatch(selectWallet(newWallet));
        dispatch(selectTransaction(transaction))
        handleClose();
    }

    const createWallet = function () {
        return (
            {
                name: name,
            }
        )
    }

    return (
        <React.Fragment>
            <Button variant="success" onClick={handleShow}><div style={{ width: "20px" }}>+</div></Button>

            <Modal show={show} onHide={handleClose} >
                <Modal.Header closeButton>
                    <Modal.Title>Add wallet</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <div>Enter wallet name</div>
                    <input className='add-input' onChange={(event) => setName(event.target.value)} placeholder="Name" minLength="1" /><br />
                    <div>Enter initial amout</div>
                    <input className='add-input' type="number" onChange={(event) => setInitAmout(Number(event.target.value))} placeholder="Initial amout" />
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="primary" onClick={handleAdd}>
                        Add
                    </Button>
                </Modal.Footer>
            </Modal>
        </React.Fragment >
    );
}