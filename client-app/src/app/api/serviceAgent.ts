import axios, { AxiosResponse } from "axios"
import { User, UserLogin, UserRegister, UserVerify } from "../models/user";
import { store } from '../stores/store';

// Define default URL
axios.defaults.baseURL = "http://localhost:5184";

const responseBody = (response: AxiosResponse) => response.data;

//Use an axios interceptor to pass Standard Authorization header using Bearer scheme
axios.interceptors.request.use(config => {
    const token = store.commonStore.token;
    if (token && config.headers) config.headers.Authorization = `Bearer ${token}`;
    return config;
})

const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body: {}) => axios.post<T>(url, body, { withCredentials: true }).then(responseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    del: <T>(url: string) => axios.delete<T>(url).then(responseBody),
}

const Records = {
    list: () => requests.get('api/Album/GetAll')
}

const Account = {
    current: () => requests.get<User>('/Auth/GetCurrentUser'),
    login: (user: UserLogin) => requests.post("Auth/Login", user),
    register: (user: UserRegister) => requests.post("Auth/Register", user),
    verify: (token: UserVerify) => requests.post("Auth/Verify", token),
    refresh: () => requests.post("Auth/RefreshToken", {})
}

const agent = {
    Records,
    Account
}

export default agent;