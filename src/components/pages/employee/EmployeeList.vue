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
                <div class="main__table--action" @click="showDeleteAll">
                    <div class="main_table--actionDisplay">
                        Thực hiện hàng loạt
                    </div>
                    <div class="main_table--actionIcon">
                        <div class="icon icon__arrowDown--black"></div>
                    </div>
                    <div class="main__table--option" v-if="isShowDeleteAll">
                        <div
                            class="main__table--optionList"
                            @click="deleteBatchEmployee"
                        >
                            Xóa
                        </div>
                    </div>
                </div>
                <div class="flex">
                    <m-tooltip :isShow="isShowTooltip" :data="tooltipData" />
                    <div class="input input__icon">
                        <input
                            v-model="filterEmployee"
                            type="text"
                            placeholder="Tìm theo mã, tên nhân viên"
                            @keydown="filterEmployeeKeyEvent($event)"
                        />
                        <div
                            class="input--icon icon icon--search"
                            @click="getFilterEmployee"
                        ></div>
                    </div>
                    <div
                        class="main__table--refresh flex"
                        @click="reload"
                        @mouseleave="hiddenTooltip"
                        @mouseover="showTooltip($event, 'Lấy lại dữ liệu')"
                    >
                        <div class="icon icon--refresh"></div>
                        <div class="icon icon__refresh--blue"></div>
                    </div>
                    <div
                        class="main__table--refresh flex"
                        @mouseleave="hiddenTooltip"
                        @mouseover="showTooltip($event, 'Xuất ra file excel')"
                    >
                        <a :href="excelExportLink">
                            <div class="icon icon--excel"></div>
                            <div class="icon icon__excel--blue"></div>
                        </a>
                    </div>
                </div>
            </div>
            <div class="main__table--content">
                <div class="main__table--detail">
                    <m-table
                        ref="tableEmployee"
                        :employees="employees.data"
                        :filterEmployee="filterEmployee"
                        @showFormEditEmployee="showFormEditEmployee"
                        @positionDropList="getPrositionDropList"
                    />
                </div>
            </div>
            <div class="main__table--paging flex">
                <div class="paging--total">
                    Tổng số: <b>{{ employees.totalCount }}</b> bản ghi
                </div>
                <div class="paging--pages flex">
                    <m-combobox
                        :propData="'value'"
                        :propName="'name'"
                        :listData="listRecordsOfPage"
                        selectedId="10"
                        @changeDataCbx="changeRecordsOfPage"
                    />
                    <div class="paging flex">
                        <div
                            class="prev pages__item"
                            @click="
                                pagination.currentPage == 1
                                    ? pagination.currentPage
                                    : pagination.currentPage--
                            "
                        >
                            Trước
                        </div>
                        <div class="pages flex">
                            <div
                                class="pages__item"
                                v-for="(item, index) in pagination.range"
                                :key="index"
                                :class="{
                                    isActive: item == pagination.currentPage,
                                }"
                                @click="changePage(item, index)"
                            >
                                {{ item }}
                            </div>
                        </div>
                        <div
                            class="next pages__item"
                            @click="
                                pagination.currentPage == pagination.totalPage
                                    ? pagination.currentPage
                                    : pagination.currentPage++
                            "
                        >
                            Sau
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <m-toast-message ref="toast" />
    </div>
    <employee-detail
        v-if="isShowDetail"
        @hiddenForm="hiddenForm"
        :employeeId="selectedEmployeeId"
        @updateEmp="updateEmployeeById"
        @createEmp="createEmployee"
        @showFormAddEmployee="showFormAddEmployee"
        ref="employeeDetail"
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
        @closeForm="isShowDetail = false"
        @cancelForm="cancelForm"
    />
</template>

<script>
import EmployeeDetail from "./EmployeeLDetail.vue";
import MTable from "../../base/MTable.vue";
import MDropList from "@/components/base/MDroplist.vue";
import MPopup from "@/components/base/MPopup.vue";
import MCombobox from "@/components/base/MCombobox.vue";
import MTooltip from "@/components/base/MTooltip.vue";
import MToastMessage from "@/components/base/MToastMessage.vue";

