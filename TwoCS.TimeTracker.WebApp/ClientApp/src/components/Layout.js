import React, { Component } from "react";
import { Col, Grid, Row } from "react-bootstrap";
import NavMenu from "./NavMenu";
import UserInfo from "../containers/UserInfo";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { actionCreators } from "../shared/store/Site";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

import "./Layout.css";

class Layout extends Component {
    componentWillReceiveProps(props) {
    if (props.message) {
      if (props.message.type === "ERROR") {
        toast.error(props.message.content);
      }

      if (props.message.type === "SUCCESS") {
        toast.success(props.message.content);
      }
    }
  }

  render() {
    return (
      <Grid fluid>
        <Row>
            <Col sm={2}>
            <NavMenu />
          </Col>
          <Col className="content" sm={8}>
                {this.props.children}
                <ToastContainer
                  position="top-right"
                  autoClose={3000}
                  hideProgressBar={false}
                  newestOnTop={false}
                  closeOnClick
                  rtl={false}
                  pauseOnVisibilityChange={false}
                  draggable
                  pauseOnHover={false}
                />
          </Col>
        <Col sm={2}>
            <UserInfo />
        </Col>
        </Row>
      </Grid>
    );
  }
}

export default connect(
  state => state.site,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Layout);
