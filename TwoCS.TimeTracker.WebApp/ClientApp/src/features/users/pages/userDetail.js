import React, { Component } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { toast } from "react-toastify";
import { actionCreators } from "../UserStore";
import Popup from "reactjs-popup";

import {
    FormGroup,
    InputGroup,
    FormControl,
    Glyphicon,
    Button,
    Form
} from "react-bootstrap";
import "./dialog-ui.css";

import Dropdown from "react-dropdown";
import 'react-dropdown/style.css'
import { debug } from "util";

import { getIdentityRole, getUserRole } from "../../../shared/helper/identity";

class UserDetail extends Component {
    constructor(props) {
        super(props);
        this.state = {
            user: null,
            projects: [],
            managers: [],
            isOpenPopupPromotion: false,
            isOpenPopupManager: false,
            isOpenPopupProject: false,
            remark: '',
            duration: 0,
            userName: null,
            promoteRole: 'Manager',
            confirmMessage: 'Ok',
            selectedManager: '',
            selectedProject: '',
        };

        this.openPopupPromote = this.openPopupPromote.bind(this);
        this.closePopupPromote = this.closePopupPromote.bind(this);

        this.openPopupManager = this.openPopupManager.bind(this);
        this.closePopupManager = this.closePopupManager.bind(this);

        this.openPopupProject = this.openPopupProject.bind(this);
        this.closePopupProject = this.closePopupProject.bind(this);

        this.handleChangeManager = this.handleChangeManager.bind(this);
        this.handleChangeProject = this.handleChangeProject.bind(this);
        this.handlePromoteSubmit = this.handlePromoteSubmit.bind(this);
        this.handleAssignMemberSubmit = this.handleAssignMemberSubmit.bind(this);
        this.handleAssignProjectSubmit = this.handleAssignProjectSubmit.bind(this);
        this.handleSignInAsManager = this.handleSignInAsManager.bind(this);

    }

    openPopupPromote = (e) => this.setState({ isOpenPopupPromotion: true, isOpenPopupManager: false, isOpenPopupProject: false })

    closePopupPromote(e) { 

        //debugger

        this.setState({ isOpenPopupPromotion: false });
    }

    openPopupManager = (e) => this.setState({ isOpenPopupManager: true, isOpenPopupPromotion: false, isOpenPopupProject: false })
    closePopupManager = (e) => this.setState({ isOpenPopupManager: false })

    openPopupProject = (e) => this.setState({ isOpenPopupProject: true, isOpenPopupPromotion: false, isOpenPopupManager: false })
    closePopupProject = (e) => this.setState({ isOpenPopupProject: false })

    componentDidMount() {

        //debugger

        var userName = this.props.location.pathname.substring(14);

        var params = {
            user: userName
        };

        this.setState({ userName: userName });

        this.props.requestUserDetail(params);

        this.props.requestManagerList();

        this.props.requestProjectList();
    }

    componentWillReceiveProps(props) {

        if (props.message) {
            if (props.message.type === "ERROR") {
                toast.error(props.message.content);
            }

            if (props.message.type === "SUCCESS") {
                toast.success(props.message.content);
            }
        }

        //debugger
        if (props.userDetail && props.userDetail.behaveOfMagager) {
            this.props.history.push('/users');
        }

        if (props.userDetail) {

            this.renderUserDetail(props);

            this.setState({
                isOpenPopupPromotion: false,
                isOpenPopupManager: false,
                isOpenPopupProject: false});
        }
    }

    handleChangeManager = (selectedItem) => {

        const selectedManager = selectedItem.value;
        this.setState({ selectedManager: selectedManager });
    }

    handleChangeProject = (selectedItem) => {
        const selectedProject = selectedItem.value;
        this.setState({ selectedProject: selectedProject });
    }

    handleAssignMemberSubmit(e) {
        e.preventDefault();

        var params = {
            manager: this.state.selectedManager,
            member: this.state.userName
        };

        this.props.assignMemberToManager(params);
    }

    handlePromoteSubmit(e) {
        e.preventDefault();

        var params = {
            userName: this.state.userName,
            confirmMessage: this.state.confirmMessage
        };

        this.props.promoteUserToManager(params);
    }


    handleAssignProjectSubmit(e) {
        e.preventDefault();

        var params = {
            project: this.state.selectedProject,
            member: this.state.userName
        };

        this.props.assignProjectToUser(params);
    }

    handleSignInAsManager(e) {

        var params = {
            manager: this.state.userName
        };

        this.props.signInAsManager(params);
    }

