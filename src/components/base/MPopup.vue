<template>
    <div class="dialog isActive">
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
                </div>
                <div class="popup__content--text">
                    {{ msg }}
                </div>
            </div>
            <div
                class="popup__footer dialog__footer flex"
                v-if="type == TypeData.Warning"
            >
                <div class="button button--gray" @click="cancelPopup">Hủy</div>
                <div class="button button--green" @click="actionPopup">
                    Đồng ý
                </div>
            </div>
            <div
                class="popup__footer popup__footer--notify dialog__footer flex"
                v-if="type == TypeData.Notify"
            >
                <div class="button button--green" @click="actionPopup">
                    Đóng
                </div>
            </div>
            <div
                class="popup__footer dialog__footer flex"
                v-if="type == TypeData.Question"
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
import fetchAPI from "@/ultis/fetchAPI.js";
export default {
    props: ["msg", "type", "data", "action"],
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
            let result = await this.actionHandle();
            if (result) {
                this.$emit("closeForm");
                this.$emit("cancelPopup", { isReload: true });
            }
        },

        // Xử lý action form
        // Author: TVKhang 19/09/22
        async actionHandle() {
            let result = 0;
            console.log;
            switch (this.action) {
                // TH xóa
                case EnumMisa.PopUp.Action.Delete:
                    result = await this.deleteEmployeeById();
                    break;
                // TH sửa
                case EnumMisa.PopUp.Action.Update:
                    result = await this.updateEmployeeById();
                    break;
                // TH thêm mới
                case EnumMisa.PopUp.Action.New:
                    result = await this.createEmployee();
                    break;
            }
            return result;
        },

        // Xóa nhân viên
        // Author: TVKhang 19/09/22
        async deleteEmployeeById() {
            let employeeId = this.data.id;
            let result = await fetchAPI(
                `${process.env.VUE_APP_URL}/Employees/${employeeId}`,
                "delete"
            );
            return result;
        },

        // Sửa nhân viên
        // Author: TVKhang 19/09/22
        async updateEmployeeById() {
            let employeeId = this.data.id;
            console.log(JSON.stringify(this.data.data));
            let result = await fetchAPI(
                `${process.env.VUE_APP_URL}/Employees/${employeeId}`,
                "put",
                "",
                this.data.data
            );
            return result;
        },

        // Thêm mới nhân viên
        // Author: TVKhang 20/09/22
        async createEmployee() {
            console.log(JSON.stringify(this.data));
            let result = await fetchAPI(
                `${process.env.VUE_APP_URL}/Employees`,
                "post",
                "",
                this.data.data
            );
            return result;
        },
    },
};
</script>
