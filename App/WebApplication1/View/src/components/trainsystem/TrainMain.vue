<!-- 首頁 線上訂票 個人訂票 -->
<!-- 網路訂票說明 -->
<!-- 開放訂票期程 -->
<!-- 完整訂票方式 -->
單行程?雙行程? -> 應該就是之前的來回票那種東ㄒㄅ
依車次、依時段
出發站、抵達站、日期
一般座票數
車次(時刻表查詢)
可接受同班車換座、靠窗、靠走道、桌座型
<template>
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-lg-6">
                <h2 class="text-center mb-4">註冊新帳號</h2>
                <form @submit.prevent="register">
                    <div class="form-group">
                        <label for="username">使用者名稱</label>
                        <input type="text" class="form-control" id="username" v-model="username" required>
                    </div>
                    <div class="form-group">
                        <label for="email">電子郵件</label>
                        <input type="email" class="form-control" id="email" v-model="email" required>
                    </div>
                    <div class="form-group">
                        <label for="password">密碼</label>
                        <input type="password" class="form-control" id="password" v-model="password" required>
                    </div>
                    <div class="form-group" :class="{'has-error': !comfirm.passwordValid.isValid}">
                        <label for="confirmPassword">確認密碼</label>
                        <input type="password" class="form-control" id="confirmPassword" v-model="confirmPassword" required>
                        <span v-if="!comfirm.passwordValid.isValid" class="help-block">{{ comfirm.passwordValid.message }}</span>
                    </div>
                    <button type="submit" class="btn btn-primary btn-block mt-4">註冊</button>
                </form>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: "RonRegister",
        data() {
            return {
                username: '',
                email: '',
                password: '',
                confirmPassword: '',

                comfirm: {
                    passwordValid: {
                        isValid: true,
                        message: "",
                    },
                },
            }
        },
        methods: {
            async register() {
                this.comfirm.passwordValid = this.confirmPasswordMatch(this.password, this.confirmPassword);
                if (!this.comfirm.passwordValid.isValid) {
                    alert(this.comfirm.passwordValid.message);//這裡要換處理方法
                    return;
                }
                try {
                    //const response = await fetch('/Account/Register', {
                    //    method: 'POST',
                    //    headers: { 'Content-Type': 'application/json' },
                    //    body: JSON.stringify({
                    //        AccountID: this.username,
                    //        //email: this.email,
                    //        password: this.password,
                    //        //confirmPassword: this.confirmPassword
                    //    })
                    //});

                    var response = { ok: true };//fake response

                    if (response.ok) {
                        // 註冊成功，導向首頁
                        this.$router.push('/');
                    } else {
                        const error = await response.json();
                        alert(error.message);
                    }
                } catch (error) {
                    console.error(error);
                    alert('註冊失敗，請稍後再試');
                }
            },
            //確認密碼輸入是否正確
            confirmPasswordMatch(password, confirmPassword) {
                if (password !== confirmPassword) {
                    return { isValid: false, message: '兩次輸入的密碼不一致' };
                }
                return { isValid: true };
            }
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