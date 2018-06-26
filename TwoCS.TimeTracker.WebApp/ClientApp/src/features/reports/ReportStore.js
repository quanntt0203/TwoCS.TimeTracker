import request from "../../shared/api/lib/request";
import config from "../../sos-config";
import { debug } from "util";

const requestProjectListType = "REQUEST_PROJECT_LIST";
const requestProjectListSuccessType = "RECEIVE_PROJECT_LIST_SUCCESS";
const requestProjectListErrorType = "RECEIVE_PROJECT_LIST_ERROR";

const requestUserListType = "REQUEST_USER_LIST";
const requestUserListSuccessType = "RECEIVE_USER_LIST_SUCCESS";
const requestUserListErrorType = "RECEIVE_USER_LIST_ERROR"

const requestReportListType = "REQUEST_REPORT_LIST";
const requestReportListSuccessType = "RECEIVE_REPORT_LIST_SUCCESS";
const requestReportListErrorType = "RECEIVE_REPORT_LIST_ERROR";


const initialState = { projects: [], users: [], reportData: null, loading: false, message: null };

export const actionCreators = {
    requestProjectList: params => async (dispatch, getState) => {

        dispatch({ type: requestProjectListType });

        const end_point = `/api/project`;
        const response = await request({
            url: `${end_point}`,
            method: "GET",
            baseURL: config.apiGateway.API_URL
        });

        if (response.data.message === 'Ok') {
            const result = response.data.result;
            dispatch({
                type: requestProjectListSuccessType,
                data: result
            });
        }
        else {
            dispatch({
                type: requestProjectListErrorType,
                message: {
                    type: "ERROR",
                    content: response.data.message,
                    errors: response.data.errors
                }
            });
        }
    },
    requestUserList: params => async (dispatch, getState) => {

        dispatch({
            type: requestUserListType,
            message: null
        });

        const end_point = `/api/user`;
        const response = await request({
            url: `${end_point}`,
            method: "GET",
            baseURL: config.apiGateway.API_URL
        });


        if (response.data.message === 'Ok') {
            const users = response.data.result;
            dispatch({
                type: requestUserListSuccessType,
                data: users
            });
        }
        else {
            dispatch({
                type: requestUserListErrorType,
                message: {
                    type: "ERROR",
                    content: response.data.message,
                    errors: response.data.errors
                }
            });
        }
    },
    requestReportList: params => async (dispatch, getState) => {

        let query = '';
        if (params.reportType) {
            query = `?reportType=${params.reportType}&groupType=${params.groupBy}&project=${params.project}&user=${params.user}&startDate=${params.startDate}&endDate=${params.endDate}`;
        }

        dispatch({ type: requestReportListType });

        const end_point = `/api/reports`.concat(query);
        const response = await request({
            url: `${end_point}`,
            method: "GET",
            baseURL: config.apiGateway.API_URL
        });

        //debugger

        if (response.data.message === 'Ok') {
            const result = response.data.result;
            dispatch({
                type: requestReportListSuccessType,
                data: result
            });
        }
        else {
            dispatch({
                type: requestReportListErrorType,
                message: {
                    type: "ERROR",
                    content: response.data.message,
                    errors: response.data.errors
                }
            });
        }
    }
};

export const reducer = (state, action) => {

    state = state || initialState;

    switch (action.type) {

        default: {
            break;
        }

        // project list
        case requestProjectListType: {
            return {
                ...state,
                loading: true,
                message: null
            };
        }
        case requestProjectListSuccessType: {
            return {
                ...state,
                loading: false,
                projects: action.data,
                message: null
            };
        }
        case requestProjectListErrorType: {
            return {
                ...state,
                loading: true,
                message: action.message
            };

        }

        // user list
        case requestUserListType: {
            return {
                ...state,
                loading: true,
                message: null
            };
        }
        case requestUserListSuccessType: {
            return {
                ...state,
                loading: false,
                users: action.data,
                message: null
            };
        }
        case requestUserListErrorType: {
            return {
                ...state,
                loading: true,
                message: action.message
            };
        }

        // report list
        case requestReportListType: {
            return {
                ...state,
                loading: true,
                message: null
            };
        }
        case requestReportListSuccessType: {
            return {
                ...state,
                loading: false,
                reportData: action.data,
                message: null
            };
        }
        case requestReportListErrorType: {
            return {
                ...state,
                loading: true,
                message: action.message
            };
        }
    }

    return state;
};