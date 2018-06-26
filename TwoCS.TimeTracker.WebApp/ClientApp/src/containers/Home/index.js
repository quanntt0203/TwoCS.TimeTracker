import React, { Component } from "react";
import { Well } from "react-bootstrap";
import { Link } from "react-router-dom";
import { getIdentityRole, getUserRole } from "../../shared/helper/identity";
import "./home-ui.css";

class Home extends Component {
    render() {

        const role = getIdentityRole();

        return (
            <div className="home-container">
                <Well bsSize="large">2Click Solution - Time tracker management</Well>
                <hr />
                <div className="home-navigator">
                    {(role.isAdmin || role.isManager) &&
                        <div className="home-item projects">
                        <Link to={`/projects`} >
                            <span className="glyphicon glyphicon-film">&nbsp;</span>
                            Project management
                            </Link>
                        </div>
                    }
                    {(role.isAdmin || role.isManager) &&
                        <div className="home-item users">
                        <Link to={`/users`} >
                            <span className="glyphicon glyphicon-user">&nbsp;</span>
                            User management
                            </Link>
                        </div>
                    }
                    {(role.isManager || role.isUser) &&
                        <div className="home-item trackers">
                            <Link to={`/trackers`} >
                            <span className="glyphicon glyphicon-time">&nbsp;</span>
                                Time management
                        </Link>
                        </div>
                    }
                    <div className="home-item reports">
                        <Link to={`/reports`} >
                            <span className="glyphicon glyphicon-signal">&nbsp;</span>
                            Report management
                        </Link>
                    </div>
               </div>
          </div>
      );
  }
}

export default Home;
