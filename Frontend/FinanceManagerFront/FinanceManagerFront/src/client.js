import axios from 'axios';

const apiClient = axios.create({
    baseURL: process.env.REACT_APP_API_URL,
});

apiClient.interceptors.request.use(function (config) 
{
    const token = localStorage.getItem('user_token');

    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
}, 
function (error) {
    // Do something with request error
    return Promise.reject(error);
});

export { apiClient };