import React from "react";
import Modal from 'react-bootstrap/Modal';
import Button from "react-bootstrap/Button";
import Dropdown from "react-bootstrap/Dropdown";
import { useState } from "react";
import WalletService from "../../services/WalletService";
import { useSelector } from 'react-redux';
import { useDispatch } from 'react-redux';
import { selectWallet } from '../../features/currentWallet';
import DropdownWallet from "./DropdownWallet";

export default function DeleteWalletModal(props) {
    const [show, setShow] = useState(false);
    const defaultSelect = { id: -1, name: "Select wallet" };
    const [wallet, setWallet] = useState(defaultSelect);
    const currentWallet = useSelector((state) => state.currentWallet.value);

    const dispatch = useDispatch();

    const handleClose = () => {
        setShow(false);
        setTimeout(() => setWallet(defaultSelect), 500);
    };
    const handleShow = () => {
        setShow(true);
    };

    const handleDelete = async () => {
        if (currentWallet.id === -1) {
            window.alert("You don't have wallets to delete");
            return;
        }
        if (wallet.id === -1) {
            window.alert("You have to select wallet");
            return;
        }

        let deletedWallet = await WalletService.deleteWallet(wallet.id);
        let newWallets = props.wallets.filter(w => w.id !== deletedWallet.id);

        if (newWallets.length === 0) {
            dispatch(selectWallet({ id: -1, name: 'No wallets', totalSum: '' }));
        } else {
            if (currentWallet.id === deletedWallet.id)
                dispatch(selectWallet(newWallets[0]));
        }
        props.setWallets(newWallets);
        handleClose();
    }


    return (
        <>
            <Button variant="danger" onClick={handleShow}><div style={{ width: "20px" }}>-</div></Button>

            <Modal show={show} onHide={handleClose} >
                <Modal.Header closeButton>
                    <Modal.Title>Select wallet to delete</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <DropdownWallet wallet={wallet} setWallet={setWallet} wallets={props.wallets} variant={"primary"} />
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="primary" onClick={handleDelete}>
                        Delete
                    </Button>
                </Modal.Footer>
            </Modal>
        </>
    );
}