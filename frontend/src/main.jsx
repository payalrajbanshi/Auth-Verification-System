import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter } from "react-router-dom";
import App from "./App";
import "bootstrap/dist/css/bootstrap.min.css";
import '@syncfusion/ej2-base/styles/material.css';
import '@syncfusion/ej2-buttons/styles/material.css';
import "@syncfusion/ej2-icons/styles/material.css";
import "@syncfusion/ej2-popups/styles/material.css";
import "@syncfusion/ej2-react-grids/styles/material.css";
import '@syncfusion/ej2-inputs/styles/material.css';
import '@syncfusion/ej2-layouts/styles/material.css';
//import '@syncfusion/ej2-react-charts/styles/material.css';
import {registerLicense} from '@syncfusion/ej2-base';

registerLicense('Ngo9BigBOggjHTQxAR8/V1JGaF5cXGpCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWH1cc3RXQ2hZWEV+WkFWYEs=');

import "./style/App.css";
ReactDOM.createRoot(document.getElementById("root")).render(
  // <React.StrictMode>
  //   <BrowserRouter>
  //     <App />
  //   </BrowserRouter>
  // </React.StrictMode>
  <BrowserRouter>
  <App/>
  </BrowserRouter>
);
