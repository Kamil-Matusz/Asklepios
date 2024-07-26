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
import EditDepartmentView from '@/views/EditDepartmentView.vue'
import ExaminationView from '@/views/ExaminationView.vue'
import OperationView from '@/views/OperationView.vue'
import OperationDetailsView from '@/views/OperationDetailsView.vue';
import DoctorView from '@/views/DoctorView.vue';
import NurseView from '@/views/NurseView.vue';
import DoctorDetailView from '@/views/DoctorDetailsView.vue';
import PatientView from '@/views/PatientView.vue';
import RoomView from '@/views/RoomView.vue';
import EditRoomView from '@/views/EditRoomView.vue';


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
      path: '/departments/edit/:id',
      name: 'DepartmentEdit',
      component: EditDepartmentView,
      props: true // Pozwól na przekazywanie parametrów jako props
    },
    {
      path: '/rooms',
      name: 'rooms',
      component: RoomView
    },
    {
      path: '/rooms/edit/:id',
      name: 'RoomEdit',
      component: EditRoomView,
      props: true // Pozwól na przekazywanie parametrów jako props
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
    },
    {
      path: '/doctors',
      name: 'doctors',
      component: DoctorView
    },
    {
      path: '/doctor/:id',
      name: 'doctorsDetails',
      component: DoctorDetailView,
      props: true
    },
    {
      path: '/nurses',
      name: 'nurses',
      component: NurseView
    },
    {
      path: '/patients',
      name: 'patients',
      component: PatientView
    },
  ]
})

export default router
