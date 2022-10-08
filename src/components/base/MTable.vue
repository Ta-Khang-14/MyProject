<template>
    <table>
        <m-tooltip :isShow="isShowTooltip" :data="tooltipData" />
        <thead>
            <tr>
                <th>
                    <input
                        class="input--selected"
                        type="checkbox"
                        name=""
                        id=""
                        @click="selectAllEmployee"
                    />
                </th>
                <th class="text--left">mã nhân viên</th>
                <th class="text--left">tên nhân viên</th>
                <th class="text--left">giới tính</th>
                <th class="text--center">ngày sinh</th>
                <th
                    class="text--left"
                    @mouseleave="hiddenTooltip"
                    @mouseover="showTooltip($event, 'Số chứng minh nhân dân')"
                >
                    số cmnnd
                </th>
                <th class="text--left">chức danh</th>
                <th class="text--left">tên đơn vị</th>
                <th class="text--left">số tài khoản</th>
                <th class="text--left">tên ngân hàng</th>
                <th
                    class="text--left"
                    @mouseleave="hiddenTooltip"
                    @mouseover="showTooltip($event, 'Tên chi nhánh ngân hàng')"
                >
                    chi nhánh tk ngân hàng
                </th>
                <th class="text--center">chức năng</th>
            </tr>
        </thead>
        <tbody>
            <tr
                class="table__row--bold"
                v-for="(employee, index) in employees"
                :key="index"
                @dblclick="
                    showFormEditEmployee(employee.employeeID, formEnum.Edit)
                "
            >
                <td class="text--center" @dblclick.stop="true">
                    <input
                        :checked="isCheckedAll"
                        type="checkbox"
                        class="input--selected"
                        @input="selectEmployee(employee)"
                    />
                </td>
                <td class="text--left">{{ employee.employeeCode || "" }}</td>
                <td class="text--left">{{ employee.employeeName || "" }}</td>
                <td class="text--left">{{ gender[employee.gender] || "" }}</td>
                <td class="text--right">
                    {{ formatTime(employee.dateOfBirth) || "" }}
                </td>
                <td class="text--left">{{ employee.identityNumber || "" }}</td>
                <td class="text--left">{{ employee.positionName || "" }}</td>
                <td class="text--left">
                    {{ employee.departmentID ? employee.departmentName : "" }}
                </td>
                <td class="text--left">
                    {{ employee.bankNumber ? employee.bankNumber : "" }}
                </td>
                <td class="text--left">
                    {{ employee.bankName ? employee.bankName : "" }}
                </td>
                <td class="text--left">
                    {{ employee.bankBranch ? employee.bankBranch : "" }}
                </td>

                <td class="text--center" @dblclick.stop="true">
                    <div class="table__item--option">
                        <div class="custom__text">Sửa</div>
                        <div
                            class="combobox"
                            @click="
                                clickBtnEdit(
                                    $event,
                                    index,
                                    employee.employeeID,
                                    employee.employeeCode
                                )
                            "
                        >
                            <div
                                class="cbx__button"
                                :class="{
                                    clicked:
                                        index === indexSelected &&
                                        isShowEditCbx,
                                }"
                            >
                                <div class="icon__arrowDown--bold"></div>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</template>

<script>
import EnumMisa from "@/ultis/enum.js";
import MTooltip from "@/components/base/MTooltip.vue";
export default {
    components: {
        MTooltip,
    },
    props: {
        employees: {
            type: Array,
            default: () => [],
        },
        filterEmployee: {
            type: String,
            default: "",
        },
    },
    data() {
        return {
            indexSelected: "",
            isShowEditCbx: false,
            positionDropListData: 0,
            formEnum: {
                ...EnumMisa.FormMode,
            },
            gender: {
                0: "Nam",
                1: "Nữ",
                2: "Khác",
            },
            isCheckedAll: false,
            listSelectedEmployee: [],

            tooltipData: {},
            isShowTooltip: false,
        };
    },
    methods: {
        /**
         * Sự kiện hiện tooltip
         * Created: TVKhang(08/10/2022)
         */
        showTooltip(e, msg) {
            this.isShowTooltip = true;

            this.tooltipData = {
                x: e.pageX - 16 - 178,
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

        // Xử lý sự kiện mở tùy chọn
        // Author: TVKhang 12/09/22
        clickBtnEdit(e, index, employeeId, employeeCode) {
            this.indexSelected = index;
            this.isShowEditCbx = !this.isShowEditCbx;

            // Lay vi tri cua drop list
            this.positionDropListData = {
                x: e.target.getBoundingClientRect().x,
                y: e.target.getBoundingClientRect().y,
                id: employeeId,
                code: employeeCode,
            };
            this.$emit("positionDropList", this.positionDropListData);
        },

        // Xử lý ngày tháng để hiển thị
        // Author: TVKhang 12/09/22
        formatTime(data) {
            try {
                if (data) {
                    data = new Date(data);
                    let day = data.getDate();
                    let month = data.getMonth() + 1;
                    let year = data.getFullYear();
                    return `${day}/${month}/${year}`;
                }
            } catch (err) {
                console.log("Error:", err);
            }
            return "";
        },

        // Xử lý sự kiện hiện form sửa thông tin nhân viên
        // Author: TVKhang 12/09/22
        showFormEditEmployee(id, mode) {
            this.$emit("showFormEditEmployee", {
                employeeId: id,
                mode,
            });
        },

        // Xử lý sự kiện click vào input checkbox nhân viên
        // Author: TVKhang 12/09/22
        selectEmployee(data) {
            if (!this.listSelectedEmployee.includes(data.employeeID)) {
                this.listSelectedEmployee.push(data.employeeID);
            } else {
                this.listSelectedEmployee.splice(
                    this.listSelectedEmployee.indexOf(this.employeeID) + 1,
                    1
                );
            }
        },

        // Xử lý sự kiện click vào input checkbox tổng
        // Author: TVKhang 12/09/22
        selectAllEmployee() {
            this.isCheckedAll = !this.isCheckedAll;

            if (
                this.listSelectedEmployee.length != this.employees.length ||
                (this.listSelectedEmployee.length == this.employees.length &&
                    this.isCheckedAll)
            ) {
                this.employees.forEach((e) => {
                    if (!this.listSelectedEmployee.includes(e.employeeID))
                        this.listSelectedEmployee.push(e.employeeID);
                });
            } else {
                this.listSelectedEmployee = [];
            }
            console.log(this.listSelectedEmployee);
        },
    },
};
</script>
