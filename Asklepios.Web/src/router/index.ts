/**
 * router/index.ts
 *
 * Automatic routes for `./src/pages/*.vue`
 */

// Composables
import { createRouter, createWebHistory } from 'vue-router'

import LoginView from '@/views/LoginView.vue'
import ProfileView from '@/views/ProfileView.vue'
import UserManagmentView from '@/views/UserManagmentView.vue'
import DepartmentView from '@/views/DepartmentView.vue'
import ExaminationView from '@/views/ExaminationView.vue'
import OperationView from '@/views/OperationView.vue'
import OperationDetailsView from '@/views/OperationDetailsView.vue';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'login',
      component: LoginView
    },
    {
      path: '/profile',
      name: 'profile',
      component: ProfileView
    },
    {
      path: '/userManagment',
      name: 'userManagment',
      component: UserManagmentView
    },
    {
      path: '/departments',
      name: 'departments',
      component: DepartmentView
    },
    {
      path: '/examinations',
      name: 'examinations',
      component: ExaminationView
    },
    {
      path: '/operations',
      name: 'operations',
      component: OperationView
    },
    {
      path: '/operation/:id',
      name: 'operationDetails',
      component: OperationDetailsView,
      props: true
    }
  ]
})

export default router
