import React, { Component } from "react";
import { connect } from "react-redux";
import { Link } from "react-router-dom";
import { LinkContainer } from "react-router-bootstrap";
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

class SignIn extends Component {
    constructor(props) {
        super(props);
        this.state = {
            email: "admin@2cs.com",
            password: "AbC!123open"
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    componentWillReceiveProps(props) {
        if (props.isAuthenticated) {
            this.props.history.push("/home");
        }

        if (props.message) {
            if (props.message.type === "ERROR") {
                toast.error(props.message.content);
            }

            if (props.message.type === "SUCCESS") {
                toast.success(props.message.content);
                this.props.history.push("/home");
            }
        }
    }

    handleChange(e) {
        this.setState({ [e.target.name]: e.target.value });
    }

    handleSubmit(e) {
        var params = {
            email: this.state.email,
            password: this.state.password
        };


        this.props.requestLogin(params);
        e.preventDefault();
    }

    render() {
        const { email, password } = this.state;
        const enabled =
            email.length > 0 &&
            password.length > 0;
    return (
      <Form onSubmit={this.handleSubmit}>
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
            Sign In
          </Button>
                &nbsp;&nbsp;
                <Link to={"/register"} exact>
                    Register
                </Link>
        </FormGroup>
      </Form>
    );
  }
}

export default connect(
  state => state.account,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(SignIn);
