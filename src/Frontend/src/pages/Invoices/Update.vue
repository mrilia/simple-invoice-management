<template>
  <q-page padding> updateInvoice id:{{ $route.params.id }} </q-page>
</template>

<script>
import { api } from "boot/axios";

export default {
  methods: {
    updateInvoice(id) {
      const invoiceToUpdate = this.invoices.find((invoice) => invoice.id == id);

      api
        .put("/Invoice", invoiceToUpdate)
        .then((response) => {
          this.$q.notify({
            progress: true,
            color: "positive",
            position: "top",
            message: "به روزرسانی فاکتور با موفقیت انجام شد",
            icon: "check_circle",
          });

          api
            .get("/Invoice/list")
            .then((response) => {
              this.invoices = response.data;
            })
            .catch((error) => {
              this.$q.notify({
                progress: true,
                color: "negative",
                position: "top",
                message: "به روزرسانی لیست فاکتورها با خطا مواجه شد",
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
            message: "به روزرسانی اطلاعات فاکتور با خطا مواجه شد",
            caption: error.response.data.message,
            icon: "report_problem",
          });
        });
    },
  },
};
</script>
