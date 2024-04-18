import { createSlice } from "@reduxjs/toolkit";

export const currentPeriodSlice = createSlice({
    name: "currentPeriod",
    initialState: { value: 'Month' },
    reducers: {
        selectPeriod: (state, action) => {
            state.value = action.payload;
        },
    },
});

export const { selectPeriod } = currentPeriodSlice.actions;
export default currentPeriodSlice.reducer;