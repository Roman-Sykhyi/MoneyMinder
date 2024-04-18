import React, { useState } from "react";
import { Button, Modal } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import RegularPaymentService from "../../API/RegularPaymentService";
import { updatePage } from "../../features/shouldUpdate";

const MyModal = (props) => {
    const dispatch = useDispatch();

    const [amount, setAmount] = useState("");

    const [day, setDay] = useState("");

    const [period, setPeriod] = useState("");

    const [name, setName] = useState("");

    const shouldUpdate = useSelector((state) => state.shouldUpdate.value);

    const createPayment = async (e) => {
        e.preventDefault();

        const data = {
            name: name,
            date: day,
            period: period,
            amount: amount,
            walletid: props.wallet,
        };
        setAmount("");
        setDay("");
        setPeriod("");
        setName("");

        const res = await RegularPaymentService.Add(data);

        if (res.status === 200) {
            props.save();
            props.onHide();
            dispatch(updatePage(!shouldUpdate));
        }
    };

    return (
        <Modal
            {...props}
            size="lg"
            aria-labelledby="contained-modal-title-vcenter"
            centered
        >
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    Create Regular Payment
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <form id="myform" onSubmit={(e) => createPayment(e)}>
                    <div class="form-group">
                        <label for="name">Name</label>
                        <input
                            type="text"
                            className="form-control"
                            id="name"
                            placeholder="Enter name"
                            onChange={(e) => setName(e.target.value)}
                            maxLength="10"
                            required
                        />
                    </div>
                    <div className="form-group">
                        <label for="date">Day</label>
                        <input
                            type="number"
                            className="form-control"
                            id="date"
                            placeholder="Enter day"
                            onChange={(e) => setDay(e.target.value)}
                            min="1"
                            max="31"
                            required
                        />
                    </div>
                    <div className="form-group">
                        <label for="period">Period</label>
                        <input
                            type="number"
                            className="form-control"
                            id="period"
                            placeholder="Enter period"
                            onChange={(e) => setPeriod(e.target.value)}
                            required
                            min="1"
                        />
                        <small className="form-text text-muted">
                            Enter how often the payments is made
                        </small>
                    </div>
                    <div className="form-group">
                        <label for="amount">Amount</label>
                        <input
                            type="number"
                            className="form-control"
                            id="amount"
                            placeholder="Enter amount"
                            onChange={(e) => setAmount(e.target.value)}
                            required
                        />
                    </div>
                </form>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={props.onHide}>
                    Close
                </Button>
                <Button variant="primary" type="submit" form="myform">
                    Add payment
                </Button>
            </Modal.Footer>
        </Modal>
    );
};

export default MyModal;
