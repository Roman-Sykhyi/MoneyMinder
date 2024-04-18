import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { configureStore } from "@reduxjs/toolkit";
import { Provider } from 'react-redux';
import walletReducer from './features/currentWallet';
import periodReducer from './features/currentPeriod'
import categoryReducer from './features/currentCategory'
import updateReducer from './features/shouldUpdate'
import transactionReducer from './features/lastTransaction'
import defaultCategoriesReducer from './features/defaultCategories'



const store = configureStore({
  reducer: {
    currentWallet: walletReducer,
    currentPeriod: periodReducer,
    currentCategory: categoryReducer,
    shouldUpdate: updateReducer,
    lastTransaction: transactionReducer,
    defaultCategories: defaultCategoriesReducer,
  },
});

ReactDOM.render(
  <React.StrictMode>
    <Provider store={store}>
      <App />
    </Provider>
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
