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


function SortableItem(props) {
    const {id} = props;
    const {
        attributes,
        listeners,
        setNodeRef,
        transform,
        transition,
    } = useSortable({id: props.id});

    const style = {
        transform: CSS.Transform.toString(transform),
        transition,
    };

    return (
        <div ref={setNodeRef} style={style} {...attributes} {...listeners}>
            Region # {id}
            <button
                onClick={() => {
                    // onRemove(id);
                }}
            >
                Delete
            </button>
        </div>
    );
}

export default () => {
    const regions = useStore(s => s.regions);
    const setRegions = useStore(s => s.setRegions);

    const sensors = useSensors(
        useSensor(PointerSensor),
        useSensor(KeyboardSensor, {
            coordinateGetter: sortableKeyboardCoordinates,
        })
    );

    return (
        <div>
            <span>Found {regions.length}</span>
            <DndContext sensors={sensors} collisionDetection={closestCenter} onDragEnd={(event) => {
                const {active, over} = event;

                if (active.id !== over.id) {
                    debugger
                    const activeIndex = findIndex(regions, function(r) { return r.id == active.id; });
                    const overIndex = findIndex(regions, function(r) { return r.id == over.id; });

                    const newRegions = arrayMoveImmutable(regions, activeIndex , overIndex)
                    setRegions(newRegions)
                }
            }}>
                <SortableContext
                    items={regions}
                    strategy={verticalListSortingStrategy}
                    onSortEnd={({oldIndex, newIndex}) => {
                        debugger
                        setRegions(arrayMoveImmutable(regions, oldIndex, newIndex));
                    }}
                    onRemove={index => {
                        regions.splice(index, 1);
                        setRegions(regions.concat());
                    }}
                >
                    {regions.map(region => <SortableItem key={region.id} id={region.id}/>)}
                </SortableContext>
            </DndContext>

        </div>
    );
};
