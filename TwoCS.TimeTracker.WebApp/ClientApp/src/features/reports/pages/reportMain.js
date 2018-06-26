import React, { Component } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { toast } from "react-toastify";
import { actionCreators } from "../ReportStore";

import { Link } from "react-router-dom";
import Dropdown from "react-dropdown";
import {
    FormGroup,
    InputGroup,
    FormControl,
    Glyphicon,
    Button,
    Form
} from "react-bootstrap";
import { getIdentityRole, getUserRole } from "../../../shared/helper/identity";
import DatePicker from 'react-datepicker';
import moment from 'moment';
import 'react-datepicker/dist/react-datepicker.css';

import 'react-dropdown/style.css';
import './report-ui.css';

const REPORT_TYPE = {
    DAILY: 'Daily',
    WEEKLY: 'Weekly',
    MONTHLY: 'Monthly'
};

const GROUP_TYPE = {
    PROJECT: 'Project',
    USER: 'User'
};

class ReportMain extends Component {
    constructor(props) {
        super(props);

        this.state = {
            data: [],
            projects: [],
            users: [],
            selectedUser: '',
            selectedProject: '',
            reportType: 'Daily',
            selectedGroupBy: 'Project',
            minDate: moment(),
            startDate: moment(),
            endDate: moment()
        }

        this.handleChangeStartDate = this.handleChangeStartDate.bind(this);
        this.handleChangeEndDate = this.handleChangeEndDate.bind(this);
        this.handleProjectChange = this.handleProjectChange.bind(this);
        this.handleUserChange = this.handleUserChange.bind(this);
        this.handleRunReport = this.handleRunReport.bind(this);
        this.handleReportTypeChange = this.handleReportTypeChange.bind(this);
        this.handleGroupByChange = this.handleGroupByChange.bind(this);
    }

    handleChangeStartDate(val) {

        this.setState({ startDate: val });

        const { endDate } = this.state;

        if (!endDate || (endDate.date() < val.date())) {

            this.setState({ endDate: val });
        }
    }

    handleChangeEndDate(val) {

        this.setState({ endDate: val });
    }

    handleProjectChange(val) {

        this.setState({ selectedProject: val.value });
    }

    handleUserChange(val) {
        this.setState({ selectedUser: val.value })
    }

    handleReportTypeChange(val) {

        this.setState({ reportType: val.value });

        this.state.reportType = val.value;
        this.handleRunReport(null);
    }

    handleGroupByChange(val) {

        this.setState({ selectedGroupBy: val.value });

        this.state.selectedGroupBy = val.value;
        this.handleRunReport(null);
    }

    handleRunReport(e) {
        if (e) {
            e.preventDefault();
        }

        const { startDate, endDate } = this.state;

        var strStartDate = new Date(startDate.locale("en")).toLocaleDateString();
        var strEndDate = new Date(endDate.locale("en")).toLocaleDateString();

        var params = {
            reportType: this.state.reportType,
            groupBy: this.state.selectedGroupBy,
            startDate: strStartDate,
            endDate: strEndDate,
            project: this.state.selectedProject,
            user: this.state.selectedUser
        };

        this.props.requestReportList(params);
    }

    componentDidMount() {

        this.fetchProjectList();
        this.fetchUserList();
    }

    componentWillReceiveProps(props) {
        //const { store } = nextProps;

        if (props.message) {
            if (props.message.type === "ERROR") {
                toast.error(props.message.content);
            }

            if (props.message.type === "SUCCESS") {
                toast.success(props.message.content);
            }
        }

        if (props.projects) {
            this.setState({ projects: props.projects });
        }

        if (props.users) {
            this.setState({ users: props.users });

            this.renderDropdownUser();
        }

        if (props.reportData) {
            this.renderReportMain(props);
        }
    }

    fetchProjectList = () => {

        const { selectedProject } = this.state;

        const params = {
            project: selectedProject
        };

        this.props.requestProjectList(params);
    }

    fetchUserList = () => {

        const { selectedUser } = this.state;

        const params = {
            user: selectedUser
        };

        const role = getIdentityRole();

        if (!role.isUser) {
            this.props.requestUserList(params);
        }
        
    }

   

    renderDropdownProject() {

        let options = [{ value: '', label: 'Any' }];
        const { projects } = this.state;
        projects.map((item, idx) => {
            options.push({ value: item.name, label: item.name });
        });

        return (
            <div className="project-list">
                Project list:
                <br />

                <Dropdown options={options} onChange={this.handleProjectChange} value={this.state.selectedProject} placeholder="Select a project" />

                <br />
            </div>
        );
    }

    renderDropdownUser() {
        let options = [{ value: '', label: 'Any' }];
        const { users } = this.state;
        users.map((item, idx) => {
            options.push({ value: item.userName, label: item.userName });
        });

        return (
            <div className="user-list">
                Member list:
                <br />

                <Dropdown options={options} onChange={this.handleUserChange} value={this.state.selectedUser} placeholder="Select a member" />

                <br />
            </div>
        );
    }

