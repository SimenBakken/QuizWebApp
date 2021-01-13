import React, { Component } from 'react';

export class FetchTest extends Component {
  static displayName = FetchTest.name;

  constructor(props) {
    super(props);
    this.state = { data: [], loading: true };
  }

  componentDidMount() {
    this.populateData();
  }

  static renderData(data) {
    return (
      <div>
        {JSON.stringify(data)}
      </div>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchTest.renderData(this.state.data);

    return (
      <div>
        <h1>Data</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateData() {
    const response = await fetch('weatherforecast/GetQuiz');
    const data = await response.json();
    this.setState({ data: data, loading: false });
  }
}
