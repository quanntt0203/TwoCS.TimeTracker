import React, { Component } from "react";
import { Col, Grid, Row } from "react-bootstrap";
class EmptyLayout extends Component {
  render() {
    return (
      <Grid fluid>
        <Row>
          <Col className="content" sm={12}>
            {this.props.children}
          </Col>
        </Row>
      </Grid>
    );
  }
}

export default EmptyLayout;
