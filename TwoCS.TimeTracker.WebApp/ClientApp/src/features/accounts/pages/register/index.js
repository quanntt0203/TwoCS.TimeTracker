import React, { Component } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { actionCreators } from "../../store/Account";
import {
  FormGroup,
  InputGroup,
  FormControl,
  Glyphicon,
  Button,
  Form
} from "react-bootstrap";
import { Redirect } from "react-router";
import { toast } from "react-toastify";

class Register extends Component {
  constructor(props) {
    super(props);
      this.state = {
          username: "user001",
          email: "user001@2cs.com",
          password: "AbC!123open"
      };

      this.handleChange = this.handleChange.bind(this);
      this.handleSubmit = this.handleSubmit.bind(this);
      this.handleCancel = this.handleCancel.bind(this);
  }

  handleChange(e) {
      this.setState({ [e.target.name]: e.target.value });
  }

    handleSubmit(e) {
        e.preventDefault();

        var params = {
            username: this.state.username,
            email: this.state.email,
            password: this.state.password
        };

        this.props.requestRegister(params);
    }

    handleCancel(e) {
        this.setState({ [e.target.name]: '' });
        this.props.history.push("/sing-in");
    }

    componentWillReceiveProps(props) {

        debugger
        if (props.isAuthenticated) {
            this.props.history.push("/home");
        }

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
        const { username, email, password } = this.state;
        const enabled =
            username.length > 0 &&
            email.length > 0 &&
            password.length > 0;
      return (
          <Form onSubmit={this.handleSubmit}>
              <FormGroup>
                  <InputGroup>
                      <InputGroup.Addon>
                          <Glyphicon glyph="user" />
                      </InputGroup.Addon>
                      <FormControl
                          type="text"
                          name="username"
                          placeholder="UserName"
                          value={this.state.username}
                          onChange={this.handleChange}
                      />
                  </InputGroup>
              </FormGroup>
              <FormGroup>
                  <InputGroup>
                      <InputGroup.Addon>
                          <Glyphicon glyph="envelope" />
                      </InputGroup.Addon>
                      <FormControl
                          type="text"
                          name="email"
                          placeholder="Email"
                          value={this.state.email}
                          onChange={this.handleChange}
                      />
                  </InputGroup>
              </FormGroup>
              <FormGroup>
                  <InputGroup>
                      <InputGroup.Addon>
                          <Glyphicon glyph="info-sign" />
                      </InputGroup.Addon>
                      <FormControl
                          type="password"
                          name="password"
                          placeholder="Password"
                          value={this.state.password}
                          onChange={this.handleChange}
                      />
                  </InputGroup>
              </FormGroup>
              <FormGroup className="text-center">
                  <Button bsStyle="primary" bsSize="medium" type="submit" disabled={!enabled}>
                      Register
                    </Button>
                  &nbsp; &nbsp;
                  <Button bsStyle="primary" bsSize="medium" type="button" onClick={this.handleCancel}>
                      Cancel
                    </Button>
              </FormGroup>
          </Form>
      );
  }
}

export default connect(
    state => state.account,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(Register);
