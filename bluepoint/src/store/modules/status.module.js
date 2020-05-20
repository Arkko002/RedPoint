const statusType = {   
	ONLINE: "online",   
	AWAY: "away",   
	OFFLINE: "offline",
};


//TODO should this be a vuex state?
export const status = {
	state : {
		userStatus: statusType.OFFLINE
	}
};