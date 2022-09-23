<template>
    <div class="dialog isActive" id="form__employee--add">
        <div class="dialog__form">
            <div class="dialog__header flex">
                <div class="dialog__header--title flex">
                    <div class="title">Thông tin nhân viên</div>
                    <div class="option flex">
                        <div class="input__field flex">
                            <input type="checkbox" id="customer" />
                            <label for="customer">Là khách hàng</label>
                        </div>
                        <div class="input__field flex">
                            <input type="checkbox" id="vendor" />
                            <label for="vendor">Là nhà cung cấp</label>
                        </div>
                    </div>
                </div>
                <div class="dialog__header--icon flex">
                    <div class="icon icon--infor"></div>
                    <div class="icon icon--close" @click="hiddenForm"></div>
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
                                :isFocus="true"
                                placeholder="Nhập mã nhân viên"
                                :defaultValue="employee.EmployeeCode || ''"
                            />
                        </div>
                        <div class="input__field cl--7">
                            <m-input
                                tabIndex="2"
                                ref="FullName"
                                labelRequired="Tên nhân viên"
                                placeholder="Nhập tên nhân viên"
                                :defaultValue="employee.FullName || ''"
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
                                    formatTime(employee.DateOfBirth) || ''
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
                                            employee.Gender == gender.Male
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
                                            employee.Gender == gender.Female
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
                                            employee.Gender == gender.Other
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
                                :propName="'DepartmentName'"
                                :propData="'DepartmentId'"
                                :selectedId="employee.DepartmentId"
                                ref="cbx_department"
                            />
                        </div>
                    </div>
                    <div class="data__field flex cl--6">
                        <div class="input__field cl--6">
                            <m-input
                                tabIndex="9"
                                label="Số CMND"
                                type="text"
                                ref="IdentityNumber"
                                placeholder="Nhập số CMND"
                                :defaultValue="employee.IdentityNumber || ''"
                            />
                        </div>
                        <div class="input__field cl--6">
                            <div class="label">Ngày cấp</div>
                            <input
                                ref="IdentityDate"
                                tabIndex="10"
                                type="date"
                                :value="formatTime(employee.IdentityDate) || ''"
                            />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="data__field flex cl--6">
                        <div class="input__field cl--12">
                            <m-input
                                tabIndex="4"
                                label="Địa chỉ"
                                type="text"
                                placeholder="Nhập địa chỉ"
                                ref="Address"
                                :defaultValue="employee.Address || ''"
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
                                :defaultValue="employee.IdentityPlace || ''"
                            />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="data__field flex cl--12">
                        <div class="input__field">
                            <m-input
                                tabIndex="11"
                                label="ĐT di động"
                                type="text"
                                ref="PhoneNumber"
                                placeholder="0123456789"
                                :defaultValue="employee.PhoneNumber || ''"
                            />
                        </div>
                        <div class="input__field">
                            <m-input
                                tabIndex="12"
                                label="ĐT cố định"
                                type="text"
                                placeholder="0123456789"
                            />
                        </div>
                        <div class="input__field">
                            <m-input
                                tabIndex="13"
                                label="Email"
                                type="text"
                                ref="Email"
                                placeholder="userexample@gmail.com"
                                :defaultValue="employee.Email || ''"
                            />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="data__field flex cl--12">
                        <div class="input__field">
                            <m-input
                                tabIndex="14"
                                label="Tài khoản ngân hàng"
                                type="text"
                                ref=""
                                placeholder="Nhập tài khoản ngân hàng"
                                :defaultValue="employee.BankNumber || ''"
                            />
                        </div>
                        <div class="input__field">
                            <m-input
                                tabIndex="15"
                                label="Tên ngân hàng"
                                type="text"
                                ref=""
                                placeholder="Nhập tên ngân hàng"
                                :defaultValue="employee.BankName || ''"
                            />
                        </div>
                        <div class="input__field">
                            <m-input
                                tabIndex="16"
                                label="Chi nhánh"
                                type="text"
                                ref=""
                                placeholder="Nhập tên chi nhanh"
                                :defaultValue="employee.BankBranch || ''"
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
                        @click="handleEventClick"
                    >
                        Cất
                    </div>
                    <div class="button button--green" tabIndex="19">
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

import EnumMisa from "@/ultis/enum.js";
import fetchAPI from "@/ultis/fetchAPI.js";
import validateHandle from "@/ultis/validateHandle.js";

