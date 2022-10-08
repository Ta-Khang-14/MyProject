<template>
    <div class="menu menu__left">
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
            <m-tooltip />
            <ul>
                <a href="#">
                    <li class="menu__item flex">
                        <div class="menu__item--icon icon icon--overview"></div>
                        <div class="menu__item--text">Tổng quan</div>
                    </li>
                </a>
                <a href="#">
                    <li class="menu__item flex">
                        <div class="menu__item--icon icon icon--money"></div>
                        <div class="menu__item--text">Tiền mặt</div>
                    </li>
                </a>
                <a href="#">
                    <li class="menu__item flex">
                        <div class="menu__item--icon icon icon--credit"></div>
                        <div class="menu__item--text">Tiền gửi</div>
                    </li>
                </a>
                <a href="#">
                    <li class="menu__item flex">
                        <div class="menu__item--icon icon icon--buy"></div>
                        <div class="menu__item--text">Mua hàng</div>
                    </li>
                </a>
                <a href="#">
                    <li class="menu__item flex">
                        <div class="menu__item--icon icon icon--sell"></div>
                        <div class="menu__item--text">Bán hàng</div>
                    </li>
                </a>
                <a href="#">
                    <li class="menu__item flex">
                        <div class="menu__item--icon icon icon--order"></div>
                        <div class="menu__item--text">Quản lý hóa đơn</div>
                    </li>
                </a>
                <a href="#">
                    <li class="menu__item flex">
                        <div
                            class="menu__item--icon icon icon--warehouse"
                        ></div>
                        <div class="menu__item--text">Kho</div>
                    </li>
                </a>
                <a href="#">
                    <li class="menu__item flex">
                        <div class="menu__item--icon icon icon--tool"></div>
                        <div class="menu__item--text">Công cụ dụng cụ</div>
                    </li>
                </a>
                <a href="#">
                    <li class="menu__item flex">
                        <div class="menu__item--icon icon icon--asset"></div>
                        <div class="menu__item--text">Tài sản cố định</div>
                    </li>
                </a>
                <a href="#">
                    <li class="menu__item flex">
                        <div class="menu__item--icon icon icon--tax"></div>
                        <div class="menu__item--text">Thuế</div>
                    </li>
                </a>
                <a href="#">
                    <li class="menu__item flex">
                        <div class="menu__item--icon icon icon--price"></div>
                        <div class="menu__item--text">Giá thành</div>
                    </li>
                </a>
                <a href="#">
                    <li class="menu__item flex">
                        <div class="menu__item--icon icon icon--total"></div>
                        <div class="menu__item--text">Tổng hợp</div>
                    </li>
                </a>
                <a href="#">
                    <li class="menu__item flex">
                        <div class="menu__item--icon icon icon--budget"></div>
                        <div class="menu__item--text">Ngân sách</div>
                    </li>
                </a>
                <a href="#">
                    <li class="menu__item flex">
                        <div class="menu__item--icon icon icon--report"></div>
                        <div class="menu__item--text">Báo cáo</div>
                    </li>
                </a>
                <a href="#">
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
};
</script>
