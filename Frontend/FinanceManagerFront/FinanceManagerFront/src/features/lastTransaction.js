import { createSlice } from "@reduxjs/toolkit";

export const lastTransactionSlice = createSlice({
    name: "lastTransaction",
    initialState: { value: { walletId: -1, amount: 0, creationTime: null, category: null } },
    reducers: {
        selectTransaction: (state, action) => {
            state.value = action.payload;
        },
    },
});

export const { selectTransaction } = lastTransactionSlice.actions;
export default lastTransactionSlice.reducer;