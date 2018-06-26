import request from "../../shared/api/lib/request";
import config from "../../sos-config";
import { debug } from "util";

const requestUserListType = "REQUEST_USER_LIST";
const requestUserListSuccessType = "RECEIVE_USER_LIST_SUCCESS";
const requestUserListErrorType = "RECEIVE_USER_LIST_ERROR";

const requestManagerListType = "REQUEST_MANAGER_LIST";
const requestManagerListSuccessType = "RECEIVE_MANAGER_LIST_SUCCESS";
const requestManagerListErrorType = "RECEIVE_MANAGER_LIST_ERROR"

const requestProjectListType = "REQUEST_PROJECT_LIST";
const requestProjectListSuccessType = "RECEIVE_PROJECT_LIST_SUCCESS";
const requestProjectListErrorType = "RECEIVE_PROJECT_LIST_ERROR";

const promoteUserToManagerType = "PROMOTE_USER_TO_MANAGER";
const promoteUserToManagerSuccessType = "PROMOTE_USER_TO_MANAGER_SUCCESS";
const promoteUserToManagerErrorType = "PROMOTE_USER_TO_MANAGER_ERROR";

const requestUserDetailType = "REQUEST_DETAIL";
const requestUserDetailSuccessType = "REQUEST_DETAIL_SUCCESS";
const requestUserDetailErrorType = "REQUEST_DETAIL_ERROR";
 
const signInAsManagerType = "SIGNIN_AS_MANAGER";
const signInAsManagerSuccessType = "SIGNIN_AS_MANAGER_SUCCESS";
const signInAsManagerErrorType = "SIGNIN_AS_MANAGER_ERROR";


const assignProjectToUserType = "ASSIGN_PROJECT_USER";
const assignProjectToUserSuccessType = "ASSIGN_PROJECT_USER_SUCCESS";
const assignProjectToUserErrorType = "ASSIGN_PROJECT_USER_ERROR";


const assignMemberToManagerType = "ASSIGN_MEMBER_MANAGER";
const assignMemberToManagerSuccessType = "ASSIGN_MEMBER_MANAGER_SUCCESS";
const assignMemberToManagerErrorType = "ASSIGN_MEMBER_MANAGER_ERROR";

const initialState = { users: [], loading: false, message: null, userDetail: null, admin: null, managers: [], projects: [] };

