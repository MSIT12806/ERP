
<template>
    <div class="row justify-content-center">
        <div class="col-lg-6">
            <h3>依路線查火車</h3>
        </div>
    </div>
</template>

<script>
    export default {
        name: "TrainSearchByLine",
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
