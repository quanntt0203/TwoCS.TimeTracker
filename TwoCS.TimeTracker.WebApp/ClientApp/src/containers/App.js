﻿import React from "react";
import { Route, Switch, Redirect } from "react-router";
import Loadable from "react-loadable";
import Loading from "../components/Loading";
import { PageLayoutRoute, LayoutRoute } from "../routes";
import Home from "./Home";

const AsyncHome = Loadable({
  loader: () => import("./Home"),
  loading: Loading
});

const AsyncSignIn = Loadable({
  loader: () => import("../features/accounts/pages/sign-in"),
  loading: Loading
});

const AsyncSignOut = Loadable({
    loader: () => import("../features/accounts/pages/sign-out"),
    loading: Loading
});

const AsyncRegister = Loadable({
    loader: () => import("../features/accounts/pages/register"),
    loading: Loading
});

const AsyncProject = Loadable({
    loader: () => import("../features/projects/pages/project"),
    loading: Loading
});

const AsyncProjectAdd = Loadable({
    loader: () => import("../features/projects/pages/projectAdd"),
    loading: Loading
});

const AsyncUserList = Loadable({
    loader: () => import("../features/users/pages/userList"),
    loading: Loading
});

const AsyncTracker = Loadable({
    loader: () => import("../features/trackers/pages/timeTracker"),
    loading: Loading
});

export default () => (
    <Switch>
        <Route exact path="/">
            <Redirect to="/home" />
        </Route>
        <PageLayoutRoute path="/sign-in" component={AsyncSignIn} />
        <PageLayoutRoute path="/register" component={AsyncRegister} />
        <LayoutRoute
            exact
            path="/home"
            component={AsyncHome}
        />
        <LayoutRoute exact path="/sign-out" component={AsyncSignOut} />
        <LayoutRoute exact path="/projects" component={AsyncProject} />
        <LayoutRoute exact path="/projects-add" component={AsyncProjectAdd} />
        <LayoutRoute exact path="/users" component={AsyncUserList} />
        <LayoutRoute exact path="/trackers" component={AsyncTracker} />

        <LayoutRoute component={AsyncHome} Redirect="/home" />

    </Switch>
);