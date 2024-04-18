import React from "react";
import Category from "./Category";
import Statistics from "./Statistics";
import s from './StatisticsAndCategories.module.css'
import { useDispatch, useSelector } from "react-redux";
import { selectCategory } from "../../features/currentCategory";

export default function StatisticsAndCategories() {
    let defaultCategoryName = 'All'
    const currentCategory = useSelector((state) => state.currentCategory.value)
    const categories = useSelector((state) => state.defaultCategories.value)

    const dispatch = useDispatch()

    function setCategory(name) {
        let category = categories.find(c => c.name === name)
        dispatch(selectCategory(category)); 
    }

    function setDefaultCategory() {
        let category = { id: -1, name: defaultCategoryName}
        dispatch(selectCategory(category))
    }
    
    if(categories.length !== 0) {
        return (
            <div className={s.container}>
                <div className={s.top}>
                    <Category category={categories[0]} setCategory={setCategory}/>    
                    <Category category={categories[1]} setCategory={setCategory}/>  
                    <Category category={categories[2]} setCategory={setCategory}/>  
                    <Category category={categories[3]} setCategory={setCategory}/>         
                </div>

                <div className={s.bot}>
                    <Category category={categories[4]} setCategory={setCategory}/>  
                    <Category category={categories[5]} setCategory={setCategory}/>  
                    <Category category={categories[6]} setCategory={setCategory}/>  
                    <Category category={categories[7]} setCategory={setCategory}/>  
                </div>

                <div className={s.left}>
                    <Category category={categories[8]} setCategory={setCategory}/>  
                    <Category category={categories[9]} setCategory={setCategory}/>  
                    <Category category={categories[10]} setCategory={setCategory}/>  
                </div>

                <div className={s.right}>
                    <Category category={categories[11]} setCategory={setCategory}/>  
                    <Category category={categories[12]} setCategory={setCategory}/>  
                    <Category category={categories[13]} setCategory={setCategory}/>  
                </div>

                <div className={s.statistics}>
                    <h1 className={s.name}>{currentCategory.name} 
                    { currentCategory.name !== defaultCategoryName && 
                        <button className={s.btn} onClick={ () => setDefaultCategory()}>&times;</button>
                    }
                    </h1>
                    
                    <div className={s.StatisticsDiagram}>
                        <Statistics />
                    </div>              
                </div>
            </div>
        )
    }
    else 
        return <h1>Default categories are not loaded</h1>
}