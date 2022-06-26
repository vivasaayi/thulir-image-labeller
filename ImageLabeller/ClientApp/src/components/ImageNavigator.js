import React, {useState} from "react";
import { Image, Layer } from "react-konva";
import useImage from "use-image";
import axios from "axios";

import useStore from "../store";

export default () => {
    const isLoading = useStore(state => state.isLoading);
    const setLoading = useStore(state => state.setLoading);
    
    
    const currentImageInfo = useStore(state => state.currentImageInfo);
    const setCurrentImageInfo = useStore(state => state.setCurrentImageInfo);

    const setRegionSequenceId = useStore((state) => state.setRegionSequenceId);
    const setRegions = useStore(state => state.setRegions);
    const selectRegion = useStore(state => state.selectRegion);
    const regions = useStore(s => s.regions);

    const newIndexVal = useStore(s => s.newIndexVal);
    const setNewIndexVal = useStore(s => s.setNewIndexVal)
    
    function setCurrentIndex () {
        // debugger
        loadImage(newIndexVal)
    }
    
    function updateCurrentIndex(e) {
        // debugger
        // console.log(e.target.value);
        setNewIndexVal(e.target.value);
    }
    
    function getMaxRegionId(regions) {
        if(regions.length === 0){
            return 1;
        }
        
        var maxId = -1;
        
        regions.forEach(region => {
            if(region.id > maxId) {
                maxId = region.id;
            }
        })
        
        return maxId + 1;
    }

    function loadImage(index) {
        setLoading(true);
        axios
            .get(`/image/next-image-info?currentIndex=${index}`)
            .then(function (response) {
                
                var maxRegionId = getMaxRegionId(response.data.imageLabels.labels);
                
                console.log(response.data.imageLabels.labels)
                console.log("Retrieved Image Info", response.data);
                setRegions(response.data.imageLabels.labels);
                selectRegion(null);
                setRegionSequenceId(maxRegionId);
                setCurrentImageInfo(response.data.sourceImage);
                setLoading(false);
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
        if(!currentImageInfo) {
            alert("Please select an image");
            return;
        }
        
        if(regions.length <=0) {
            alert("Please add some labels");
            return;
        }
        
        console.log(currentImageInfo, regions);

        setLoading(true);
        return axios
            .post(`/labels?imageId=${currentImageInfo.imageId}`, regions)
            .then(function (response) {
                console.log("Saved Image", response.data);
                setLoading(false);
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
            <div>
                <input type="number" value={newIndexVal} onChange={updateCurrentIndex}/>
                <button onClick={setCurrentIndex}>Load</button>
            </div>
            <br />
            <br />
            <div>
                <button onClick={loadPreviousImage}>Previous Image</button>
            </div>
            <br />
            <div>
                <button onClick={loadNextImage}>Next Image</button>
            </div>
            <span>{currentImageInfo.imageIndex} - {currentImageInfo.imageName}</span>
            <br />
            <br />
            <br />
            <div>
                <button onClick={saveImageLabels}>Save</button>
            </div>
            <br />
            <br />
        </div>
    );
};
