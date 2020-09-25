import React from "react";
import ReactDOM from "react-dom";

const TestComponent = () => <h1>Our test component</h1>;

ReactDOM.render(
    <TestComponent />,
    document.getElementById("#component-container")
);
