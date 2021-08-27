<template>
  <q-page padding>
    <div class="row">
      <h4 class="text-weight-bold">مدیریت فاکتورها</h4>
    </div>

    <div class="row">
      <q-form @submit="addNewInvoice" @reset="clearForm">
        <div class="col">
          <q-input
            filled
            v-model="invoiceNameToAdd"
            label="نام کالا *"
            lazy-rules
            :rules="[
              (val) =>
                (val && val.length > 0) || 'نوشتن مقدار این فیلد ضروری است',
            ]"
          />
        </div>

        <div class="col">
          <q-input
            class="col"
            filled
            type="number"
            v-model="invoiceFeeToAdd"
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

        <div class="col">
          <q-btn label="ثبت" type="submit" color="primary" />
          <q-btn
            label="پاک کن"
            type="reset"
            color="primary"
            flat
            class="q-ml-sm"
          />
        </div>
      </q-form>
    </div>

    <div class="inline q-pa-md">
      <q-table
        :rows="invoices"
        :columns="columns"
        title="لیست فاکتورهای ثبت شده"
        :rows-per-page-options="[]"
        row-key="id"
      >
        <template v-slot:body="props">
          <q-tr :props="props">
            <q-td key="number" :props="props">
              {{ props.row.number }}
              <q-popup-edit
                v-model.number="props.row.number"
                buttons
                v-slot="scope"
                @before-hide="updateInvoice(props.row.id)"
              >
                <q-input
                  type="number"
                  v-model.number="scope.value"
                  dense
                  autofocus
                  @keyup.enter="scope.set"
                />
              </q-popup-edit>
            </q-td>
            <q-td key="createdOn" :props="props">
              {{ props.row.createdOn }}
              <q-popup-edit
                v-model="props.row.createdOn"
                buttons
                v-slot="scope"
                @before-hide="updateInvoice(props.row.id)"
              >
                <q-input
                  v-model="scope.value"
                  dense
                  autofocus
                  counter
                  @keyup.enter="scope.set"
                />
              </q-popup-edit>
            </q-td>
            <q-td key="buyerName" :props="props">
              {{ props.row.buyerName }}
              <q-popup-edit
                v-model="props.row.buyerName"
                buttons
                v-slot="scope"
                @before-hide="updateInvoice(props.row.id)"
              >
                <q-input
                  v-model="scope.value"
                  dense
                  autofocus
                  counter
                  @keyup.enter="scope.set"
                />
              </q-popup-edit>
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
              <q-popup-edit
                v-model="props.row.description"
                buttons
                v-slot="scope"
                @before-hide="updateInvoice(props.row.id)"
              >
                <q-input
                  v-model="scope.value"
                  dense
                  autofocus
                  counter
                  @keyup.enter="scope.set"
                />
              </q-popup-edit>
            </q-td>
            <q-td>
              <q-btn-group outline>
                <q-btn outline color="negative" icon-right="delete" @click="deleteInvoice(props.row.id)"/>
                <q-btn outline color="primary" icon-right="print" @click="printInvoice(props.row.id)"/>
                <q-btn outline color="accent" icon-right="list" @click="showInvoiceRows(props.row.id)"/>
              </q-btn-group>              
            </q-td>
          </q-tr>
        </template>
      </q-table>
    </div>
  </q-page>
</template>

<script>
import { api } from "boot/axios";

export default {
  data() {
    return {
      invoiceNameToAdd: "",
      invoiceFeeToAdd: null,
      invoices: [],
      columns: [
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
            message: "به روزرسانی کالا با موفقیت انجام شد",
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
                message: "به روزرسانی لیست کالاها با خطا مواجه شد",
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
            message: "به روزرسانی اطلاعات کالا با خطا مواجه شد",
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
            message: "حذف کالا با موفقیت انجام شد",
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
                message: "به روزرسانی لیست کالاها با خطا مواجه شد",
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
            message: "حذف اطلاعات کالا با خطا مواجه شد",
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
            message: "ثبت کالا با موفقیت انجام شد",
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
                message: "به روزرسانی لیست کالاها با خطا مواجه شد. ",
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
            message: "ثبت کالا با خطا مواجه شد. ",
            caption: error.response.data.message,
            icon: "report_problem",
          });
        });
    },

    clearForm() {
      this.invoiceNameToAdd = null;
      this.invoiceFeeToAdd = null;
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
