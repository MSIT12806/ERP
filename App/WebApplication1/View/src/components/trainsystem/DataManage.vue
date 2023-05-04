<template>
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th v-for="header in headers" :key="header">{{ header }}</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(row, index) in rows" :key="index">
                            <td v-for="header in headers" :key="header">{{ row[header] }}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: "DataManage",
        props: {
            data: {
                type: Array,
                required: true,
            },
        },
        data() {
            return {
                headers: [],
                rows: [],
            };
        },
        mounted() {
            this.headers = Object.keys(this.data[0]);
            this.rows = this.data;
        },
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