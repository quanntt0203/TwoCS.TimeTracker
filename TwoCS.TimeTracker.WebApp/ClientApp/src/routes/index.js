import React from "react";
import { Route } from "react-router";
import Layout from "../components/Layout";
import PageLayout from "../components/PageLayout";
import { isSignedIn } from "../shared/helper";
import { Redirect } from "react-router";

export const LayoutRoute = ({ component: Component, ...rest }) => {
  if (!isSignedIn()) {
    return <Redirect to="/sign-in" />;
  }

  return (
    <Route
      {...rest}
      render={matchProps => (
        <Layout>
          <Component {...matchProps} />
        </Layout>
      )}
    />
  );
};

export const PageLayoutRoute = ({ component: Component, ...rest }) => {
  if (isSignedIn()) {
    return <Redirect to="/home" />;
  }

  return (
    <Route
      {...rest}
      render={matchProps => (
        <PageLayout>
          <Component {...matchProps} />
        </PageLayout>
      )}
    />
  );
};
