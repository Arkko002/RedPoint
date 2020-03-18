<template>
    <div class="server-container">
        <li v-for="server in serverArray" :key="server.id">
            <label>
                <input type="radio" v-bind:value="server.name"/>
                <img v-bind:src="server.imagepath"/>
            </label>          
        </li>
    </div>
</template>

<script>
export default {
    name: 'ServerList',
    props: ['serverArray']
}
</script>