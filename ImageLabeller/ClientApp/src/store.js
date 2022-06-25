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
    
    

    labels: {
        youngleaf: {
            color: "lightgreen"
        },
        leaf: {
            color: "green"
        },
        dryleaf: {
            color: "grey"
        },
        stem: {
            color: "brown"
        },
        bud: {
            color: "darkolivegreen"
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

