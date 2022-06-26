import create from "zustand";

const useStore = create(set => ({
    width: window.innerWidth,
    height: window.innerHeight,
    setSize: ({ width, height }) => set({ width, height }),

    imageWidth: 100,
    imageHeight: 100,

    setImageSize: size =>
        set(() => ({ imageWidth: size.width, imageHeight: size.height })),
    scale: 1,
    setScale: scale => set({ scale }),
    isDrawing: false,
    toggleIsDrawing: () => set(state => ({ isDrawing: !state.isDrawing })),

    regionSequenceId: 1,
    setRegionSequenceId: regionSequenceId => set(state => ({ regionSequenceId })),
    
    regions: [],
    setRegions: regions => set(state => ({ regions })),

    selectedRigionId: null,
    selectRegion: selectedRigionId => set({ selectedRigionId }),

    brightness: 0,
    setBrightness: brightness => set({ brightness }),

    currentImageInfo: null,
    setCurrentImageInfo: currentImageInfo => set({currentImageInfo}),

    newIndexVal: 0,
    setNewIndexVal: newIndexVal => set({newIndexVal}),

    isLoading: false,
    setLoading: isLoading => set({isLoading}),

    labels: {
        youngleaf: {
            color: "lightgreen"
        },
        leaf: {
            color: "green"
        },
        unhealthyleaf: {
            color: "darkgreen"
        },
        dryleaf: {
            color: "grey"
        },
        stem: {
            color: "brown"
        },
        crown: {
            color: "darkred"
        },
        bud: {
            color: "darkolivegreen"
        },
        youngboll: {
            color: "darkblue"
        },
        damagedboll: {
            color: "red"
        },
        youngflower: {
            color: "gold"
        },
        flower: {
            color: "yellow"
        },
        boll: {
            color: "white"
        },
        land: {
            color: "black"
        },
        weed: {
            color: "pink"
        },
        gressweed: {
            color: "darkblue"
        },
        arugampulweed: {
            color: "darkyellow"
        },
        dryweed: {
            color: "purple"
        },
        drygrassweed: {
            color: "indigo"
        },
        dryriceplantroot: {
            color: "blue"
        },
    },
    selectedLabel: "leaf",
    setLabel: selectedLabel => set({ selectedLabel })
}));

export default useStore;

