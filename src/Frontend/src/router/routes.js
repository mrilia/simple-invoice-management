
const routes = [
  {
    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      { path: '', component: () => import('pages/Index.vue') },
      { path: '/Items', component: () => import('src/pages/Items/Items.vue') },
      { path: '/Invoices', component: () => import('src/pages/Invoices/List.vue') },
      { path: '/Invoices/AddNew', component: () => import('src/pages/Invoices/AddNew.vue') },
      { path: '/Invoices/Update/:id', component: () => import('src/pages/Invoices/Update.vue') },
    ]
  },
  

  // Always leave this as last one,
  // but you can also remove it
  {
    path: '/:catchAll(.*)*',
    component: () => import('pages/Error404.vue')
  }
]

export default routes
