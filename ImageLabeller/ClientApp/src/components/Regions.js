import React from "react";
import {Layer, Line, Rect} from "react-konva";

import useStore from "../store";

export default () => {
    const regions = useStore(s => s.regions);
    const layerRef = React.useRef(null);

    const selectedId = useStore(s => s.selectedRigionId);
    const selectRegion = useStore(s => s.selectRegion);

    const labels = useStore(s => s.labels);

    return (
        <Layer ref={layerRef}>
            {regions.map(region => {
                const isSelected = region.id === selectedId;
                const x1y1 = region.points[0];
                const x2y2 = region.points[region.points.length -1]
                const width = x2y2.x - x1y1.x;
                const height = x2y2.y - x1y1.y;
                const selectedColor = labels[region.label].color;
                return (
                    <React.Fragment key={region.id}>
                        <Rect
                            x={x1y1.x}
                            y={x1y1.y}
                            width={width}
                            height={height}
                            fill={selectedColor}
                            listening={false}
                            opacity={isSelected ? 1 : 0.8}
                            onClick={() => {
                                selectRegion(region.id);
                            }}
                        />
                    </React.Fragment>
                );
            })}
        </Layer>
    );
};
