import axios from 'axios';

const httpClient = axios.create({
  baseURL: "http://localhost:5102",
});


export default httpClient
