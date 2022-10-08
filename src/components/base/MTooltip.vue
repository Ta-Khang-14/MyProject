<template>
    <div
        class="tooltip"
        :style="{ top: data.y + 'px', left: data.x + 'px' }"
        v-if="isShow"
    >
        {{ data.msg }}
    </div>
</template>

<script>
export default {
    props: ["title", "data", "isShow"],
    data() {
        return {
            position: {},
        };
    },
};
</script>
