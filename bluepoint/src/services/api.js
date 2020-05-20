import Axios from "axios";

const API_BASE_URL = "TODO"; //TODO

export default Axios.create({
	baseURL: API_BASE_URL,
	headers: {
		"Authorization": "Bearer " + JSON.parse(localStorage.getItem("user")),
		"Accept" : "application/json",
		"Content-Type": "application/json"
	}
});

Axios.interceptors.response.use((response) => {
	return response.data;
}, (error) => {
	if (error.response && error.response.data){
		//TODO Global error handling
	}
	return Promise.reject(error);
});
