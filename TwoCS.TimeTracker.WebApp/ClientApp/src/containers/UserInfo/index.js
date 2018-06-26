import React, { Component } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { actionCreators } from "../../features/accounts/store/Account";
import { Well } from "react-bootstrap";
import { Link } from "react-router-dom";
import { Glyphicon, Nav, Navbar, NavItem } from "react-bootstrap";
import { debug } from "util";
import { getIdentity, getIdentityRole } from "../../shared/helper/identity";

class UserInfo extends Component {
    constructor(props) {
        super(props);
        this.state = {
        };

    }

    render() {

        let userName = 'N/A';
        const user = getIdentity();

        if (user) {
            userName = user.userName;
        }

        return (
            <Well>
                <Glyphicon glyph="user" /> {userName}
                &nbsp;
                <Link to={"/sign-out"} exact>
                     Sign out
                </Link>
            </Well>
        );
    }
}

export default connect(
    state => state.account,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(UserInfo);