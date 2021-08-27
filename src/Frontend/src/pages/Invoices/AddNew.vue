<template>
  <q-page padding>
    <div class="row">
      <div class="col q-pa-md">
        <h4 class="text-weight-bold">ثبت فاکتور جدید</h4>
      </div>
    </div>
    <q-form @submit="addNewInvoice" class="q-pa-md">
      <q-card class="my-card" bordered>
        <q-card-section>
          <div class="text-h6">سربرگ فاکتور</div>
          <div class="row">
            <div class="col q-pa-md">
              <q-input
                v-model="number"
                label="شماره فاکتور *"
                lazy-rules
                readonly
                filled
                :rules="[
                  (val) => (val && val > 0) || 'نوشتن مقدار این فیلد ضروری است',
                ]"
              />
            </div>
            <div class="col q-pa-md">
              <q-input
                v-model="buyerName"
                label="نام خریدار *"
                lazy-rules
                :rules="[
                  (val) =>
                    (val && val.length > 0) || 'نوشتن مقدار این فیلد ضروری است',
                ]"
              />
            </div>
            <div class="col q-pa-md">
              <q-input filled v-model="createdOn" mask="date" :rules="['date']">
                <template v-slot:append>
                  <q-icon name="event" class="cursor-pointer">
                    <q-popup-proxy
                      ref="qDateProxy"
                      transition-show="scale"
                      transition-hide="scale"
                    >
                      <q-date
                        v-model="createdOn"
                        mask="YYYY/MM/DD"
                        calendar="persian"
                        today-btn
                      >
                        <div class="row items-center justify-end">
                          <q-btn
                            v-close-popup
                            label="Close"
                            color="primary"
                            flat
                          />
                        </div>
                      </q-date>
                    </q-popup-proxy>
                  </q-icon>
                </template>
              </q-input>
            </div>
          </div>

          <div class="row">
            <div class="col q-pa-md">
              <q-input v-model="description" label="توضیحات" />
            </div>
          </div>
        </q-card-section>
      </q-card>

      <q-card class="my-card" bordered>
        <q-card-section>
          <div class="text-h6">اقلام فاکتور</div>
          <div class="row">
            <q-list>
              <q-item v-for="row in invoiceRows" :key="row.key">
                <q-item-section>
                  <q-select
                    padding
                    dense
                    v-model="row.itemName"
                    :options="allItems"
                    option-value="name"
                    option-label="name"
                    emit-value
                    map-optionsإ
                    label="کالا"
                    @update:model-value="
                      resetItemNumbers(row.key);
                      refreshAllPrices();
                    "
                  />
                </q-item-section>
                <q-item-section>
                  <q-input
                    type="number"
                    v-model.number="row.quantity"
                    label="تعداد *"
                    lazy-rules
                    :rules="[(val) => (val && val >= 0) || 'مقدار اشتباه است.']"
                    @update:model-value="refreshAllPrices()"
                  />
                </q-item-section>
                <q-item-section>
                  <q-input
                    type="number"
                    v-model.number="row.fee"
                    label="قیمت واحد *"
                    lazy-rules
                    :rules="[(val) => val >= 0 || 'مقدار اشتباه است.']"
                    @update:model-value="refreshAllPrices()"
                  />
                </q-item-section>
                <q-item-section>
                  <q-input
                    type="number"
                    v-model.number="row.discountPercent"
                    label="درصد تخفیف *"
                    lazy-rules
                    :rules="[
                      (val) => (val >= 0 && val <= 100) || 'مقدار اشتباه است.',
                    ]"
                    @update:model-value="refreshAllPrices()"
                  />
                </q-item-section>
                <q-item-section>
                  <q-input
                    type="number"
                    v-model.number="row.feeAfterDiscount"
                    label="قیمت واحد با تخفیف *"
                    lazy-rules
                    readonly
                    filled
                    :rules="[(val) => val >= 0 || 'مقدار اشتباه است.']"
                  />
                </q-item-section>
                <q-item-section>
                  <q-input
                    type="number"
                    v-model.number="row.totalPriceBeforeDiscount"
                    label="سر جمع بدون تخفیف *"
                    lazy-rules
                    readonly
                    filled
                    :rules="[(val) => val >= 0 || 'مقدار اشتباه است.']"
                  />
                </q-item-section>
                <q-item-section>
                  <q-input
                    type="number"
                    v-model.number="row.payablePrice"
                    label="سر جمع با تخفیف *"
                    lazy-rules
                    readonly
                    filled
                    :rules="[(val) => val >= 0 || 'مقدار اشتباه است.']"
                  />
                </q-item-section>
                <q-item-section>
                  <q-input
                    type="number"
                    v-model.number="row.feeDiscountPrice"
                    label="سود هر واحد *"
                    lazy-rules
                    readonly
                    filled
                    :rules="[(val) => val >= 0 || 'مقدار اشتباه است.']"
                  />
                </q-item-section>
                <q-item-section>
                  <q-input
                    type="number"
                    v-model.number="row.totalDiscountPrice"
                    label="جمع سود *"
                    lazy-rules
                    readonly
                    filled
                    :rules="[(val) => val >= 0 || 'مقدار اشتباه است.']"
                  />
                </q-item-section>

                <q-item-section side padding>
                  <q-btn
                    icon="delete"
                    color="negative"
                    @click="deleteRow(row.key)"
                  />
                </q-item-section>
              </q-item>
            </q-list>
          </div>
          <div class="row">
            <div class="col q-pa-md">
              <q-btn
                label="افزودن کالا به فاکتور"
                color="secondary"
                @click="addRow"
              />
            </div>
          </div>
          <div class="row">
            <div class="col q-pa-md">
              <q-input
                type="number"
                v-model.number="totalPrice"
                label="جمع کل *"
                lazy-rules
                readonly
                filled
                :rules="[(val) => (val && val >= 0) || 'مقدار اشتباه است.']"
              />
            </div>
            <div class="col q-pa-md">
              <q-input
                type="number"
                v-model.number="totalDiscount"
                label="جمع تخفیف *"
                lazy-rules
                readonly
                filled
                :rules="[(val) => (val && val >= 0) || 'مقدار اشتباه است.']"
              />
            </div>
            <div class="col q-pa-md">
              <q-input
                type="number"
                v-model.number="payablePrice"
                label="قابل پرداخت *"
                lazy-rules
                readonly
                filled
                :rules="[(val) => (val && val >= 0) || 'مقدار اشتباه است.']"
              />
            </div>
          </div>
        </q-card-section>
      </q-card>
      <div class="row">
        <div class="col q-pa-md">
          <q-btn label="ثبت فاکتور" type="submit" color="primary" />
        </div>
      </div>
    </q-form>
  </q-page>
