import React from "react";
import s from "./Category.module.css"
import "../../assets/css/all.css"

export default function Category( {category, setCategory} ) {
    return (
        <button className={s.category} onClick={() => setCategory(category.name)}>
            <i className={category.icon}></i>
        </button>
    )
}