<template>
  <q-page padding>
    <div class="row">
      <h4 class="text-weight-bold">مدیریت کالاها</h4>
    </div>
    <q-form @submit="addNewItem" @reset="clearForm" class="q-pa-md">
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

    <div class="inline q-pa-md">
      <q-table
        :rows="items"
        :columns="columns"
        title="لیست اقلام ثبت شده"
        :rows-per-page-options="[]"
        row-key="id"
      >
        <template v-slot:body="props">
          <q-tr :props="props">
            <q-td key="name" :props="props">
              <q-icon name="edit" />
              {{ props.row.name }}
              <q-popup-edit
                v-model="props.row.name"
                buttons
                v-slot="scope"
                @before-hide="updateItem(props.row.id)"
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
            <q-td key="fee" :props="props">
              <q-icon name="edit" />
              {{ props.row.fee }}
              <q-popup-edit
                v-model.number="props.row.fee"
                buttons
                v-slot="scope"
                @before-hide="updateItem(props.row.id)"
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
            <q-td>
              <q-btn
                dense
                autofocus
                color="negative"
                icon="delete"
                @click="deleteItem(props.row.id)"
              />
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
      itemNameToAdd: "",
      itemFeeToAdd: null,
      items: [],
      columns: [
        { name: "name", align: "left", label: "نام کالا", field: "Name" },
        { name: "fee", align: "left", label: "قیمت واحد", field: "Fee" },
        { name: "operations", align: "left", label: "" },
      ],
    };
  },
  methods: {
    updateItem(id) {
      const itemToUpdate = this.items.find((item) => item.id == id);

      api
        .put("/Item", itemToUpdate)
        .then((response) => {
          this.$q.notify({
            progress: true,
            color: "positive",
            position: "top",
            message: "به روزرسانی کالا با موفقیت انجام شد",
            icon: "check_circle",
          });

          api
            .get("/Item/list")
            .then((response) => {
              this.items = response.data;
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
    deleteItem(id) {
      api
        .delete("/Item/" + id)
        .then((response) => {
          this.$q.notify({
            progress: true,
            color: "positive",
            position: "top",
            message: "حذف کالا با موفقیت انجام شد",
            icon: "check_circle",
          });

          api
            .get("/Item/list")
            .then((response) => {
              this.items = response.data;
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
    addNewItem() {
      api
        .post("/Item", { name: this.itemNameToAdd, fee: this.itemFeeToAdd })
        .then((response) => {
          this.$q.notify({
            progress: true,
            color: "positive",
            position: "top",
            message: "ثبت کالا با موفقیت انجام شد",
            icon: "check_circle",
          });

          api
            .get("/Item/list")
            .then((response) => {
              this.items = response.data;
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
      this.itemNameToAdd = null;
      this.itemFeeToAdd = null;
    },
  },
  created() {
    api
      .get("/Item/list")
      .then((response) => {
        this.items = response.data;
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
