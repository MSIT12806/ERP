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
                <h2 class="text-center mb-4">個人訂票</h2>
                <form @submit.prevent="register">
                    <div class="form-group">
                        <label class="form-label" for="isId">身分證字號</label>
                        <input id="isId" type="radio" name="idod" value="id" />
                        <label class="form-label" for="isOd">居留證號</label>
                        <input id="isOd" type="radio" name="idod" value="od" />
                        <input type="text" class="form-control" id="username" v-model="username" required>
                    </div>
                    <div class="input-group">
                        <input type="text" id="StartStation" class="form-control" placeholder="出發站" aria-label="出發站" />
                        <a class="input-group-text btn">
                            <i class="bi bi-arrow-left"></i><br />
                            <i class="bi bi-arrow-right"></i>
                        </a>
                        <input type="text" id="ArriveStation" class="form-control" placeholder="抵達站" aria-label="抵達站" />
                        <label>日期</label>
                        <input type="date" name="BoardingDate" />
                    </div>
                    <div class="input-group">
                        <label>訂票方式</label>
                        <div class="btn-group">
                            <input class="btn-check" type="radio" name="BuyType" id="ByCar" v-model="Interaction.buyType" value="ByCar" />
                            <label class="btn btn-outline-danger" for="ByCar">依車次</label>
                            <input class="btn-check" type="radio" name="BuyType" id="ByTime" v-model="Interaction.buyType" value="ByTime"  />
                            <label class="btn btn-outline-danger" for="ByTime">依時段</label>
                        </div>
                        <div class="input-group">
                            <label>一般座票數</label>
                            <a class="input-group-text btn">+</a>
                            <input type="text" />
                            <a class="input-group-text btn">-</a>
                        </div>
                        <!--<div class="input-group">
                <label>輪椅座票數</label>
                <a class="input-group-text btn">+</a>
                <input type="text" />
                <a class="input-group-text btn">-</a>
            </div>-->
                    </div>
                    <div v-if="Interaction.buyType=='ByCar'" class="card">
                        <input type="date" placeholder="日期" />
                        <input type="text" placeholder="車次(必填)" />
                    </div>
                    <div class="card"  v-if="Interaction.buyType=='ByTime'">
                        <input type="date" placeholder="日期" />
                        <input type="time" name="TimeFrom" placeholder="時段" />
                        <input type="time" name="TimeTo" placeholder="時段" />

                        <div class="btn-group">
                            <label>車種</label>
                            <input class="btn-check" type="checkbox" name="BuyType" id="3000" />
                            <label class="btn btn-outline-danger" for="3000">3000</label>
                            <input class="btn-check" type="checkbox" name="BuyType" id="Taruku" />
                            <label class="btn btn-outline-danger" for="Taruku">太魯閣</label>
                            <input class="btn-check" type="checkbox" name="BuyType" id="Puyuma" />
                            <label class="btn btn-outline-danger" for="Puyuma">普悠瑪</label>
                            <input class="btn-check" type="checkbox" name="BuyType" id="TzeChiang" />
                            <label class="btn btn-outline-danger" for="TzeChiang">自強</label>
                            <input class="btn-check" type="checkbox" name="BuyType" id="Juguang" />
                            <label class="btn btn-outline-danger" for="Juguang">莒光</label>
                            <input class="btn-check" type="checkbox" name="BuyType" id="FuHsing" />
                            <label class="btn btn-outline-danger" for="FuHsing">復興</label>
                        </div>
                    </div>

                    <button type="submit" class="btn btn-primary btn-block mt-4">註冊</button>
                </form>
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