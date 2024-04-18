import React from "react";
import Button from "react-bootstrap/Button";
import Dropdown from "react-bootstrap/Dropdown";
import { useState } from "react";




export default function DropdownWallet(props) {
    const handleSelect = (e) => {
        let wllt = props.wallets.find(w => w.id == e);
        props.setWallet(wllt);
    }

    return (
        <React.Fragment>
            <Dropdown onSelect={handleSelect}>
                <Dropdown.Toggle variant={props.variant} id="dropdown-basic">
                    {props.wallet.name}
                </Dropdown.Toggle>

                <Dropdown.Menu >
                    {props.wallets.map(wallet => <Dropdown.Item key={wallet.id} eventKey={wallet.id}>{wallet.name}</Dropdown.Item>)}
                </Dropdown.Menu>
            </Dropdown>
        </React.Fragment>
    );
}