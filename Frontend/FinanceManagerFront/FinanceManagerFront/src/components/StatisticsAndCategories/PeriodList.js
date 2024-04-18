import React from "react"
import { Dropdown } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux"
import { selectPeriod } from "../../features/currentPeriod";
import s from './PeriodList.module.css'

export default function PeriodList() {

    const period = useSelector((state) => state.currentPeriod.value)

    const dispatch = useDispatch()

    const choosePeriod = (e) => {
        dispatch(selectPeriod(e));   
    };

    return (
        <Dropdown className={s.periodSelect} onSelect={choosePeriod}>
            <Dropdown.Toggle className={s.toggle} variant="info" id="dropdown-basic">
                    {period}
                </Dropdown.Toggle>
            <Dropdown.Menu>
                <Dropdown.Item eventKey={'Month'}>Month</Dropdown.Item>
                <Dropdown.Item eventKey={'Week'}>Week</Dropdown.Item>
                <Dropdown.Item eventKey={'Year'}>Year</Dropdown.Item>
                <Dropdown.Item eventKey={'All'}>All</Dropdown.Item>
            </Dropdown.Menu>
        </Dropdown>
    ) 
}