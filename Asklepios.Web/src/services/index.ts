import jwtStoreController from "./jwt";
import userStoreController from "./users";
import departmentStoreController from "./departments";
import examinationStoreController from "./examinations";
import operationStoreController from "./operations";
import doctorStoreController from "./doctors";
import nurseStoreController from "./nurses";
import patientStoreControler from "./patients"

export const API = {
  jwt: jwtStoreController,
  users: userStoreController,
  departments: departmentStoreController,
  examinations: examinationStoreController,
  operations: operationStoreController,
  doctors: doctorStoreController,
  nurses: nurseStoreController,
  patients: patientStoreControler
};
