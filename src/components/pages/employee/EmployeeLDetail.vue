<template>
    <div
        class="dialog isActive"
        id="form__employee--add"
        @keydown="handleEnventKeyDown($event)"
    >
        <div class="dialog__form">
            <m-tooltip :isShow="isShowTooltip" :data="tooltipData" />

            <div class="dialog__header flex">
                <div class="dialog__header--title flex">
                    <div class="title">Thông tin nhân viên</div>
                    <div class="option flex">
                        <div class="input__field flex">
                            <input
                                type="checkbox"
                                id="customer"
                                v-model="typeOfCustomerData.customer"
                            />
                            <label for="customer">Là khách hàng</label>
                        </div>
                        <div class="input__field flex">
                            <input
                                type="checkbox"
                                id="vendor"
                                v-model="typeOfCustomerData.vendor"
                            />
                            <label for="vendor">Là nhà cung cấp</label>
                        </div>
                    </div>
                </div>
                <div class="dialog__header--icon flex">
                    <div
                        class="icon icon--infor"
                        @mouseleave="hiddenTooltip"
                        @mouseover="showTooltip($event, 'Trợ giúp(F1)')"
                    ></div>
                    <div
                        class="icon icon--close"
                        @click="hiddenFormDetail"
                        @mouseleave="hiddenTooltip"
                        @mouseover="showTooltip($event, 'Thoát (ESC)')"
                    ></div>
                </div>
            </div>
            <div class="dialog__body">
                <div class="row">
                    <div class="data__field flex cl--6">
                        <div class="input__field cl--5">
                            <m-input
                                tabIndex="1"
                                labelRequired="Mã"
                                ref="EmployeeCode"
                                placeholder="Nhập mã nhân viên"
                                :defaultValue="
                                    employee ? employee.employeeCode : ''
                                "
                            />
                        </div>
                        <div class="input__field cl--7">
                            <m-input
                                tabIndex="2"
                                ref="EmployeeName"
                                labelRequired="Tên nhân viên"
                                placeholder="Nhập tên nhân viên"
                                :defaultValue="
                                    employee ? employee.employeeName : ''
                                "
                            />
                        </div>
                    </div>
                    <div class="data__field flex cl--6">
                        <div class="input__field cl--6">
                            <m-input
                                tabIndex="5"
                                label="Ngày sinh"
                                ref="DateOfBirth"
                                type="date"
                                :defaultValue="
                                    formatTime(
                                        employee ? employee.dateOfBirth : ''
                                    ) || ''
                                "
                            />
                        </div>
                        <div class="input__field cl--6 input__field--gender">
                            <div class="label">Giới tính</div>
                            <div class="radio__wrap flex">
                                <div class="radio__field flex">
                                    <input
                                        @click="selectGender(0)"
                                        tabIndex="6"
                                        type="radio"
                                        class="input--radio"
                                        name="gender"
                                        id="nam"
                                        :checked="
                                            employee
                                                ? employee.gender == gender.Male
                                                : false
                                        "
                                    />
                                    <label class="label__gender" for="nam"
                                        >Nam</label
                                    >
                                </div>
                                <div class="radio__field flex">
                                    <input
                                        @click="selectGender(1)"
                                        tabIndex="7"
                                        type="radio"
                                        class="input--radio"
                                        name="gender"
                                        id="nu"
                                        :checked="
                                            employee
                                                ? employee.gender ==
                                                  gender.Female
                                                : false
                                        "
                                    />
                                    <label class="label__gender" for="nu"
                                        >Nữ</label
                                    >
                                </div>
                                <div class="radio__field flex">
                                    <input
                                        @click="selectGender(2)"
                                        tabIndex="8"
                                        type="radio"
                                        class="input--radio"
                                        name="gender"
                                        id="khac"
                                        :checked="
                                            employee
                                                ? employee.gender ==
                                                  gender.Other
                                                : false
                                        "
                                    />
                                    <label class="label__gender" for="khac"
                                        >Khác</label
                                    >
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="data__field flex cl--6">
                        <div class="input__field cl--12">
                            <div class="label">Đơn vị<b>*</b></div>
                            <m-combobox
                                tabIndex="3"
                                :listData="deparments"
                                :propName="'departmentName'"
                                :propData="'departmentID'"
                                :selectedId="
                                    employee ? employee.departmentID : ''
                                "
                                ref="cbx_department"
                            />
                            <div
                                class="input__msg"
                                v-if="!this.validate[2].status"
                            >
                                {{ this.validate[2].msg }}
                            </div>
                        </div>
                    </div>
                    <div class="data__field flex cl--6">
                        <div class="input__field cl--6">
                            <div
                                class="label"
                                @mouseleave="hiddenTooltip"
                                @mouseover="
                                    showTooltip(
                                        $event,
                                        'Số chứng minh nhân dân'
                                    )
                                "
                            >
                                Số CMND
                            </div>
                            <m-input
                                tabIndex="9"
                                type="text"
                                ref="IdentityNumber"
                                placeholder="Nhập số CMND"
                                :defaultValue="
                                    employee ? employee.identityNumber : ''
                                "
                            />
                        </div>
                        <div class="input__field cl--6">
                            <m-input
                                label="Ngày cấp"
                                ref="IdentityDate"
                                tabIndex="10"
                                type="date"
                                :defaultValue="
                                    formatTime(
                                        employee ? employee.identityDate : ''
                                    ) || ''
                                "
                            />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="data__field flex cl--6">
                        <div class="input__field cl--12">
                            <m-input
                                tabIndex="4"
                                label="Chức danh"
                                type="text"
                                placeholder="Nhập tên chức danh"
                                ref="PositionName"
                                :defaultValue="
                                    employee ? employee.positionName : ''
                                "
                            />
                        </div>
                    </div>
                    <div class="data__field flex cl--6">
                        <div class="input__field cl--12">
                            <m-input
                                tabIndex="10"
                                label="Nơi cấp"
                                type="text"
                                ref="IdentityPlace"
                                placeholder="Nơi cấp CMND"
                                :defaultValue="
                                    employee ? employee.identityPlace : ''
                                "
                            />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="data__field flex cl--12">
                        <div class="input__field cl--12">
                            <m-input
                                tabIndex="11"
                                label="Địa chỉ"
                                type="text"
                                placeholder="Nhập địa chỉ"
                                ref="EmployeeAddress"
                                :defaultValue="
                                    employee ? employee.employeeAddress : ''
                                "
                            />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="data__field flex cl--12">
                        <div class="input__field">
                            <div
                                class="label"
                                @mouseleave="hiddenTooltip"
                                @mouseover="
                                    showTooltip($event, 'Số điện thoại di động')
                                "
                            >
                                Số ĐT di động
                            </div>
                            <m-input
                                tabIndex="12"
                                type="text"
                                ref="PhoneNumber"
                                placeholder="0123456789"
                                :defaultValue="
                                    employee ? employee.phoneNumber : ''
                                "
                            />
                        </div>
                        <div class="input__field">
                            <div
                                class="label"
                                @mouseleave="hiddenTooltip"
                                @mouseover="
                                    showTooltip($event, 'Số điện thoại cố định')
                                "
                            >
                                Số ĐT cố định
                            </div>
                            <m-input
                                tabIndex="13"
                                type="text"
                                placeholder="0123456789"
                                ref="LandlineNumber"
                                :defaultValue="
                                    employee ? employee.landlineNumber : ''
                                "
                            />
                        </div>
                        <div class="input__field">
                            <m-input
                                tabIndex="14"
                                label="Email"
                                type="text"
                                ref="Email"
                                placeholder="userexample@gmail.com"
                                :defaultValue="employee ? employee.email : ''"
                            />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="data__field flex cl--12">
                        <div class="input__field">
                            <m-input
                                tabIndex="15"
                                label="Tài khoản ngân hàng"
                                type="text"
                                ref="BankNumber"
                                placeholder="Nhập tài khoản ngân hàng"
                                :defaultValue="
                                    employee ? employee.bankNumber : ''
                                "
                            />
                        </div>
                        <div class="input__field">
                            <m-input
                                tabIndex="16"
                                label="Tên ngân hàng"
                                type="text"
                                ref="BankName"
                                placeholder="Nhập tên ngân hàng"
                                :defaultValue="
                                    employee ? employee.bankName : ''
                                "
                            />
                        </div>
                        <div class="input__field">
                            <m-input
                                tabIndex="17"
                                label="Chi nhánh"
                                type="text"
                                ref="BankBranch"
                                placeholder="Nhập tên chi nhanh"
                                :defaultValue="
                                    employee ? employee.bankBranch : ''
                                "
                            />
                        </div>
                    </div>
                </div>
            </div>
            <div class="dialog__footer flex">
                <div
                    class="button button--gray btn--close"
                    tabIndex="17"
                    @click="hiddenForm"
                >
                    Hủy
                </div>
                <div class="button__field flex">
                    <div
                        class="button button--gray"
                        tabIndex="18"
                        @click="handleEventClick()"
                        @mouseleave="hiddenTooltip"
                        @mouseover="showTooltip($event, 'Cất (Ctrl + S)')"
                    >
                        Cất
                    </div>
                    <div
                        class="button button--green"
                        tabIndex="19"
                        @click="saveAndNew"
                        @mouseleave="hiddenTooltip"
                        @mouseover="
                            showTooltip(
                                $event,
                                'Cất và thêm (Ctrl + Shift + S)'
                            )
                        "
                    >
                        Cất và Thêm
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import MCombobox from "@/components/base/MCombobox.vue";
import MInput from "@/components/base/MInput.vue";
import MTooltip from "@/components/base/MTooltip.vue";

