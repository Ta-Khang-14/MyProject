<template>
    <div
        class="toastMessage flex"
        v-if="isShowToast"
        :class="{
            success: data.status == EnumToast.Success,
            danger: data.status == EnumToast.Danger,
        }"
    >
        <div class="toastMessage__icon">
            <div class="icon icon__toast--success"></div>
            <div class="icon icon__toast--danger"></div>
        </div>
        <div class="toastMessage__content">{{ data.msg }}</div>
        <div class="toastMessage__action" @click="hiddenToast">
            <div class="icon icon__cancel--small"></div>
        </div>
    </div>
</template>
<script>
import EnumMisa from "@/ultis/enum.js";

export default {
    data() {
        return {
            EnumToast: {
                ...EnumMisa.Toast,
            },
            isShowToast: false,
            data: {
                status: EnumMisa.Toast.Success,
                msg: "Thông báo thành công",
            },
        };
    },
    methods: {
        /**
         * Bắt sự kiên click vào nút tắt Toast Message
         * Created: TVKhang(08/10/2022)
         */
        hiddenToast() {
            this.isShowToast = false;
        },
    },
};
</script>
