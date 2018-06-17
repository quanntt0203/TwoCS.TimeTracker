const showNotificationMessageType = "SHOW_NOTIFICATION";

const initialState = {
  message: null
};

export const actionCreators = {};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === showNotificationMessageType) {
    return {
      ...state,
      message: action.message,
      dateTime: new Date()
    };
  }

  return { ...state, message: null };
};
