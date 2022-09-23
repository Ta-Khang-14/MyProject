<template>
    <div class="label" v-if="labelRequired">{{ labelRequired }}<b>*</b></div>
    <div class="label" v-if="label">{{ label }}</div>
    <input
        :tabindex="tabIndex"
        class="input__value"
        :type="type"
        :placeholder="placeholder"
        v-model="value"
        :class="{ 'input--error': hasError }"
        @input="eventInput"
    />
    <div v-if="hasError" class="input__msg">{{ msg }}</div>
</template>

<script>
export default {
    props: {
        labelRequired: {
            type: String,
            default: "",
        },
        label: {
            type: String,
            default: "",
        },
        type: {
            type: String,
            default: "text",
        },
        validateField: {
            type: Array,
            default: () => [],
        },
        tabIndex: {
            type: String,
        },
        isFocus: {
            type: Boolean,
            // default: false,
        },
        defaultValue: {
            type: String,
            default: "",
        },
        placeholder: {
            type: String,
            default: "",
        },
    },
    data() {
        return {
            value: "",
            hasError: false,
            msg: "",
        };
    },
    methods: {
        // Xử lý khi nhập vào input
        // Author: TVKhang(20/09/22)
        eventInput() {
            this.hasError = false;
            console.log("NHập");
        },
    },
    watch: {
        defaultValue() {
            this.value = this.defaultValue;
        },
    },
};
</script>
