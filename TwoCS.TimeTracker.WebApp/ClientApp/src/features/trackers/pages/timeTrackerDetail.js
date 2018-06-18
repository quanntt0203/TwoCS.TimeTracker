import React, { Component } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { toast } from "react-toastify";
import { actionCreators } from "../TrackerStore";
import Dialog from "react-dialog";
import {
    FormGroup,
    InputGroup,
    FormControl,
    Glyphicon,
    Button,
    Form
} from "react-bootstrap";

import "./dialog-ui.css";

class TrackerDetail extends Component {
    constructor(props) {
        super(props);
        this.state = {
            record: null,
            isDialogOpen: false,
            remark: '',
            duration: 0,
            recordId: null
        };

        this.handleCloseDialog = this.handleCloseDialog.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleChange = this.handleChange.bind(this);
    }

    openDialog = (e) => this.setState({ isDialogOpen: true })

    handleCloseDialog = (e) => this.setState({ isDialogOpen: false })

    componentWillMount() {

        //debugger

        var recordParam = this.props.location.pathname.substring(17);

        var params = {
            recordId: recordParam
        };

        this.setState({ recordId: recordParam });

        this.props.requestRecordDetail(params);
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

            this.setState({ record: props.record });
            this.renderRecordDetail(props);
        }


        if (props.logTime) {

            this.renderRecordDetail(props);

            this.handleCloseDialog();
        }
    }

    handleSubmit(e) {
        e.preventDefault();

        var params = {
            timeRecordId: this.state.recordId,
            remark: this.state.remark,
            duration: this.state.duration
        };

        this.props.requestLogTimeAdd(params);
    }

    handleChange(e) {

        this.setState({ [e.target.name]: e.target.value });
    }

    renderRecordDetail(props) {

        if (!props.record) {
            return;
        }

        return (
            <div className="detail">
                <p>Name: {props.record.name}</p>
                <p>Description: {props.record.description}</p>
                <p>Start time: {props.record.startTime}</p>
                <p>End time: {props.record.endTime}</p>
                <p>Duration time: {props.record.duration}</p>
                <p></p>
                <h2>Log time records</h2>
                <hr />
                <Button bsStyle="primary" bsSize="medium" type="button" onClick={this.openDialog}>Add new record</Button>
                {
                    this.state.isDialogOpen &&
                    <Dialog
                        title="Log time information"
                        modal={true}
                        onClose={this.handleCloseDialog}
                        height={200} >
                        <Form onSubmit={this.handleSubmit}>
                            <FormGroup>
                                <InputGroup>
                                    <InputGroup.Addon>
                                        <Glyphicon glyph="edit" />
                                    </InputGroup.Addon>
                                    <FormControl
                                        type="text"
                                        name="remark"
                                        placeholder="Time remark"
                                        value={this.state.remark}
                                        onChange={this.handleChange}
                                    />
                                </InputGroup>
                            </FormGroup>
                            <FormGroup>
                                <InputGroup>
                                    <InputGroup.Addon>
                                    <Glyphicon glyph="time" />
                                    </InputGroup.Addon>
                                    <FormControl
                                        type="text"
                                        name="duration"
                                        placeholder="Time duration"
                                        value={this.state.duration}
                                        onChange={this.handleChange}
                                    />
                                </InputGroup>
                            </FormGroup>

                            <FormGroup className="text-center">
                                <Button bsStyle="primary" bsSize="medium" type="submit">
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
                    <table className='table'>
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Remark</th>
                                <th>Duration</th>
                                <th>LogTime</th>
                            </tr>
                        </thead>
                        <tbody>
                            {props.record.logTimeRecords.map((item, idx) =>
                            <tr>
                                <td>{idx + 1}</td>
                                    <td>{item.remark}</td>
                                    <td>{item.duration}</td>
                                <td>{item.logTime}</td>
                            </tr>
                        )}
                        </tbody>
                </table>
        </div>);
    }

    render() {
        return (
            <div className="container">
                <h2>Time Tracker Detail</h2>
                <hr />
                {this.renderRecordDetail(this.props) }
            </div>
        );
    }
}


export default connect(
    state => state.tracker,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(TrackerDetail);