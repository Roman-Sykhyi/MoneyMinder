import React, { useEffect } from "react";
import StatisticsAndCategories from '../components/StatisticsAndCategories/StatisticsAndCategories';
import WalletBalance from '../components/wallet/WalletBalance';
import WalletList from '../components/wallet/WalletList'
import TransactionTable from '../components/transactions/TransactionTable'
import PeriodList from "../components/StatisticsAndCategories/PeriodList";
import RegularPayment from "../components/RegularPayment/RegularPayment";
import s from '../Pages/Home.module.css'
import AddTransactionModal from '../components/transactions/AddTransactionModal'
import CategoryLimitsModal from "../components/limits/CategoryLimitsModal";
import DefaultCategoriesService from "../services/DefaultCategoriesService";
import { useDispatch } from "react-redux";
import { selectDefaultCategories } from "../features/defaultCategories";
import LimitAlert from "../components/limits/LimitAlert";

const Home = () => {

    const dispatch = useDispatch()

    useEffect(async () => {
        const categories = await DefaultCategoriesService.loadDefaultCategories()
        dispatch(selectDefaultCategories(categories.data));
    }, [])

    return (
        <>
            <div className={s.controls}>
                <WalletList />
                <CategoryLimitsModal />
                <PeriodList />
            </div>
            <div className={s.container}>

                <div className={s.column}>
                    <RegularPayment />
                </div>
                
                <div className={s.column}>
                    <StatisticsAndCategories />
                    <WalletBalance />
                    {AddTransactionModal()}
                </div>

                <div className={s.column}>
                    <TransactionTable />
                </div>
            </div>

            <LimitAlert />
        </>
    );
};

export default Home;
