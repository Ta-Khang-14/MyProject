<template>
    <table>
        <thead>
            <tr>
                <th>
                    <input
                        class="input--selected"
                        type="checkbox"
                        name=""
                        id=""
                    />
                </th>
                <th class="text--left">mã nhân viên</th>
                <th class="text--left">tên nhân viên</th>
                <th class="text--left">giới tính</th>
                <th class="text--center">ngày sinh</th>
                <th class="text--left">số cmnnd</th>
                <th class="text--left">chức danh</th>
                <th class="text--left">tên đơn vị</th>
                <th class="text--left">số tài khoản</th>
                <th class="text--left">tên ngân hàng</th>
                <th class="text--left">chi nhánh tk ngân hàng</th>
                <th class="text--center">chức năng</th>
            </tr>
        </thead>
        <tbody>
            <tr
                class="table__row--bold"
                v-for="(employee, index) in findEmployees"
                :key="index"
                @dblclick="showFormEditEmployee(employee.EmployeeId, 1)"
            >
                <td class="text--center" @dblclick.stop="true">
                    <input type="checkbox" class="input--selected" />
                </td>
                <td class="text--left">{{ employee.EmployeeCode || "" }}</td>
                <td class="text--left">{{ employee.FullName || "" }}</td>
                <td class="text--left">{{ employee.Gender || "" }}</td>
                <td class="text--right">
                    {{ formatTime(employee.DateOfBirth) || "" }}
                </td>
                <td class="text--left">{{ employee.IdentityNumber || "" }}</td>
                <td class="text--left">{{ employee.PositionName || "" }}</td>
                <td class="text--left">{{ employee.DepartmentName || "" }}</td>
                <td class="text--left">{{}}</td>
                <td class="text--left">{{}}</td>
                <td class="text--left">{{}}</td>

                <td class="text--center" @dblclick.stop="true">
                    <div class="table__item--option">
                        <div class="custom__text">Sửa</div>
                        <div
                            class="combobox"
                            @click="
                                clickBtnEdit(
                                    $event,
                                    index,
                                    employee.EmployeeId,
                                    employee.EmployeeCode
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
export default {
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
        };
    },
    methods: {
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
    },
    computed: {
        // Xử lý dữ liệu trước khi xuất ra màn hình
        // Author: TVKhang 12/09/22
        findEmployees() {
            let key = this.filterEmployee.toUpperCase();

            return this.employees.filter((e) =>
                e.EmployeeCode.toUpperCase().includes(key)
            );
        },
    },
};
</script>
