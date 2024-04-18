import React from 'react'
import Dropdown from "react-bootstrap/Dropdown";
import { useSelector } from 'react-redux';

export default function DefaultCategoriesDropdown(props) {

    const categories = useSelector( (state) => state.defaultCategories.value)

    const handleSelect = (e) => {
        let category = categories.find(c => c.id == e);
        props.setCategory(category);
    }

    return (
        <Dropdown className={props.className} onSelect={handleSelect}>
            <Dropdown.Toggle variant='light' id="dropdown-basic">
                {props.category.name}
            </Dropdown.Toggle>

            <Dropdown.Menu >
                {categories.map(category => <Dropdown.Item key={category.id} eventKey={category.id}>{category.name}</Dropdown.Item>)}
            </Dropdown.Menu>
        </Dropdown>
    )
}