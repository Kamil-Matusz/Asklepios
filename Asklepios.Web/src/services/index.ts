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
  discharges: dischargeStoreController
};
