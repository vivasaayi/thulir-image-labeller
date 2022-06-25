import React from "react";
import useStore from "../store";


function Region(props) {
    const {id, region, onRemove, selected} = props;

    return (
        <div>
            {selected} # {id} {region.label}
            <button onClick={() => {
                onRemove(region);
            }}>Delete
            </button>
        </div>
    );
}

export default () => {
    const regions = useStore(s => s.regions);
    const setRegions = useStore(s => s.setRegions);
    const selectedId = useStore(s => s.selectedRigionId);

    function onRemoveRegion(region) {
        var newRegions = [];

        regions.forEach(r => {
            if (r.id !== region.id) {
                newRegions.push(r)
            }
        });

        setRegions(newRegions);
    }

    return (
        <div>
            <span>Found {regions.length}</span>

            {regions.map(region => {
                var selected = (region.id == selectedId);
                
                return <Region key={region.id} id={region.id} region={region} onRemove={onRemoveRegion} selected={selected}/>
            })}
        </div>
    );
};
