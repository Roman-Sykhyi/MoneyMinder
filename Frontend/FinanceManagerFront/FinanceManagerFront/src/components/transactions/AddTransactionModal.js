import React from 'react';
//import './TransactionModalStyles.css'
import { useState } from "react";
import { useSelector } from 'react-redux';
import { useDispatch } from 'react-redux';
import { selectTransaction } from '../../features/lastTransaction';
import TransactionService from '../../services/TransactionService';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import { ButtonGroup } from 'react-bootstrap';
import DefaultCategoriesDropdown from '../StatisticsAndCategories/DefaultCategoriesDropdown'
import s from '../StatisticsAndCategories/DefaultCategoriesDropdown.module.css'

export default function AddTransaction(props) {

    const [transactionSize, setTransactionSize] = useState(0);
    const [transactions, setTransactionsList] = useState([]);
    const [show, setShow] = useState(false);
    const [isTransactioSumPositive, setTransactionSumType] = useState(false);
    const defaultSelect = { id: -1, name: "Select category" }
    const [category, setCategory] = useState(defaultSelect)

    let dispatch = useDispatch();
   
    const lastTransaction = useSelector((state) => state.lastTransaction.value);

    const wallet = useSelector((state) => state.currentWallet.value)

    const handleClose = () => {
        setShow(false);
    }

    const handlePutShow = () => {
        if(wallet.id === -1){
            window.alert('You have to choose wallet first');
            return;
        }

        setShow(true);
        setTransactionSumType(true);
    }

    const handleSpentShow = () => {
        if(wallet.id === -1){
            window.alert('You have to choose wallet first');
            return;
        }

        setShow(true);
        setTransactionSumType(false);
    }

    const handleAdd = () => {
       
        if (transactionSize <= 0) {
            window.alert('You have to enter positive sum for transaction');
            return;
        }
        if(category.id === -1){
            window.alert('You have to choose category');
            return;
        }

        if (isTransactioSumPositive === true) {
            const put = async () => {
                let transaction = await TransactionService.createPutTransaction(wallet, transactionSize, category);
                dispatch(selectTransaction(transaction))
            }
            put();
        } else {
            const spend = async () => {
                let transaction = await TransactionService.createSpentTransaction(wallet, transactionSize, category);
                dispatch(selectTransaction(transaction))
            }
            spend();
        }
        setTransactionSize(0);
        handleClose();
    }

    const handleSumInput = (moneyAmount) => {
        if(moneyAmount < 0){
            setTransactionSize(0);
        } else {
            setTransactionSize(moneyAmount);
        }
    }

    return (
        <React.Fragment>
            <ButtonGroup>
                <Button variant="warning" style={{marginRight: "20px"}}><div style={{ width: "40px"}}><i class="fa-solid fa-chart-line"></i></div></Button>
                
                <Button variant="success" onClick={handlePutShow}><div style={{ width: "40px" }}>+</div></Button>
                <Button variant="danger" onClick={handleSpentShow}><div style={{ width: "40px" }}>-</div></Button>

                <Button variant="info" style={{marginLeft: "20px"}}><div style={{ width: "40px"}}><i class="fa solid fa-receipt"></i></div></Button>
            </ButtonGroup>
            <Modal show={show} onHide={handleClose} >
                <Modal.Header closeButton>
                    <Modal.Title>Create transaction</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <div>Enter the transaction size</div>
                    <input placeholder='Sum of money' type='number' min='1' value={transactionSize} onChange={(event) => {handleSumInput(event.target.value)}}></input>
                    <div>Choose category</div>
                    <DefaultCategoriesDropdown className={s.dropdown} category={category} setCategory={setCategory} />
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
        </React.Fragment>
    )
}