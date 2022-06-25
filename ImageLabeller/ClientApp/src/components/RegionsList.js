import React from "react";
import {
    useSortable,
    SortableContext,
    sortableKeyboardCoordinates,
    verticalListSortingStrategy
} from '@dnd-kit/sortable';
import {CSS} from '@dnd-kit/utilities';
import {arrayMoveImmutable} from "array-move";

import { findIndex } from 'lodash/array'

import useStore from "../store";
import {closestCenter, DndContext, KeyboardSensor, PointerSensor, useSensor, useSensors} from "@dnd-kit/core";


function Region(props) {
    const {id, region, onRemove} = props;
    
    return (
        <div>
            Region # {id} {region.label}
            <button onClick={() => {
                onRemove(region);
            }}>Delete</button>
        </div>
    );
}

export default () => {
    const regions = useStore(s => s.regions);
    const setRegions = useStore(s => s.setRegions);
    
    console.log(regions)
    
    function onRemoveRegion(region) {
        debugger;
        var newRegions = [];
        
        regions.forEach(r => {
            if(r.id !== region.id) {
                newRegions.push(r)
            }
        });
        
        setRegions(newRegions);
    }

    return (
        <div>
            <span>Found {regions.length}</span>

            {regions.map(region => <Region key={region.id} id={region.id} region={region} onRemove={onRemoveRegion}/>)}
        </div>
    );
};
