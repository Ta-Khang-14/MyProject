import { createApp } from "vue";
import App from "./App.vue";
import "./assets/css/main.css";
import MDatepicker from "@vuepic/vue-datepicker";

const app = createApp(App);

app.component("MDatepicker", MDatepicker);

app.mount("#app");
