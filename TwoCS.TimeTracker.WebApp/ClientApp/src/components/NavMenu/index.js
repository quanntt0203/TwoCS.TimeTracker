import React from "react";
import { Link } from "react-router-dom";
import { Glyphicon, Nav, Navbar, NavItem } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
import "./NavMenu.css";

export default props => (
  <div>
    <Navbar default fixedTop fluid collapseOnSelect>
      <Navbar.Header>
        <Navbar.Brand>
          <Link to={"/"}>User Control</Link>
        </Navbar.Brand>
        <Navbar.Toggle />
      </Navbar.Header>
      <Navbar.Collapse>
        <Nav>
          <LinkContainer to={"/home"} exact>
            <NavItem>
              <Glyphicon glyph="home" /> Home
            </NavItem>
          </LinkContainer>
          <LinkContainer to={"/users"} exact>
            <NavItem>
              <Glyphicon glyph="wrench" /> Users
            </NavItem>
          </LinkContainer>
          <LinkContainer to={"/projects"} exact>
            <NavItem>
              <Glyphicon glyph="th-list" /> Projects
            </NavItem>
          </LinkContainer>
          <LinkContainer to={"/trackers"} exact>
            <NavItem>
              <Glyphicon glyph="hdd" /> Time Trakers
            </NavItem>
          </LinkContainer>
          <LinkContainer to={"/reports"} exact>
            <NavItem>
              <Glyphicon glyph="transfer" /> Reports
            </NavItem>
          </LinkContainer>
        </Nav>
      </Navbar.Collapse>
    </Navbar>
  </div>
);
