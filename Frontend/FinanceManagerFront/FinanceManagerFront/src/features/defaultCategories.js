import { createSlice } from "@reduxjs/toolkit";

export const defaultCategoriesSlice = createSlice({
    name: "defaultCategories",
    initialState: { value: [] },
    reducers: {
        selectDefaultCategories: (state, action) => {
            state.value = action.payload;
        },
    },
});

export const { selectDefaultCategories } = defaultCategoriesSlice.actions;
export default defaultCategoriesSlice.reducer;