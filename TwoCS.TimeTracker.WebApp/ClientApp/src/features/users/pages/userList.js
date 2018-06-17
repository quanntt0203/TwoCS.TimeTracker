import React, { Component } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { actionCreators } from "../UserStore";
import { toast } from "react-toastify";
import { Link } from "react-router-dom";
//import Popup from "reactjs-popup";
import Dialog from 'react-dialog'

class UserList extends Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: false,
            users: [],
            message: {},
            isDialogOpen: false,
            promoteUser: ''
        };
    }

    openDialog = () => this.setState({ isDialogOpen: true })

    handleClose = () => this.setState({ isDialogOpen: false })

    handleUserToPromote(userName) {

        var confirm = prompt("Do you want to promote this user to ?", "Manager");

        if (confirm) {
            this.handlePromote(userName, confirm);
        }

        //this.setState({ promoteUser: username });
        //this.openDialog();
    }

    handlePromote(userName = '', confirm = '') {

        const confirmed = userName.length > 0 && confirm.length > 0;

        if (confirmed === true) {
            var params = {
                userName: userName,
                confirmMessage: confirm
            };

            this.props.promoteUserToManager(params);
        }
    }

    componentWillMount() {

        this.props.requestUserList();
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

        if (props.promoteUser) {
            this.props.requestUserList();
        }
    }

    renderUsersTable(props) {
        return (
            <table className='table'>
                <thead>
                    <tr>
                        <th>#</th>
                        <th>User Name</th>
                        <th>Email</th>
                        <th>Role</th>
                        <th><Link to={"/users"} exact>
                            <span className="glyphicon glyphicon-plus"></span>Add New
                </Link></th>
                    </tr>
                </thead>
                <tbody>
                    {props.users.map((item, idx) =>
                        <tr key={item.id}>
                            <td>{idx+1}</td>
                            <td>{item.userName}</td>
                            <td>{item.email}</td>
                            <td>{item.roles}</td>
                            <td>
                                <Link to="/users" onClick={(e) => { this.handleUserToPromote(item.userName) }}>
                                    <span title="Promote" className="glyphicon glyphicon-cog">
                                    </span></Link>
                                &nbsp;|&nbsp;
                                <span title="Edit" className="glyphicon glyphicon-edit"></span>
                                &nbsp;|&nbsp;
                                <span title="Remove" className="glyphicon glyphicon-remove"></span>
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        return (
            <div className="container">
                <h2>User List</h2>
                {this.renderUsersTable(this.props)}
            </div>
        );
    }
}

export default connect(
    state => state.user,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(UserList);
