import request from "../../shared/api/lib/request";
import config from "../../sos-config";
import { debug } from "util";

const requestUserListType = "REQUEST_USER_LIST";
const requestUserListSuccessType = "RECEIVE_USER_LIST_SUCCESS";
const requestUserListErrorType = "RECEIVE_USER_LIST_ERROR"

const promoteUserToManagerType = "PROMOTE_USER_TO_MANAGER";
const promoteUserToManagerSuccessType = "PROMOTE_USER_TO_MANAGER_SUCCESS";
const promoteUserToManagerErrorType = "PROMOTE_USER_TO_MANAGER_ERROR";

const initialState = { users: [], loading: false, message: null, promoteUser: null };

export const actionCreators = {
    
    requestUserList: params => async (dispatch, getState) => {

        dispatch({ type: requestUserListType });

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
    promoteUserToManager: params => async (dispatch, getState) => {

        dispatch({ type: promoteUserToManagerType });

        const end_point = `/api/user/promotions`;
        const response = await request({
            url: `${end_point}`,
            method: "POST",
            baseURL: config.apiGateway.API_URL,
            data: params
        });


        if (response.data.message === 'Ok') {
            const users = response.data.result;
            dispatch({
                type: promoteUserToManagerSuccessType,
                data: users
            });
        }
        else {
            dispatch({
                type: promoteUserToManagerErrorType,
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
    if (action.type === requestUserListType) {
        return {
            ...state,
            loading: true
        };
    }

    if (action.type === requestUserListSuccessType) {
        return {
            ...state,
            loading: false,
            users: action.data
        };
    }

    if (action.type === requestUserListErrorType) {
        return {
            ...state,
            loading: false,
            message: action.message
        };
    }

    // admin promote user to manager
    if (action.type === promoteUserToManagerType) {
        return {
            ...state,
            loading: true,
            promoteUser: null
        };
    }

    if (action.type === promoteUserToManagerSuccessType) {
        return {
            ...state,
            loading: false,
            promoteUser: action.data
        };
    }

    if (action.type === promoteUserToManagerErrorType) {
        return {
            ...state,
            loading: false,
            message: action.message,
            promoteUser: null
        };
    }

    return state;
};