import { createSlice } from "@reduxjs/toolkit";

export const currentWalletSlice = createSlice({
    name: "currentWallet",
    initialState: { value: { id: -1, name: "No wallets", totalSum: '' } },
    reducers: {
        selectWallet: (state, action) => {
            state.value = action.payload;
        },
    },
});

export const { selectWallet } = currentWalletSlice.actions;
export default currentWalletSlice.reducer;