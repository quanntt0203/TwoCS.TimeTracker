import React, { Component } from "react";
import { Col, Grid, Row } from "react-bootstrap";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { actionCreators } from "../shared/store/Site";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

import "./Layout.css";

class PageLayout extends Component {
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
      <Grid>
        <Row>
          <Col className="content" sm={6} xsOffset={3}>
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
        </Row>
      </Grid>
    );
  }
}

export default connect(
  state => state.site,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(PageLayout);