    renderUserDetail(props) {

        //debugger

        if (!props.userDetail) {
            return;
        }

        let projects = 'N/A';

        let managers = 'N/A';
        if (props.userDetail.manager) {
            managers = props.userDetail.manager.userName;
        }

        if (props.userDetail.assignedProjects) {
            var arr = [];
            props.userDetail.assignedProjects.map((p, i) => { arr.push(p.name) });

            projects = arr.join(', ');
        }

        const loggedRole = getIdentityRole();
        const userRole = getUserRole(props.userDetail);

        const roles = ''.concat(props.userDetail.roles.join(', '));

        const isAdmin = roles.indexOf('Admin') >= 0;
        const isManager = roles.indexOf('Manager') >= 0;
        const isUser = roles.indexOf('User') >= 0;


        let optionProjects = [{ value: '', label: 'Select a project' }];
        let optionManagers = [{ value: '', label: 'Select a manager' }];

        if (props.managers) {
            props.managers.map((item) => {
                optionManagers.push({ value: item.userName, label: item.userName });
            });
            
        }

        if (props.projects) {
            props.projects.map((item) => {
                optionProjects.push({ value: item.name, label: item.name });
            });

        }

        const enableProject = this.state.selectedProject.length > 0;
        const enableManager = this.state.selectedManager.length > 0;
    

        return (
            <div className="detail">
                <p>Name: {props.userDetail.userName}</p>
                <p>Email: {props.userDetail.email}</p>
                <p>Role(s): {roles}</p>
                <p>Manager(s): {managers}</p>
                <p>Projects: {projects}</p>
                <hr />
                <Button bsStyle="primary" bsSize="medium" type="button" onClick={this.openPopupPromote} disabled={!(loggedRole.isAdmin && userRole.isUser && !userRole.isManager)}>Promote</Button>
                {this.state.isOpenPopupPromotion &&
                    <Popup
                        position="bottom center"
                        modal={true}
                        open={this.state.isOpenPopupPromotion}
                        onClose={this.closePopupPromote}
                        closeOnEscape
                        closeOnDocumentClick
                        mouseLeaveDelay={300}
                        mouseEnterDelay={0}
                        contentStyle={{ padding: "20px", width: "400px", "z-index": "9999" }}
                        arrow={false}>
                        <div>Promote user to manager</div>
                        <Form onSubmit={this.handlePromoteSubmit}>
                            <FormGroup>
                                <InputGroup>
                                    <InputGroup.Addon>
                                        <Glyphicon glyph="edit" />
                                    </InputGroup.Addon>
                                    <FormControl
                                        type="text"
                                        name="role"
                                        placeholder="Role Manager"
                                        value={this.state.promoteRole}
                                        readOnly
                                    />
                                </InputGroup>
                            </FormGroup>
                            <FormGroup className="text-center">
                                <Button bsStyle="primary" bsSize="medium" type="submit">
                                    Ok
                                </Button>
                                &nbsp; &nbsp;
                                <Button bsStyle="primary" bsSize="medium" type="button" onClick={this.closePopupPromote}>
                                    Cancel
                                </Button>
                            </FormGroup>
                        </Form>
                    </Popup>
                }
                
                &nbsp;
                <Button bsStyle="primary" bsSize="medium" type="button" onClick={this.openPopupManager} disabled={!(loggedRole.isAdmin && userRole.isUser && !userRole.isManager)}>Assign to manager</Button>
                {this.state.isOpenPopupManager &&
                    <Popup
                        position="bottom center"
                        modal={true}
                        open={this.state.isOpenPopupManager}
                        closeOnEscape
                        closeOnDocumentClick
                        mouseLeaveDelay={300}
                        mouseEnterDelay={0}
                        contentStyle={{ padding: "20px", width: "400px" }}
                        arrow={false}>
                        <div>Assign member to manager</div>
                        <Form onSubmit={this.handleAssignMemberSubmit}>
                            <FormGroup>
                                <InputGroup>
                                    <InputGroup.Addon>
                                        <Glyphicon glyph="edit" />
                                    </InputGroup.Addon>
                                <Dropdown options={optionManagers} onChange={this.handleChangeManager} value={this.state.selectedManager} placeholder="Select a manager" />
                                </InputGroup>
                            </FormGroup>
                            <FormGroup className="text-center">
                            <Button bsStyle="primary" bsSize="medium" type="submit" disabled={!enableManager}>
                                    Ok
                                </Button>
                                &nbsp; &nbsp;
                                <Button bsStyle="primary" bsSize="medium" type="button" onClick={this.closePopupManager}>
                                    Cancel
                                </Button>
                            </FormGroup>
                        </Form>
                    </Popup>
                }
                &nbsp;
                <Button bsStyle="primary" bsSize="medium" onClick={this.openPopupProject} type="button" disabled={!((loggedRole.isAdmin || loggedRole.isManager) && (userRole.isManager || userRole.isUser))}>Assign project</Button>
                {this.state.isOpenPopupProject &&
                    <Popup
                        position="bottom center"
                        modal={true}
                        open={this.state.isOpenPopupProject}
                        closeOnEscape
                        closeOnDocumentClick
                        mouseLeaveDelay={300}
                        mouseEnterDelay={0}
                        contentStyle={{ padding: "20px", width: "400px" }}
                        arrow={false}>
                        <div>Assign product to user</div>
                        <Form onSubmit={this.handleAssignProjectSubmit}>
                            <FormGroup>
                                <InputGroup>
                                    <InputGroup.Addon>
                                        <Glyphicon glyph="edit" />
                                    </InputGroup.Addon>
                                <Dropdown options={optionProjects} onChange={this.handleChangeProject} value={this.state.selectedProject} placeholder="Select a project" />
                                </InputGroup>
                            </FormGroup>
                        <FormGroup className="text-center">
                            <Button bsStyle="primary" bsSize="medium" type="submit" disabled={!enableProject}>
                                    Ok
                                </Button>
                                &nbsp; &nbsp;
                                <Button bsStyle="primary" bsSize="medium" type="button" onClick={this.closePopupProject}>
                                    Cancel
                                </Button>
                            </FormGroup>
                        </Form>
                    </Popup>
                }
                &nbsp;
                <Button bsStyle="primary" bsSize="medium" onClick={this.handleSignInAsManager} type="button" disabled={!(loggedRole.isAdmin && userRole.isManager)}>Sign in as manager</Button>
        </div>);
    }

    render() {
        return (
            <div className="container">
                <h2>User Detail</h2>
                <hr />
                {this.renderUserDetail(this.props) }
            </div>
        );
    }
}

export default connect(
    state => state.user,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(UserDetail);


//const mapStateToProps = (state) => {
//    return {
//        user: state.user
//    };
//};

//export default connect(mapStateToProps)(UserDetail);