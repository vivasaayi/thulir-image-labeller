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

    function loadImage(index) {
        axios
            .get(`/image/next-image-info?currentIndex=${index}`)
            .then(function (response) {
                console.log("Retrieved Image Info", response.data);
                selectRegion(null);
                setRegions([]);
                setCurrentImageInfo(response.data)
            });
    }
    
    function loadNextImage() {
        var index = currentImageInfo ? currentImageInfo.imageIndex: 0;
        loadImage(index)   
    }
    
    function loadPreviousImage() {
        var index = currentImageInfo ? currentImageInfo.imageIndex: 0;
        index = index - 2;
        if(index < 0) {
            index = 0;
        }
        loadImage(index);
    }
    
    function saveImageLabels() {
        
    }
    
    function saveAndLoadNextImage() {
        axios
            .post(`/image/next-image-info?currentIndex=${currentImageInfo.imageIndex}`)
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
            <br />
            <br />
            <div>
                <button onClick={loadPreviousImage}>Previous Image</button>
            </div>
            <br />
            <br />
            <div>
                <button onClick={loadNextImage}>Next Image</button>
            </div>
            <span>{currentImageInfo.imageIndex} - {currentImageInfo.imageName}</span>
            <br />
            <br />
            <div>
                <button onClick={saveImageLabels}>Save</button>
            </div>
            <br />
            <br />
            <div>
                <button onClick={saveAndLoadNextImage}>Save and Load Image</button>
            </div>
        </div>
    );
};
