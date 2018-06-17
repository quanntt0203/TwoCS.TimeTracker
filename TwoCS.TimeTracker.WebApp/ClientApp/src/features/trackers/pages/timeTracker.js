import React, { Component } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { actionCreators } from "../TrackerStore";
import { toast } from "react-toastify";
import { Link } from "react-router-dom";
import Dialog from 'react-dialog'
import { fail } from "assert";

class TimeTracker extends Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: false,
            projects: [],
            records: [],
            record: null,
            message: {},
            trackers: [],
            tracker: null,
            isDialogOpen: false,
            selectedProject: null
        };
    }

    openDialog = () => this.setState({ isDialogOpen: true })

    handleClose = () => this.setState({ isDialogOpen: false })

    handleProjectChange = (e) => {

        this.setState({ selectedProject: e.target.value });

        const { selectedProject } = this.state;

        alert(this.state.selectedProject);

        var params = {
            project: selectedProject
        };

        this.props.requestRecordList(params);
    }


    handleUserLogTime(recordId, recordName) {

        var confirm = prompt("Log time on this record:", recordName);

        if (confirm) {
            this.handlePromote(recordId, confirm);
        }

        //this.setState({ promoteUser: username });
        //this.openDialog();
    }

    handleLogTime(recordId = '', confirm = '') {
        const infos = confirm.split(' ');
        const duration = infos[1]
            , idx = confirm.indexOf(duration)
            , remark = confirm.substring(idx);

        const confirmed = recordId.length > 0
            && duration > 0
            && remark.length > 0;

        if (confirmed === true) {
            var params = {
                timeRecordId: recordId,
                duration: duration,
                remark: remark
            };

            this.props.requestTrackerAdd(params);
        }
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

        if (props.record) {
            this.props.requestRecordList();
        }

        if (props.tracker) {
            this.props.requestRecordList();
        }

        if (props.projects) {
            this.renderProjectList(props);
        }
    }


    renderProjectList(props) {
        return (
            <div className="project-list">
                Project List:
                <br />
                <select value={this.state.selectedProject} onChange={this.handleProjectChange}>
                    <option value={NaN}>Select project</option>
                    {props.projects.map((item, idx) =>
                        <option value={item.id}>{item.name}</option>
                        )}
                </select>
                <br />
            </div>
        );
    }

    renderRecordsTable(props) {
        return (
            <table className='table'>
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Name</th>
                        <th>Description</th>
                        <th>StartTime</th>
                        <th>EndTime</th>
                        <th>Duration</th>
                        <th><Link to={"/trackers"} exact>
                            <span className="glyphicon glyphicon-plus"></span>Add New
                </Link></th>
                    </tr>
                </thead>
                <tbody>
                    {props.records.map((item, idx) =>
                        <tr key={item.id}>
                            <td>{idx + 1}</td>
                            <td>{item.name}</td>
                            <td>{item.description}</td>
                            <td>{item.startTime}</td>
                            <td>{item.endTime}</td>
                            <td>{item.duration}</td>
                            <td>
                                <Link to="/trackers" onClick={(e) => { this.handleUserLogTime(item.id) }}>
                                    <span title="Log time" className="glyphicon glyphicon-cog">
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
                <h2>Time Tracker Management</h2>
                <hr />
                {this.renderProjectList(this.props)}
                {this.renderRecordsTable(this.props)}
            </div>
        );
    }
}

export default connect(
    state => state.tracker,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(TimeTracker);
