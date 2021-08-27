<template>
  <q-page padding>
    <q-form @submit="addNewInvoice" class="q-pa-md">
      <div class="row">
        <div class="col q-pa-md">
          <q-input
            filled
            v-model="itemNameToAdd"
            label="نام کالا *"
            lazy-rules
            :rules="[
              (val) =>
                (val && val.length > 0) || 'نوشتن مقدار این فیلد ضروری است',
            ]"
          />
        </div>
        <div class="col q-pa-md">
          <q-input
            class="col"
            filled
            type="number"
            v-model="itemFeeToAdd"
            label="قیمت واحد *"
            lazy-rules
            :rules="[
              (val) =>
                (val !== null && val !== '') ||
                'نوشتن مقدار این فیلد ضروری است',
              (val) => val > 0 || 'مقدار فیلد نامعتبر است',
            ]"
          />
        </div>
        <div class="col q-pa-md">
          <q-btn label="ثبت" type="submit" color="primary" />
          <q-btn
            label="پاک کن"
            type="reset"
            color="primary"
            flat
            class="q-ml-sm"
          />
        </div>
      </div>
    </q-form>
  </q-page>
</template>

<script>
export default {
  data() {
    return {
      createdOn: null,
      buyerName: null,
      number: null,
      description: null,
      totalPrice: null,
      totalDiscount: null,
      payablePrice: null,
      invoiceRows: [
        {
          itemName: null,
          quantity: null,
          fee: null,
          discountPercent: null,
          feeAfterDiscount: null,
          totalPriceBeforeDiscount: null,
          payablePrice: null,
          feeDiscountPrice: null,
          totalDiscountPrice: null,
        },
      ],
    };
  },
  methods: {
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

          this.$router.go(-1);
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
  },
};
</script>
