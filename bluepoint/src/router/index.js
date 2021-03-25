import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/home/Home.vue";

Vue.use(VueRouter);

const routes = [
	{
		path: "/",
		name: "home",
		component: Home,
		children: [
			{
				path: "/",
				name: "main",
				component: () => import("../views/home/Main.vue")
			},
			{
				path: "/about",
				name: "about",
				component: () => import("../views/home/About.vue")
			},
			{
				path: "/register",
				name: "register",
				component: () => import("../views/home/Register.vue")
			},
			{
				path: "/login",
				name: "login",
				component: () => import("../views/home/LogIn.vue")
			},
		]
		
	},
	{
		path: "/chat",
		name: "chat",
		component: () => import("../views/chat/Chat.vue"),
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
