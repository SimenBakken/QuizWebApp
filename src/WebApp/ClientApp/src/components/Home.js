import React, { Component } from "react";
import { LinkContainer } from "react-router-bootstrap";
import { Link } from "react-router-dom";
import { NavLink } from "reactstrap";

import "./Home.css";

export class Home extends Component {
  static displayName = Home.name;

  constructor(props) {
    super(props);
    this.state = { data: [], loading: true };
  }

  componentDidMount() {
    this.populateData();
  }

  renderData(data) {
    if (Array.isArray(data) && data.length > 0) {
      return (
        <ul>
          {data.map((quiz) => (
            <li>
              <Link to={"/quiz/" + quiz.id}>
                <h2>{quiz.name}</h2>
                <p>{quiz.description}</p>
              </Link>
            </li>
          ))}
        </ul>
      );
    }

    return <p>No quizzes to show</p>;
  }

  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      this.renderData(this.state.data)
    );

    return (
      <div>
        <h1>Welcome to the Quiz App!</h1>
        <p>Please select one of the quizzes below to start:</p>
        {contents}
      </div>
    );
  }

  async populateData() {
    const response = await fetch("api/quiz");
    const data = await response.json();
    this.setState({ data: data, loading: false });
  }
}