import fetchAPI from "@/ultis/fetchAPI.js";
import EnumMisa from "@/ultis/enum.js";
import { simpleFormatString, handleRecordCode } from "@/ultis/format.js";
import pagination from "@/ultis/pagination.js";
export default {
    components: {
        EmployeeDetail,
        MTable,
        MDropList,
        MPopup,
        MCombobox,
        MTooltip,
        MToastMessage,
    },
    data() {
        return {
            isShowDetail: false,
            employees: {
                data: [],
                totalCount: 0,
            },
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

            pagination: {
                currentPage: 1,
                recordPerPage: 10,
                range: [],
            },
            subPagination: {
                index: 0,
                page: 1,
            },
            isShowDeleteAll: false,
            listRecordsOfPage: [
                { value: 10, name: "10 bản ghi trên 1 trang" },
                { value: 20, name: "20 bản ghi trên 1 trang" },
                { value: 30, name: "30 bản ghi trên 1 trang" },
                { value: 50, name: "50 bản ghi trên 1 trang" },
                { value: 100, name: "100 bản ghi trên 1 trang" },
            ],

            tooltipData: {},
            isShowTooltip: false,

            excelExportLink: `${process.env.VUE_APP_URL}/Employees/exportEmployees`,
        };
    },
    methods: {
        // Xử lý sự hiện hiện form thêm mới nhân viên
        // Author: TVKHANG(11/09/22)
        async showFormAddEmployee() {
            this.isShowDetail = true;
            this.selectedEmployeeId = "";
            // Truyền mã code mới
            let employeeCode = await this.getMaxCode();
            employeeCode = handleRecordCode(employeeCode, "NV-");

            this.$refs.employeeDetail.employee.employeeCode = employeeCode;
        },

        // Xủ lý sự kiện ẩn form thêm mới nhân viên
        // Author: TVKHANG(11/09/22)
        hiddenForm() {
            this.isShowDetail = false;
        },
        cancelForm(data) {
            this.cancelPopup(data);
            this.hiddenForm();
        },
        // Lấy mã nhân viên lớn nhất từ serve
        async getMaxCode() {
            let code = await fetchAPI(
                `${process.env.VUE_APP_URL}/Employees/max-code`
            );
            return code;
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
                `${process.env.VUE_APP_URL}/Employees/filter?offset=${
                    (this.pagination.currentPage - 1) *
                    this.pagination.recordPerPage
                }&limit=${this.pagination.recordPerPage}`
            );
            this.handlePagination();
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

        // Bắt sự kiện click vào nút tìm kiếm nhân viên
        async getFilterEmployee() {
            this.filterEmployee = simpleFormatString(this.filterEmployee);
            this.employees = await fetchAPI(
                `${process.env.VUE_APP_URL}/Employees/filter?keyword=${this.filterEmployee}`
            );
        },

        // Bắt sự kiện keyup trong input tìm kiếm nhân viên
        filterEmployeeKeyEvent(e) {
            if (e.keyCode == EnumMisa.KeyCode.Enter) {
                this.getFilterEmployee();
            }
        },

        // Bắt sự kiện click trang
        changePage(item, index) {
            if (Number.isInteger(item)) {
                this.pagination.currentPage = item;
                this.subPagination.page = item;
            } else {
                this.subPagination.page =
                    this.pagination.range.length - 2 == index
                        ? this.subPagination.page + 2
                        : this.subPagination.page - 2;
                this.pagination.range = pagination(
                    this.subPagination.page,
                    this.pagination.totalPage
                );
            }
        },

        // Tính toán cho phân trang
        handlePagination() {
            // Phân trang
            this.pagination.totalPage = Math.ceil(
                this.employees.totalCount / this.pagination.recordPerPage
            );
            this.pagination.range = pagination(
                this.pagination.currentPage,
                this.pagination.totalPage
            );
        },
        // Bắt sự kiện khi người dùng chọn số trang 1 page
        changeRecordsOfPage(data) {
            this.pagination.recordPerPage = data;
        },

        // Click vào thao tác hàng loạt
        showDeleteAll() {
            if (this.$refs.tableEmployee.listSelectedEmployee.length != 0) {
                this.isShowDeleteAll = !this.isShowDeleteAll;
            }
        },

        // Thực hiện xóa nhiều vân viên
        async deleteBatchEmployee() {
            this.isShowDetail = false;

            await fetchAPI(
                `${process.env.VUE_APP_URL}/Employees/delete-batch`,
                "post",
                "",
                this.$refs.tableEmployee.listSelectedEmployee
            );

            await this.reload();
        },

        /**
         * Sự kiện hiện tooltip
         * Created: TVKhang(08/10/2022)
         */
        showTooltip(e, msg) {
            this.isShowTooltip = true;

            this.tooltipData = {
                x: e.pageX - 16 - 280,
                y: e.pageY - 56 - 8 - 32,
                with: e.target.offsetWidth,
                height: e.target.offsetHeight,
                msg,
            };
        },

        /**
         * Sự kiện ẩn tooltip
         * Created: TVKhang(08/10/2022)
         */
        hiddenTooltip() {
            this.isShowTooltip = false;
        },

        /**
         * Hiện toast message
         * CreatedBy: TVKhang(08/10/2022)
         */
        showToast(msg, status) {
            this.$refs["toast"].isShowToast = true;

            // Tự động ẩn toast sau 3s
            setTimeout(() => {
                this.$refs["toast"].isShowToast = true;
            }, 3000);

            this.$$refs["toast"].data.msg = msg;
            this.$$refs["toast"].data.status = status;
        },
    },
    // Gọi dữ liệu từ API
    // Author: TVKHANG(11/09/22)
    async created() {
        this.employees = await fetchAPI(
            `${process.env.VUE_APP_URL}/Employees/filter?offset=0&limit=10&keyword=${this.filterEmployee}`
        );
        this.handlePagination();
    },
    watch: {
        async "pagination.currentPage"() {
            this.employees = await fetchAPI(
                `${process.env.VUE_APP_URL}/Employees/filter?offset=${
                    (this.pagination.currentPage - 1) *
                    this.pagination.recordPerPage
                }&limit=${this.pagination.recordPerPage}&keyword=${
                    this.filterEmployee
                }`
            );
        },

        async "pagination.recordPerPage"() {
            this.employees = await fetchAPI(
                `${process.env.VUE_APP_URL}/Employees/filter?offset=${
                    (this.pagination.currentPage - 1) *
                    this.pagination.recordPerPage
                }&limit=${this.pagination.recordPerPage}&keyword=${
                    this.filterEmployee
                }`
            );
            this.handlePagination();
        },

        employees() {
            this.handlePagination();
        },
    },
};
</script>
