import React, { useEffect, useMemo, useState } from "react";
import { Button } from "react-bootstrap";
import MyModal from "./ModalCreatePayment";
import RegularPaymentService from "../../API/RegularPaymentService";
import { useFetching } from "../../hooks/useFetching";
import { ReactReduxContext, useSelector } from "react-redux";
import TableOfRegularPayments from "./TableOfRegularPayments";

const RegularPayment = () => {
    const [regularPayments, setRegularPayments] = useState([]);

    const wallet = useSelector((state) => state.currentWallet.value);

    const [fetchPayments] = useFetching(async (id) => {
        const payments = await RegularPaymentService.GetAllInWallet(id);
        setRegularPayments(payments.data);
    });

    const [Delete] = useFetching(async (id) => {
        const res = await RegularPaymentService.Delete(id);

        if (res.status === 200) {
            fetchPayments(wallet.id);
        }
    });

    const [modal, setModal] = useState(false);

    useEffect(() => {
        fetchPayments(wallet.id);
    }, [wallet]);

    const showModal = () => {
        if(wallet.id === -1){
            window.alert('You have to choose wallet first');
            return;
        }
        setModal(true)
    }

    const deletePayment = async (id) => {
        Delete(id);
    };

    const paymentsToShow = useMemo(() => {
        if(regularPayments)
            return regularPayments;

        return []
    }, [regularPayments]);

    return (
        <div className="d-inline-block m-3">
            <MyModal
                show={modal}
                onHide={() => setModal(false)}
                save={() => fetchPayments(wallet.id)}
                wallet={wallet.id}
            />

            <TableOfRegularPayments
                title="Regular Payments"
                delete={deletePayment}
                payments={paymentsToShow}
            />

            <Button
                variant="success"
                style={{ width: "100%" }}
                onClick={showModal}
            >
                Add Regular Payment
            </Button>
        </div>
    );
};

export default RegularPayment;
