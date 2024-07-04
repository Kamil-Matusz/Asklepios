import jwtStoreController from "./jwt";
import userStoreController from "./users";

export const API = {
  jwt: jwtStoreController,
  users: userStoreController,
};