import EnumMisa from "@/ultis/enum.js";
import fetchAPI from "@/ultis/fetchAPI.js";
import validateHandle from "@/ultis/validateHandle.js";

export default {
    components: { MCombobox, MInput, MTooltip },
    props: ["employeeId"],
    data() {
        return {
            type: EnumMisa.FormMode.New,
            isActive: true,
            employee: {},
            deparments: [],
            typeOfCustomer: {
                ...EnumMisa.TypeOfCustomer,
            },
            gender: {
                ...EnumMisa.Gender,
            },
            genderValue: "",
            validate: [
                {
                    name: "EmployeeCode",
                    required: [
                        EnumMisa.Validate.Required,
                        EnumMisa.Validate.EmployeeCode,
                    ],
                    status: true,
                    msg: "",
                    value: "",
                },
                {
                    name: "EmployeeName",
                    required: [EnumMisa.Validate.Required],
                    status: true,
                    msg: "",
                    value: "",
                },
                {
                    name: "cbx_department",
                    required: [EnumMisa.Validate.Required],
                    status: true,
                    msg: "",
                    value: "",
                },
                {
                    name: "IdentityNumber",
                    required: [EnumMisa.Validate.Number],
                    status: true,
                    msg: "",
                    value: "",
                },
                {
                    name: "DateOfBirth",
                    required: [EnumMisa.Validate.Date],
                    status: true,
                    msg: "",
                    value: "",
                },
                {
                    name: "IdentityDate",
                    required: [EnumMisa.Validate.Date],
                    status: true,
                    msg: "",
                    value: "",
                },
            ],

            tooltipData: {},
            isShowTooltip: false,

            typeOfCustomerData: {
                customer: false,
                vendor: false,
            },
        };
    },
    methods: {
        /**
         * Xủ lý sự kiện ẩn form nhập
         * Author: TVKHANG(11/09/22
         */
        hiddenFormDetail() {
            let newEmp = this.getInputData().employee;
            if (!this.compareEmployee(this.employee, newEmp)) {
                this.handleEventClick();
                this.$emit("hiddenFormDetail", { isShow: true });
            } else {
                this.$emit("hiddenFormDetail", { isShow: false });
            }
        },
        hiddenForm() {
            this.$emit("hiddenForm");
        },
        /**
         * Xử lý ngày tháng để hiển thị
         * Author: TVKhang 12/09/22
         */
        formatTime(data) {
            if (data) {
                data = new Date(data);
                let day =
                    data.getDate() < 10 ? "0" + data.getDate() : data.getDate();

                let month =
                    data.getMonth() + 1 < 10
                        ? "0" + (data.getMonth() + 1)
                        : data.getMonth() + 1;

                let year = data.getFullYear();

                return `${year}-${month}-${day}`;
            }
            return "";
        },

        /**
         * Lấy dữ liệu từ input
         * Author: TVKhang 20/09/22
         */
        getInputData() {
            let employee = {
                employeeCode: this.$refs.EmployeeCode.value,
                employeeName: this.$refs.EmployeeName.value,
                dateOfBirth: this.$refs.DateOfBirth.value,
                identityNumber: this.$refs.IdentityNumber.value,
                identityDate: this.$refs.IdentityDate.value,
                identityPlace: this.$refs.IdentityPlace.value,
                positionName: this.$refs.PositionName.value,
                employeeAddress: this.$refs.EmployeeAddress.value,
                phoneNumber: this.$refs.PhoneNumber.value,
                landlineNumber: this.$refs.LandlineNumber.value,
                bankNumber: this.$refs.BankNumber.value,
                bankName: this.$refs.BankName.value,
                bankBranch: this.$refs.BankBranch.value,
                email: this.$refs.Email.value,
                departmentID:
                    this.$refs.cbx_department.currentItem.departmentID,
                departmentName:
                    this.$refs.cbx_department.currentItem.departmentName,
                gender: this.genderValue,
            };

            if (
                this.typeOfCustomerData.customer &&
                this.typeOfCustomerData.vendor
            ) {
                employee.typeOfCustomer = this.typeOfCustomer.CustomerAndVendor;
            } else {
                if (this.typeOfCustomerData.customer) {
                    employee.typeOfCustomer = this.typeOfCustomer.Customer;
                }
                if (this.typeOfCustomerData.vendor) {
                    employee.typeOfCustomer = this.typeOfCustomer.Vendor;
                }
            }

            // Xóa các trường date time trống
            this.formatUpdateInfor(employee);

            // Lấy dữ liệu vào validate
            this.validate.forEach((e) => {
                if (!e.name.includes("cbx")) {
                    e.value = this.$refs[e["name"]].value;
                }
            });

            this.validate[2].value = employee.departmentID
                ? employee.departmentID
                : "";
            validateHandle(this.validate);

            // Kiểm tra xem đã validate toàn bộ hay chưa
            let checkValidate = this.validate.find((e) => !e.status);

            if (checkValidate) {
                this.addValidateToInput();
                return {
                    status: false,
                    employee,
                    validate: this.validate,
                };
            }
            return {
                status: true,
                employee,
            };
        },

        /**
         * Thêm cảnh báo vào input
         * Author: TVKhang 24/09/22
         */
        addValidateToInput() {
            this.validate.forEach((e) => {
                this.$refs[e["name"]].msg = e.msg;
                this.$refs[e["name"]].hasError = e.status ? false : true;
            });
        },

        /**
         * Xử lý khi click cất
         * Author: TVKhang 20/09/22
         */
        handleEventClick() {
            let result = this.getInputData();
            if (result.status) {
                let employee = result.employee;
                // Nếu tồn tại dữ liệu của nhân viên - Sửa thông tin nhân viên
                if (this.type == EnumMisa.FormMode.Edit) {
                    this.$emit("updateEmp", {
                        id: this.employee.employeeID,
                        data: employee,
                        code: this.employee.employeeCode,
                    });
                } else {
                    // Nếu không tồn tại dữ liệu của nhân viên - Thêm mới nhân viên
                    this.$emit("createEmp", employee);
                }
            } else {
                let fieldError = result.validate.find((e) => !e.status);
                this.$emit(
                    "validateEmployeeFail",
                    (EnumMisa.Employee[fieldError.name]
                        ? EnumMisa.Employee[fieldError.name] + " "
                        : "") + fieldError.msg
                );
            }
        },

        /**
         * Xử lý khi click cất
         * Author: TVKhang 20/09/22
         */
        saveAndNew() {
            this.handleEventClick();
            this.$emit("saveAndNew");
        },
        /**
         * Lấy mã nhân viên lớn nhất
         * Author: TVKhang()
         */
        async getMaxEmployeeCode() {
            try {
                let maxCode = await fetchAPI(
                    `${process.env.VUE_APP_URL}/Employees/max-code`
                );
                maxCode = +maxCode.split("NV")[1] + 1;
                maxCode = "NV" + maxCode;
                return maxCode;
            } catch (err) {
                return "";
            }
        },

        // Loại bỏ các trường date time trống
        formatUpdateInfor(emp) {
            Object.entries(emp).forEach((e) => {
                if (
                    (e[0] == "identityDate" && !e[1]) ||
                    (e[0] == "dateOfBirth" && !e[1])
                ) {
                    delete emp[e[0]];
                }
            });
        },

        // Lấy dữ liệu của gender
        selectGender(data) {
            this.genderValue = data;
        },

        /**
         * Xử lý event key up
         * Author: TVKhang(28/09/22)
         */
        handleEnventKeyDown(e) {
            switch (e.keyCode) {
                case EnumMisa.KeyCode.ESC:
                    this.hiddenFormDetail();
                    break;
            }

            if (e.keyCode == EnumMisa.KeyCode.S && e.ctrlKey) {
                e.preventDefault();
                this.handleEventClick();
            }

            if (e.keyCode == EnumMisa.KeyCode.S && e.ctrlKey && e.shiftKey) {
                e.preventDefault();
                this.handleEventClick();
            }
        },
        /**
         * Sự kiện hiện tooltip
         * Created: TVKhang(08/10/2022)
         */
        showTooltip(e, msg) {
            this.isShowTooltip = true;

            this.tooltipData = {
                x: e.pageX,
                y: e.pageY + 15,
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
         * So sánh 2 nhân viên
         * Created: TVKhang(09/10/2022)
         */
        compareEmployee(emp1, emp2) {
            let emp = { ...emp1 };
            // loại bỏ các trường k cần dùng
            let removeField = [
                "createdDate",
                "createdBy",
                "modifiedDate",
                "modifiedBy",
                "isActive",
                "employeeCodeNumber",
                "employeeID",
            ];
            Object.keys(emp).forEach((e) => {
                if (removeField.includes(e)) delete emp[e];
            });
            emp.dateOfBirth = this.formatTime(emp.dateOfBirth);
            emp.identityDate = this.formatTime(emp.identityDate);
            this.formatUpdateInfor(emp);

            // so sánh
            if (Object.keys(emp).length != Object.keys(emp2).length) {
                return false;
            }

            for (const key of Object.keys(emp)) {
                let value = emp[key] ? emp[key] : "";
                if (value != emp2[key]) {
                    return false;
                }
            }

            return true;
        },
    },
    async created() {
        let employee = {};
        if (this.employeeId) {
            employee = fetchAPI(
                `${process.env.VUE_APP_URL}/Employees/${this.employeeId}`
            );
            this.type = EnumMisa.FormMode.Edit;
        }
        let departments = fetchAPI(`${process.env.VUE_APP_URL}/Departments`);

        this.employee = await employee;

        // Gán giá trị loại khách hàng
        if (Number.isInteger(this.employee.typeOfCustomer)) {
            switch (this.employee.typeOfCustomer) {
                case this.typeOfCustomer.Customer:
                    this.typeOfCustomerData.customer = true;
                    break;
                case this.typeOfCustomer.Vendor:
                    this.typeOfCustomerData.vendor = true;
                    break;
                case this.typeOfCustomer.CustomerAndVendor:
                    this.typeOfCustomerData.customer = true;
                    this.typeOfCustomerData.vendor = true;
                    break;
            }
        }

        // Gán giá trị gender
        if (Number.isInteger(this.employee.gender)) {
            this.genderValue = this.employee.gender;
        }

        this.deparments = (await departments).data;
    },
    mounted() {
        // Tự động focus vào ô input đầu tiên
        this.$refs.EmployeeCode.focus();
    },
};
</script>
