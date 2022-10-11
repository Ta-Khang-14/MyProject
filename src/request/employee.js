import fetchAPI from "@/ultis/fetchAPI.js";
// import axios from "axios";

const EmployeeRequest = {
    // Xóa nhân viên
    // Author: TVKhang 08/10/22
    async deleteEmployeeById(data) {
        console.log(data);
        let result = await fetchAPI(
            `${process.env.VUE_APP_URL}/Employees/${data.employeeId}`,
            "delete"
        );
        return { ...result, completeMsg: "Xóa nhân viên thành công" };
    },

    // Sửa nhân viên
    // Author: TVKhang 08/10/22
    async updateEmployeeById(data) {
        let result = await fetchAPI(
            `${process.env.VUE_APP_URL}/Employees/${data.employeeId}`,
            "put",
            "",
            data.data
        );
        return { ...result, completeMsg: "Sửa nhân viên thành công" };
    },

    // Thêm mới nhân viên
    // Author: TVKhang 08/10/22
    async createEmployee(data) {
        console.log(JSON.stringify(data));
        let result = await fetchAPI(
            `${process.env.VUE_APP_URL}/Employees`,
            "post",
            "",
            data
        );

        return { ...result, completeMsg: "Thêm mới nhân viên thành công" };
    },

    // Thực hiện xóa nhiều vân viên
    // Author: TVKhang 08/10/22
    async deleteBatchEmployee(data) {
        let result = await fetchAPI(
            `${process.env.VUE_APP_URL}/Employees/delete-batch`,
            "post",
            "",
            data.listSelectedEmployee
        );
        return { ...result, completeMsg: "Xóa nhân viên thành công" };
    },
};

export default EmployeeRequest;
