import request from "../../shared/api/lib/request";
import config from "../../sos-config";
import { debug } from "util";

const requestProjectListType = "REQUEST_PROJECT_LIST";
const requestProjectListSuccessType = "RECEIVE_PROJECT_LIST_SUCCESS";
const requestProjectListErrorType = "RECEIVE_PROJECT_LIST_ERROR"

const requestTrackerListType = "REQUEST_TRACKER_LIST";
const requestTrackerListSuccessType = "RECEIVE_TRACKER_LIST_SUCCESS";
const requestTrackerListErrorType = "RECEIVE_TRACKER_LIST_ERROR";

const requestTrackerAddType = "REQUEST_TRACKER_ADD";
const requestTrackerAddSuccessType = "RECEIVE_TRACKER_ADD_SUCCESS";
const requestTrackerAddErrorType = "RECEIVE_TRACKER_ADD_ERROR";

const requestRecordListType = "REQUEST_RECORD_LIST";
const requestRecordListSuccessType = "RECEIVE_RECORD_LIST_SUCCESS";
const requestRecordListErrorType = "RECEIVE_RECORD_LIST_ERROR";

const requestRecordAddType = "REQUEST_RECORD_ADD";
const requestRecordAddSuccessType = "RECEIVE_RECORD_ADD_SUCCESS";
const requestRecordAddErrorType = "RECEIVE_RECORD_ADD_ERROR";


const initialState = { projects: [], records: [], record: null, trackers: [], tracker: null, loading: false, message: null };

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
    requestTrackerList: params => async (dispatch, getState) => {

        let query = '';
        if (params.record) {
            query = '?record='.concat(params.record);
        }   

        dispatch({ type: requestTrackerListType });

        const end_point = `/api/tracker/logtimes`.concat(query);
        const response = await request({
            url: `${end_point}`,
            method: "GET",
            baseURL: config.apiGateway.API_URL
        });


        if (response.data.message === 'Ok') {
            const result = response.data.result;
            dispatch({
                type: requestTrackerListSuccessType,
                data: result
            });
        }
        else {
            dispatch({
                type: requestTrackerListErrorType,
                message: {
                    type: "ERROR",
                    content: response.data.message,
                    errors: response.data.errors
                }
            });
        }
    },
    requestRecordList: params => async (dispatch, getState) => {

        let query = '';
        if (params.project) {
            query = '?project='.concat(params.project);
        }   

        dispatch({ type: requestRecordListType });

        const end_point = `/api/tracker`.concat(query);
        const response = await request({
            url: `${end_point}`,
            method: "GET",
            baseURL: config.apiGateway.API_URL
        });


        if (response.data.message === 'Ok') {
            const result = response.data.result;
            dispatch({
                type: requestRecordListSuccessType,
                data: result
            });
        }
        else {
            dispatch({
                type: requestRecordListErrorType,
                message: {
                    type: "ERROR",
                    content: response.data.message,
                    errors: response.data.errors
                }
            });
        }
    },
    requestRecordAdd: params => async (dispatch, getState) => {

        dispatch({ type: requestRecordAddType });

        const end_point = `/api/tracker/records`;
        const response = await request({
            url: `${end_point}`,
            method: "POST",
            baseURL: config.apiGateway.API_URL,
            data: params
        });


        if (response.data.message === 'Ok') {
            const result = response.data.result;
            dispatch({
                type: requestRecordAddSuccessType,
                data: result
            });
        }
        else {
            dispatch({
                type: requestRecordAddErrorType,
                message: {
                    type: "ERROR",
                    content: response.data.message,
                    errors: response.data.errors
                }
            });
        }
    },
    requestTrackerAdd: params => async (dispatch, getState) => {

        dispatch({ type: requestTrackerAddType });

        const end_point = `/api/tracker`;
        const response = await request({
            url: `${end_point}`,
            method: "POST",
            baseURL: config.apiGateway.API_URL,
            data: params
        });


        if (response.data.message === 'Ok') {
            const result = response.data.result;
            dispatch({
                type: requestTrackerAddSuccessType,
                data: result
            });
        }
        else {
            dispatch({
                type: requestTrackerAddErrorType,
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

    // list project
    if (action.type === requestProjectListType) {
        return {
            ...state,
            loading: true
        };
    }

    if (action.type === requestProjectListSuccessType) {
        return {
            ...state,
            loading: false,
            projects: action.data
        };
    }

    if (action.type === requestProjectListErrorType) {
        return {
            ...state,
            loading: false,
            message: action.message
        };
    }

    // list traker
    if (action.type === requestTrackerListType) {
        return {
            ...state,
            loading: true,
            trackers: []
        };
    }

    if (action.type === requestTrackerListSuccessType) {
        return {
            ...state,
            loading: false,
            trackers: action.data
        };
    }

    if (action.type === requestTrackerListErrorType) {
        return {
            ...state,
            loading: false,
            message: action.message
        };
    }

    // list records
    if (action.type === requestRecordListType) {
        return {
            ...state,
            loading: true,
            records: []
        };
    }

    if (action.type === requestRecordListSuccessType) {
        return {
            ...state,
            loading: false,
            records: action.data
        };
    }

    if (action.type === requestRecordListErrorType) {
        return {
            ...state,
            loading: false,
            message: action.message,
            records: []
        };
    }

    // add record
    if (action.type === requestRecordAddType) {
        return {
            ...state,
            loading: true,
            record: null
        };
    }

    if (action.type === requestRecordAddSuccessType) {
        return {
            ...state,
            loading: false,
            record: action.data
        };
    }

    if (action.type === requestRecordAddErrorType) {
        return {
            ...state,
            loading: false,
            message: action.message,
            record: null
        };
    }

    // add tracker
    if (action.type === requestTrackerAddType) {
        return {
            ...state,
            loading: true,
            tracker: null
        };
    }

    if (action.type === requestTrackerAddSuccessType) {
        return {
            ...state,
            loading: false,
            tracker: action.data
        };
    }

    if (action.type === requestTrackerAddErrorType) {
        return {
            ...state,
            loading: false,
            message: action.message,
            tracker: null
        };
    }

    return state;
};