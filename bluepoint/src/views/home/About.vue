<template>
	<section class="about">
		<article class="about__text">

			<h1>About</h1>

			<!--TODO All of about-->
			<p>
			Logo was provided free of charge by FreeLogoDesign at FreeLogoDesign.org.
			</p>
		</article>
	</section>
</template>;

<script>
/**
 * Component used as a router view for about page`.
 */
export default {
	name: "About",
};
</script>

<style>
.about {
	display: flex;
	flex-direction: column;
	justify-content: center;
	align-items: center;

	text-align: center;

	background-color: var(--alt-bg-color);
	border-radius: 30px;
	box-shadow: 5px 5px 3px 1px;
}
</style>>
