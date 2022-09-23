<template>
    <div
        class="combobox__data droplist__data"
        :style="{
            display: 'block',
            left: `${postionDropList.x - 96}px`,
            top: `${postionDropList.y + 30}px`,
        }"
    >
        <div class="combobox__item">Nhân bản</div>
        <div class="combobox__item" @click="deleteEmployee">Xóa</div>
        <div class="combobox__item">Ngừng sử dụng</div>
    </div>
</template>

<script>
export default {
    props: ["postionDropList"],
    methods: {
        deleteEmployee() {
            this.$emit("deleteEmployee");
        },
    },
};
</script>
