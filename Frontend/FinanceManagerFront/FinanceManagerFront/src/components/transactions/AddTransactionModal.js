import React from 'react';
//import './TransactionModalStyles.css'
import { useState, useEffect } from "react";
import { useSelector } from 'react-redux';
import { useDispatch } from 'react-redux';
import { apiClient } from '../../client';
import { selectTransaction } from '../../features/lastTransaction';
import TransactionService from '../../services/TransactionService';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import { ButtonGroup } from 'react-bootstrap';
import DefaultCategoriesDropdown from '../StatisticsAndCategories/DefaultCategoriesDropdown'
import s from '../StatisticsAndCategories/DefaultCategoriesDropdown.module.css'
import { Line } from 'react-chartjs-2';
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend,
  } from 'chart.js';

  ChartJS.register(
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend
  );

export default function AddTransaction(props) {

    const [transactionSize, setTransactionSize] = useState(0);
    const [show, setShow] = useState(false);
    const [isTransactioSumPositive, setTransactionSumType] = useState(false);
    const defaultSelect = { id: -1, name: "Select category" }
    const [category, setCategory] = useState(defaultSelect)

    const [showPrediction, setShowPrediction] = useState(false);
    const [showScanReceipt, setShowScanReceipt] = useState(false);

    const [chartData, setChartData] = React.useState([])
    const wallet = useSelector((state) => state.currentWallet.value)
    const currentCategory = useSelector((state) => state.currentCategory.value)

    let dispatch = useDispatch();
   
    const handleClose = () => {
        setShow(false);
    }

    const handlePredictionClose = () => {
        setShowPrediction(false);
    }

    const handleScanReceiptClose = () => {
        setShowScanReceipt(false);
        setPreviewSrc(null);
    }

    const handlePutShow = () => {
        if(wallet.id === -1){
            window.alert('You have to choose wallet first');
            return;
        }

        setShow(true);
        setTransactionSumType(true);
    }

    const handleSpentShow = () => {
        if(wallet.id === -1){
            window.alert('You have to choose wallet first');
            return;
        }

        setShow(true);
        setTransactionSumType(false);
    }

    const handleAdd = () => {
       
        if (transactionSize <= 0) {
            window.alert('You have to enter positive sum for transaction');
            return;
        }
        if(category.id === -1){
            window.alert('You have to choose category');
            return;
        }

        if (isTransactioSumPositive === true) {
            const put = async () => {
                let transaction = await TransactionService.createPutTransaction(wallet, transactionSize, category);
                dispatch(selectTransaction(transaction))
            }
            put();
        } else {
            const spend = async () => {
                let transaction = await TransactionService.createSpentTransaction(wallet, transactionSize, category);
                dispatch(selectTransaction(transaction))
            }
            spend();
        }
        setTransactionSize(0);
        handleClose();
    }

    const handleSumInput = (moneyAmount) => {
        if(moneyAmount < 0){
            setTransactionSize(0);
        } else {
            setTransactionSize(moneyAmount);
        }
    }

    const handlePredictionShow = () => {
        if(wallet.id === -1){
            window.alert('You have to choose wallet first');
            return;
        }

        if(currentCategory.id === -1) {
            window.alert('You have to choose category first');
            return;
        }

        // apiClient.get('python/prediction/' + currentCategory.id + '/' + wallet.id)
        //         .then(response => console.log(response.data));

        setShowPrediction(true);
    }

    const handleScanReceiptShow = () => {
        setShowScanReceipt(true);
    }

    const data = {
        labels: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20', '21', '22', '23', '24', '25', '26', '27', '28', '29', '30',], // Add labels if you have specific day labels
        datasets: [
          {
            label: '',
            data: [-4668, -5102, -5484, -5827, -5980, -6305, -6681, -7359, -7952, -8556, -8778, -9344, -9904, -10675, -11319, -11867, -12185, -12648, -13246, -14034, -14350, -14830, -14753, -15051, -15464, -15974, -16138, -15939, -15755, -15558, -15369], // Your data here
            fill: false,
            borderColor: 'rgb(75, 192, 192)',
            tension: 0.1,
          },
        ],
      };
      
      const options = {
        scales: {
          y: {
            beginAtZero: true,
          },
        },
      };

      const [selectedFile, setSelectedFile] = useState(null);
      const [previewSrc, setPreviewSrc] = useState(null);
  
      const handleFileChange = (event) => {
          const file = event.target.files[0];
          setSelectedFile(file);
  
          // Create a preview of the selected image
          const reader = new FileReader();
          reader.onloadend = () => {
              setPreviewSrc(reader.result);
          };
          reader.readAsDataURL(file);
      };
  
      const handleUpload = async () => {
          if (!selectedFile) return;
  
          const formData = new FormData();
          formData.append('file', selectedFile);
  
          
          try {
            const response = await apiClient.post(
                process.env.REACT_APP_API_URL + "python/ocr",
                formData
            );
            
            if (response.status === 200) {
                setTransactionSize(response.data.result)
            } else {
                window.alert('Upload failed');
            }
          } catch (error) {
                window.alert('Error uploading file:' + error);
          }

          handleScanReceiptClose();
      };

    return (
        <React.Fragment>
            <ButtonGroup>
                <Button variant="warning" onClick={handlePredictionShow}><div style={{ width: "40px"}}><i class="fa-solid fa-chart-line"></i></div></Button>
                
                <Modal show={showPrediction} onHide={handlePredictionClose}>
                    <Modal.Header closeButton>
                        <Modal.Title style={{marginLeft: "auto"}}>AI Prediction</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <div style={{height: "300px"}}>
                            <Line data={data} options={options} />
                        </div>
                        <div><b>Day when spendings will reach -15200 is 25</b></div>
                    </Modal.Body>
                </Modal>

                <Button variant="success" onClick={handlePutShow}><div style={{ width: "40px" }}>+</div></Button>
                <Button variant="danger" onClick={handleSpentShow}><div style={{ width: "40px" }}>-</div></Button>
            </ButtonGroup>
            <Modal show={show} onHide={handleClose} >
                <Modal.Header closeButton>
                    <Modal.Title>Create transaction</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <div>Enter the transaction size</div>
                    <input placeholder='Sum of money' type='number' min='1' value={transactionSize} onChange={(event) => {handleSumInput(event.target.value)}}></input>
                    <Button variant="info" onClick={handleScanReceiptShow} style={{marginLeft: "20px"}}><div style={{ width: "40px"}}><i class="fa solid fa-receipt"></i></div></Button>
                        <Modal show={showScanReceipt} onHide={handleScanReceiptClose} >
                            <Modal.Header><Modal.Title>Load Receipt</Modal.Title></Modal.Header>
                            <Modal.Body>
                                <div>
                                    <input type="file" accept="image/*" onChange={handleFileChange} />
                                    <button onClick={handleUpload}>Upload</button>
                                    <div>
                                    {previewSrc && <img src={previewSrc} alt="Selected Preview" width="250" />}</div>
                                    </div>
                            </Modal.Body>
                        </Modal>
                    <div>Choose category</div>
                    <DefaultCategoriesDropdown className={s.dropdown} category={category} setCategory={setCategory} />
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="primary" onClick={handleAdd}>
                        Add
                    </Button>
                </Modal.Footer>
            </Modal>
        </React.Fragment>
    )
}