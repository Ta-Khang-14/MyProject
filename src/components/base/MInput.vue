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
        ref="input"
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
            isFocus: false,
        };
    },
    methods: {
        // Xử lý khi nhập vào input
        // Author: TVKhang(20/09/22)
        eventInput() {
            this.hasError = false;
            this.msg = "";
        },

        /**
         * Set auto focus cho input
         * Author: TVKhang(27/09/22)
         */
        focus() {
            this.$refs.input.focus();
        },
    },
    watch: {
        defaultValue() {
            this.value = this.defaultValue || "";
        },
    },
};
</script>
