import axios from 'axios';

const axiosInstance = axios.create({
  baseURL: process.env.VUE_APP_API_URL, 
  timeout: 40000, 
  headers: {
    'Content-Type': 'application/json',
  },
});

export default axiosInstance;