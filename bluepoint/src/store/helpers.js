import { mapGetters } from "vuex"

//TODO use this in navbar and shit
export const authComputed = {
    ...mapGetters(["loggedIn"])
}