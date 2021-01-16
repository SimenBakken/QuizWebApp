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

  renderAnswers(question) {
    switch (question.question.typeId) {
      case 1:
        return (
          <AvRadioGroup
            name={"Q" + question.question.id}
            label={question.question.question}
            errorMessage="Pick one of the options"
            required
          >
            {question.answers.map((answer) => (
              <AvRadio label={answer.answer.answer} value={answer.answer.id} />
            ))}
          </AvRadioGroup>
        );
      case 2:
        return (
          <AvCheckboxGroup
            name={"Q" + question.question.id}
            label={question.question.question}
            errorMessage="Pick atleast one of the options"
            required
          >
            {question.answers.map((answer) => (
              <AvCheckbox
                label={answer.answer.answer}
                value={answer.answer.id}
              />
            ))}
          </AvCheckboxGroup>
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
            <Button type="submit">Submit</Button>
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
      this.renderData(this.state.data)
    );

    return <div>{contents}</div>;
  }

  async populateData() {
    const response = await fetch("api/quiz/" + this.state.quizId);
    const data = await response.json();
    console.log(data);
    this.setState({ data: data, loading: false, done: false });
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
    console.log(dataSend);
    const response = await fetch("api/quiz/" + this.state.quizId, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(dataSend),
    });
    const data = await response.json();
    console.log(data);
    cl.setState({ ...cl.state, loading: false, done: true });
  }
}
