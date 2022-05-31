import React from "react";
import {Layer, Line, Rect} from "react-konva";

import useStore from "../store";

export default () => {
    const labels = useStore(s => s.labels);
    const selectedLabel = useStore(s => s.selectedLabel);
    const setLabel = useStore(s => s.setLabel);
    const labelDetails = labels[selectedLabel];

    function handleChange(event) {
        setLabel(event.target.value)
    }

    debugger

    return (
        <div>
            <select value={selectedLabel} onChange={handleChange}>
            {Object.keys(labels).map(key => {
                const isSelected = key === selectedLabel;

                return (
                    <option value={key}>{key}</option>
                );
            })}
            </select>
            <div style={{background: labelDetails.color}}>
                Selected <br/>
                {selectedLabel}
            </div>
        </div>
    );
};
