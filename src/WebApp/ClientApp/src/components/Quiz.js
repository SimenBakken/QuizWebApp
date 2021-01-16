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
import "./Quiz.css";

export class Quiz extends Component {
  static displayName = Quiz.name;

  constructor(props) {
    super(props);
    this.state = { data: [], loading: true, quizId: props.match.params.quizid };
  }

  componentDidMount() {
    this.populateData();
  }

  renderAnswers(question) {
    switch (question.question.typeId) {
      case 1:
        return (
          <>
            <AvRadioGroup
              name={"Q" + question.question.id}
              label={question.question.question}
              errorMessage="Pick one of the options"
              required
              value={
                question.answers.find((x) => x.selected == true)?.answer.id
              }
              disabled={this.state.done}
              invalid={
                this.state.done &&
                question.answers.find(
                  (x) => x.selected == true && x.answer.correct == false
                ) != null
              }
            >
              {question.answers.map((answer) =>
                this.state.done ? (
                  <AvRadio
                    label={answer.answer.answer}
                    value={answer.answer.id}
                    valid={answer.answer.correct}
                    invalid={answer.selected && !answer.answer.correct}
                  />
                ) : (
                  <AvRadio
                    label={answer.answer.answer}
                    value={answer.answer.id}
                  />
                )
              )}
            </AvRadioGroup>
            {this.state.done && (
              <p>
                {question.answers.find(
                  (x) => x.selected == true && x.answer.correct == false
                )
                  ? "0/1"
                  : "1/1"}
              </p>
            )}
          </>
        );
      case 2:
        return (
          <>
            <AvCheckboxGroup
              name={"Q" + question.question.id}
              label={question.question.question}
              errorMessage="Pick atleast one of the options"
              required
              value={question.answers
                .filter((x) => x.selected == true)
                .map((x) => x.answer.id)}
              disabled={this.state.done}
            >
              {question.answers.map((answer) =>
                this.state.done ? (
                  <AvCheckbox
                    label={answer.answer.answer}
                    value={answer.answer.id}
                    valid={answer.answer.correct}
                    invalid={answer.selected && !answer.answer.correct}
                  />
                ) : (
                  <AvCheckbox
                    label={answer.answer.answer}
                    value={answer.answer.id}
                  />
                )
              )}
            </AvCheckboxGroup>
            {this.state.done && (
              <p>
                {question.answers.find(
                  (x) => x.selected == true && x.answer.correct == false
                )
                  ? "0/1"
                  : "1/1"}
              </p>
            )}
          </>
        );
      default:
        return "";
    }
  }

  renderData(data) {
    return (
      <div>
        <h1>{data.quiz.name}</h1>
        <p>{data.quiz.description}</p>
        <AvForm
          onValidSubmit={(event, values) =>
            this.handleValidSubmit(this, event, values)
          }
        >
          {data.questions.map((question) => (
            <>{this.renderAnswers(question)}</>
          ))}
          <AvGroup>
            {this.state.done == true ? (
              // This should have used this.populateData(), but a bug preventing checkboxes to reload properly prevents this
              <Button type="button" onClick={() => window.location.reload()}>
                Retry
              </Button>
            ) : (
              <Button type="submit">Submit</Button>
            )}
          </AvGroup>
        </AvForm>
        {this.state.done && (
          <p class="total">
            Total:{" "}
            {this.state.data.questions.filter((x) => x.correct).length +
              "/" +
              this.state.data.questions.length}
          </p>
        )}
      </div>
    );
  }

  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      this.renderData(this.state.data)
    );

    return <div>{contents}</div>;
  }

  async populateData() {
    const response = await fetch("api/quiz/" + this.state.quizId);
    const data = await response.json();
    this.setState({ ...this.state, data: data, loading: false, done: false });
  }

  async handleValidSubmit(cl, event, values) {
    cl.setState({ ...cl.state, loading: true });
    let dataSend = cl.state.data;
    for (var key of Object.keys(values)) {
      const newKey = parseInt(key.replace("Q", ""));
      let value = values[key];
      if (!Array.isArray(value)) {
        dataSend.questions
          .find((x) => x.question.id == newKey)
          .answers.find((x) => x.answer.id == value).selected = true;
      } else {
        for (const selected of value) {
          dataSend.questions
            .find((x) => x.question.id == newKey)
            .answers.find((x) => x.answer.id == selected).selected = true;
        }
      }
    }
    const response = await fetch("api/quiz/" + this.state.quizId, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(dataSend),
    });
    const data = await response.json();
    cl.setState({ ...cl.state, data: data, loading: false, done: true });
    console.log(cl.state);
  }
}
