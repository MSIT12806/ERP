<!-- 首頁 線上訂票 個人訂票 -->
<!-- 網路訂票說明 -->
<!-- 開放訂票期程 -->
<!-- 完整訂票方式 -->
<template>
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-lg-6">
                <a>個人訂票</a>
                <a>團體訂票</a>
                <a>列車查詢</a>
                <a>剩餘座位查詢</a>
                <a>個人訂票紀錄查詢</a>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: "TrainMain",
        data() {
            return {
                username: '',
                email: '',
                password: '',
                confirmPassword: '',
                Interaction: {

                },
                postdata: {},
                comfirm: {
                    passwordValid: {
                        isValid: true,
                        message: "",
                    },
                },
            }
        },
        methods: {
        }
    }
</script>

<style>
    /* 在這裡可以設定樣式 */
    .has-error .form-control {
        border-color: #a94442;
    }

    .has-error .help-block {
        color: #a94442;
    }
</style>