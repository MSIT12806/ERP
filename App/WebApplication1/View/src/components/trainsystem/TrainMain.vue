<!-- 首頁 線上訂票 個人訂票 -->
<!-- 網路訂票說明 -->
<!-- 開放訂票期程 -->
<!-- 完整訂票方式 -->
<template>
    <div class="row justify-content-center">
        <div class="col-lg-12">
            <button>個人訂票</button>
            <button>團體訂票</button>
            <button @click="fetchData()">列車查詢</button>
            <button>剩餘座位查詢</button>
            <button>個人訂票紀錄查詢</button>
            <button @click="fetchData()">列車後台</button>
            <button @click="fetchData()">路線後台</button>
            <button @click="fetchData()">車站後台</button>
        </div>
        <div class="col-lg-12">
            <button class="btn btn-primary" v-for="trunkLine in trunkLines" key="trunkLine" @click="fetchData(`Station?${trunkLine.no}`)">{{trunkLine.chiName}}</button>
        </div>
    </div>
</template>

<script>
    export default {
        name: "TrainMain",
        data() {
            return {
                trunkLines: [],
                post: {},
            }
        },
        methods: {
            fetchData(url,params) {
                this.post = null;
                this.loading = true;

                this.$getData(url)
                    .then(json => {
                        this.post = json;
                        this.loading = false;
                        return;
                    });
            },
            getTrunkLine() {
                this.trunkLines = null;
                this.loading = true;
                this.$getData("TrunkLine")
                    .then(json => {
                        console.log(json);
                        this.trunkLines = json;
                        this.loading = false;
                        return;
                    });
            },

        },
        created() {
            this.getTrunkLine();
        },
    }
</script>
