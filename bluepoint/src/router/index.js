import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";

Vue.use(VueRouter);

const routes = [
	{
		path: "/",
		name: "home",
		component: Home,
		children: [
			{
				path: "/about",
				name: "about",
				component: () => import("../views/About.vue")
			},
			{
				path: "/register",
				name: "register",
				component: () => import("../views/Register.vue")
			},
			{
				path: "/login",
				name: "login",
				component: () => import("../views/LogIn.vue")
			},
		]
		
	},
	{
		path: "/chat",
		name: "chat",
		component: () => import("../views/Chat.vue"),
		// TODO
		children: []
	},
];

const router = new VueRouter({
	routes
});

router.beforeEach((to, from, next) => {
	const privatePages = ["/chat"];
	const authRequired = privatePages.includes(to.path);
	const loggedIn = localStorage.getItem("userToken");

	if (authRequired && !loggedIn) {
		return next("/login");
	}
	
	return next();
});

export default router;
