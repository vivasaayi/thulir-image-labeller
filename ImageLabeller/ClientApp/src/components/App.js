import React from "react";

import Canvas from "./Canvas";
import RegionsList from "./RegionsList";

import useStore from "../store";
import Labels from "./Labels";
import ImageNavigator from "./ImageNavigator";

export default () => {
    const { setBrightness } = useStore();

    return (
        <React.Fragment>
            <div className="App">
                <div className="left-panel">
                    <div>
                        <ImageNavigator />
                    </div>
                    <div>
                        <Labels />
                    </div>
                    <br />
                    <br />
                    Brigthess
                    <input
                        id="slider"
                        type="range"
                        min="-1"
                        max="1"
                        step="0.05"
                        defaultValue="0"
                        onChange={e => {
                            setBrightness(parseFloat(e.target.value));
                        }}
                    />
                    <RegionsList />
                </div>
                <div className="right-panel">
                    <Canvas />
                </div>
            </div>
        </React.Fragment>
    );
};

