import React, { Component } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { actionCreators } from "../ProjectStore";
import { toast } from "react-toastify";
import { Link } from "react-router-dom";

class Project extends Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: false,
            projects: [],
            message: {}
        };
    }

    componentWillMount() {

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
    }

    renderProjectsTable(props) {
        return (
            <table className='table'>
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Project Name</th>
                        <th>Description</th>
                        <th><Link to={"/projects-add"} exact>
                            <span className="glyphicon glyphicon-plus"></span>Add New
                </Link></th>
                    </tr>
                </thead>
                <tbody>
                    {props.projects.map((project,idx) =>
                        <tr key={project.id}>
                            <td>{idx+1}</td>
                            <td>{project.name}</td>
                            <td>{project.description}</td>
                            <td><span className="glyphicon glyphicon-edit"></span>&nbsp;|&nbsp;<span className="glyphicon glyphicon-trash"></span></td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        return (
            <div>
                <h2>Project List</h2>
                {this.renderProjectsTable(this.props)}
            </div>
        );
    }
}

export default connect(
    state => state.project,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(Project);
