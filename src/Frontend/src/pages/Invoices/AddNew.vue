<template>
  <q-page padding>
    addnew
  </q-page>
</template>

<script>
export default {
  methods:{
    addNewInvoice() {
      api
        .post("/Invoice", {
          name: this.invoiceNameToAdd,
          fee: this.invoiceFeeToAdd,
        })
        .then((response) => {
          this.$q.notify({
            progress: true,
            color: "positive",
            position: "top",
            message: "ثبت فاکتور با موفقیت انجام شد",
            icon: "check_circle",
          });

          api
            .get("/Invoice/list")
            .then((response) => {
              this.invoices = response.data;
            })
            .catch(() => {
              this.$q.notify({
                progress: true,
                color: "negative",
                position: "top",
                message: "به روزرسانی لیست فاکتورها با خطا مواجه شد. ",
                caption: error.response.data.message,
                icon: "report_problem",
              });
            });
        })
        .catch((error) => {
          this.$q.notify({
            progress: true,
            color: "negative",
            position: "top",
            message: "ثبت فاکتور با خطا مواجه شد. ",
            caption: error.response.data.message,
            icon: "report_problem",
          });
        });
    },
  }
}
</script>
