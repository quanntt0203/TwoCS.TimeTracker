import axios from "axios";
import { getToken } from "../../helper";

/**
 * Create an Axios Client with defaults
 */
var client = axios.create();

/**
 * Request Wrapper with default success/error actions
 */
const request = function (options) {

    const onSuccess = function (response) {
        return response;
    };

    const onError = function (error) {
        console.error("Request Failed:", error.config);
        if (error.response) {
            console.error("Status:", error.response.status);
            console.error("Data:", error.response.data);
            console.error("Headers:", error.response.headers);
        } else {
            console.error("Error Message:", error.message);
        }

        return Promise.reject(error.response || error.message);
    };

    options["headers"] = {
        Authorization: `Bearer ${getToken()}`
    };

    return client(options)
        .then(onSuccess)
        .catch(onError);
};

export default request;
