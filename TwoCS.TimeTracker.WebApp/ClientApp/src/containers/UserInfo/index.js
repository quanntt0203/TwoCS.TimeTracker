import React, { Component } from "react";
import { Well } from "react-bootstrap";
import { Link } from "react-router-dom";
import { Glyphicon, Nav, Navbar, NavItem } from "react-bootstrap";


export default props => (
    <Well> <Link to={"/sign-out"} exact>
        <Glyphicon glyph="user" /> Sign out

                </Link>
    </Well>);
