<template>
  <q-page>
    <table width="100%">
      <tbody>
        <tr>
          <td colspan="8" rowspan="3" class="top-bordered left-bordered">
            نام خریدار: {{ invoiceToPrint.buyerName }}
          </td>
          <td colspan="2" class="top-bordered right-bordered">تاریخ: {{ invoiceToPrint.createdOn }}</td>
        </tr>
        <tr>
          <td colspan="2" class="right-bordered">شماره: {{ invoiceToPrint.number }}</td>
        </tr>
        <tr>
          <td colspan="2" class="right-bordered">&nbsp;</td>
        </tr>
        <tr class="text-center">
          <td class="bordered">ردیف</td>
          <td class="bordered">عنوان</td>
          <td class="bordered">تعداد</td>
          <td class="bordered">قیمت واحد<br />(بدون تخفیف)</td>
          <td class="bordered">درصد تخفیف</td>
          <td class="bordered">قیمت واحد<br />(با تخفیف)</td>
          <td class="bordered">سر جمع<br />(بدون تخفیف)</td>
          <td class="bordered">سر جمع<br />(با تخفیف)</td>
          <td class="bordered">سود مشتری از تخفیف واحد</td>
          <td class="bordered">سود مشتری از تخفیف کل</td>
        </tr>
        <tr class="text-center" v-for="row in invoiceToPrint.invoiceRows" :key="row.id">
          <td class="bordered">{{counter++}}</td>
          <td class="bordered">{{row.itemName}}</td>
          <td class="bordered">{{row.quantity}}</td>
          <td class="bordered">{{row.fee}}</td>
          <td class="bordered">{{row.discountPercent}}</td>
          <td class="bordered">{{row.feeAfterDiscount}}</td>
          <td class="bordered">{{row.totalPriceBeforeDiscount}}</td>
          <td class="bordered">{{row.payablePrice}}</td>
          <td class="bordered">{{row.feeDiscountPrice}}</td>
          <td class="bordered">{{row.totalDiscountPrice}}</td>
        </tr>
        <tr class="text-center">
          <td colspan="10" class="bordered">&nbsp;</td>
        </tr>
        <tr>
          <td colspan="8" rowspan="3" class="bordered">
            توضیحات: {{ invoiceToPrint.description }}
          </td>
          <td class="bordered">جمع کل</td>
          <td class="bordered">{{ invoiceToPrint.totalPrice }}</td>
        </tr>
        <tr>
          <td class="bordered">جمع تخفیف</td>
          <td class="bordered">{{ invoiceToPrint.totalDiscount }}</td>
        </tr>
        <tr>
          <td class="bordered">قابل پرداخت</td>
          <td class="bordered">{{ invoiceToPrint.payablePrice }}</td>
        </tr>
      </tbody>
    </table>
   
  </q-page>
</template>

<script>
import { api } from "boot/axios";

export default {
  data() {
    return {
      counter:1,
      invoiceToPrint: {},
    };
  },
  created() {
    api
      .get("/Invoice/" + this.$route.params.id)
      .then((response) => {
        this.invoiceToPrint = response.data;
      })
      .catch((error) => {
        this.$q.notify({
          progress: true,
          color: "negative",
          position: "top",
          message: "واکشی اطلاعات فاکتور با خطا مواجه شد. ",
          caption: error.response.data.message,
          icon: "report_problem",
        });
      });
  },
};
</script>

<style lang="sass" scoped>
.top-bordered
  border-top: 1px solid black
  margin: 0
.left-bordered
  border-left: 1px solid black
  margin: 0
.right-bordered
  border-right: 1px solid black
  margin: 0
.bordered
  border: 1px solid black
  margin: 0
table
  border-spacing: 0
  border-collapse: collapse
</style>
