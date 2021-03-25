<template>
	<section class="main">
		<article class="main__grid main__grid--first">
			<div class="grid__text">
			Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris gravida arcu id ipsum hendrerit feugiat. Maecenas at lacus et lectus elementum convallis non vitae odio. Etiam quis rhoncus nisl. In consequat efficitur leo vel pellentesque. Ut viverra, lacus eget hendrerit rutrum, diam est egestas lacus, at vulputate velit nulla ac nulla.
			</div>

			<div class="grid__image">
				<!--TODO-->
				<img src="@/assets/logo-200px.png">
			</div>
		</article>
		
		<article class="main__grid main__grid--second">
			<div class="grid__text">
				Etiam egestas tellus turpis. Ut feugiat diam in ullamcorper eleifend. Maecenas at eros auctor, iaculis nibh in, suscipit augue. Vivamus posuere a erat eget commodo. Praesent ornare, lectus ac fermentum laoreet, diam velit egestas augue, non condimentum turpis urna ac nisl. In accumsan nisi rutrum tincidunt auctor.
			</div>

			<div class="grid__image">
				<!--TODO-->
				<img src="@/assets/logo-200px.png" alt="">
			</div>
		</article>
	</section>
</template>

<script>
export default {
	name: "Main"
	
};
</script>

<style>
.main {
	display: grid;
	height: 70%;
	width: 90%;
	grid-template-columns: 1fr;
	gap: 30%;
}

.main__grid {
	display: grid;
}

.main__grid--first {
	grid-template-areas: "text image";
	animation: fade-in 2s, slidin-from-left 1s;
}

.main__grid--second {
	grid-template-areas: "image text";
	animation: fade-in 2s, slidin-from-right 1s;
}

.grid__image {
	grid-area: image;
}

.grid__text {
	grid-area: text;

	display: flex;
	align-items: center;
	justify-content: center;
	text-align: center;

	background-color: var(--alt-bg-color);
	border-radius: 30px;
	box-shadow: 5px 5px 3px 1px;
}

@keyframes fade-in {
	from {
		opacity: 0;
	}

	to {
		opacity: 1;
	}
}

@keyframes slidin-from-left {
	/*TODO*/
	from {
		transform: translateX(-50%);
	}

	to {
		transform: translateX(0%);
	}
}

@keyframes slidin-from-right {
	/*TODO*/
	from {
		transform: translateX(50%);
	}

	to {
		transform: translateX(0%);
	}
}

</style>>
