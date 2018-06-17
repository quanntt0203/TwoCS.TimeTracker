import React, { Component } from "react";

class TableToolBar extends Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <div style={{ margin: "15px" }}>
        <div className="row">
          <div className="col-md-8">{this.props.children}</div>
          <div className="col-md-4">{this.props.components.searchPanel}</div>
        </div>
      </div>
    );
  }
}

export default TableToolBar;
