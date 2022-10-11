<template>
    <div class="dialog isActive" @keyup="handleKeyEvent($event)" tabindex="-1">
        <div class="popup">
            <div class="popup__content flex">
                <div class="popup__content--icon">
                    <div
                        class="icon__warning"
                        v-if="type === TypeData.Warning"
                    ></div>
                    <div
                        class="icon__question"
                        v-if="type === TypeData.Question"
                    ></div>
                    <div
                        class="icon__error"
                        v-if="type === TypeData.Error"
                    ></div>
                </div>
                <div class="popup__content--text">
                    {{ msg }}
                </div>
            </div>
            <div
                class="popup__footer dialog__footer flex"
                v-if="action == TypeData.Action.Delete"
            >
                <div class="button button--gray" @click="cancelPopup">Hủy</div>
                <div class="button button--green" @click="actionPopup">
                    Đồng ý
                </div>
            </div>
            <div
                class="popup__footer popup__footer--notify dialog__footer flex"
                v-if="action == TypeData.Action.Notify"
            >
                <div class="button button--green" @click="cancelPopup">
                    Đóng
                </div>
            </div>
            <div
                class="popup__footer dialog__footer flex"
                v-if="
                    action == TypeData.Action.Update ||
                    action == TypeData.Action.New
                "
            >
                <div class="button button--gray" @click="cancelPopup">Hủy</div>
                <div class="button__field flex">
                    <div class="button button--gray" @click="cancelForm">
                        Không
                    </div>
                    <div class="button button--green" @click="actionPopup">
                        Đồng ý
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import EnumMisa from "@/ultis/enum.js";
export default {
    props: {
        msg: {
            type: String,
        },
        type: {
            type: Number,
        },
        data: {
            type: Object,
        },
        action: {
            type: Number,
        },
        method: {
            type: Function,
            default: () => ({ errorCode: 1 }),
        },
    },
    data() {
        return {
            TypeData: {
                ...EnumMisa.PopUp,
            },
        };
    },
    methods: {
        // Bắt sự kiện click vào hủy, đóng
        // Author: TVKhang 18/09/22
        cancelPopup() {
            this.$emit("cancelPopup", { isReload: false });
        },
        cancelForm() {
            this.$emit("cancelForm", { isReload: false });
        },

        // Bắt sự kiện click vào xóa/thêm/sửa
        // Author: TVKhang 19/09/22
        async actionPopup() {
            this.$emit("loading");
            let result = await this.actionHandle();
            if (result) {
                this.$emit("closeForm");
                this.$emit("cancelPopup", { isReload: true });
            }
        },

        // Xử lý action form
        // Author: TVKhang 19/09/22
        async actionHandle() {
            let result = { errorCode: 1 };
            result = await this.method(this.data);
            this.$emit("completeActionPopup", result);
        },

        // Xử lý keyup popup
        // Author: TVKhang 09/10/22
        handleKeyEvent(e) {
            switch (e.keyCode) {
                case EnumMisa.KeyCode.Enter:
                    this.actionHandle();
                    break;
                case EnumMisa.KeyCode.ESC: {
                    this.cancelPopup();
                    break;
                }
            }
        },
    },
};
</script>
