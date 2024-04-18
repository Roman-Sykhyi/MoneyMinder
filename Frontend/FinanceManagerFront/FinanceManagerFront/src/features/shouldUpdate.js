import { createSlice } from "@reduxjs/toolkit";

export const shouldUpdateSlice = createSlice({
    name: "shouldUpdate",
    initialState: { value: false },
    reducers: {
        updatePage: (state, action) => {
            state.value = action.payload;
        },
    },
});

export const { updatePage } = shouldUpdateSlice.actions;
export default shouldUpdateSlice.reducer;