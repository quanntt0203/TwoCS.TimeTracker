import React, { Component } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { actionCreators } from "../ProjectStore";
import {
    FormGroup,
    InputGroup,
    FormControl,
    Glyphicon,
    Button,
    Form
} from "react-bootstrap";
import { toast } from "react-toastify";
import { setTimeout } from "timers";

class ProjectAdd extends Component {
    constructor(props) {
        super(props);
        this.state = this.initialState;

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleCancel = this.handleCancel.bind(this);
    }

    get initialState() {
        return {
            projectName: "",
            description: ""
        };
    }

    handleChange(e) {
        this.setState({ [e.target.name]: e.target.value });
    }

    handleSubmit(e) {
        e.preventDefault();

        var params = {
            name: this.state.projectName,
            description: this.state.description
        };

        this.props.requestProjectAdd(params);
    }

    handleCancel(e) {
        this.setState(this.initialState);
        this.props.history.push("/projects");
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

        if (props.project) {
            this.props.history.push("/projects");
        }
    }

    render() {
        const { projectName, description } = this.state;
        const enabled =
            projectName.length > 0;
        return (
            <Form onSubmit={this.handleSubmit}>
                <FormGroup>
                    <InputGroup>
                        <InputGroup.Addon>
                            <Glyphicon glyph="user" />
                        </InputGroup.Addon>
                        <FormControl
                            type="text"
                            name="projectName"
                            placeholder="Project name"
                            value={this.state.projectName}
                            onChange={this.handleChange}
                        />
                    </InputGroup>
                </FormGroup>
                <FormGroup>
                    <InputGroup>
                        <InputGroup.Addon>
                            <Glyphicon glyph="envelope" />
                        </InputGroup.Addon>
                        <FormControl
                            type="text"
                            name="description"
                            placeholder="Description"
                            value={this.state.description}
                            onChange={this.handleChange}
                        />
                    </InputGroup>
                </FormGroup>
                
                <FormGroup className="text-center">
                    <Button bsStyle="primary" bsSize="medium" type="submit" disabled={!enabled}>
                        Create
                    </Button>
                    &nbsp; &nbsp;
                  <Button bsStyle="primary" bsSize="medium" type="button" onClick={this.handleCancel}>
                        Cancel
                    </Button>
                </FormGroup>
            </Form>
        );
    }
}

export default connect(
    state => state.project,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(ProjectAdd);