export const actionCreators = {
    
    requestUserList: params => async (dispatch, getState) => {

        dispatch({
            type: requestUserListType,
            message: null });

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
    requestManagerList: params => async (dispatch, getState) => {

        dispatch({
            type: requestManagerListType,
            message: null
        });

        const end_point = `/api/user/managers`;
        const response = await request({
            url: `${end_point}`,
            method: "GET",
            baseURL: config.apiGateway.API_URL
        });


        if (response.data.message === 'Ok') {
            const managers = response.data.result;
            dispatch({
                type: requestManagerListSuccessType,
                data: managers
            });
        }
        else {
            dispatch({
                type: requestManagerListErrorType,
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
    },
    promoteUserToManager: params => async (dispatch, getState) => {

        dispatch({
            type: promoteUserToManagerType,
            message: null });

        const end_point = `/api/user/promotions`;
        const response = await request({
            url: `${end_point}`,
            method: "POST",
            baseURL: config.apiGateway.API_URL,
            data: params
        });


        if (response.data.message === 'Ok') {
            const user = response.data.result;
            dispatch({
                type: promoteUserToManagerSuccessType,
                data: user,
                message: {
                    type: "SUCCESS",
                    content: response.data.message
                }
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
    },
    assignMemberToManager: params => async (dispatch, getState) => {

        dispatch({
            type: assignMemberToManagerType,
            message: null });

        const end_point = `/api/user/assignment-members`;
        const response = await request({
            url: `${end_point}`,
            method: "POST",
            baseURL: config.apiGateway.API_URL,
            data: params
        });


        if (response.data.message === 'Ok') {
            const user = response.data.result;
            dispatch({
                type: assignMemberToManagerSuccessType,
                data: user,
                message: {
                    type: "SUCCESS",
                    content: response.data.message
                }
            });
        }
        else {
            dispatch({
                type: assignMemberToManagerErrorType,
                message: {
                    type: "ERROR",
                    content: response.data.message,
                    errors: response.data.errors
                }
            });
        }
    },
    assignProjectToUser: params => async (dispatch, getState) => {

        dispatch({
            type: assignProjectToUserType,
            message: null
        });

        const end_point = `/api/user/assignment-projects`;
        const response = await request({
            url: `${end_point}`,
            method: "POST",
            baseURL: config.apiGateway.API_URL,
            data: params
        });


        if (response.data.message === 'Ok') {
            const user = response.data.result;
            dispatch({
                type: assignProjectToUserSuccessType,
                data: user,
                message: {
                    type: "SUCCESS",
                    content: response.data.message
                }
            });
        }
        else {
            dispatch({
                type: assignProjectToUserErrorType,
                message: {
                    type: "ERROR",
                    content: response.data.message,
                    errors: response.data.errors
                }
            });
        }
    },
    signInAsManager: params => async (dispatch, getState) => {

        dispatch({
            type: signInAsManagerType,
            message: null

        });

        const end_point = `/api/user/signIn-managers`;
        const response = await request({
            url: `${end_point}`,
            method: "POST",
            baseURL: config.apiGateway.API_URL,
            data: params
        });


        if (response.data.message === 'Ok') {
            const user = response.data.result;
            localStorage.setItem("identity", JSON.stringify(user));
            dispatch({
                type: signInAsManagerSuccessType,
                data: user,
                message: {
                    type: "SUCCESS",
                    content: response.data.message
                }
            });
        }
        else {
            dispatch({
                type: signInAsManagerErrorType,
                message: {
                    type: "ERROR",
                    content: response.data.message,
                    errors: response.data.errors
                }
            });
        }
    },
    requestUserDetail: params => async (dispatch, getState) => {
        if (!params.user) {
            return;
        }

        dispatch({
            type: requestUserDetailType
            , message: null
        });
        const end_point = `/api/user/details/${params.user}`;
        const response = await request({
            url: `${end_point}`,
            method: "GET",
            baseURL: config.apiGateway.API_URL
        });

        //debugger

        if (response.data.message === 'Ok') {
            const user = response.data.result;
            dispatch({
                type: requestUserDetailSuccessType,
                data: user
            });
        }
        else {
            dispatch({
                type: requestUserDetailErrorType,
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

    // list user
    if (action.type === requestUserListType) {
        return {
            ...state,
            loading: true,
            message: null,
            users: []
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

    // list manager
    if (action.type === requestManagerListType) {
        return {
            ...state,
            loading: true,
            message: null,
            managers: []
        };
    }

    if (action.type === requestManagerListSuccessType) {
        return {
            ...state,
            loading: false,
            managers: action.data
        };
    }

    if (action.type === requestManagerListErrorType) {
        return {
            ...state,
            loading: false,
            message: action.message
        };
    }

    // project list
    if (action.type === requestProjectListType) {
        return {
            ...state,
            loading: true,
            message: null,
            projects:[]
        };
    }

    if (action.type === requestProjectListSuccessType) {
        return {
            ...state,
            loading: false,
            projects: action.data,
            message: null
        };
    }
    
    if (action.type === requestProjectListErrorType) {
        return {
            ...state,
            loading: true,
            message: action.message
        };

    }

    // admin promote user to manager
    if (action.type === promoteUserToManagerType) {
        return {
            ...state,
            loading: true,
            userDetail: null,
            message: null
        };
    }

    if (action.type === promoteUserToManagerSuccessType) {
        return {
            ...state,
            loading: false,
            userDetail: action.data,
            message: action.message
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

    // user detail
    if (action.type === requestUserDetailType) {
        return {
            ...state,
            message: null,
            loading: true
        };
    }

    if (action.type === requestUserDetailSuccessType) {
        return {
            ...state,
            loading: false,
            userDetail: action.data
        };
    }

    if (action.type === requestUserDetailErrorType) {
        return {
            ...state,
            loading: false,
            message: action.message
        };
    }

    //signInAsManager
    if (action.type === signInAsManagerType) {
        return {
            ...state,
            message: null,
            loading: true
        };
    }

    if (action.type === signInAsManagerSuccessType) {
        return {
            ...state,
            loading: false,
            userDetail: action.data,
            message: action.message
        };
    }

    if (action.type === signInAsManagerErrorType) {
        return {
            ...state,
            loading: false,
            message: action.message
        };
    }

    //assignProjectToUser
    if (action.type === assignProjectToUserType) {
        return {
            ...state,
            message: null,
            loading: true
        };
    }

    if (action.type === assignProjectToUserSuccessType) {
        return {
            ...state,
            loading: false,
            userDetail: action.data,
            message: action.message
        };
    }

    if (action.type === assignProjectToUserErrorType) {
        return {
            ...state,
            loading: false,
            message: action.message
        };
    }

    //assignMemberToManager
    if (action.type === assignMemberToManagerType) {
        return {
            ...state,
            message: null,
            loading: true
        };
    }

    if (action.type === assignMemberToManagerSuccessType) {
        return {
            ...state,
            loading: false,
            userDetail: action.data,
            message: action.message
        };
    }

    if (action.type === assignMemberToManagerErrorType) {
        return {
            ...state,
            loading: false,
            message: action.message
        };
    }

    return state;
};