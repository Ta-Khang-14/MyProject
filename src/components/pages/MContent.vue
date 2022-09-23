<template>
    <div class="content">
        <m-header />
        <employee-list />
    </div>
</template>
<script>
import MHeader from "../layouts/MHeader.vue";
import EmployeeList from "./employee/EmployeeList.vue";
export default {
    components: {
        MHeader,
        EmployeeList,
    },
};
</script>
