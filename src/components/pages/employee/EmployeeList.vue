<template>
    <div class="main">
        <div class="main--function flex">
            <div class="title">Nhân viên</div>
            <div
                class="button button--green"
                id="btn__employee--add"
                @click="showFormAddEmployee"
            >
                Thêm mới nhân viên
            </div>
        </div>
        <div class="main__table">
            <div class="main__table--search flex">
                <div class="input input__icon">
                    <input
                        v-model="filterEmployee"
                        type="text"
                        placeholder="Tìm theo mã, tên nhân viên"
                    />
                    <div class="input--icon icon icon--search"></div>
                </div>
                <div class="main__table--refresh flex" @click="reload">
                    <div class="icon icon--refresh"></div>
                </div>
            </div>
            <div class="main__table--content">
                <div class="main__table--detail">
                    <m-table
                        ref="tableEmployee"
                        :employees="employees"
                        :filterEmployee="filterEmployee"
                        @showFormEditEmployee="showFormEditEmployee"
                        @positionDropList="getPrositionDropList"
                    />
                </div>
            </div>
            <div class="main__table--paging flex">
                <div class="paging--total">
                    Tổng số: <b>{{ totalNumberOfEmployees }}</b> bản ghi
                </div>
                <div class="paging--pages flex">
                    <div class="combobox">
                        <input
                            class="cbx__input"
                            type="text"
                            placeholder="- None -"
                        />
                        <div class="cbx__button">
                            <div class="icon__arrowDown--black"></div>
                        </div>
                        <div class="combobox__data">
                            <div class="combobox__item">
                                10 bản ghi trên trang
                            </div>
                            <div class="combobox__item">
                                20 bản ghi trên trang
                            </div>
                            <div class="combobox__item">
                                30 bản ghi trên trang
                            </div>
                            <div class="combobox__item">
                                50 bản ghi trên trang
                            </div>
                            <div class="combobox__item">
                                100 bản ghi trên trang
                            </div>
                        </div>
                    </div>
                    <div class="paging flex">
                        <div class="prev pages__item">Trước</div>
                        <div class="pages flex">
                            <div class="pages__item isActive">1</div>
                            <div class="pages__item">2</div>
                            <div class="pages__item">...</div>
                            <div class="pages__item">10</div>
                        </div>
                        <div class="next pages__item">Sau</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <employee-detail
        v-if="isShowDetail"
        @hiddenForm="hiddenForm"
        :employeeId="selectedEmployeeId"
        @updateEmp="updateEmployeeById"
        @createEmp="createEmployee"
    />
    <m-drop-list
        :postionDropList="postionDropList"
        v-if="isShowDropList"
        @deleteEmployee="deleteEmployee"
    />
    <m-popup
        :msg="popupData.msg"
        :type="popupData.type"
        :data="popupData.data"
        :action="popupData.action"
        @cancelPopup="cancelPopup"
        v-if="isShowPopup"
        @closeForm="hiddenForm"
    />
</template>

<script>
import EmployeeDetail from "./EmployeeLDetail.vue";
import MTable from "../../base/MTable.vue";
import MDropList from "@/components/base/MDroplist.vue";
import MPopup from "@/components/base/MPopup.vue";

import fetchAPI from "@/ultis/fetchAPI.js";
import EnumMisa from "@/ultis/enum.js";

export default {
    components: { EmployeeDetail, MTable, MDropList, MPopup },
    data() {
        return {
            isShowDetail: false,
            employees: [],
            totalNumberOfEmployees: 0,
            filterEmployee: "",
            selectedEmployeeId: "",
            postionDropList: {
                x: 0,
                y: 0,
            },
            isShowDropList: false,
            isShowPopup: false,
            popupData: {
                msg: "",
                type: 0,
                action: 0,
                data: {},
            },
        };
    },
    methods: {
        // Xử lý sự hiện hiện form thêm mới nhân viên
        // Author: TVKHANG(11/09/22)
        showFormAddEmployee() {
            this.isShowDetail = true;
            this.selectedEmployeeId = "";
        },

        // Xủ lý sự kiện ẩn form thêm mới nhân viên
        // Author: TVKHANG(11/09/22)
        hiddenForm() {
            this.isShowDetail = false;
        },

        // Bắt sự kiện mở form chỉnh sửa nhân viên và xử lý
        // Author: TVKhang 12/09/22
        showFormEditEmployee(data) {
            if (data.mode == EnumMisa.FormMode.Edit) {
                // Lấy thông tin nhân viên được chọn
                this.isShowDetail = true;
                this.selectedEmployeeId = data.employeeId;
            }
        },

        // Bắt sự kiện click btn reload lại data
        // Author: TVKhang 12/09/22
        async reload() {
            this.employees = await fetchAPI(
                `${process.env.VUE_APP_URL}/Employees`
            );
        },

        // Lấy vị trí droplist được click
        // Author: TVKhang 14/09/22
        getPrositionDropList(data) {
            this.isShowDropList = !this.isShowDropList;
            this.postionDropList = data;
        },

        // Bắt sự kiện click vào xóa
        // Author: TVKhang 18/09/22
        async deleteEmployee() {
            // Truyền dữ liệu cho popup
            let msg = `Bạn có muốn xóa nhân viên ${this.postionDropList.code} không?`;
            let type = EnumMisa.PopUp.Warning;
            let action = EnumMisa.PopUp.Action.Delete;
            let data = {
                id: this.postionDropList.id,
                code: this.postionDropList.code,
            };
            this.tranferDataToPopup(msg, type, action, data);

            // Ẩn drop list khi hiện popup
            this.$refs.tableEmployee.isShowEditCbx = false;
            this.isShowDropList = false;
        },

        // Bắt sự kiện click vào sửa
        // Author: TVKhang 19/09/22
        updateEmployeeById(emp) {
            //Truyền dữ liệu cho popup
            let msg = `Bạn có muốn sửa nhân viên ${emp.code} không?`;
            let type = EnumMisa.PopUp.Question;
            let action = EnumMisa.PopUp.Action.Update;
            let data = {
                id: emp.id,
                data: emp.data,
            };
            this.tranferDataToPopup(msg, type, action, data);
        },

        // Bắt sự kiện click vào cất
        // Author: TVKhang 19/09/22
        createEmployee(data) {
            let msg = `Bạn có muốn thêm mới nhân viên không?`;
            let type = EnumMisa.PopUp.Question;
            let action = EnumMisa.PopUp.Action.New;
            this.tranferDataToPopup(msg, type, action, data);
        },

        // Truyền dữ liệu cho popup
        tranferDataToPopup(msg, type, action, data) {
            this.isShowPopup = true;
            this.popupData.msg = msg;
            this.popupData.type = type;
            this.popupData.data = data;
            this.popupData.action = action;
        },

        // Bắt sự kiện click vào nút hủy Popup
        // Author: TVKhang 18/09/22
        cancelPopup(data) {
            this.isShowPopup = false;
            if (data.isReload) {
                this.reload();
            }
        },
    },
    // Gọi dữ liệu từ API
    // Author: TVKHANG(11/09/22)
    async created() {
        this.employees = await fetchAPI(`${process.env.VUE_APP_URL}/Employees`);
        this.totalNumberOfEmployees = this.employees.length;
    },
};
</script>
