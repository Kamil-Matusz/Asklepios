import axios from 'axios';
import { addErrorInterceptor } from './interceptors/errorInterceptors';
import { addJwtInterceptor } from './interceptors/jwtInterceptor';

const httpClient = axios.create({
  baseURL: "http://localhost:5102",
});

addErrorInterceptor(httpClient);
addJwtInterceptor(httpClient);

export default httpClient
