import React, { useState } from 'react'
import { useEffect } from 'react';
import { useSelector } from 'react-redux'
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import { InputGroup } from 'react-bootstrap';
import s from './CategoryLimitsModal.module.css'
import { apiClient } from '../../client';
import { ModalFooter } from 'react-bootstrap';
import DefaultCategoriesDropdown from '../StatisticsAndCategories/DefaultCategoriesDropdown';

export default function CategoryLimitsModal() {
    const defaultSelect = { id: -1, name: "Select category" }
    const [category, setCategory] = React.useState(defaultSelect)
    const [limit, setLimit] = React.useState()
    const [newLimit, setNewLimit] = React.useState()
    const [show, setShow] = useState(false)
    const [showInput, setShowInput] = useState(false)

    const wallet = useSelector((state) => state.currentWallet.value)

    const handleClose = () => {
        setShow(false);
        setCategory(defaultSelect);
    };

    const handleCloseInput = () => {
        setNewLimit();
        setShowInput(false);
        setShow(true);
    }

    const handleShow = () => {

        if(wallet.id === -1){
            window.alert('You have to choose wallet first');
            return;
        }
        setShow(true);
    };

    const handleShowInput = () => {
        setShow(false);
        setShowInput(true);
    }

    const handeInputSubmit = () => {
        setCategory(defaultSelect);
        sendCategoryLimit();
        handleCloseInput();
    }

    useEffect( () => {
        if(category.id !== -1)
        apiClient.get('categorylimits/' + wallet.id + '/' + category.id)
        .then(response => {
            setLimit(response.data)
        })
    }, [category])

    const handleSetNewLimit = (newLimit) => {
        if(newLimit < 0)
            setNewLimit(0)
        else
            setNewLimit(newLimit)
    }

    const sendCategoryLimit = () => {
        apiClient.post('categorylimits', {
            walletId: wallet.id,
            categoryId: category.id,
            limit: newLimit
        })
    }

    function getLimit() {
        if(category.id === -1)
            return
        else if(limit > 0)
            return (
                <><div><span>Monthly limit: </span><b className={s.limit}>{limit}</b></div>
                <Button className={s.changeLimitBtn} onClick={handleShowInput}>Change limit</Button></>
         )
        else
            return(
                <><div><span>Monthly limit: </span><b>no limit</b></div>
                <Button className={s.changeLimitBtn} onClick={handleShowInput}>Change limit</Button></>
            ) 
    }

    return (
        <React.Fragment>
            <Button className={s.limitBtn} variant="warning" onClick={handleShow}>Limits</Button>
            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Category Limits</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <div className={s.categorySelect}>
                        Category:
                        <DefaultCategoriesDropdown className={s.dropdown} category={category} setCategory={setCategory} />
                    </div>

                    {getLimit()}
                    
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                </Modal.Footer>
            </Modal>

            <Modal show={showInput} onHide={handleCloseInput}>
                <Modal.Header closeButton>
                    <Modal.Title>Enter monthly limit (0 if no limit)</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <InputGroup>
                        <InputGroup.Text >$</InputGroup.Text>
                            <input
                                type="number"
                                className="form-control"
                                placeholder="Enter limit"
                                required
                                value={newLimit}
                                pattern="^[0-9]"
                                onChange={ (e) => handleSetNewLimit(e.target.value)}
                                min="1"
                            />
                    </InputGroup>
                </Modal.Body>
                <ModalFooter>
                    <Button variant="success" onClick={handeInputSubmit}>Submit</Button>
                    <Button variant="danger" onClick={handleCloseInput}>Close</Button>
                </ModalFooter>
            </Modal>
        </React.Fragment>
    )
}