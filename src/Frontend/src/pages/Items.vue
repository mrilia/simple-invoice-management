<template>
  <q-page padding>
    <h4 class="text-weight-bold">مدیریت کالاها</h4>
  </q-page>
</template>

<script>
import { api } from "boot/axios";
import { useQuasar } from "quasar";

export default {
  data() {
    return {
      options: [],
    };
  },
  setup() {
    const $q = useQuasar();
    const data = {};

    api
      .get("/Item/list")
      .then((response) => {
        data.value = response.data;
      })
      .catch((c) => {
        console.log(c)
        $q.notify({
          color: "negative",
          position: "top",
          message: "واکشی اطلاعات با خطا مواجه شد",
          icon: "report_problem",
        });
      });

    return data;
  },
};
</script>
