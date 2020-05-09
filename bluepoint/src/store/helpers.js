import { mapGetters } from "vuex"l

export const authComputed = {
    ...mapGetters(["loggedIn"]);
}