export default {
    components: { MCombobox, MInput },
    props: ["employeeId"],
    data() {
        return {
            isActive: true,
            employee: {},
            deparments: [],
            gender: {
                ...EnumMisa.Gender,
            },
            genderValue: 2,
            validate: [
                {
                    name: "EmployeeCode",
                    required: [EnumMisa.Validate.Required],
                    status: true,
                    msg: "",
                },
                {
                    name: "FullName",
                    required: [
                        EnumMisa.Validate.Required,
                        EnumMisa.Validate.StringUTF8,
                    ],
                    status: true,
                    msg: "",
                },
                {
                    name: "IdentityNumber",
                    required: [EnumMisa.Validate.Number],
                    status: true,
                    msg: "",
                },
                {
                    name: "IdentityPlace",
                    required: [EnumMisa.Validate.StringUTF8],
                    status: true,
                    msg: "",
                },
                {
                    name: "Address",
                    required: [EnumMisa.Validate.StringUTF8],
                    status: true,
                    msg: "",
                },
                {
                    name: "PhoneNumber",
                    required: [EnumMisa.Validate.Phone],
                    status: true,
                    msg: "",
                },
                {
                    name: "Email",
                    required: [EnumMisa.Validate.Email],
                    status: true,
                    msg: "",
                },
            ],
        };
    },
    methods: {
        // Xủ lý sự kiện ẩn form nhập
        // Author: TVKHANG(11/09/22)
        hiddenForm() {
            this.$emit("hiddenForm");
        },
        // Xử lý ngày tháng để hiển thị
        // Author: TVKhang 12/09/22
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

        // Lấy dữ liệu từ input
        // Author: TVKhang 20/09/22
        getInputData() {
            let employee = {
                EmployeeCode: this.$refs.EmployeeCode.value,
                FullName: this.$refs.FullName.value,
                DateOfBirth: this.$refs.DateOfBirth.value,
                IdentityNumber: this.$refs.IdentityNumber.value,
                IdentityDate: this.$refs.IdentityDate.value,
                IdentityPlace: this.$refs.IdentityPlace.value,
                Address: this.$refs.Address.value,
                PhoneNumber: this.$refs.PhoneNumber.value,
                Email: this.$refs.Email.value,
                DepartmentID:
                    this.$refs.cbx_department.currentItem.DepartmentID,
                DepartmentName:
                    this.$refs.cbx_department.currentItem.DepartmentName,
                Gender: this.genderValue,
            };

            // Lấy dữ liệu vào validate
            this.validate.forEach((e) => {
                e.value = this.$refs[e["name"]].value;
            });
            validateHandle(this.validate);
            // Kiểm tra xem đã validate toàn bộ hay chưa
            let checkValidate = this.validate.find((e) => !e.status);

            if (checkValidate) {
                this.addValidateToInput();
                return {
                    status: false,
                    employee,
                };
            }
            return {
                status: true,
                employee,
            };
        },
        // Thêm cảnh vào input
        addValidateToInput() {
            this.validate.forEach((e) => {
                this.$refs[e["name"]].msg = e.msg;
                this.$refs[e["name"]].hasError = e.status ? false : true;
            });
        },

        // Xử lý khi click cất
        // Author: TVKhang 20/09/22
        handleEventClick() {
            if (this.getInputData().status) {
                let employee = this.getInputData().employee;

                // Nếu tồn tại dữ liệu của nhân viên - Sửa thông tin nhân viên
                if (this.employee.EmployeeCode) {
                    this.$emit("updateEmp", {
                        id: this.employee.EmployeeId,
                        data: employee,
                        code: this.employee.EmployeeCode,
                    });
                } else {
                    // Nếu không tồn tại dữ liệu của nhân viên - Thêm mới nhân viên
                    this.$emit("createEmp", {
                        data: employee,
                    });
                }
            }
        },

        // Lấy các trường dữ liệu đã thay đổi thông tin
        formatUpdateInfor(emp) {
            Object.entries(this.employee).forEach((e) => {
                if (emp[e[0]] == e[1]) delete emp[e[0]];
            });
        },

        // Lấy dữ liệu của gender
        selectGender(data) {
            this.genderValue = data;
        },
    },
    async created() {
        let employee = {};
        if (this.employeeId) {
            employee = fetchAPI(
                `${process.env.VUE_APP_URL}/Employees/${this.employeeId}`
            );
        }
        let departments = fetchAPI(`${process.env.VUE_APP_URL}/Departments`);

        this.employee = await employee;
        this.deparments = await departments;
    },
};
</script>
