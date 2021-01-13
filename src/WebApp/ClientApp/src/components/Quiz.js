import React, { Component } from "react";
import { Form, FormGroup, Label, Input, Button } from "reactstrap";
import {
  AvForm,
  AvField,
  AvGroup,
  AvInput,
  AvFeedback,
  AvRadioGroup,
  AvRadio,
  AvCheckboxGroup,
  AvCheckbox,
} from "availity-reactstrap-validation";

export class Quiz extends Component {
  static displayName = Quiz.name;

  constructor(props) {
    super(props);
    this.state = { data: [], loading: true, quizId: props.match.params.quizid };
  }

  componentDidMount() {
    this.populateData();
  }

  static renderAnswers(question) {
    switch (question.question.typeId) {
      case 1:
        return (
          <AvRadioGroup
            name={"radioQ" + question.question.id}
            label={question.question.question}
            errorMessage="Pick one of the options"
            required
          >
            {question.answers.map((answer) => (
              <AvRadio label={answer.answer} value={answer.id} />
            ))}
          </AvRadioGroup>
        );
      case 2:
        return (
          <AvCheckboxGroup
            name={"radioQ" + question.question.id}
            label={question.question.question}
            errorMessage="Pick atleast one of the options"
            required
          >
            {question.answers.map((answer) => (
              <AvCheckbox label={answer.answer} value={answer.id} />
            ))}
          </AvCheckboxGroup>
        );
      default:
        return "";
    }
  }

  static renderData(data) {
    console.log(data);
    return (
      <div>
        <h1>{data.quiz.name}</h1>
        <p>{data.quiz.description}</p>
        <AvForm>
          {data.questions.map((question) => (
            <>{Quiz.renderAnswers(question)}</>
          ))}
          <AvGroup>
            <Button>Submit</Button>
          </AvGroup>
        </AvForm>
      </div>
    );
  }

  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      Quiz.renderData(this.state.data)
    );

    return <div>{contents}</div>;
  }

  async populateData() {
    const response = await fetch("api/quiz/" + this.state.quizId);
    const data = await response.json();
    this.setState({ data: data, loading: false });
  }
}
