import React from "react";
import "./styles.css";
import { createRoot } from 'react-dom/client';

import App from "./components/App";

const rootElement = document.getElementById("root");
const root = createRoot(rootElement); // createRoot(container!) if you use TypeScript
root.render(<App />);