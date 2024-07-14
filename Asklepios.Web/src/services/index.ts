import jwtStoreController from "./jwt";
import userStoreController from "./users";
import departmentStoreController from "./departments";
import examinationStoreController from "./examinations";
import operationStoreController from "./operations";

export const API = {
  jwt: jwtStoreController,
  users: userStoreController,
  departments: departmentStoreController,
  examinations: examinationStoreController,
  operations: operationStoreController,
};
