import React, { Component } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { actionCreators } from "../TrackerStore";
import { toast } from "react-toastify";
import { Link } from "react-router-dom";
import Dialog from "react-dialog";
import { fail } from "assert";
import Dropdown from "react-dropdown";
import {
    FormGroup,
    InputGroup,
    FormControl,
    Glyphicon,
    Button,
    Form
} from "react-bootstrap";

import 'react-dropdown/style.css'
import "./dialog-ui.css";

class TimeTracker extends Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: false,
            projects: [],
            records: [],
            record: {
                name: '',
                description: ''
            },
            message: {},
            trackers: [],
            tracker: null,
            isDialogOpen: false,
            selectedProject: '',
            //============= record info
            taskName: 'dgdgd dfdfd',
            taskDescription: 'dfd dfdfdf',
            startTime: new Date().toDateString(),
            endTime: new Date(new Date().getTime() + (5 * 24 * 60 * 60 * 1000)).toDateString()
        };

        this.handleCloseDialog = this.handleCloseDialog.bind(this);
        this.handleSubmitRecord = this.handleSubmitRecord.bind(this);
        this.handleChangeRecord = this.handleChangeRecord.bind(this);
        this.handleProjectChange = this.handleProjectChange.bind(this);
    }

    openDialog = (e) => this.setState({ isDialogOpen: true })

    handleCloseDialog = (e) => this.setState({ isDialogOpen: false })

    handleProjectChange = (selectedItem) => {

        const projectValue = selectedItem.value;

        this.setState({ selectedProject: selectedItem.label });

        //const { outProject } = this.state;

        //alert(projectValue);

        var params = {
            project: projectValue
        };

        this.props.requestRecordList(params);
    }

    handleChangeRecord(e) {
        this.setState({ [e.target.name]: e.target.value });
    }

    handleSubmitRecord(e) {
        e.preventDefault();

        var params = {
            name: this.state.taskName,
            description: this.state.taskDescription,
            startTime: this.state.startTime,
            endTime: this.state.endTime,
            projectId: this.state.selectedProject
        };

        this.props.requestRecordAdd(params);
    }

    handleUserLogTime(recordId) {

        debugger

        this.props.history.push("/trackers/".concat(recordId))

        //var confirm = prompt("Log time on this record:", recordName);

        //if (confirm) {
        //    this.handlePromote(recordId, confirm);
        //}

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
        

        if (props.records) {

            this.renderRecordsTable(props);

            this.handleCloseDialog();
        }

        //if (props.tracker) {
        //    var params = {
        //        project: this.state.selectedProject
        //    };

        //    this.props.requestRecordList(params);
        //}

        //if (props.projects) {
        //    //this.setState({ projects: props.projects});
        //    this.renderProjectList(props);
        //}
    }


    renderProjectList(props) {
        let options = [];
        options.push({ value: '', label: 'All'});
        props.projects.map((item, idx) =>
            options.push({ value: item.name, label: item.name })
        )
        return (
             
            <div className="project-list">
                Project List:
                <br />

                <Dropdown options={options} onChange={this.handleProjectChange} value={this.state.selectedProject} placeholder="Select a project" />
                
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
                        <th>Action</th>
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
                                <Link to={`/trackers-detail/${item.id}`}>
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
        const { selectedProject } = this.state;
        const enabled =
            selectedProject.length > 0;
        return (
            <div className="container">
                <h2>Time Tracker Management</h2>
                <hr />
                {this.renderProjectList(this.props)}
                <Button bsStyle="primary" bsSize="medium" type="button" onClick={this.openDialog} disabled={!enabled}>Add new record</Button>
                {
                    this.state.isDialogOpen &&
                    <Dialog
                        title="Time tracker information"
                        modal={true}
                        onClose={this.handleCloseDialog}
                        height={300} >
                        <Form onSubmit={this.handleSubmitRecord}>
                            <FormGroup>
                                <InputGroup>
                                    <InputGroup.Addon>
                                        <Glyphicon glyph="info-sign" />
                                    </InputGroup.Addon>
                                    <FormControl
                                        type="text"
                                        name="taskName"
                                        placeholder="Task name"
                                        value={this.state.taskName}
                                        onChange={this.handleChangeRecord}
                                    />
                                </InputGroup>
                            </FormGroup>
                            <FormGroup>
                                <InputGroup>
                                    <InputGroup.Addon>
                                        <Glyphicon glyph="edit" />
                                    </InputGroup.Addon>
                                    <FormControl
                                        type="text"
                                        name="taskDescription"
                                        placeholder="Description"
                                        value={this.state.taskDescription}
                                        onChange={this.handleChangeRecord}
                                    />
                                </InputGroup>
                            </FormGroup>
                            <FormGroup>
                                <InputGroup>
                                    <InputGroup.Addon>
                                        <Glyphicon glyph="calendar" />
                                    </InputGroup.Addon>
                                    <FormControl
                                        type="text"
                                        name="startTime"
                                        placeholder="Start time"
                                        value={this.state.startTime}
                                        onChange={this.handleChangeRecord}
                                    />
                                </InputGroup>
                            </FormGroup>

                            <FormGroup>
                                <InputGroup>
                                    <InputGroup.Addon>
                                        <Glyphicon glyph="calendar" />
                                    </InputGroup.Addon>
                                    <FormControl
                                        type="text"
                                        name="endTime"
                                        placeholder="End time"
                                        value={this.state.endTime}
                                        onChange={this.handleChangeRecord}
                                    />
                                </InputGroup>
                            </FormGroup>

                            <FormGroup className="text-center">
                                <Button bsStyle="primary" bsSize="medium" type="submit" disabled={!enabled}>
                                    Create
                                </Button>
                                &nbsp; &nbsp;
                                <Button bsStyle="primary" bsSize="medium" type="button" onClick={this.handleCloseDialog}>
                                    Cancel
                                </Button>
                            </FormGroup>
                        </Form>
                    </Dialog>
                }
                <hr />
                {this.renderRecordsTable(this.props)}
            </div>
        );
    }
}

export default connect(
    state => state.tracker,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(TimeTracker);
