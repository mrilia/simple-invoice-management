<template>
  <q-page padding>
    <div class="row">
      <h4 class="text-weight-bold">مدیریت فاکتورها</h4>
    </div>

    <div class="inline q-pa-md">
      <q-table
        :rows="invoices"
        :columns="invoiceTableColumns"
        title="لیست فاکتورهای ثبت شده"
        :rows-per-page-options="[]"
        row-key="id"
      >
        <template v-slot:body="props">
          <q-tr>
            <q-td key="number" :props="props">
              {{ props.row.number }}
            </q-td>
            <q-td key="createdOn" :props="props">
              {{ props.row.createdOn }}
            </q-td>
            <q-td key="buyerName" :props="props">
              {{ props.row.buyerName }}
            </q-td>
            <q-td key="totalPrice" :props="props">
              {{ props.row.totalPrice }}
            </q-td>
            <q-td key="totalDiscount" :props="props">
              {{ props.row.totalDiscount }}
            </q-td>
            <q-td key="payablePrice" :props="props">
              {{ props.row.payablePrice }}
            </q-td>
            <q-td key="description" :props="props">
              {{ props.row.description }}
            </q-td>
            <q-td>
              <q-btn-group outline>
                <q-btn
                  outline
                  color="negative"
                  icon-right="delete"
                  @click="deleteInvoice(props.row.id)"
                />
                <q-btn
                  outline
                  color="primary"
                  icon-right="print"
                  @click="printInvoice(props.row.id)"
                />
                <q-btn
                  outline
                  color="secondary"
                  icon-right="list"
                  @click="showInvoiceRows(props.row.id)"
                />
              </q-btn-group>
            </q-td>
          </q-tr>
        </template>
      </q-table>
    </div>

    <q-dialog v-model="invoiceRowsShown" full-width>
      <q-card>
        <q-card-section>
          <div class="text-h6">لیست اقلام فاکتور</div>
        </q-card-section>

        <q-card-section class="q-pt-none">
          <q-table
            :rows="invoiceRowsToShow"
            :columns="invoiceRowsTableColumns"
            :rows-per-page-options="[]"
            row-key="id"
          >
            <template v-slot:body="props">
              <q-tr :props="props">
                <q-td key="itemName" :props="props">
                  {{ props.row.itemName }}
                </q-td>
                <q-td key="quantity" :props="props">
                  {{ props.row.quantity }}
                </q-td>
                <q-td key="fee" :props="props">
                  {{ props.row.fee }}
                </q-td>
                <q-td key="discountPercent" :props="props">
                  {{ props.row.discountPercent }}
                </q-td>
                <q-td key="feeAfterDiscount" :props="props">
                  {{ props.row.feeAfterDiscount }}
                </q-td>
                <q-td key="totalPriceBeforeDiscount" :props="props">
                  {{ props.row.totalPriceBeforeDiscount }}
                </q-td>
                <q-td key="payablePrice" :props="props">
                  {{ props.row.payablePrice }}
                </q-td>
                <q-td key="feeDiscountPrice" :props="props">
                  {{ props.row.feeDiscountPrice }}
                </q-td>
                <q-td key="totalDiscountPrice" :props="props">
                  {{ props.row.totalDiscountPrice }}
                </q-td>
              </q-tr>
            </template>
          </q-table>
        </q-card-section>

        <q-card-actions align="right" class="bg-white text-teal">
          <q-btn flat label="بستن" v-close-popup />
        </q-card-actions>
      </q-card>
    </q-dialog>
  </q-page>
</template>

<script>
import { api } from "boot/axios";

export default {
  data() {
    return {
      invoiceRowsShown: false,
      invoiceRowsToShow: [],

      invoiceNameToAdd: "2021/10/10",
      invoiceFeeToAdd: null,

      invoices: [],

      invoiceTableColumns: [
        {
          name: "number",
          align: "left",
          label: "شماره فاکتور",
          field: "number",
        },
        {
          name: "createdOn",
          align: "left",
          label: "تاریخ",
          field: "createdOn",
        },
        {
          name: "buyerName",
          align: "left",
          label: "خریدار",
          field: "buyerName",
        },
        {
          name: "totalPrice",
          align: "left",
          label: "جمع کل",
          field: "totalPrice",
        },
        {
          name: "totalDiscount",
          align: "left",
          label: "جمع تخفیف",
          field: "totalDiscount",
        },
        {
          name: "payablePrice",
          align: "left",
          label: "قابل پرداخت",
          field: "payablePrice",
        },
        {
          name: "description",
          align: "left",
          label: "توضیحات",
          field: "description",
        },
        { name: "operations", align: "left", label: "" },
      ],

      invoiceRowsTableColumns: [
        {
          name: "itemName",
          align: "left",
          label: "کالا",
          field: "itemName",
        },
        {
          name: "quantity",
          align: "left",
          label: "تعداد",
          field: "quantity",
        },
        {
          name: "fee",
          align: "left",
          label: "مبلغ واحد",
          field: "fee",
        },
        {
          name: "discountPercent",
          align: "left",
          label: "درصد تخفیف",
          field: "discountPercent",
        },
        {
          name: "feeAfterDiscount",
          align: "left",
          label: "قیمت واحد با تخفیف",
          field: "feeAfterDiscount",
        },
        {
          name: "totalPriceBeforeDiscount",
          align: "left",
          label: "سر جمع بدون تخفیف",
          field: "totalPriceBeforeDiscount",
        },
        {
          name: "payablePrice",
          align: "left",
          label: "سر جمع با تخفیف",
          field: "payablePrice",
        },
        {
          name: "feeDiscountPrice",
          align: "left",
          label: "سود مشتری از تخفیف واحد",
          field: "feeDiscountPrice",
        },
        {
          name: "totalDiscountPrice",
          align: "left",
          label: "سود مشتری از تخفیف کل",
          field: "totalDiscountPrice",
        },
      ],
    };
  },
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
    deleteInvoice(id) {
      api
        .delete("/Invoice/" + id)
        .then((response) => {
          this.$q.notify({
            progress: true,
            color: "positive",
            position: "top",
            message: "حذف فاکتور با موفقیت انجام شد",
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
            message: "حذف اطلاعات فاکتور با خطا مواجه شد",
            caption: error.response.data.message,
            icon: "report_problem",
          });
        });
    },
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

    clearForm() {
      this.invoiceNameToAdd = null;
      this.invoiceFeeToAdd = null;
    },
    showInvoiceRows(id) {
      this.invoiceRowsToShow = [];
      const invoice = this.invoices.find((invoice) => invoice.id == id);
      this.invoiceRowsToShow=invoice.invoiceRows;
      this.invoiceRowsShown = true;
    },
  },
  created() {
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
          message: "واکشی اطلاعات با خطا مواجه شد. ",
          caption: error.response.data.message,
          icon: "report_problem",
        });
      });
  },
};
</script>
