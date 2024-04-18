import { Alert, Modal } from "react-bootstrap"
import React, { useEffect, useState } from "react"
import { useSelector } from "react-redux"
import { apiClient } from "../../client"
import s from './LimitAlert.module.css'

export default function LimitAlert() {
    const [showApproaching, setShowApproaching] = useState(false)
    const [showExceeded, setShowExceeded] = useState(false)

    const lastTransaction = useSelector((state) => state.lastTransaction.value)

    useEffect( () => {
        if (lastTransaction.amount < 0 && lastTransaction.category)
            apiClient.get('categorylimits/checklimit/' + lastTransaction.walletId + '/' + lastTransaction.category.id)
                .then( response => {
                    if(response.data === 1)
                        setShowApproaching(true)
                    else if (response.data === 2)
                        setShowExceeded(true)
                })
        }, [lastTransaction])

        const getCategoryName = () => {
            if(lastTransaction.category)
                return lastTransaction.category.name

            return ''
        }

    return (
        <React.Fragment>
            <Modal dialogClassName={s.modal} show={showApproaching}>
                <Modal.Body>
                    <Alert className={s.alert} onClose={() => setShowApproaching(false)} variant="warning" dismissible>
                        You are approaching the monthly limit in the {getCategoryName()} category
                    </Alert>
                </Modal.Body>
            </Modal>

            <Modal dialogClassName={s.modal} show={showExceeded}>
                <Modal.Body>
                    <Alert className={s.alert} onClose={() => setShowExceeded(false)} variant="danger" dismissible>
                        <p>You have exceeded the monthly limit in the {getCategoryName()} category</p> 
                    </Alert>
                </Modal.Body>
            </Modal>
        </React.Fragment>
    )
}