<template>
    <div class="combobox" @keyup="keyEvent($event)">
        <input
            type="text"
            placeholder="- None -"
            :class="{ clicked: isShowDataList }"
            @input="filterData"
            v-model="textInput"
            class="cbx__input"
        />
        <div
            class="cbx__button"
            @click="showDataList"
            :class="{ clicked: isShowDataList }"
        >
            <div class="icon__arrowDown--bold"></div>
        </div>
        <div class="combobox__data" :class="{ clicked: isShowDataList }">
            <div
                class="combobox__item"
                v-for="(data, index) in listDataFilter"
                :key="index"
                :class="{ selected: index === selectedIndex }"
                @click="selectItem(data, index)"
            >
                {{ data[propName] }}
            </div>
        </div>
    </div>
</template>

<script>
// import fetchAPI from "@/ultis/fetchAPI.js";
import EnumMisa from "@/ultis/enum.js";
export default {
    props: {
        listData: {
            type: Array,
        },
        propData: {
            type: String,
        },
        propName: {
            type: String,
        },
        selectedId: {
            type: String,
            default: null,
        },
    },
    data() {
        return {
            listDataFilter: [],
            currentItem: {},
            textInput: "",
            isShowDataList: false,
            selectedIndex: -1,
        };
    },
    methods: {
        // Xử lý sự kiện click vào button cbx
        // Author: TVKhang(12/09/22)
        showDataList() {
            this.isShowDataList = !this.isShowDataList;
        },

        // Xử lý sự kiện click vào cbx data item
        // Author: TVKhang(12/09/22)
        selectItem(value, index) {
            Object.assign(this.currentItem, value);
            this.selectedIndex = index;
            this.textInput = this.currentItem[this.propName];
            this.isShowDataList = false;

            this.$emit("changeDataCbx", this.currentItem[this.propData]);
        },

        // Xử lý sự kiện thao tác với bàn phím
        // Author: TVKhang(12/09/22)
        keyEvent(event) {
            // Ấn mũi tên xuống
            if (event.keyCode == EnumMisa.KeyCode.DownArrow) {
                this.isShowDataList = true;
                this.selectedIndex =
                    this.listData.length - 1 > this.selectedIndex
                        ? this.selectedIndex + 1
                        : 0;
                this.currentItem = { ...this.listData[this.selectedIndex] };
                this.textInput = this.currentItem[this.propName];
            }

            // Ấn mũi tên lên
            if (event.keyCode == EnumMisa.KeyCode.UpArrow) {
                this.isShowDataList = true;
                this.selectedIndex =
                    0 < this.selectedIndex
                        ? this.selectedIndex - 1
                        : this.listData.length - 1;
                this.currentItem = { ...this.listData[this.selectedIndex] };
                this.textInput = this.currentItem[this.propName];
            }

            // Ấn Enter
            if (event.keyCode == EnumMisa.KeyCode.Enter) {
                this.isShowDataList = false;
                this.$emit("changeDataCbx", this.currentItem[this.propData]);
            }
        },
        // Xử lý sự kiện nhập dữ liệu vào input
        // Author: TVKhang(12/09/22)
        filterData() {
            this.isShowDataList = true;
            this.listDataFilter = this.listData.filter((e) =>
                e[this.propName]
                    .toUpperCase()
                    .includes(this.textInput.toUpperCase())
            );
        },
    },
    created() {
        this.listDataFilter = [...this.listData];

        if (this.listDataFilter.length) {
            // Chọn giá trị của combobox
            this.listDataFilter.forEach((e, i) => {
                if (e[this.propData] == this.selectedId) {
                    Object.assign(this.currentItem, e);
                    this.textInput = this.currentItem[this.propName];
                    this.selectedIndex = i;
                }
            });
        }
    },
    watch: {
        listData() {
            this.listDataFilter = [...this.listData];

            // Chọn giá trị của combobox
            this.listDataFilter.forEach((e, i) => {
                if (e[this.propData] == this.selectedId) {
                    Object.assign(this.currentItem, e);
                    this.textInput = this.currentItem[this.propName];
                    this.selectedIndex = i;
                }
            });
        },
    },
};
</script>
