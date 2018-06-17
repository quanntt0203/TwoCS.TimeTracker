import request from "../../../shared/api/lib/request";
import config from "../../../sos-config";
import { fail } from "assert";
import { access } from "fs";

const appTokenSchema = "token";
const requestRegisterType = "REQUEST_REGISTER";
const requestRegisterSuccessType = "REQUEST_REGISTER_SUCCESS";
const requestRegisterErrorType = "REQUEST_REGISTER_ERROR";
const requestSignInType = "REQUEST_LOGIN";
const requestSignOutType = "REQUEST_LOGOUT";
const requestSignInSuccessType = "REQUEST_LOGIN_SUCCESS";
const requestSignInErrorType = "REQUEST_LOGIN_ERROR";
const requestSignOutSuccessType = "REQUEST_LOGOUT_SUCCESS"


const initialState = { user: null, isAuthenticated: false, message: null };

export const actionCreators = {
  requestLogin: params => async (dispatch, getState) => {
        if (getState().account.isAuthenticated) {
          return;
        }

        dispatch({ type: requestSignInType });
        const end_point = `/oauth/connect-token`;
        const response = await request({
          url: `${end_point}`,
          method: "POST",
            baseURL: config.apiGateway.API_URL,
          data: params
      });


      if (response.data.message === 'Ok') {
          localStorage.setItem(appTokenSchema, response.data.result.access_token);
          dispatch({
              type: requestSignInSuccessType,
              message: {
                  type: "SUCCESS",
                  content: response.data.message
              }
          });
      }
      else {
          dispatch({
              type: requestSignInErrorType,
              message: {
                  type: "ERROR",
                  content: response.data.message,
                  errors: response.data.errors
              }
          });
      }
    },
    requestLogout: params => async (dispatch, getState) => {
        localStorage.removeItem(appTokenSchema);
        dispatch({ type: requestSignOutSuccessType });
    },
    requestRegister: params => async (dispatch, getState) => {

        dispatch({ type: requestRegisterType });
        const end_point = `/api/account/register-user`;
        const response = await request({
            url: `${end_point}`,
            method: "POST",
            baseURL: config.apiGateway.API_URL,
            data: params
        });

        if (response.data.message === 'Ok') {
            localStorage.setItem(appTokenSchema, response.data.result.access_token);
            dispatch({
                type: requestRegisterSuccessType,
                message: {
                    type: "SUCCESS",
                    content: response.data.message
                }
            });
        }
        else
        {
            dispatch({
                type: requestRegisterErrorType,
                message: {
                    type: "ERROR",
                    content: response.data.message
                }
            });
        }
    }
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === requestSignInType) {
    return {
      ...state,
      isAuthenticated: false
    };
  }

    if (action.type === requestSignInSuccessType) {
        return {
            ...state,
            isAuthenticated: true,
            message: action.message
        };
    }

    if (action.type === requestSignInErrorType) {
        return {
            ...state,
            isAuthenticated: false,
            message: action.message
        };
    }

    if (action.type === requestSignOutSuccessType) {
        return {
            ...state,
            isAuthenticated: false
        }
    }

    if (action.type === requestRegisterType) {
        return {
            ...state,
            isAuthenticated: false
        };
    }

    if (action.type === requestRegisterSuccessType) {
        return {
            ...state,
            isAuthenticated: true
        };
    }

    if (action.type === requestRegisterErrorType) {
        return {
            ...state,
            isAuthenticated: false,
            message: action.message
        };
    }

  return state;
};
