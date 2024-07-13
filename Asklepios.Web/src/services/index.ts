import jwtStoreController from "./jwt";
import userStoreController from "./users";
import departmentStoreController from "./departments";

export const API = {
  jwt: jwtStoreController,
  users: userStoreController,
  departments: departmentStoreController
};
