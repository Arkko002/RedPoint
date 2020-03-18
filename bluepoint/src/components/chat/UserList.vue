<template>
    <div class="user-container">
        <li v-for="user in userArray" :key="user.id">
            <!-- TODO -->       
        </li>
    </div>
</template>

<script>
export default {
    name: 'UserList',
    props: ['userArray']
}
</script>