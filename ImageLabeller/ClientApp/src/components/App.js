import React from "react";

import Canvas from "./Canvas";
import RegionsList from "./RegionsList";

import useStore from "../store";
import Labels from "./Labels";
import ImageNavigator from "./ImageNavigator";
import PropagateLoader from "react-spinners/PropagateLoader";

export default () => {
    const { setBrightness } = useStore();
    const isLoading = useStore(state => state.isLoading);

    return (
        <React.Fragment>
            <div className="App">
                <div>
                    <div className="spinner">
                        <PropagateLoader
                            size={15}
                            color={"#123abc"}
                            loading={isLoading}
                        />
                    </div>
                </div>
                <div className="left-panel">
                    <div>
                        <ImageNavigator />
                    </div>
                    <div>
                        <Labels />
                    </div>
                    <br />
                    <br />
                    Opacity
                    <input
                        id="slider"
                        type="range"
                        min="0"
                        max="1"
                        step="0.05"
                        defaultValue="0.4"
                        onChange={e => {
                            setBrightness(parseFloat(e.target.value));
                        }}
                    />
                    <br />
                    <RegionsList />
                </div>

                <div className="right-panel">
                    <Canvas />
                </div>
            </div>
        </React.Fragment>
    );
};

