export const chatState = {
    state: {
        currentChannelId: 0,
        currentServerId: 0
    },
    
    mutations: {
        changeCurrentChannel (state, newId) {
            state.currentChannelId = newId;
        },
        changeCurrentServer (state, newId) {
            state.currentServerId = newId;
        }
    }
}