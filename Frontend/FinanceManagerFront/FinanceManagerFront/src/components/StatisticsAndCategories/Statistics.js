import React, { useEffect } from 'react'
import { useSelector } from 'react-redux';
import { Chart, ArcElement } from 'chart.js';
import { Doughnut } from 'react-chartjs-2'
import { apiClient } from '../../client';
import s from './StatisticsAndCategories.module.css'

export default function Statistics() {
    Chart.register(ArcElement);

    const [statistics, setStatistics] = React.useState([])
    const period = useSelector((state) => state.currentPeriod.value)
    const category = useSelector((state) => state.currentCategory.value)
    const wallet = useSelector((state) => state.currentWallet.value)
    const lastTransaction = useSelector((state) => state.lastTransaction.value);

    useEffect(() => {
        apiClient.get('statistics/' + period + '/' + wallet.id + '/' + category.id)
            .then(response => setStatistics(response.data));
    }, [period, category, lastTransaction, wallet])

    let data = {}

    if (statistics[0] === 0 && statistics[1] === 0) {
        data = {
            responsive: true,
            maintainAspectRatio: true,
            labels: ['Red', 'Green'],
            datasets: [{
                label: 'statistics',
                data: [1, 1],
                backgroundColor: [
                    'rgba(0, 192, 0, 1)'
                ]
            }]
        }
    }
    else {
        data = {
            responsive: true,
            maintainAspectRatio: true,
            labels: ['Red', 'Green'],
            datasets: [{
                label: 'statistics',
                data: [statistics[0], statistics[1]],
                backgroundColor: [
                    'rgba(255, 10, 0, 1)',
                    'rgba(0, 192, 0, 1)',
                ]
            }]
        }
    }

    const options = {
        legend: {
            display: false
        },
        cutout: "80%"
    }

    return (
        <><Doughnut data={data} options={options} /><div className={s.statisticsText}>
            <h1 className={s.spendings}>{statistics[0]}</h1>
            <h1 className={s.earnings}>+{statistics[1]}</h1>
        </div></>
    )
}