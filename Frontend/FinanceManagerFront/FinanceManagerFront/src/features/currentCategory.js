import { createSlice } from "@reduxjs/toolkit";

let defaultCategory = 'All'

export const currentCategorySlice = createSlice({
    name: "currentCategory",
    initialState: { value: { id: -1, name: defaultCategory } },
    reducers: {
        selectCategory: (state, action) => {
            state.value = action.payload;
        },
    },
});

export const { selectCategory } = currentCategorySlice.actions;
export default currentCategorySlice.reducer;