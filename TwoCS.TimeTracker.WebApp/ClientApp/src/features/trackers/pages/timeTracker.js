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

import DatePicker from 'react-datepicker';
import moment from 'moment';
import 'react-datepicker/dist/react-datepicker.css';

import 'react-dropdown/style.css'
import "./dialog-ui.css";
import { getIdentityRole } from "../../../shared/helper/identity";

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
            minTime: moment(),
            taskName: 'Task name',
            taskDescription: 'Task description',
            startTime: moment(),
            endTime: moment()
        };

        this.handleCloseDialog = this.handleCloseDialog.bind(this);
        this.handleSubmitRecord = this.handleSubmitRecord.bind(this);
        this.handleChangeRecord = this.handleChangeRecord.bind(this);
        this.handleProjectChange = this.handleProjectChange.bind(this);
        this.handleChangeStartTime = this.handleChangeStartTime.bind(this);
        this.handleChangeEndTime = this.handleChangeEndTime.bind(this);
    }

    openDialog = (e) => this.setState({ isDialogOpen: true })

    handleCloseDialog = (e) => this.setState({ isDialogOpen: false })

    handleProjectChange = (selectedItem) => {

        const projectValue = selectedItem.value;

        this.setState({ selectedProject: selectedItem.label });

        const pageIndex = parseInt(this.props.match.params.pageIndex) || 1;

        var params = {
            project: projectValue,
            pageIndex: pageIndex
        };

        

        this.props.requestRecordList(params);
    }

    handleChangeStartTime(date, e) {

        this.setState({ startTime: date });
        
        const { endTime } = this.state;

        if (endTime.date() < date.date()) {

            this.setState({ endTime: date });
        }
    }

    handleChangeEndTime(date, e) {

        this.setState({ endTime: date });
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
    }


    renderProjectList(props) {
        let options = [];
        options.push({ value: '', label: 'All'});
        props.projects.map((item, idx) =>
            options.push({ value: item.name, label: item.name })
        )
        const role = getIdentityRole();
        const enabled = !role.isAdmin;
        return (
             
            <div className="project-list">
                Project List:
                <br />

                <Dropdown options={options} onChange={this.handleProjectChange} value={this.state.selectedProject} placeholder="Select a project" disabled={role.isAdmin} />
                
                <br />
            </div>
        );
    }

    renderPagination(props) {
        const prevStartDateIndex = (props.startDateIndex || 0) - 5;
        const nextStartDateIndex = (props.startDateIndex || 0) + 5;

        return (<p className='clearfix text-center'>
            <Link className='btn btn-default pull-left' to={`/fetchdata/${prevStartDateIndex}`}>Previous</Link>
            <Link className='btn btn-default pull-right' to={`/fetchdata/${nextStartDateIndex}`}>Next</Link>
            {props.isLoading ? <span>Loading...</span> : []}
        </p>);
    }

    renderRecordsTable(props) {
        return (
            <table className='table'>
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Name</th>
                        <th>Description</th>
                        <th>Start Date</th>
                        <th>End Date</th>
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
                            <td>{item.startDate}</td>
                            <td>{item.endDate}</td>
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
        const role = getIdentityRole();

        const { selectedProject } = this.state;
        const enabled =
            selectedProject.length > 0 && selectedProject != 'All' && !role.isAdmin;

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
                                    <DatePicker
                                        name="startTime"
                                        selected={this.state.startTime}
                                        onChange={this.handleChangeStartTime}
                                        minDate={this.state.minTime}
                                        placeholderText="Select start date"
                                        isClearable={true}
                                        showDisabledMonthNavigation
                                    />
                                    
                                </InputGroup>
                               
                            </FormGroup>

                            <FormGroup>
                                <InputGroup>
                                    <InputGroup.Addon>
                                        <Glyphicon glyph="calendar" />
                                    </InputGroup.Addon>
                                   
                                    <DatePicker
                                        name="endTime"
                                        selected={this.state.endTime}
                                        onChange={this.handleChangeEndTime}
                                        minDate={this.state.startTime}
                                        placeholderText="Select end date"
                                        isClearable={true}
                                        showDisabledMonthNavigation
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
