import React from "react";
import { Image, Layer } from "react-konva";
import useImage from "use-image";
import axios from "axios";

import useStore from "../store";

export default () => {
    const currentImageInfo = useStore(state => state.currentImageInfo);
    const setCurrentImageInfo = useStore(state => state.setCurrentImageInfo);

    const setRegions = useStore(state => state.setRegions);
    const selectRegion = useStore(state => state.selectRegion);

    function loadNextImage() {
        debugger
        var index = currentImageInfo ? currentImageInfo.imageId: -1;
        axios
            .get(`/image/next-image-info?index=${index}`)
            .then(function (response) {
                console.log("Retrieved Image Info", response.data);
                selectRegion(null);
                setRegions([]);
                setCurrentImageInfo(response.data)
            });
    }
    
    function saveAndLoadNextImage() {
        axios
            .post(`/image/next-image-info?index=${currentImageInfo.imageId}`)
            .then(function (response) {
                console.log("Saved Image", response.data);
                loadNextImage();
            });
    }
    
    if(!currentImageInfo) {
        console.log("Current Image Info is Empty")
        setTimeout(() => {
            loadNextImage()
        }, 500)
        return <div>
            <span>Loading</span>
        </div>
    }
    
    return (
        <div>
            <div>
                <button onClick={loadNextImage}>Next Image</button>
            </div>
            <span>{currentImageInfo.imageId} - {currentImageInfo.imageName}</span>
            <div>
                <button onClick={saveAndLoadNextImage}>Save and Load Image</button>
            </div>
        </div>
    );
};
