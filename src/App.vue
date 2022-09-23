<template>
    <div class="container flex">
        <m-menu-left />
        <m-content />
    </div>
</template>

<script>
import MMenuLeft from "./components/layouts/MMenuLeft.vue";
import MContent from "./components/pages/MContent.vue";
export default {
    name: "App",
    components: {
        MMenuLeft,
        MContent,
    },
};
</script>
