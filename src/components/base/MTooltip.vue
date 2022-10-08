<template>
    <div class="tooltip">
        {{ title }}
    </div>
</template>

<script>
export default {
    props: ["title", "data"],
    data() {
        return {
            position: {},
        };
    },
};
</script>