    renderReportFilter = () => {
        const optionReportType = [{ value: "Daily", label: "Daily" }, { value: "Weekly", label: "Weekly" }, { value: "Monthly", label: "Monthly" }]
        const optionGroupBy = [{ value: "Project", label: "Project" }, { value: "User", label: "User" }]
        return (
            <div className="time-range">
                Time range:
                <br />
                From
                &nbsp;
                <FormGroup>
                    <InputGroup>
                        <InputGroup.Addon>
                            <Glyphicon glyph="calendar" />
                        </InputGroup.Addon>
                        <DatePicker
                            name="startTime"
                            selected={this.state.startDate}
                            onChange={this.handleChangeStartDate}
                            placeholderText="Select start date"
                        />

                    </InputGroup>

                </FormGroup>
                
                &nbsp;to&nbsp;
                 <FormGroup>
                    <InputGroup>
                        <InputGroup.Addon>
                            <Glyphicon glyph="calendar" />
                        </InputGroup.Addon>
                        <DatePicker
                            name="endTime"
                            selected={this.state.endDate}
                            minDate={this.state.startDate}
                            onChange={this.handleChangeEndDate}
                            placeholderText="Select end date"
                        />

                    </InputGroup>

                </FormGroup>
                Report By:
                <Dropdown options={optionReportType} onChange={this.handleReportTypeChange} value={this.state.reportType} placeholder="Select report type" />
                <br/>
                Group By:
                <Dropdown options={optionGroupBy} onChange={this.handleGroupByChange} value={this.state.selectedGroupBy} placeholder="Select a group" />
                <br />
            </div>
        );
    }

    renderReportMain = (props) => {

        //debugger

        let records = [];
        if (props.reportData) {
            records = props.reportData.records;
        }

        const { reportType, selectedGroupBy } = this.state;

        if (reportType == REPORT_TYPE.WEEKLY) {
            return (
                <div className="report-content">
                    <table className='table'>
                        <thead>
                            <tr className="report-header">
                                <th>#</th>
                                {selectedGroupBy == GROUP_TYPE.PROJECT &&
                                    <th>Project</th>
                                }
                                {selectedGroupBy == GROUP_TYPE.USER &&
                                    <th>User</th>
                                }

                                <th>Week No</th>
                                <th>Durations</th>
                            </tr>
                        </thead>
                        {records.length > 0 &&
                            <tbody>
                                {records.map((item, idx) =>
                                    <tr className={item.isMarked ? 'report-row-marked' : 'report-row'}>
                                        <td>{idx + 1}</td>
                                        {selectedGroupBy == GROUP_TYPE.PROJECT &&
                                            <td>{item.project}</td>
                                        }
                                        {selectedGroupBy == GROUP_TYPE.USER &&
                                            <td>{item.user}</td>
                                        }
                                        <td>{item.weekName}</td>
                                        <td>{item.duration}</td>
                                    </tr>
                                )}
                            </tbody>
                        }
                    </table>
                </div>
            );
        }

        if (reportType == REPORT_TYPE.MONTHLY) {
            return (
                <div className="report-content">
                    <table className='table'>
                        <thead>
                            <tr className="report-header">
                                <th>#</th>
                                {selectedGroupBy == GROUP_TYPE.PROJECT &&
                                    <th>Project</th>
                                }
                                {selectedGroupBy == GROUP_TYPE.USER &&
                                    <th>User</th>
                                }

                                <th>Month</th>
                                <th>Durations</th>
                            </tr>
                        </thead>
                        {records.length > 0 &&
                            <tbody>
                                {records.map((item, idx) =>
                                    <tr className={item.isMarked ? 'report-row-marked' : 'report-row'}>
                                        <td>{idx + 1}</td>
                                        {selectedGroupBy == GROUP_TYPE.PROJECT &&
                                            <td>{item.project}</td>
                                        }
                                        {selectedGroupBy == GROUP_TYPE.USER &&
                                            <td>{item.user}</td>
                                        }
                                        <td>{item.monthName}</td>
                                        <td>{item.duration}</td>
                                    </tr>
                                )}
                            </tbody>
                        }
                    </table>
                </div>
            );
        }

        // DAILY as default template
        return (
            <div className="report-content">
                <table className='table'>
                <thead>
                    <tr className="report-header">
                            <th>#</th>
                            {selectedGroupBy == GROUP_TYPE.PROJECT &&
                                <th>Project</th>
                            }
                            {selectedGroupBy == GROUP_TYPE.USER &&
                                <th>User</th>
                            }
                        
                        <th>Log Date</th>
                        <th>Durations</th>
                    </tr>
                    </thead>
                    {records.length > 0 &&
                        <tbody>
                            {records.map((item, idx) =>
                            <tr className={item.isMarked ? 'report-row-marked' : 'report-row'}>
                                    <td>{idx + 1}</td>
                                    {selectedGroupBy == GROUP_TYPE.PROJECT &&
                                        <td>{item.project}</td>
                                    }
                                    {selectedGroupBy == GROUP_TYPE.USER &&
                                        <td>{item.user}</td>
                                    }
                                    <td>{item.date}</td>
                                    <td>{item.duration}</td>
                                </tr>
                            )}
                        </tbody>
                    }
                </table>
            </div>
        );
    }

    renderReportForm() {

        const enabled = true;
        const role = getIdentityRole();

        return (
            <div className="report-filter">
                <Form name="rptForm" onSubmit={this.handleRunReport}>
                    <div className="report-filter-project">
                        {this.renderDropdownProject()}
                    </div>
                    {(role.isAdmin || role.isManager) &&
                        <div className="report-filter-user">
                        {this.renderDropdownUser()}
                        </div>
                    }
                    <div className="report-filter-time">
                        {this.renderReportFilter()}
                    </div>
                    <div className="report-command">
                        <Button bsStyle="primary" bsSize="medium" type="submit" onClick={this.handleRunReport} disabled={!enabled}>Run report</Button>
                    </div>
                </Form>
                <hr />
            </div>
            );
    }

    render() {
        
        return (
            <div className="report-main">
                {this.renderReportForm()}
                <div className="report-list">
                    {this.renderReportMain(this.props)}
                </div>
            </div>
        );
    }
}
export default connect(
    state => state.report,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(ReportMain);


//const mapStateToProps = (state) => {
//    return {
//        data: state.ReportStore,
//        product: state.ProductStore,
//        user: state.UserStore
//    };
//};

//export default connect(mapStateToProps)(ReportMain);