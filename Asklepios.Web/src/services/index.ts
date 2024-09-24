import jwtStoreController from "./jwt";
import userStoreController from "./users";
import departmentStoreController from "./departments";
import roomStoreController from "./rooms";
import examinationStoreController from "./examinations";
import operationStoreController from "./operations";
import doctorStoreController from "./doctors";
import nurseStoreController from "./nurses";
import patientStoreControler from "./patients";
import examResultStoreController from "./examResults";
import dischargeStoreController from "./discharges";
import statisticStoreController from "./statistics";
import patientHistoriesController from "./patientHistories";
import clinicAppointmentsController from "./clinicAppointments";
import clinicPatientsController from "./clinicPatients";

export const API = {
  jwt: jwtStoreController,
  users: userStoreController,
  departments: departmentStoreController,
  examinations: examinationStoreController,
  operations: operationStoreController,
  doctors: doctorStoreController,
  nurses: nurseStoreController,
  patients: patientStoreControler,
  rooms: roomStoreController,
  examResults: examResultStoreController,
  discharges: dischargeStoreController,
  statistic: statisticStoreController,
  patientHistories: patientHistoriesController,
  clinicAppointments: clinicAppointmentsController,
  clinicPatients: clinicPatientsController
};
