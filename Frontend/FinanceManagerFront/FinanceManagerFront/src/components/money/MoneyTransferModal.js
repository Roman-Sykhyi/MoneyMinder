import React from "react";
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import { useState } from "react";
import DropdownWallet from "../wallet/DropdownWallet";
import st from './MoneyTransferModal.module.css'
import '../wallet/style/WalletBalance.css'
import TransactionService from "../../services/TransactionService";
import { useDispatch } from 'react-redux';
import { selectTransaction } from '../../features/lastTransaction';

export default function MoneyTransferModal(props) {

    const [show, setShow] = useState(false);
    const sourceDefault = { id: -1, name: "Source", totalSum: '' };
    const destinationDefault = { id: -1, name: "Destination", totalSum: '' };
    const [sourceWallet, setSourceWallet] = useState(sourceDefault);
    const [destinationWallet, setDestinationWallet] = useState(destinationDefault);
    const [money, setMoney] = useState(0);
    let dispatch = useDispatch();

    const handleClose = () => {
        setShow(false);
        setTimeout(() => setSourceWallet(sourceDefault), 500);
        setTimeout(() => setDestinationWallet(destinationDefault), 500);
    };
    const handleTrasfer = async () => {
        if (sourceWallet.id === -1 || destinationWallet.id === -1) {
            window.alert("Select wallets")
            return;
        }

        if (sourceWallet.id === destinationWallet.id) {
            window.alert("Select different wallets")
            return;
        }

        if (money === 0) {
            window.alert("Enter sum to transfer");
            return
        }


        await TransactionService.createSpentTransaction(sourceWallet, money, null);
        let transaction = await TransactionService.createPutTransaction(destinationWallet, money, null);
        dispatch(selectTransaction(transaction));
        setMoney(0);
        handleClose();

        setSourceWallet(sourceDefault);
        setDestinationWallet(destinationDefault);
    };

    const handleShow = () => {
        setShow(true);
    };

    return (
        <React.Fragment>
            <Button variant="dark" onClick={handleShow}>Transfer</Button>

            <Modal show={show} onHide={handleClose}  >
                <Modal.Header closeButton>
                    <Modal.Title>Transfer money</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <div className={st.selectorsRow}>
                        <div className={st.selectorColumn}>
                            <DropdownWallet wallet={sourceWallet} setWallet={setSourceWallet} wallets={props.wallets} variant={"primary"} />
                            <div className={st.balance}>
                                <div>Balance</div>
                                <div className={st.balanceOutput}>{sourceWallet.totalSum}</div>
                            </div>
                        </div>
                        <div className={st.selectorColumn}>
                            <DropdownWallet wallet={destinationWallet} setWallet={setDestinationWallet} wallets={props.wallets} variant={"primary"} />
                            <div className={st.balance}>
                                <div>Balance</div>
                                <div className={st.balanceOutput}>{destinationWallet.totalSum}</div>
                            </div>
                        </div>
                    </div>
                    <div className={st.sumRow}>
                        <div>Enter sum: </div>
                        <input className='add-input' type="number" onChange={(event) => setMoney(Number(event.target.value))} />
                    </div>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="primary" onClick={handleTrasfer}>
                        Ok
                    </Button>
                </Modal.Footer>
            </Modal>

        </React.Fragment>
    );
}