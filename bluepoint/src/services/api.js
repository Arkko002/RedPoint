import Axios from "axios";

const API_BASE_URL = "http://localhost:5000"; //TODO
const DEV = process.env.NODE_ENV;

export default Axios.create({
	baseURL: API_BASE_URL,
	headers: {
		"Authorization": "Bearer " + localStorage.getItem("userToken"),
		"Accept": "application/json",
		"Content-Type": "application/json"
	}
});

Axios.interceptors.request.use((config) => {
	if (DEV) {
		console.log(config);
	}
	
	return config;
}, (error) => {
	//TODO error handling
	if (DEV) {
		console.log(error);
	}
	
	return Promise.reject(error);
});

Axios.interceptors.response.use((response) => {
	if (DEV) {
		console.log(response);
	}
	
	return response.data;
}, (error) => {
	if (DEV) {
		console.log(error);
	}

	if (error.response && error.response.data) {
		//TODO Global error handling
	}
	
	return Promise.reject(error);
});
