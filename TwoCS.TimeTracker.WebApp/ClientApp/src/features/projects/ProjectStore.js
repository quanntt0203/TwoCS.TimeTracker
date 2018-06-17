import request from "../../shared/api/lib/request";
import config from "../../sos-config";

const requestProjectAddType = "REQUEST_PROJECT_ADD";
const requestProjectAddSuccessType = "REQUEST_PROJECT_ADD_SUCCESS";
const requestProjectAddErrorType = "REQUEST_PROJECT_ADD_ERROR";

const requestProjectListType = "REQUEST_PROJECT_LIST";
const requestProjectListSuccessType = "RECEIVE_PROJECT_LIST_SUCCESS";
const requestProjectListErrorType = "RECEIVE_PROJECT_LIST_ERROR"

const initialState = { projects: [], item: null, loading: false, message: null };

export const actionCreators = {
    requestProjectAdd: params => async (dispatch, getState) => {

        dispatch({ type: requestProjectAddType });

        const end_point = `/api/project`;
        const response = await request({
            url: `${end_point}`,
            method: "POST",
            baseURL: config.apiGateway.API_URL,
            data: params
        });

        if (response.data.message === 'Ok') {
            const project = response.data.result;
            dispatch({
                type: requestProjectAddSuccessType,
                data: project
            });
        }
        else {
            dispatch({
                type: requestProjectAddErrorType,
                message: {
                    type: "ERROR",
                    content: response.data.message,
                    errors: response.data.errors
                }
            });
        }

    },
    requestProjectList: params => async (dispatch, getState) => {

        dispatch({ type: requestProjectListType });

        const end_point = `/api/project`;
        const response = await request({
            url: `${end_point}`,
            method: "GET",
            baseURL: config.apiGateway.API_URL
        });

        if (response.data.message === 'Ok') {
            const projects = response.data.result;
            dispatch({
                type: requestProjectListSuccessType,
                data: projects
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
    }
};

export const reducer = (state, action) => {

    state = state || initialState;

    // add project
    if (action.type === requestProjectAddType) {
        return {
            ...state,
            loading: true
        };
    }

    if (action.type === requestProjectAddSuccessType) {
        return {
            ...state,
            loading: false,
            item: action.data
        };
    }

    if (action.type === requestProjectAddErrorType) {
        return {
            ...state,
            loading: false,
            message: action.message
        };
    }

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

    return state;
};