</template>

<script>
import { api } from "boot/axios";

export default {
  data() {
    return {
      allItems: [],
      rowCounter: 0,

      createdOn: null,
      buyerName: null,
      number: null,
      description: null,
      totalPrice: null,
      totalDiscount: null,
      payablePrice: null,
      invoiceRows: [],
    };
  },
  methods: {
    addRow() {
      this.rowCounter++;
      this.invoiceRows.push({
        key: this.rowCounter,
        itemName: null,
        quantity: 0,
        fee: 0,
        discountPercent: 0,
        feeAfterDiscount: 0,
        totalPriceBeforeDiscount: 0,
        payablePrice: 0,
        feeDiscountPrice: 0,
        totalDiscountPrice: 0,
      });
    },
    resetItemNumbers(rowKey) {
      var rowIndex = this.invoiceRows.findIndex(function (o) {
        return o.key === rowKey;
      });

      if (rowIndex !== -1) {
        this.invoiceRows[rowIndex].quantity = 1;
        var rowItemName = this.invoiceRows[rowIndex].itemName;

        var itemIndex = this.allItems.findIndex(function (o) {
          return o.name === rowItemName;
        });
        if (itemIndex !== -1) {
          this.invoiceRows[rowIndex].fee = this.allItems[itemIndex].fee;
        }
      }
    },
    refreshAllPrices() {
      var totalPrice = 0;
      var totalDiscount = 0;
      var totalPayable = 0;

      for (let i = 0; i < this.invoiceRows.length; i++) {
        if (this.invoiceRows[i].quantity == null)
          this.invoiceRows[i].quantity = 0;
        if (this.invoiceRows[i].fee == null) this.invoiceRows[i].fee = 0;
        if (this.invoiceRows[i].discountPercent == null)
          this.invoiceRows[i].discountPercent = 0;

        this.invoiceRows[i].feeAfterDiscount =
          this.invoiceRows[i].fee -
          (this.invoiceRows[i].fee * this.invoiceRows[i].discountPercent) / 100;
        this.invoiceRows[i].totalPriceBeforeDiscount =
          this.invoiceRows[i].fee * this.invoiceRows[i].quantity;
        this.invoiceRows[i].payablePrice =
          this.invoiceRows[i].feeAfterDiscount * this.invoiceRows[i].quantity;
        this.invoiceRows[i].feeDiscountPrice =
          this.invoiceRows[i].fee - this.invoiceRows[i].feeAfterDiscount;
        this.invoiceRows[i].totalDiscountPrice =
          this.invoiceRows[i].totalPriceBeforeDiscount -
          this.invoiceRows[i].payablePrice;

        totalPrice += this.invoiceRows[i].totalPriceBeforeDiscount;
        totalDiscount += this.invoiceRows[i].totalDiscountPrice;
        totalPayable += this.invoiceRows[i].payablePrice;
      }

      this.totalPrice = totalPrice;
      this.totalDiscount = totalDiscount;
      this.payablePrice = totalPayable;
    },
    deleteRow(rowKey) {
      var rowIndex = this.invoiceRows.findIndex(function (o) {
        return o.key === rowKey;
      });
      if (rowIndex !== -1) this.invoiceRows.splice(rowIndex, 1);
    },
    addNewInvoice() {
      api
        .post("/Invoice", {
          createdOn: this.createdOn,
          buyerName: this.buyerName,
          number: this.number,
          description: this.description,
          totalPrice: this.totalPrice,
          totalDiscount: this.totalDiscount,
          payablePrice: this.payablePrice,
          invoiceRows: this.invoiceRows,
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
  created() {
    api
      .get("/Invoice/NewInvoiceNumber")
      .then((response) => {
        this.number = response.data.newInvoiceNumber;
      })
      .catch((error) => {
        this.$q.notify({
          progress: true,
          color: "negative",
          position: "top",
          message: "واکشی شماره جدید فاکتور با خطا مواجه شد. ",
          caption: error.response.data.message,
          icon: "report_problem",
        });
      });

    api
      .get("/Item/list")
      .then((response) => {
        this.allItems = response.data;
      })
      .catch((error) => {
        this.$q.notify({
          progress: true,
          color: "negative",
          position: "top",
          message: "واکشی لیست اقلام با خطا مواجه شد. ",
          caption: error.response.data.message,
          icon: "report_problem",
        });
      });
  },
  setup() {},
};
</script>
