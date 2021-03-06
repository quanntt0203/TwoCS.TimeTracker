﻿import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import thunk from 'redux-thunk';
import { routerReducer, routerMiddleware } from 'react-router-redux';
//import * as Counter from './Counter';
//import * as WeatherForecasts from './WeatherForecasts';
import * as Account from "../features/accounts/store/Account";
import * as Project from "../features/projects/ProjectStore";
import * as User from "../features/users/UserStore";
import * as Tracker from "../features/trackers/TrackerStore";
import * as Report from "../features/reports/ReportStore";

export default function configureStore(history, initialState) {
  const reducers = {
    //counter: Counter.reducer,
   // weatherForecastsX: WeatherForecasts.reducer,
      account: Account.reducer,
      project: Project.reducer,
      user: User.reducer,
      tracker: Tracker.reducer,
      report: Report.reducer
  };

  const middleware = [
    thunk,
    routerMiddleware(history)
  ];

  // In development, use the browser's Redux dev tools extension if installed
  const enhancers = [];
  const isDevelopment = process.env.NODE_ENV === 'development';
  if (isDevelopment && typeof window !== 'undefined' && window.devToolsExtension) {
    enhancers.push(window.devToolsExtension());
  }

  const rootReducer = combineReducers({
    ...reducers,
    routing: routerReducer
  });

  return createStore(
    rootReducer,
    initialState,
    compose(applyMiddleware(...middleware), ...enhancers)
  );
}
