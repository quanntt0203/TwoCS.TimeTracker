import request from "../../shared/api/lib/request";
import config from "../../sos-config";
import { debug } from "util";

const requestProjectListType = "REQUEST_PROJECT_LIST";
const requestProjectListSuccessType = "RECEIVE_PROJECT_LIST_SUCCESS";
const requestProjectListErrorType = "RECEIVE_PROJECT_LIST_ERROR"

const requestLogTimeListType = "REQUEST_TRACKER_LIST";
const requestLogTimeListSuccessType = "RECEIVE_TRACKER_LIST_SUCCESS";
const requestLogTimeListErrorType = "RECEIVE_TRACKER_LIST_ERROR";

const requestLogTimeAddType = "REQUEST_TRACKER_ADD";
const requestLogTimeAddSuccessType = "RECEIVE_TRACKER_ADD_SUCCESS";
const requestLogTimeAddErrorType = "RECEIVE_TRACKER_ADD_ERROR";

const requestRecordListType = "REQUEST_RECORD_LIST";
const requestRecordListSuccessType = "RECEIVE_RECORD_LIST_SUCCESS";
const requestRecordListErrorType = "RECEIVE_RECORD_LIST_ERROR";

const requestRecordAddType = "REQUEST_RECORD_ADD";
const requestRecordAddSuccessType = "RECEIVE_RECORD_ADD_SUCCESS";
const requestRecordAddErrorType = "RECEIVE_RECORD_ADD_ERROR";

const requestRecordDetailType = "REQUEST_RECORD_DETAIL";
const requestRecordDetailSuccessType = "RECEIVE_RECORD_DETAIL_SUCCESS";
const requestRecordDetailErrorType = "RECEIVE_RECORD_DETAIL_ERROR";


const initialState = { projects: [], records: [], record: null, logTime: null, loading: false, message: null };

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
    requestLogTimeList: params => async (dispatch, getState) => {

        let query = '';
        if (params.record) {
            query = '?record='.concat(params.record);
        }   

        dispatch({ type: requestLogTimeListType });

        const end_point = `/api/tracker/logtimes`.concat(query);
        const response = await request({
            url: `${end_point}`,
            method: "GET",
            baseURL: config.apiGateway.API_URL
        });


        if (response.data.message === 'Ok') {
            const result = response.data.result;
            dispatch({
                type: requestLogTimeListSuccessType,
                data: result
            });
        }
        else {
            dispatch({
                type: requestLogTimeListErrorType,
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
            query = `?project=${params.project}&page=${params.pageIndex}`;
        }   

        dispatch({ type: requestRecordListType });

        const end_point = `/api/tracker${query}`;
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
    requestRecordDetail: params => async (dispatch, getState) => {

        let query = '';
        if (!params.recordId) {
            return;
        }

        query = '/'.concat(params.recordId);

        dispatch({ type: requestRecordDetailType });

        const end_point = `/api/tracker`.concat(query);
        const response = await request({
            url: `${end_point}`,
            method: "GET",
            baseURL: config.apiGateway.API_URL
        });

        if (response.data.message === 'Ok') {
            const result = response.data.result;
            dispatch({
                type: requestRecordDetailSuccessType,
                data: result
            });
        }
        else {
            dispatch({
                type: requestRecordDetailErrorType,
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
    requestLogTimeAdd: params => async (dispatch, getState) => {

        dispatch({ type: requestLogTimeAddType });

        const end_point = `/api/tracker/logtimes`;
        const response = await request({
            url: `${end_point}`,
            method: "POST",
            baseURL: config.apiGateway.API_URL,
            data: params
        });


        if (response.data.message === 'Ok') {
            const result = response.data.result;
            dispatch({
                type: requestLogTimeAddSuccessType,
                data: result
            });
        }
        else {
            dispatch({
                type: requestLogTimeAddErrorType,
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
            loading: true,
            message: null
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
    if (action.type === requestLogTimeListType) {
        return {
            ...state,
            loading: true,
            message: null
        };
    }

    if (action.type === requestLogTimeListSuccessType) {
        return {
            ...state,
            loading: false,
            trackers: action.data
        };
    }

    if (action.type === requestLogTimeListErrorType) {
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
            message: null
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
            message: action.message
        };
    }

    // record detail
    if (action.type === requestRecordDetailType) {
        return {
            ...state,
            loading: true,
            message: null
        };
    }

    if (action.type === requestRecordDetailSuccessType) {

        return {
            ...state,
            loading: false,
            record: action.data
        };
    }

    if (action.type === requestRecordDetailErrorType) {
        return {
            ...state,
            loading: false,
            message: action.message,
            record: null
        };
    }
    
    

    // add record
    if (action.type === requestRecordAddType) {
        return {
            ...state,
            loading: true,
            message: null
        };
    }

    if (action.type === requestRecordAddSuccessType) {

        const { records } = state;
        records.push(action.data);

        return {
            ...state,
            loading: false,
            records: records
        };
    }

    if (action.type === requestRecordAddErrorType) {
        return {
            ...state,
            loading: false,
            message: action.message
        };
    }

    // add tracker
    if (action.type === requestLogTimeAddType) {
        return {
            ...state,
            loading: true,
            message: null,
            records: []
        };
    }

    if (action.type === requestLogTimeAddSuccessType) {

        const { record } = state;

        if (record) {

            record.duration += action.data.duration;
            record.logTimeRecords.push(action.data);

            return {
                ...state,
                loading: false,
                logTime: action.data
            };
        }
    }

    if (action.type === requestLogTimeAddErrorType) {
        return {
            ...state,
            loading: false,
            message: action.message,
            logTime: null
        };
    }

    return state;
};