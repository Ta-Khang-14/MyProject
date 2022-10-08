<template>
    <div class="menu menu__left">
        <m-tooltip :data="tooltipData" :isShow="isShowTooltip" />
        <div class="menu__header flex">
            <div class="menu__header--btn">
                <div class="icon icon--toggle"></div>
            </div>
            <div class="menu__header--title">
                <img
                    src="../../assets/Resource/img/Logo_Module_TiengViet_White.66947422.svg"
                    alt=""
                />
            </div>
        </div>
        <div class="menu__list">
            <ul>
                <a
                    href="#"
                    @mouseleave="hiddenTooltip"
                    @mouseover="showTooltip($event, 'Tổng quan')"
                >
                    <li class="menu__item flex">
                        <div class="menu__item--icon icon icon--overview"></div>
                        <div class="menu__item--text">Tổng quan</div>
                    </li>
                </a>
                <a
                    href="#"
                    @mouseleave="hiddenTooltip"
                    @mouseover="showTooltip($event, 'Tiền mặt')"
                >
                    <li class="menu__item flex">
                        <div class="menu__item--icon icon icon--money"></div>
                        <div class="menu__item--text">Tiền mặt</div>
                    </li>
                </a>
                <a
                    href="#"
                    @mouseleave="hiddenTooltip"
                    @mouseover="showTooltip($event, 'Tiền gửi')"
                >
                    <li class="menu__item flex">
                        <div class="menu__item--icon icon icon--credit"></div>
                        <div class="menu__item--text">Tiền gửi</div>
                    </li>
                </a>
                <a
                    href="#"
                    @mouseleave="hiddenTooltip"
                    @mouseover="showTooltip($event, 'Mua hàng')"
                >
                    <li class="menu__item flex">
                        <div class="menu__item--icon icon icon--buy"></div>
                        <div class="menu__item--text">Mua hàng</div>
                    </li>
                </a>
                <a
                    href="#"
                    @mouseleave="hiddenTooltip"
                    @mouseover="showTooltip($event, 'Bán hàng')"
                >
                    <li class="menu__item flex">
                        <div class="menu__item--icon icon icon--sell"></div>
                        <div class="menu__item--text">Bán hàng</div>
                    </li>
                </a>
                <a
                    href="#"
                    @mouseleave="hiddenTooltip"
                    @mouseover="showTooltip($event, 'Quản lý hóa đơn')"
                >
                    <li class="menu__item flex">
                        <div class="menu__item--icon icon icon--order"></div>
                        <div class="menu__item--text">Quản lý hóa đơn</div>
                    </li>
                </a>
                <a
                    href="#"
                    @mouseleave="hiddenTooltip"
                    @mouseover="showTooltip($event, 'Kho')"
                >
                    <li class="menu__item flex">
                        <div
                            class="menu__item--icon icon icon--warehouse"
                        ></div>
                        <div class="menu__item--text">Kho</div>
                    </li>
                </a>
                <a
                    href="#"
                    @mouseleave="hiddenTooltip"
                    @mouseover="showTooltip($event, 'Công cụ dụng cụ')"
                >
                    <li class="menu__item flex">
                        <div class="menu__item--icon icon icon--tool"></div>
                        <div class="menu__item--text">Công cụ dụng cụ</div>
                    </li>
                </a>
                <a
                    href="#"
                    @mouseleave="hiddenTooltip"
                    @mouseover="showTooltip($event, 'Tài sản cố định')"
                >
                    <li class="menu__item flex">
                        <div class="menu__item--icon icon icon--asset"></div>
                        <div class="menu__item--text">Tài sản cố định</div>
                    </li>
                </a>
                <a
                    href="#"
                    @mouseleave="hiddenTooltip"
                    @mouseover="showTooltip($event, 'Thuế')"
                >
                    <li class="menu__item flex">
                        <div class="menu__item--icon icon icon--tax"></div>
                        <div class="menu__item--text">Thuế</div>
                    </li>
                </a>
                <a
                    href="#"
                    @mouseleave="hiddenTooltip"
                    @mouseover="showTooltip($event, 'Giá thành')"
                >
                    <li class="menu__item flex">
                        <div class="menu__item--icon icon icon--price"></div>
                        <div class="menu__item--text">Giá thành</div>
                    </li>
                </a>
                <a
                    href="#"
                    @mouseleave="hiddenTooltip"
                    @mouseover="showTooltip($event, 'Tổng hợp')"
                >
                    <li class="menu__item flex">
                        <div class="menu__item--icon icon icon--total"></div>
                        <div class="menu__item--text">Tổng hợp</div>
                    </li>
                </a>
                <a
                    href="#"
                    @mouseleave="hiddenTooltip"
                    @mouseover="showTooltip($event, 'Ngân sách')"
                >
                    <li class="menu__item flex">
                        <div class="menu__item--icon icon icon--budget"></div>
                        <div class="menu__item--text">Ngân sách</div>
                    </li>
                </a>
                <a href="#" @mouseover="showTooltip($event, 'Báo cáo')">
                    <li class="menu__item flex">
                        <div class="menu__item--icon icon icon--report"></div>
                        <div class="menu__item--text">Báo cáo</div>
                    </li>
                </a>
                <a
                    href="#"
                    @mouseover="showTooltip($event, 'Phân tích tài chính')"
                >
                    <li class="menu__item flex">
                        <div class="menu__item--icon icon icon--analysis"></div>
                        <div class="menu__item--text">Phân tích tài chính</div>
                    </li>
                </a>
            </ul>
        </div>
    </div>
</template>

<script>
import MTooltip from "@/components/base/MTooltip.vue";
export default {
    components: {
        MTooltip,
    },
    data() {
        return {
            tooltipData: {},
            isShowTooltip: false,
        };
    },
    methods: {
        showTooltip(e, msg) {
            this.isShowTooltip = true;

            this.tooltipData = {
                x: e.pageX + 15,
                y: e.pageY + 30,
                with: e.target.offsetWidth,
                height: e.target.offsetHeight,
                msg,
            };
        },

        hiddenTooltip() {
            this.isShowTooltip = false;
        },
    },
};
</script>
