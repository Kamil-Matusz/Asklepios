/**
 * router/index.ts
 *
 * Automatic routes for `./src/pages/*.vue`
 */

// Composables
import { createRouter, createWebHistory } from 'vue-router'

import LoginView from '@/views/Users/LoginView.vue';
import ProfileView from '@/views/Users/ProfileView.vue';
import UserManagmentView from '@/views/Users/UserManagmentView.vue';
import ChangeUserPasswordView from '@/views/Users/ChangePasswordView.vue';
import DepartmentView from '@/views/Departments/DepartmentView.vue';
import EditDepartmentView from '@/views/Departments/EditDepartmentView.vue';
import ExaminationView from '@/views/Examinations/ExaminationView.vue';
import EditExaminationView from '@/views/Examinations/EditExaminationView.vue';
import OperationView from '@/views/Operations/OperationView.vue';
import OperationDetailsView from '@/views/Operations/OperationDetailsView.vue';
import DoctorView from '@/views/Doctors/DoctorView.vue';
import NurseView from '@/views/Nurses/NurseView.vue';
import DoctorDetailView from '@/views/Doctors/DoctorDetailsView.vue';
import PatientView from '@/views/Patients/PatientView.vue';
import EditPatientView from '@/views/Patients/EditPatientView.vue';
import PatientsByDoctorView from '@/views/Patients/PatientByDoctorView.vue';
import RoomView from '@/views/Rooms/RoomView.vue';
import EditRoomView from '@/views/Rooms/EditRoomView.vue';
import EditNurseView from '@/views/Nurses/EditNurseView.vue';
import EditDoctorView from '@/views/Doctors/EditDoctorView.vue';
import AddExamResultView from '@/views/Examinations/AddExamResultView.vue';
import DischargePatientView from '@/views/Patients/DischargePatientView.vue';
import DoctorDischargesView from '@/views/Doctors/DoctorDischargesView.vue';
import DischargesView from '@/views/Patients/DischargesView.vue';
import DischargeTemplateView from '@/views/Patients/DischargeTemplateView.vue';
import DashboardView from '@/views/Users/DashboardView.vue';
import NotFoundView from '@/views/NotFoundView.vue';
import MonthlyDischargesView from '@/views/Users/MonthlyDischargesView.vue';
import MonthlyAdmissionsView from '@/views/Users/MonthlyAdmissionsView.vue';
import PatientHistorySearchView from '@/views/Patients/PatientHistorySearchView.vue';
import AddAppointmentView from '@/views/Clinics/AddAppointmentView.vue';
import AppointmentSearchView from '@/views/Clinics/AppointmentSerarchView.vue';

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
      path: '/changePassword',
      name: 'changePassword',
      component: ChangeUserPasswordView
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
      props: true
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
      props: true
    },
    {
      path: '/examinations',
      name: 'examinations',
      component: ExaminationView
    },
    {
      path: '/examinations/edit/:id',
      name: 'ExaminationEdit',
      component: EditExaminationView,
      props: true
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
      path: '/doctor/edit/:id',
      name: 'DoctorEdit',
      component: EditDoctorView,
      props: true
    },
    {
      path: '/nurses',
      name: 'nurses',
      component: NurseView
    },
    {
      path: '/nurse/edit/:id',
      name: 'NurseEdit',
      component: EditNurseView,
      props: true
    },
    {
      path: '/patients',
      name: 'patients',
      component: PatientView
    },
    {
      path: '/patientHistory',
      name: 'patientHistory',
      component: PatientHistorySearchView
    },
    {
      path: '/yourPatients',
      name: 'yourPatients',
      component: PatientsByDoctorView
    },
    {
      path: '/patient/edit/:id',
      name: 'PatientEdit',
      component: EditPatientView,
      props: true
    },
    {
      path: '/patient/dischargePatient/:id',
      name: 'PatientDischarge',
      component: DischargePatientView,
      props: true
    },
    {
      path: '/yourDischarges',
      name: 'yourDischarges',
      component: DoctorDischargesView
    },
    {
      path: '/allDischarges',
      name: 'allDischarges',
      component: DischargesView
    },
    {
      path: '/discharges/:id',
      name: 'DischargeTemplate',
      component: DischargeTemplateView,
      props: true
    },
    {
      path: '/addExamResult',
      name: 'AddExamResult',
      component: AddExamResultView
    },
    {
      path: '/dashboard',
      name: 'Dashboard',
      component: DashboardView
    },
    {
      path: '/monthlyDischarges',
      name: 'MonthlyDischarges',
      component: MonthlyDischargesView
    },
    {
      path: '/monthlyAdmissions',
      name: 'MonthlyAdmissions',
      component: MonthlyAdmissionsView
    },
    {
      path: '/addAppointment',
      name: 'AddAppointment',
      component: AddAppointmentView
    },
    {
      path: '/dayAppointments',
      name: 'DayAppointments',
      component: AppointmentSearchView
    },
    {
      path: '/:pathMatch(.*)*',
      name: 'NotFound',
      component: NotFoundView
    }
  ]
})

export default router
