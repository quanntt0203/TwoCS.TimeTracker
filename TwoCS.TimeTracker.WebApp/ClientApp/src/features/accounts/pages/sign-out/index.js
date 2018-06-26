import React, { Component } from "react";
import { connect } from "react-redux";
import { Well } from "react-bootstrap";
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

class SignOut extends Component {
    constructor(props) {
        super(props);
        this.state = {
        };
    }

    componentDidMount() {
        this.props.requestLogout();
    }

    componentWillReceiveProps(props) {

        //debugger

        this.props.history.push("/sign-in");

        if (props.isAuthenticated === false) {

            this.props.history.push("/sign-in");
        }
        
    }

    render() {
        return (
            <Well> <Link to={"/sign-out"} exact>
                <Glyphicon glyph="user" /> SignOut
    
                </Link>
            </Well>);
    }
}

export default connect(
  state => state.account,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(SignOut);
